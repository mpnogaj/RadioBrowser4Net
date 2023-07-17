using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Internals.JsonConverters
{
	internal class UriConverter : JsonConverter<Uri>
	{
		public override Uri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var uriString = reader.GetString()!;
			try
			{
				return new Uri(uriString);
			}
			catch (UriFormatException)
			{
				Trace.WriteLine($"Cannot parse URI ({uriString}).");
				return null;
			}
		}

		public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
