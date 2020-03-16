using System.Collections.Generic;

namespace OnlineShop.ViewModels
{
    public class MenuGenreViewModel
    {
        public Dictionary<string, int> Genres { get; set; }
        public string CurrentGenre { get; set; }
    }
}