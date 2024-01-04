using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MacroDataSendler
{
    public class IndexHistoricalInfo
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Month { get; set; }
        public string Actual { get; set; }
        public string Previous { get; set; }
        public string Consensus { get; set; }
        public string Forecast { get; set; }

        public static List<IndexHistoricalInfo> GetMarketDataList(string url)
        {
            var housingMarketDataList = new List<IndexHistoricalInfo>();
            HtmlDocument doc = GetHtml(url);
            HtmlNodeCollection rowNodes = doc.DocumentNode.SelectNodes("//tr[@class='an-estimate-row']");

            if (rowNodes != null)
            {
                foreach (var rowNode in rowNodes)
                {
                    var data = new IndexHistoricalInfo
                    {
                        Date = DateTime.Parse(rowNode.SelectSingleNode("td[1]").InnerText),
                        Name = rowNode.SelectSingleNode("td[3]").InnerText.Trim(),
                        Time = rowNode.SelectSingleNode("td[2]").InnerText.Trim(),
                        Month = rowNode.SelectSingleNode("td[4]").InnerText.Trim(),
                        Actual = rowNode.SelectSingleNode("td[5]").InnerText.Trim(),
                        Previous = rowNode.SelectSingleNode("td[6]").InnerText.Trim(),
                        Consensus = rowNode.SelectSingleNode("td[7]").InnerText.Trim(),
                        Forecast = rowNode.SelectSingleNode("td[8]").InnerText.Trim(),
                    };
                    housingMarketDataList.Add(data);
                }
            }

            return housingMarketDataList;
        }
        private static HtmlDocument GetHtml(string name)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(name);
            return doc;
        }

        public static List<string> ReadUrlsFromFile(string filePath)
        {
            List<string> Urls = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Urls.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }

            return Urls;
        }
    }
}
