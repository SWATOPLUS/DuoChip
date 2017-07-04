using System;
using System.Collections.Generic;
using System.Linq;
using DuoChip.Models;

namespace DuoChip.Controllers.Parsers
{
    public class RuChipDipSearcher
    {
        static System.Globalization.CultureInfo format = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        const string site = "http://www.ru-chipdip.by/";
        static HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

        static public List<Product> search(string text)
        {
            var url = site + "search?searchtext=" + text + "&page=";
            var idx = 1;
            var list = new List<Product>();
            while (true)
            {
                var address = url + idx;
                var document = web.Load(address);
                if (web.StatusCode != System.Net.HttpStatusCode.OK) break;
                var notFoundElement = document.DocumentNode.SelectNodes("//span[@class='bc__item']");
                if (null != notFoundElement) {
                    var node = notFoundElement.First();
                    if (node.InnerText == "Не найдено") break;
                }
                idx++;
                var nodes = document.DocumentNode.SelectNodes("//a[@class='link group-header']");
                if(nodes!=null)
                foreach (var x in nodes)
                {
                    list.AddRange(processCat(x.Attributes["href"].Value));
                }
            }          
            return list;
        }
        static private List<Product> processCat(string url) {
            if (url.IndexOf('?') == -1) url = site+url+"?page=";
            else url=site + url + "&page=";
            var list = new List<Product>();
            var idx = 1;
            while (true)
            {
                var document = web.Load(url + idx);
                if (web.StatusCode != System.Net.HttpStatusCode.OK) break;
                idx++;
                var nodes = document.DocumentNode.SelectNodes("//div[@class='item__content']");
                if(nodes!=null)
                foreach (var x in nodes)
                {
                    list.Add(processProduct(x));
                }
            }
            return list;
        }
        static private Product processProduct(HtmlAgilityPack.HtmlNode node) {
            var priceNode = node.SelectNodes("*/*/*/span[@class='price__value']").First();
            var imageNode = node.SelectNodes("*/*/img[@class='item__image']").First();
            var mnfNode = node.SelectNodes("*/div[@class='item__mnf']").First();
            var nameNode = node.SelectNodes("*/div[@class='item__name']/*").First();
            var prodNode = node.SelectNodes("a[@class='link link_no-underline']").First();
            var availNode = node.SelectNodes("div[@class='item__avail']/div").First();

            string avail= availNode.InnerText;

            if (avail == "Со склада")
            {
                avail = "";
            }
            else if (avail == "По запросу")
            {
                avail = null;
            }
            var cost_str = priceNode.InnerText.Replace(" ","");
            var cost = Convert.ToDecimal(cost_str,format);
            var pictureLink = imageNode.Attributes["src"].Value;
            var productLink = site + prodNode.Attributes["href"].Value;
            var name = nameNode.InnerText;
            var dict = new Dictionary<string, string>();
            dict.Add("Производитель", mnfNode.InnerText);

            return new Product(name,cost,avail,dict,productLink, pictureLink);
        }
    }
}