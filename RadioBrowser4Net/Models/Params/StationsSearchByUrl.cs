using System;
using System.Collections.Generic;
using System.Text;

namespace RadioBrowser4Net.Models.Params
{
	public class StationsSearchByUrl
	{
		public StationsSearchByUrl(string url)
		{
			Url = url;
		}

		public string Url { get; set; }
	}
}
