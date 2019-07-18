using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Models
{
    public class MenuFilterViewModel
    {
        [Required]
        [Display(Name = "minimum price")]
        [Range(typeof(decimal), minimum: "0.00", maximum: "999999.9", ErrorMessage = "Please, write correct book's price")]
        public decimal MinPrice { get; set; }

        [Required]
        [Display(Name = "maximum price")]
        [Range(typeof(decimal), minimum: "0.00", maximum: "999999.9", ErrorMessage = "Please, write correct book's price")]
        public decimal MaxPrice { get; set; }

        public string SortItem { get; set; }

        public string Genre { get; set; }

        public IEnumerable<SelectListItem> SortList => _sortItems;
        private readonly List<SelectListItem> _sortItems = new List<SelectListItem> { new SelectListItem { Text = "Default", Value = "Default" },
                                                                                      new SelectListItem { Text = "----------Name--------", Disabled = true },
                                                                                      new SelectListItem { Text = "Name (A-Z)", Value = "Name_ASC"},
                                                                                      new SelectListItem { Text = "Name (Z-A)", Value = "Name_DESC"},
                                                                                      new SelectListItem { Text = "----------PRICE-------", Disabled = true },
                                                                                      new SelectListItem { Text = "Price (from Min to Max)", Value = "Price_ASC" },
                                                                                      new SelectListItem { Text = "Price (from Max to Min)", Value = "Price_DESC" }};
    }
}