using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadioBrowser4Net.Models.Params
{
	public class StationCheckStepsListParams
	{
		public string Uuids { get; }

		public StationCheckStepsListParams(IEnumerable<Guid> uuids)
		{
			var uuidList = uuids.ToList();
			if (!uuidList.Any())
				throw new ArgumentException("Uuids list must contain at least one element", nameof(uuids));

			Uuids = string.Join(',', uuidList);
		}
	}
}
