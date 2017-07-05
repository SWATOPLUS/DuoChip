using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using DuoChip.Controllers.Parsers;
using DuoChip.Models;

namespace DuoChip.Controllers.Helpers
{
    public class SearchCache
    {
        private const int items_per_page=20;
        private string text { get; }
        private List<Product> list=new List<Product>();
        private Thread thread;

        public int LastPage
        {
            get
            {
                var x = list.Count;
                return x / items_per_page + (x % items_per_page == 0 ? 0 : 1);
            }
        }
        public bool IsProcessing
        {
            get {
                return thread.IsAlive;
            }
        }          

        public SearchCache(string text) {
            this.text = text;
            thread = new Thread(processSearch);
            thread.Start();
                     
        }
        public List<Product> GetPage(int page) {
            if (page == 0) {
                if(thread.IsAlive)
                    thread.Join();
                return list;
            }
            var idx = page * items_per_page;
            while (list.Count < idx && thread.IsAlive) {
                Thread.Sleep(1000);
            }
            var first = (page - 1) * items_per_page;
            if (list.Count < idx)
            {
                var c = list.Count - first;
                if (c < 0) return new List<Product>();
                return list.GetRange(first, c);
            }
            return list.GetRange(first, items_per_page);

        }
        private void processSearch() {
            list.AddRange(BelChipSearcher.search(text));
            list.AddRange(RuChipDipSearcher.search(text));
        }
        public void Kill() {
            try
            {
                if (thread.IsAlive)
                    thread.Abort();
            }
            catch (ThreadAbortException) { }
        }
    }
}