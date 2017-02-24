using System;

namespace ConsoleApplication7
{
    public class GoodTesting : AbstractTesting
    {
        public override void WriteHello()
        {
            Console.WriteLine("From Good Hello");
        }

        public override void WriteMorning()
        {
            Console.WriteLine("From Good Morning");
        }
    }
}
