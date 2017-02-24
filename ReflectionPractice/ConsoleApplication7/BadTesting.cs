using System;

namespace ConsoleApplication7
{
    public class BadTesting : AbstractTesting
    {
        public override void WriteHello()
        {
            Console.WriteLine("From Bad Hello");
        }

        public override void WriteMorning()
        {
            Console.WriteLine("From Bad Morning");
        }
    }
}
