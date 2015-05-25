using System;
using System.Collections.Generic;
using System.Text;

namespace RSSAgregator.Model
{
    public class Bookmarks
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Bookmarks(string _name, string _url)
        {
            this.Name = _name;
            this.Url = _url;
        }

        // Parameterless constructor to allow serialization
        public Bookmarks()
        {

        }
    }
}
