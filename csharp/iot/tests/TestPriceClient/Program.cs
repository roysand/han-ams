// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Xml.Serialization;
using Application.Common.Helpers;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

IConfiguration config = null;

var client = new WebApiClientPrice(config);
var response = await client.GetPriceDayAhead();

var content = await response.Content.ReadAsStringAsync();

var serializer = new XmlSerializer(typeof(Publication_MarketDocument));
Publication_MarketDocument result;

using (TextReader reader = new StringReader(content))
{
    result = (Publication_MarketDocument)serializer.Deserialize(reader);
}

Console.WriteLine(result.ToString());
Console.WriteLine($"Currency: {result.TimeSeries.currency_Unitname} Measure unit: {result.TimeSeries.price_Measure_Unitname}");
var startDate = DateTime.Parse(result.periodtimeInterval.start).ToLocalTime();
var endDate = DateTime.Parse(result.periodtimeInterval.end).ToLocalTime();

Console.WriteLine($"StartDate: {startDate} EndDate: {endDate}");
Console.WriteLine(content);