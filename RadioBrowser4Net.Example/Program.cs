using RadioBrowser4Net;
using RadioBrowser4Net.Models;
using RadioBrowser4Net.Models.Params;

var client = new RadioBrowserClient();

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

var stations = await client.ListStations(new StationsListParams
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
stations?.Select(x => x.Name)
	.ToList()
	.ForEach(Console.WriteLine);