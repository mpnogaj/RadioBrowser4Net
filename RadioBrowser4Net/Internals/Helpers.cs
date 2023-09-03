using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioBrowser4Net.Internals
{
	internal static class Helpers
	{
		public static List<string> ParseCommaSeparatedString(this string? str)
		{
			return string.IsNullOrWhiteSpace(str)
				? new List<string>()
				: str.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
		}
	}
}
