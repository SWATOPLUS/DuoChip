﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuoChip.Models;
using DuoChip.Controllers.Parsers;
namespace DuoChip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string text)
        {
            if (text.Replace(" ", "") == "")
                return new EmptyResult();
            var list = new List<Product>();

            //list.Add(Product.SampleProductOne());
            //list.Add(Product.SampleProductTwo());
            //list.Add(Product.SampleProductThree());


            list.AddRange(BelChipSearcher.search(text));
            list.AddRange(RuChipDipSearcher.search(text));
            return PartialView("AllProductsView",list);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}