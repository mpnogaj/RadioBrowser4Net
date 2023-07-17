using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
    public class StationCheckResult
    {
        [JsonPropertyName("stationuuid")]
        public Guid StationUuid { get; set; }

        [JsonPropertyName("checkuuid")]
        public Guid CheckUuid { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("codec")]
        public string Codec { get; set; }

        [JsonPropertyName("bitrate")]
        public int BitRate { get; set; }

        [JsonPropertyName("hls")]
        public int HlsRaw { get; set; }
        [JsonIgnore]
        public bool Hls => HlsRaw == 1;

        [JsonPropertyName("ok")]
        public int OkRaw { get; set; }
		[JsonIgnore]
		public bool Ok => OkRaw == 1;

		[JsonPropertyName("timestamp_iso8601")]
        public string TimestampRaw { get; set; }
        [JsonIgnore] 
        public DateTime Timestamp => DateTime.Parse(TimestampRaw);

        [JsonPropertyName("urlcache")]
        public string UrlCacheRaw { get; set; }
        [JsonIgnore]
        public Uri UrlCache => new Uri(UrlCacheRaw);

		[JsonPropertyName("metainfo_overrides_database")]
        public int MetaInfoOverridesDatabaseRaw { get; set; }
        [JsonIgnore]
        public bool MetaInfoOverridesDatabase => MetaInfoOverridesDatabaseRaw == 1;

		[JsonPropertyName("public")]
        public int? PublicRaw { get; set; }
        [JsonIgnore]
        public bool? Public
        {
	        get
	        {
		        if (!PublicRaw.HasValue)
			        return null;
		        return PublicRaw.Value == 1;
	        }
        }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("tags")]
        public string? TagsRaw { get; set; }
		[JsonIgnore]
        public List<string>? Tags => TagsRaw?.Split(',').ToList() ?? null;

        [JsonPropertyName("countrycode")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("countrysubdivisioncode")]
        public string? CountrySubdivisionCode { get; set; }

        [JsonPropertyName("homepage")]
        public string? HomepageRaw { get; set; }
        [JsonIgnore] 
        public Uri? Homepage => HomepageRaw != null ? new Uri(HomepageRaw) : null;

        [JsonPropertyName("favicon")]
        public string? FaviconRaw { get; set; }
		[JsonIgnore]
        public Uri? Favicon => FaviconRaw != null ? new Uri(FaviconRaw) : null;

        [JsonPropertyName("loadbalancer")]
        public string? LoadBalancerRaw { get; set; }
        [JsonIgnore]
        public Uri? LoadBalancer => LoadBalancerRaw != null ? new Uri(LoadBalancerRaw) : null;

        [JsonPropertyName("server_software")]
        public string? ServerSoftware { get; set; }

        [JsonPropertyName("sampling")]
        public int? Sampling { get; set; }

        [JsonPropertyName("timing_ms")]
        public int TimingMs { get; set; }

        [JsonPropertyName("languagecodes")]
        public string? LanguageCodes { get; set; }

        [JsonPropertyName("ssl_error")]
        public int SslErrorRaw { get; set; }
        [JsonIgnore]
        public bool SslError => SslErrorRaw == 1;

        [JsonPropertyName("geo_lat")]
        public double? GeoLat { get; set; }

        [JsonPropertyName("geo_long")]
        public double? GeoLong { get; set; }
    }
}
