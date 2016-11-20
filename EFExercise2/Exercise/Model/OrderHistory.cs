using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Model
{
    public class OrderHistory
    {
        public Customer Customer { get; set; }
        public List<Product> Order { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
