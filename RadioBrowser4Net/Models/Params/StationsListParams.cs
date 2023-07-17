using System;
using System.Collections.Generic;
using System.Text;

namespace RadioBrowser4Net.Models.Params
{
	public enum StationsListOrder
	{
		Name,
		Url,
		Homepage,
		Favicon,
		Tags,
		Country,
		State,
		Language,
		Votes,
		Codec,
		BitRate,
		LastCheckOk,
		LastCheckTime,
		ClickTimestamp,
		ClickCount,
		ClickTrend,
		ChangeTimestamp,
		Random
	}

	public class StationsListParams
	{
		public StationsListOrder? Order { get; set; } = StationsListOrder.Name;
		public bool? Reverse { get; set; } = false;
		public int? Offset { get; set; } = 0;
		public int? Limit { get; set; } = 100000;
		public bool? HideBroken { get; set; } = false;
	}
}
