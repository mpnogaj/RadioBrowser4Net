using System;
using System.Collections.Generic;
using System.Text;

namespace RadioBrowser4Net.Internals
{
	internal class QueryPropertyNameAttribute : Attribute
	{
		public string QueryName { get; set; }

		public QueryPropertyNameAttribute(string queryName)
		{
			QueryName = queryName;
		}
	}
}
