using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5.Common
{
    public class Order
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public int customerId { get; set; }
        public DateTime orderDate { get; set; }
    }
}
