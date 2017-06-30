using System;
using System.Collections.Generic;
using System.Linq;
using DuoChip.Models;

namespace DuoChip.Controllers.Parsers
{
    public class BelChipSearcher
    {
        static System.Globalization.CultureInfo format = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");

        static public List<Product> search(string text)
        {
            var site = "http://belchip.by/";
            var url = site + "search/?query=" + text;
            var list = new List<Product>();
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlWeb().Load(url);
            var nodes = document.DocumentNode.SelectNodes("//div[@class='cat-item']");
            if(nodes!=null)
            foreach (var x in nodes)
            {
                var nameNode = x.SelectNodes("h3/a").First();
                var imgNode = x.SelectNodes("div[1]/a[2]/img").First();
                var costNode = x.SelectNodes("*/*/div[@class='denoPrice']");

                decimal? cost = null;
                string days = null;

                if (costNode != null)
                {
                    cost = Convert.ToDecimal(costNode.First().FirstChild.InnerText, format);
                    days = "";
                }

                var name = nameNode.FirstChild.InnerText;
                var productLink = site + nameNode.Attributes["href"].Value;
                var pictureLink = site + imgNode.Attributes["src"].Value;
                

                var infoNode = x.SelectNodes("div[@class='popup']").First();
                var infoTable = infoNode.SelectNodes("*/*/*/*/table");

                var infoDict = new Dictionary<string, string>();

                if (infoTable != null && infoTable.Count>0) {
                    foreach (var p in infoTable.First().ChildNodes) {
                        if (p.Name != "tr") continue;
                        var key = p.SelectNodes("td[1]").First().InnerText;
                        var value = p.SelectNodes("td[2]").First().InnerText;
                        infoDict.Add(key, value);
                    }
                }

                list.Add(new Product(name, cost, days, infoDict, productLink, pictureLink));

                x.Clone();

            }
            return list;
        }
    }
}