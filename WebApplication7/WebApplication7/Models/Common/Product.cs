using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication7.Models.Common
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDes { get; set; }
        public string ProductCat { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
