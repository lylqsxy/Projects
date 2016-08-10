using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> dictionaryString = new List<string>();
            System.Console.WriteLine("Please Input the Word: ");
            string input = System.Console.ReadLine();
            System.Console.WriteLine("=================");
            StreamReader streamReader = new StreamReader(@"C:\dic.txt");
            while (!streamReader.EndOfStream)
            {
                string outputWord = streamReader.ReadLine();
                dictionaryString.Add(outputWord);
            }
            string[] dictionary = dictionaryString.ToArray();
            string[] ouput = Test.fourLettersInCommon(input, dictionary);
            for (int i = 0; i < ouput.Length; i++)
            {
                System.Console.WriteLine(ouput[i]);
            }
            System.Console.WriteLine("=================");
            System.Console.WriteLine(ouput.Length);
            System.Console.ReadLine();
        }
    }
}
