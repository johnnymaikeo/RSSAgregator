using System;
using System.Collections.Generic;
using System.Text;

namespace RSSAgregator.Model
{
    public class RSSItem
    {
        // Low cases to match RSS content
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string pubDate { get; set; }
    }
}
