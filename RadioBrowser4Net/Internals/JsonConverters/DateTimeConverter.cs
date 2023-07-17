using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Internals.JsonConverters
{
	internal class DateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var time = new DateTime(0);
			var stringDate = reader.GetString()!;
			if(DateTime.TryParseExact(
				   stringDate, 
				   "yyyy-MM-dd HH:mm:ss", 
				   CultureInfo.InvariantCulture,
				   DateTimeStyles.None,
				   out time))
			{
				return time;
			}

			if (DateTime.TryParseExact(
				    stringDate,
				    "yyyy-MM-ddTHH:mm:ssZ",
				    CultureInfo.InvariantCulture,
				    DateTimeStyles.None,
				    out time))
			{
				return time;
			}

			Trace.WriteLine($"Cannot parse date ({stringDate}).");
			return time;
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue($"{value:yyyy-MM-dd HH:mm:ss}");
		}
	}
}
