using System;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
    [Obsolete("CountryCode endpoint is obsolete by API. Please use countries endpoint instead.")]
    public class CountryCodeInfo
    {
		[JsonPropertyName("name")]
        public string Name { get; set; }

		[JsonPropertyName("stationcount")]
        public uint StationCount { get; set; }
    }
}
