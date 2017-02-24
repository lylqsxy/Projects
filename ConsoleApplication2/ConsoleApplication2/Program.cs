using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Base : IDisposable
    {
        public Base()
        {
            Console.WriteLine(" Base Constructor");
        }
        static Base()
        {
            Console.WriteLine(" Base Static Constructor");
        }
        public void Dispose()
        {
            Console.WriteLine(" Base Dispose");
        }
        ~Base()
        {
            Console.WriteLine(" Base Destructor");
        }
    }
    class Derived : Base, IDisposable
    {
        public Derived()
        {
            Console.WriteLine(" Derived Constructor");
        }
        static Derived()
        {
            Console.WriteLine(" Derived Static Constructor");
        }
        ~Derived()
        {
            Console.WriteLine(" Derived Destructor");
        }
        void IDisposable.Dispose()
        {
            Console.WriteLine(" Derived Dispose");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            using (Base test = new Derived())
            {
                Console.WriteLine(" Using test");
            }
            Console.WriteLine("End");
        }
    }
}
