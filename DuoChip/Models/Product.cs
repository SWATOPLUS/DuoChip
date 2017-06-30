using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuoChip.Models
{
    public class Product
    {
        public string Name { get; }
        public decimal? Cost { get; }
        public uint? DaysToDeliver { get; }
        public Dictionary<string,string> Characteristics { get; }
        public string ProductLink { get; }
        public string PictureLink { get; }

        public Product(string name, decimal? cost, uint? daysToDeliver, Dictionary<string, string> characteristics,string productLink, string pictureLink)
        {
            Name = name;
            Cost = cost;
            DaysToDeliver = daysToDeliver;
            Characteristics = characteristics;
            ProductLink = productLink;
            PictureLink = pictureLink;
        }

        public static Product SampleProductOne() {
            var dict = new Dictionary<string, string>();
            dict.Add("Напряжение","10 А");
            dict.Add("Температура", "180 С");
            dict.Add("Производитель","Китай");

            var pict = "http://lib.chipdip.ru/540/DOC001540661.jpg";
            var prod = "https://www.ru-chipdip.by/product/zh103-135";

            return new Product("Термопредохранитель",0.65m,null,dict,prod,pict);
        }
        public static Product SampleProductTwo()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Напряжение", "10 А");
            dict.Add("Температура", "180 С");
            dict.Add("Производитель", "Китай");

            var pict = "http://lib.chipdip.ru/540/DOC001540661.jpg";
            var prod = "https://www.ru-chipdip.by/product/zh103-135";

            return new Product("Термопредохранитель", 0.65m, 0, dict, prod, pict);
        }
        public static Product SampleProductThree()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Напряжение", "10 А");
            dict.Add("Температура", "180 С");
            dict.Add("Производитель", "Китай");

            var pict = "http://lib.chipdip.ru/540/DOC001540661.jpg";
            var prod = "https://www.ru-chipdip.by/product/zh103-135";

            return new Product("Термопредохранитель", 0.65m, 5, dict, prod, pict);
        }

    }
}