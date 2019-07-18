﻿using OnlineShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class OrderDetailsViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        public Cart Cart { get; set; }
    }
}