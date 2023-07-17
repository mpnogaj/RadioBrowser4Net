using System;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
	public class StationClickInfo
	{
		[JsonPropertyName("stationuuid")]
		public Guid StationUuid { get; set; }

		[JsonPropertyName("clickuuid")]
		public Guid ClickUuid { get; set; }

		[JsonPropertyName("clicktimestamp_iso8601")]
		public string ClickTimestampRaw { get; set; }
		[JsonIgnore]
		public DateTime ClickTimestamp => DateTime.Parse(ClickTimestampRaw);
	}
}
