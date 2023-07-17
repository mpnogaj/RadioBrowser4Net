using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
    public class CountryInfo
    {
		[JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iso_3166_1")]
		public string ISO_3166_1 { get; set; }

		[JsonPropertyName("stationcount")]
        public uint StationCount { get; set; }
    }
}
