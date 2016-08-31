using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            Type T = typeof(BadTesting);      
            AbstractTesting t2 = ReflectionHelper.CreateInstance<AbstractTesting>(T);
            t2.WriteHello();
            Console.Read();
        }
    }
}
