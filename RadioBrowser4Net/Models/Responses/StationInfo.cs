using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using RadioBrowser4Net.Internals;

namespace RadioBrowser4Net.Models.Responses
{
	[XmlRoot("station")]
    public class StationInfo
    {
		[JsonPropertyName("changeuuid")]
        public Guid ChangeUuid { get; set; }

		[JsonPropertyName("stationuuid")]
        public Guid StationUuid { get; set; }

		[JsonPropertyName("name")]
        public string Name { get; set; }

		[JsonPropertyName("url")]
        public string UrlRaw { get; set; }
        [JsonIgnore]
        public Uri Url => new Uri(UrlRaw);

        [JsonPropertyName("url_resolved")]
        public string UrlResolvedRaw { get; set; }
        [JsonIgnore] 
        public Uri UrlResolved => new Uri(UrlResolvedRaw);

	    [JsonPropertyName("homepage")]
        public string HomePageRaw { get; set; }
        [JsonIgnore] 
        public Uri HomePage => new Uri(HomePageRaw);

		[JsonPropertyName("favicon")]
        public string FaviconRaw { get; set; }

        [JsonIgnore]
        public Uri? Favicon
	        => Uri.TryCreate(FaviconRaw, UriKind.Absolute, out var uri) ? uri : null;

	    [JsonPropertyName("tags")]
		public string TagsRaw { get; set; }
		[JsonIgnore] 
		public List<string> Tags => TagsRaw.ParseCommaSeparatedString();

	    [Obsolete("Use CountryCode instead")]
		[JsonPropertyName("country")]
        public string Country { get; set; }

		[JsonPropertyName("countrycode")]
        public string CountryCode { get; set; }

		[JsonPropertyName("state")]
        public string State { get; set; }

		[JsonPropertyName("language")]
		public string LanguagesRaw { get; set; }
		[JsonIgnore] 
		public List<string> Language => LanguagesRaw.ParseCommaSeparatedString();

		[JsonPropertyName("languagecodes")]
		public string LanguageCodesRaw { get; set; }
        [JsonIgnore] 
        public List<string> LanguageCodes => LanguageCodesRaw.ParseCommaSeparatedString();

		[JsonPropertyName("votes")]
        public int Votes { get; set; }

		[JsonPropertyName("lastchangetime_iso8601")]
        public string LastChangeTimeRaw { get; set; }
        [JsonIgnore] 
        public DateTime LastChangeTime => DateTime.Parse(LastChangeTimeRaw);

		[JsonPropertyName("codec")]
		public string Codec { get; set; }
        
		[JsonPropertyName("bitrate")]
		public int BitRate { get; set; }

		[JsonPropertyName("hls")]
		public int HlsRaw { get; set; }
		[JsonIgnore] 
		public bool Hls => HlsRaw == 1;

		[JsonPropertyName("lastcheckok")]
        public int LastCheckOkRaw { get; set; }
        [JsonIgnore]
        public bool LastCheckOk => LastCheckOkRaw == 1;

        [JsonPropertyName("lastchecktime_iso8601")]
        public string LastCheckTimeRaw { get; set; }
        [JsonIgnore]
        public DateTime LastCheckTime => DateTime.Parse(LastCheckTimeRaw);

        [JsonPropertyName("lastcheckoktime_iso8601")]
        public string LastCheckOkTimeRaw { get; set; }
        [JsonIgnore]
        public DateTime LastCheckOkTime => DateTime.Parse(LastCheckOkTimeRaw);

        [JsonPropertyName("lastlocalchecktime_iso8601")]
        public string LastLocalCheckTimeRaw { get; set; }
        [JsonIgnore]
        public DateTime LastLocalCheckTime => DateTime.Parse(LastLocalCheckTimeRaw);

        [JsonPropertyName("clicktimestamp_iso8601")]
        public string ClickTimestampRaw { get; set; }
        [JsonIgnore]
        public DateTime ClickTimestamp => DateTime.Parse(ClickTimestampRaw);

		[JsonPropertyName("clickcount")]
		public int ClickCount { get; set; }

		[JsonPropertyName("clicktrend")]
        public int ClickTrend { get; set; }

        [JsonPropertyName("ssl_error")]
        public int SSLErrorRaw { get; set; }
        [JsonIgnore]
        public bool SSLError => SSLErrorRaw == 1;

        [JsonPropertyName("geo_lat")]
        public double? GeoLat { get; set; }

        [JsonPropertyName("geo_long")]
        public double? GeoLong { get; set; }

		[JsonPropertyName("has_extended_info")]
        public bool? HasExtendedInfo { get; set; }
    }
}
