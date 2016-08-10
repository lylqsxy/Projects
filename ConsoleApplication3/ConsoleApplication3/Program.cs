using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Cat cat1 = new Cat
            {
                Id = 1,
                Name = "aaa",
                Color = "blue"
            };
            Cat cat2 = new Cat
            {
                Id = 2,
                Name = "bbb",
                Color = "blue"
            };
            Cat cat3 = new Cat
            {
                Id = 3,
                Name = "ccc",
                Color = "yellow"
            };
            List<Cat> cats = new List<Cat>();
            cats.Add(cat1);
            cats.Add(cat2);
            cats.Add(cat3);
            var catGroup = cats.GroupBy(x => x.Color).Select(x => new {Name = x.Key, Total = x.Count()});
            foreach(var c in catGroup)
            {
                Console.WriteLine(c.Name + " " + c.Total.ToString());
            }
            Console.Read();
        }
    }
}
