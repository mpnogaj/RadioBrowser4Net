using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Internals.JsonConverters
{
	internal class ListConverter : JsonConverter<List<string>>
	{
		public override List<string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetString()?.
				Split(',').
				Select(x => x.Trim()).
				ToList();
		}

		public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(string.Join(',', value));
		}
	}
}
