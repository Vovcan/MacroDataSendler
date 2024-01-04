using MacroDataSendler;

List<string> urls = IndexHistoricalInfo.ReadUrlsFromFile("data.txt");

for (int i = 0; i < urls.Count; i++)
{
    var info = IndexHistoricalInfo.GetMarketDataList(urls[i]);
    foreach (var data in info)
    {
        Console.WriteLine($"Name: {data.Name}, Date: {data.Date}, Time: {data.Time}, Month: {data.Month}, Actual: {data.Actual}, Previous: {data.Previous}, Consensus: {data.Consensus}, Forceast: {data.Forecast}");
    }
}