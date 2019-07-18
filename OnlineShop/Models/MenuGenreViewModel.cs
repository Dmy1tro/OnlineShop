using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class MenuGenreViewModel
    {
        public Dictionary<string, int> Genres { get; set; }
        public string CurrentGenre { get; set; }
    }
}