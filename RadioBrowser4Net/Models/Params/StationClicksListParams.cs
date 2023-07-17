using System;

namespace RadioBrowser4Net.Models.Params
{
	public class StationClicksListParams
	{
		public Guid? LastClickUuid { get; set; }
		public int Seconds { get; set; } = 0;
	}
}
