using System;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses.Actions
{
    public class ClickActionResult : ActionResult
    {
		[JsonPropertyName("stationuuid")]
        public Guid StationUuid { get; set; }
		
        [JsonPropertyName("name")]
        public string Name { get; set; }

		[JsonPropertyName("url")]
		public string UrlRaw { get; set; }
		[JsonIgnore]
        public Uri Url => new Uri(UrlRaw);
    }
}
