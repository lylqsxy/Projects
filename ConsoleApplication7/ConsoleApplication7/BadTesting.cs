using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
