using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "asdfg";
            string b = new string(a.ToCharArray().Reverse().ToArray());

            Console.WriteLine(b);

            Console.ReadLine();
        }
    }
}
