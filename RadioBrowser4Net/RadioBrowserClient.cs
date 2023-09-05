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

		public Task<List<CountryInfo?>?> ListCountries(BasicListParams listParams, string? filter = null)
			=> ListRequest<CountryInfo>("countries", listParams, filter);

		public Task<IAsyncEnumerable<CountryInfo?>?> ListCountriesAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<CountryInfo>("countries", listParams, filter);

		[Obsolete("Please use countries endpoint instead. It has name and countrycode information.")]
		public Task<List<CountryCodeInfo?>?> ListCountryCodes(BasicListParams listParams, string? filter = null)
			=> ListRequest<CountryCodeInfo>("countrycodes", listParams, filter);

		[Obsolete("Please use countries endpoint instead. It has name and countrycode information.")]
		public Task<IAsyncEnumerable<CountryCodeInfo?>?> ListCountryCodesAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<CountryCodeInfo>("countrycodes", listParams, filter);

		public Task<List<CodecInfo?>?> ListCodecs(BasicListParams listParams, string? filter = null)
			=> ListRequest<CodecInfo>("codecs", listParams, filter);

		public Task<IAsyncEnumerable<CodecInfo?>?> ListCodecsAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<CodecInfo>("codecs", listParams, filter);

		public Task<List<StateInfo?>?> ListStates(BasicListParams listParams, string? filter = null)
			=> ListRequest<StateInfo>("states", listParams, filter);

		public Task<IAsyncEnumerable<StateInfo?>?> ListStatesAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<StateInfo>("states", listParams, filter);

		public Task<List<LanguageInfo?>?> ListLanguages(BasicListParams listParams, string? filter = null)
			=> ListRequest<LanguageInfo>("languages", listParams, filter);

		public Task<IAsyncEnumerable<LanguageInfo?>?> ListLanguagesAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<LanguageInfo>("languages", listParams, filter);

		public Task<List<TagInfo?>?> ListTags(BasicListParams listParams, string? filter = null)
			=> ListRequest<TagInfo>("tags", listParams, filter);

		public Task<IAsyncEnumerable<TagInfo?>?> ListTagsAsyncEnumerable(BasicListParams listParams, string? filter = null)
			=> AsyncEnumerableRequest<TagInfo>("tags", listParams, filter);

		public Task<List<StationInfo?>?> ListStations(StationsListParams listParams, StationsSearchBy searchBy,
			string searchTerm)
			=> ListRequest<StationInfo>($"stations/by{searchBy.ToString().ToLower()}", listParams, searchTerm);

		public Task<IAsyncEnumerable<StationInfo?>?> ListStationsAsyncEnumerable(StationsListParams listParams, StationsSearchBy searchBy,
			string searchTerm)
			=> AsyncEnumerableRequest<StationInfo>($"stations/by{searchBy.ToString().ToLower()}", listParams, searchTerm);

		public Task<List<StationInfo?>?> ListAllStations(StationsListParams listParams)
			=> ListRequest<StationInfo>($"stations", listParams);

		public Task<IAsyncEnumerable<StationInfo?>?> ListAllStationsAsyncEnumerable(StationsListParams listParams)
			=> AsyncEnumerableRequest<StationInfo>($"stations", listParams);

		public Task<List<StationCheckResult?>?> ListStationsChecks(StationChecksListParams listParams, Guid? stationUuid = null)
			=> ListRequest<StationCheckResult>($"checks", listParams, stationUuid?.ToString() ?? null);

		public Task<IAsyncEnumerable<StationCheckResult?>?> ListStationsChecksAsyncEnumerable(StationChecksListParams listParams, Guid? stationUuid = null)
			=> AsyncEnumerableRequest<StationCheckResult>($"checks", listParams, stationUuid?.ToString() ?? null);

		public Task<List<StationStepCheck?>?> ListStationsCheckSteps(StationCheckStepsListParams listParams)
			=> ListRequest<StationStepCheck>($"checksteps", listParams);

		public Task<IAsyncEnumerable<StationStepCheck?>?> ListStationsCheckStepsAsyncEnumerable(StationCheckStepsListParams listParams)
			=> AsyncEnumerableRequest<StationStepCheck>($"checksteps", listParams);

		public Task<List<StationClickInfo?>?> ListStationsClicks(StationClicksListParams listParams, Guid? stationUuid = null)
			=> ListRequest<StationClickInfo>($"clicks", listParams, stationUuid?.ToString() ?? null);

		public Task<IAsyncEnumerable<StationClickInfo?>?> ListStationsClicksAsyncEnumerable(StationClicksListParams listParams, Guid? stationUuid = null)
			=> AsyncEnumerableRequest<StationClickInfo>($"clicks", listParams, stationUuid?.ToString() ?? null);

		public Task<ClickActionResult?> ModifyStationClick(Guid stationUuid)
			=> MakeGetRequest<ClickActionResult>($"url/{stationUuid}");

		public Task<List<StationInfo?>?> SearchAdvancedStations(StationsAdvancedSearch searchParams)
			=> ListRequest<StationInfo>("stations/search", searchParams);

		public Task<IAsyncEnumerable<StationInfo?>?> SearchAdvancedStationsAsyncEnumerable(StationsAdvancedSearch searchParams)
			=> AsyncEnumerableRequest<StationInfo>("stations/search", searchParams);

		public Task<List<StationInfo?>?> SearchStationsByUuid(StationsSearchByUuid searchParams)
			=> ListRequest<StationInfo>("stations/byuuid", searchParams);

		public Task<IAsyncEnumerable<StationInfo?>?> SearchStationsByUuidAsyncEnumerable(StationsSearchByUuid searchParams)
			=> AsyncEnumerableRequest<StationInfo>("stations/byuuid", searchParams);

		public Task<List<StationInfo?>?> SearchStationsByUrl(StationsSearchByUrl searchParams)
			=> ListRequest<StationInfo>("stations/byurl", searchParams);
		
		public Task<IAsyncEnumerable<StationInfo?>?> SearchStationsByUrlAsyncEnumerable(StationsSearchByUrl searchParams)
			=> AsyncEnumerableRequest<StationInfo>("stations/byurl", searchParams);

		private async Task<List<T?>?> ListRequest<T>(string baseUrl, object? listParams, string? filter = null)
			where T : class
		{
			var queryString = GetQueryString(listParams);
			var url = filter == null
				? $"{baseUrl}{queryString}"
				: $"{baseUrl}/{filter}{queryString}";
			return await MakeGetRequest<List<T?>>(url);
		}

		private async Task<T?> MakeGetRequest<T>(string url)
			where T : class
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
