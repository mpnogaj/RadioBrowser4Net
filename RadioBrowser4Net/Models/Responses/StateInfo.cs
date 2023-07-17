﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses
{
    public class StateInfo
    {
		[JsonPropertyName("name")]
        public string Name { get; set; }

		[JsonPropertyName("country")]
        public string Country { get; set; }

		[JsonPropertyName("stationcount")]
        public uint StationCount { get; set; }
    }
}
