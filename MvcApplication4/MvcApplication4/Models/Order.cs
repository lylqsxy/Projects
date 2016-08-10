using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication4.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ContactId { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public Contact Contact { get; set; }
    }
}