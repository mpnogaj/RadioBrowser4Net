using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using RadioBrowser4Net.Internals;
using RadioBrowser4Net.Models;
using RadioBrowser4Net.Models.Params;
using RadioBrowser4Net.Models.Responses;
using RadioBrowser4Net.Models.Responses.Actions;

namespace RadioBrowser4Net
{
	public sealed class RadioBrowserClient
	{
		private readonly HttpClient _httpClient;
		private readonly string _assemblyVersion =
			Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public RadioBrowserClient(
			string? appUrl = null,
			string? customUserAgent = null,
			TimeSpan? timeout = null,
			bool useJson = true)
		{
			var basePath =
				$"https://{appUrl ?? GetRadioBrowserApiUrl()}/{(useJson ? "json" : "xml")}/";

			var userAgent = customUserAgent ?? $"RadioBrowser4Net/{_assemblyVersion}";

			_httpClient = new HttpClient()
			{
				BaseAddress = new Uri(basePath),
				Timeout = timeout ?? TimeSpan.FromSeconds(10)
			};

			_httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
		}

		public async Task<List<CountryInfo>?> ListCountries(BasicListParams listParams, string? filter = null)
			=> await ListRequest<CountryInfo>("countries", listParams, filter);


		[Obsolete("Please use countries endpoint instead. It has name and countrycode information.")]
		public async Task<List<CountryCodeInfo>?> ListCountryCodes(BasicListParams listParams, string? filter = null)
			=> await ListRequest<CountryCodeInfo>("countrycodes", listParams, filter);

		public async Task<List<CodecInfo>?> ListCodecs(BasicListParams listParams, string? filter = null)
			=> await ListRequest<CodecInfo>("codecs", listParams, filter);

		public async Task<List<StateInfo>?> ListStates(BasicListParams listParams, string? filter = null)
			=> await ListRequest<StateInfo>("states", listParams, filter);

		public async Task<List<LanguageInfo>?> ListLanguages(BasicListParams listParams, string? filter = null)
			=> await ListRequest<LanguageInfo>("languages", listParams, filter);

		public async Task<List<TagInfo>?> ListTags(BasicListParams listParams, string? filter = null)
			=> await ListRequest<TagInfo>("tags", listParams, filter);

		public async Task<List<StationInfo>?> ListStations(StationsListParams listParams, StationsSearchBy searchBy,
			string searchTerm)
			=> await ListRequest<StationInfo>($"stations/by{searchBy.ToString().ToLower()}", listParams, searchTerm);

		public async Task<List<StationInfo>?> ListAllStations(StationsListParams listParams)
			=> await ListRequest<StationInfo>($"stations", listParams, null);

		public async Task<IAsyncEnumerable<StationInfo?>?> AsyncListAllStations(StationsListParams listParams)
			=> await AsyncEnumerableRequest<StationInfo>($"stations", listParams, null);

		public async Task<List<StationCheckResult>?> ListStationsChecks(StationChecksListParams listParams, Guid? stationUuid = null)
			=> await ListRequest<StationCheckResult>($"checks", listParams, stationUuid?.ToString() ?? null);

		public async Task<List<StationStepCheck>?> ListStationsCheckSteps(StationCheckStepsListParams listParams)
			=> await ListRequest<StationStepCheck>($"checksteps", listParams);

		public async Task<List<StationClickInfo>?> ListStationsClicks(StationClicksListParams listParams, Guid? stationUuid = null)
			=> await ListRequest<StationClickInfo>($"clicks", listParams, stationUuid?.ToString() ?? null);

		public async Task<ClickActionResult?> ModifyStationClick(Guid stationUuid)
			=> await MakeGetRequest<ClickActionResult>($"url/{stationUuid}");

		public async Task<List<StationInfo>?> SearchAdvancedStations(StationsAdvancedSearch searchParams)
			=> await ListRequest<StationInfo>("stations/search", searchParams);

		public async Task<List<StationInfo>?> SearchStationsByUuid(StationsSearchByUuid searchParams)
			=> await ListRequest<StationInfo>("stations/byuuid", searchParams);

		public async Task<List<StationInfo>?> SearchStationsByUrl(StationsSearchByUrl searchParams)
			=> await ListRequest<StationInfo>("stations/byurl", searchParams);


		private async Task<List<T>?> ListRequest<T>(string baseUrl, object? listParams, string? filter = null)
		{
			var queryString = GetQueryString(listParams);
			var url = filter == null
				? $"{baseUrl}{queryString}"
				: $"{baseUrl}/{filter}{queryString}";
			return await MakeGetRequest<List<T>>(url);
		}

		private async Task<T?> MakeGetRequest<T>(string url) where T : class
		{
			await using var resp = await _httpClient.GetStreamAsync(url);
			return resp == null
				? null
				: await JsonSerializer.DeserializeAsync<T>(resp);
		}

		private async Task<IAsyncEnumerable<T?>?> AsyncEnumerableRequest<T>(string baseUrl,
			object? listParams,
			string? filter = null) where T : class
		{
			var queryString = GetQueryString(listParams);
			var url = filter == null
				? $"{baseUrl}{queryString}"
				: $"{baseUrl}/{filter}{queryString}";

			var stream = await _httpClient.GetStreamAsync(url);

			return stream == null
				? null
				: GetAsyncEnumerableFromStream<T>(stream);
		}

		private static async IAsyncEnumerable<T?> GetAsyncEnumerableFromStream<T>(Stream stream)
			where T : class
		{
			var data = JsonSerializer.DeserializeAsyncEnumerable<T>(stream);
			await foreach (var x in data)
			{
				yield return x;
			}

			stream.Close();
		}

		private static string GetQueryString(object? obj)
		{
			if (obj == null)
				return string.Empty;

			var properties = obj.GetType()
				.GetProperties()
				.Where(p => p.GetValue(obj, null) != null)
				.Select(p => PropertyToQueryParam(p, obj));

			return $"?{string.Join("&", properties.ToArray())}";
		}

		private static string PropertyToQueryParam(PropertyInfo property, object obj)
		{
			var propertyType = property.PropertyType;

			//handle nullable types
			{
				var nullableUnderlyingType = Nullable.GetUnderlyingType(propertyType);
				if (nullableUnderlyingType != null)
				{
					propertyType = nullableUnderlyingType;
				}
			}

			var propertyValue = property.GetValue(obj, null);

			var nameAttribute = property.GetCustomAttributes(true)
				.OfType<QueryPropertyNameAttribute>()
				.FirstOrDefault();
			var paramName = nameAttribute?.QueryName ?? property.Name.ToLower();

			string paramValue;
			if (propertyType.IsEnum)
			{
				paramValue = GetEnumName(propertyType, propertyValue);
			}
			else if (propertyType.IsGenericType &&
					 propertyType.GetGenericTypeDefinition() == typeof(List<>))
			{
				var objList = (IList)propertyValue;
				var sb = new StringBuilder();
				foreach (var t in objList)
					sb.Append($"{t},");
				paramValue = sb.ToString()[..^1];
			}
			else
			{
				paramValue = propertyValue.ToString();
				if (propertyType == typeof(bool) || (propertyType == typeof(bool?)))
					paramValue = paramValue.ToLower();
			}

			return $"{paramName}={HttpUtility.UrlEncode(paramValue)}";
		}

		private static string GetEnumName(Type enumType, object enumValue)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException("Provided type is not an enum type", nameof(enumType));
			var fallbackName = enumValue.ToString().ToLower();

			var name = Enum.GetName(enumType, enumValue);
			if (name == null) return fallbackName;

			var field = enumType.GetField(name);
			if (field == null) return fallbackName;

			var descriptionAttribute = field.GetCustomAttributes(true)
				.OfType<DescriptionAttribute>()
				.FirstOrDefault();

			return descriptionAttribute != null
				? descriptionAttribute.Description
				: fallbackName;
		}

		private static string GetRadioBrowserApiUrl()
		{
			const string baseUrl = @"all.api.radio-browser.info";
			var ips = Dns.GetHostAddresses(baseUrl);
			var lastRoundTripTime = long.MaxValue;
			var searchUrl = @"de1.api.radio-browser.info"; // Fallback
			foreach (var ipAddress in ips)
			{
				var reply = new Ping().Send(ipAddress);
				if (reply == null ||
					reply.RoundtripTime >= lastRoundTripTime) continue;

				lastRoundTripTime = reply.RoundtripTime;
				searchUrl = ipAddress.ToString();
			}

			// Get clean name
			var hostEntry = Dns.GetHostEntry(searchUrl);
			if (!string.IsNullOrEmpty(hostEntry.HostName))
			{
				searchUrl = hostEntry.HostName;
			}

			return searchUrl;
		}
	}
}
