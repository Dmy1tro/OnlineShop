using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class OrderDetail
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(OrderHeader))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        public int Count { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual Book Book { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }
    }
}