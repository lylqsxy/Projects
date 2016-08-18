using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Model
{
    public abstract class Product
    {
        public int IdGuid { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
