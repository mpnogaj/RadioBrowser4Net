using RadioBrowser4Net.Internals;

namespace RadioBrowser4Net.Models.Params
{
	public class StationsAdvancedSearch
	{
		public string? Name { get; set; }

		[QueryPropertyName("nameExact")]
		public bool NameExact { get; set; } = false;

		public string? Country { get; set; }

		[QueryPropertyName("countryExact")] 
		public bool CountryExact { get; set; } = false;

		public string? CountryCode { get; set; }

		public string? State { get; set;}

		[QueryPropertyName("stateExact")]
		public bool StateExact { get; set; } = false;

		public string? Language { get; set; }

		[QueryPropertyName("languageExact")]
		public bool LanguageExact { get; set; } = false;

		public string? Tag { get; set; }

		[QueryPropertyName("tagExact")]
		public bool TagExact { get; set; } = false;

		[QueryPropertyName("tagList")]
		public string? TagList { get; set; }

		public string? Codec { get; set; }

		[QueryPropertyName("bitrateMin")]
		public int BitRateMin { get; set; } = 0;

		[QueryPropertyName("bitrateMax")]
		public int BitRateMax { get; set; } = 1000000;

		[QueryPropertyName("has_geo_info")]
		public bool? HasGeoInfo { get; set; }

		[QueryPropertyName("has_extended_info")]
		public bool? HasExtendInfo { get; set; }

		[QueryPropertyName("is_https")]
		public bool? IsHttps { get; set; }

		public StationsListOrder Order { get; set; } = StationsListOrder.Name;

		public bool Reverse { get; set; } = false;

		public int Offset { get; set; } = 0;

		public int Limit { get; set; } = 100000;

		[QueryPropertyName("hidebroken")]
		public bool HideBroken { get; set; } = false;
	}
}
