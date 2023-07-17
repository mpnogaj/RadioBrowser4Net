using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
	public enum UrlType
	{
		[Description("STREAM")]
		Stream,
		[Description("REDIRECT")]
		Redirect,
		[Description("PLAYLIST")]
		Playlist
	}

	public class StationStepCheck
	{
		[JsonPropertyName("stepuuid")]
		public Guid StepUuid { get; set; }

		[JsonPropertyName("parent_stepuuid")]
		public Guid? ParentStepUuid { get; set; }

		[JsonPropertyName("checkuuid")]
		public Guid CheckUuid { get; set; }

		[JsonPropertyName("stationuuid")]
		public Guid StationUuid { get; set; }

		[JsonPropertyName("url")]
		public string UrlRaw { get; set; }
		[JsonIgnore]
		public Uri Url => new Uri(UrlRaw);

		[JsonPropertyName("urltype")]
		public string? UrlTypeRaw { get; set; }
		[JsonIgnore]
		public UrlType? UrlType
		{
			get
			{
				if (UrlTypeRaw == null)
					return null;

				var fields = typeof(UrlType).GetFields();
				foreach (var field in fields)
				{
					var descriptionAttribute = field
						.GetCustomAttributes(true)
						.OfType<DescriptionAttribute>()
						.FirstOrDefault();
					if (descriptionAttribute != null && descriptionAttribute.Description == UrlTypeRaw)
						return (UrlType)field.GetValue(null);
				}

				throw new Exception("Invalid enum value");
			}
		}

		[JsonPropertyName("error")]
		public string? Error { get; set; }

		[JsonPropertyName("creation_iso8601")]
		public string CreationTimeRaw { get; set; }
		[JsonIgnore]
		public DateTime CreationTime => DateTime.Parse(CreationTimeRaw);
	}
}
