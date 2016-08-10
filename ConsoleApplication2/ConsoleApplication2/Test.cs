using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication2
{
    class Test
    {
        // Summary:
        //
        // Parameters:
        //     A “source" English word in a string, and an English dictionary 
        //     supplied in an array
        //
        // Returns:
        //     Are those from the dictionary that have four consecutive letters 
        //     (or more) in common with the “source” word.  
        //     For example, the word MATTER has the four letters in a row “ATTE" 
        //     in common ATTEND.
        //
        public static string[] fourLettersInCommon(string inputWord, string[] dic)
        {
            const int length = 4;  //4 consecutive char          
            List<string> candidatePattern = new List<string>();
            List<string> output = new List<string>();

            if (inputWord.Length >= length)
            {
                for (int i = 0; i <= inputWord.Length - length; i++)
                {
                    //Prepare the regular expression for each candidate
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
            for (int i = 0; i < dic.Length; i++)
            {
                foreach (string pattern in candidatePattern)
                {
                    //If is match, jump out of the loop
                    if (Regex.IsMatch(dic[i].Trim(), pattern))
                    {
                        output.Add(dic[i]);
                        break;
                    }
                }
            }
            return output.ToArray();
        }
    }
}
