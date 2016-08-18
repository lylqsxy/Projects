using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] { 1, 3, 4, 5, 6, 7, 8, 13, 15 };
            int total = 0;
            int temp1 = 0;
            int max = 0;
            int min = 0;
            int temp2 = test.Max();
            foreach(var i in test)
            {
                Console.WriteLine("{0}", i);
                if(i >= temp1)
                {
                    max = i;
                }
                if (i <= temp2)
                {
                    min = i;
                }
                temp1 = i;
                temp2 = i;
                total += i;
            }
            Console.WriteLine("Total = {0}", total);
            Console.WriteLine("Max = {0}", max);
            Console.WriteLine("Min = {0}", min);
            int t = 0;
            for(int j = 0; j < test.Length - 1; j++)
            {
                for(int k = 0; k < test.Length - 1; k++)
                {
                    if(test[k] < test[k+1])
                    {
                        t = test[k];
                        test[k] = test[k + 1];
                        test[k + 1] = t;
                    }
                }
            }
            foreach(var i in test)
            {
                Console.WriteLine("{0}", i);
            }
            Console.Read();
        }
    }
}
