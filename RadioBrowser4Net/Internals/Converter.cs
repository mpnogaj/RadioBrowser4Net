using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace RadioBrowser4Net.Internals
{
	internal class Converter
	{
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		internal Converter()
		{
			_jsonSerializerOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
			};
		}
	}
}
