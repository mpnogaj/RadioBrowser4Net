using RadioBrowser4Net;
using RadioBrowser4Net.Models;
using RadioBrowser4Net.Models.Params;

var client = new RadioBrowserClient();

var stationNames = new List<string>();
var stations = await client.AsyncListAllStations(new StationsListParams
{
	HideBroken = false,
	Limit = 1000000,
	Order = StationsListOrder.Votes,
	Reverse = true
});

if (stations != null)
{
	await foreach (var station in stations)
	{
		if (station != null) stationNames.Add(station.Name);
	}
}

Console.WriteLine(stationNames.Count);

stationNames.Clear();
var stations2 = await client.ListAllStations(new StationsListParams
{
	HideBroken = false,
	Limit = 1000000,
	Order = StationsListOrder.Votes,
	Reverse = true
});

if (stations2 != null)
{
	foreach (var station in stations2)
	{
		stationNames.Add(station.Name);
	}
}

Console.WriteLine(stationNames.Count);


var languages = await client.ListLanguages(new BasicListParams
{
	Order = BasicListOrder.Name
});

var countries = await client.ListCountries(new BasicListParams
{
	Order = BasicListOrder.StationCount,
	Reverse = true
});

var tags = await client.ListTags(new BasicListParams
{
	Order = BasicListOrder.StationCount,
	Reverse = true,
	Limit = 10
});

var rmf = await client.ListStations(new StationsListParams
{
	Reverse = true
}, StationsSearchBy.Name, "rmf");

Console.WriteLine("==Languages==");
languages?.Select(x => x.Name)
	.ToList()
	.ForEach(Console.WriteLine);

Console.WriteLine(string.Empty);

Console.WriteLine("==Countries==");
countries?.Select(x => $"{x.Name}-{x.StationCount}")
	.ToList()
	.ForEach(Console.WriteLine);

Console.WriteLine(string.Empty);

Console.WriteLine("==Tags==");
tags?.Select(x => $"{x.Name}-{x.StationCount}")
	.ToList()
	.ForEach(Console.WriteLine);

Console.WriteLine(string.Empty);

Console.WriteLine("==Stations==");
rmf?.Select(x => x.Name)
	.ToList()
	.ForEach(Console.WriteLine);