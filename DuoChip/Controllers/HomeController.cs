using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuoChip.Models;
using DuoChip.Controllers.Parsers;
using DuoChip.Controllers.Helpers;
namespace DuoChip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string text, string page,string operation)
        {
            if (string.IsNullOrWhiteSpace(text))
                text = Request.QueryString["text"];

            if (string.IsNullOrWhiteSpace(page) || page == "0")
                page = Request.QueryString["page"];

            if (string.IsNullOrWhiteSpace(text))
                return new EmptyResult();
            int req_page;
            try
            {
                req_page = Convert.ToInt32(page);
            }
            catch (Exception)
            {
                req_page = 0;
            }

            SearchCache cache;
            if (globalCache.ContainsKey(text))
            {
                cache = globalCache[text];
            }
            else
            {
                cache = new SearchCache(text);
                globalCache.TryAdd(text, cache);
            }
            var list = cache.GetPage(req_page);
            if (req_page > 0)
            {
                ViewBag.Page = req_page;
                ViewBag.Text = text;
                ViewBag.Processing = cache.IsProcessing;
                ViewBag.LastPage = cache.LastPage;
                if (Request.QueryString["operation"]!=null || operation!=null)
                    return PartialView("AjaxPageProductsView", list);
                return PartialView("PageProductsView", list);
            }
            return PartialView("AllProductsView", list);
        }

        private static ConcurrentDictionary<string, SearchCache> globalCache = new ConcurrentDictionary<string, SearchCache>();


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