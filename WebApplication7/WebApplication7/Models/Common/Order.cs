using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication7.Models.Common
{
    public class Order
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Product product { get; set; }
        public virtual Customer customer { get; set; }
    }
}
