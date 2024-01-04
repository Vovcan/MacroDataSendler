using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MacroDataSendler
{
    public class ListCreator
    {

        private List<(string TickerName, string LinkToTocker)> data;
        public void ListCrete()
        {
            data = new List<(string, string)>();
            ScrapData();
        }
        void ScrapData()
        {
            HtmlDocument doc = GetHtml("https://tradingeconomics.com/calendar");

            var eventNodes = doc.DocumentNode.SelectNodes("//td[@style='max-width: 250px; overflow-x: hidden;']");

            if (eventNodes != null)
            {
                foreach (var eventNode in eventNodes)
                {
                    // Отримати текст та атрибут href з елементу a
                    string eventName = eventNode.SelectSingleNode(".//a")?.InnerText.Trim();
                    string eventLink = eventNode.SelectSingleNode(".//a")?.GetAttributeValue("href", "").Trim();

                    // Додати дані до списку
                    if (!string.IsNullOrEmpty(eventName) && !string.IsNullOrEmpty(eventLink))
                    {
                        data.Add((eventName, eventLink));
                    }
                }
            }

            // Вивести дані
            foreach (var item in data)
            {
                Console.WriteLine($"TickerName: {item.TickerName}, LinkToTocker: {item.LinkToTocker}");
            }
        }

        private static HtmlDocument GetHtml(string name)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(name);
            return doc;
        }
    }
}
