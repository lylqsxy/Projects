using System;

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
