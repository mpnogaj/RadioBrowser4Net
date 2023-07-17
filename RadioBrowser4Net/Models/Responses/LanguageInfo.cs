using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
    public class LanguageInfo
    {
		[JsonPropertyName("name")]
        public string Name { get; set; }

		[JsonPropertyName("iso_639")]
        public string? ISO_639 { get; set; }

		[JsonPropertyName("stationcount")]
        public uint StationCount { get; set; }
    }
}
