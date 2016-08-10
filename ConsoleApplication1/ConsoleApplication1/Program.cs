using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int length = 4;  //4 consecutive char
            List<string> candidatePattern = new List<string>();
            List<string> output = new List<string>();
            System.Console.WriteLine("Please Input the Word: ");
            string inputWord = System.Console.ReadLine();
            System.Console.WriteLine("====================");
            if (inputWord.Length >= length)
            {
                for (int i = 0; i <= inputWord.Length - length; i++)
                {
                    //Prep for the regular expression for each candidate
                    char[] c = inputWord.ToCharArray(i, length);
                    string pattern = @"[a-zA-Z]*";
                    for (int j = 0; j < length; j++)
                    {
                        pattern += "[" + c[j] + "]";
                    }
                    pattern += @"[a-zA-Z]*";  
                    candidatePattern.Add(pattern);
                }
            }
            //Read the Dictionary
            StreamReader streamReader = new StreamReader(@"C:\Users\Administrator\Desktop\dic.txt");
            while (!streamReader.EndOfStream)
            {
                string outputWord = streamReader.ReadLine();
                foreach (string pattern in candidatePattern)
                {
                    //If is match, jump out of the loop
                    if (Regex.IsMatch(outputWord.Trim(), pattern))
                    {
                        System.Console.WriteLine(outputWord);
                        output.Add(outputWord);
                        break;
                    }
                }
            }
            System.Console.WriteLine("====================");
            System.Console.WriteLine(output.Count);
            System.Console.Read();
        }
    }
}
