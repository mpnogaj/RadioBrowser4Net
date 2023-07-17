using System;
using System.Collections.Generic;
using System.Text;

namespace RadioBrowser4Net.Models.Params
{
	public enum BasicListOrder
	{
		Name,
		StationCount
	}

	public class BasicListParams
	{
		public BasicListOrder? Order { get; set; } = BasicListOrder.Name;
        public bool? Reverse { get; set; } = false;
        public bool? HideBroken { get; set; } = false;
        public int? Offset { get; set; } = 0;
		public int? Limit { get; set; } = 100000;
    }
}
