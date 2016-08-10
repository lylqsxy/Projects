using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication7.Models.Common
{
    public class Customer
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddr { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
