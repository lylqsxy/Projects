using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            Enumerable.Range(1, 100).ToList().ForEach(x => Console.WriteLine(x % 15 == 0 ? "fizzbuzz" : x % 5 == 0 ? "buzz" : x % 3 == 0 ? "fizz" : x.ToString()));
            Console.Read();
        }
    }
}
