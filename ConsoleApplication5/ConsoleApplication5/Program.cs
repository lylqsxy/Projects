using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("email", "nickyqi@gmail.com");
            test.Add("roleId", "1");
            string queryString = "";
            foreach(var key in test.Keys)
            {
                Console.WriteLine(key);
                Console.WriteLine(test[key]);
                queryString += string.Format("|{0}={1}", key, test[key]);
            }
            Console.WriteLine(queryString);
            string hashed = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(queryString));
            Console.WriteLine(hashed);
            string dehased = System.Text.Encoding.Default.GetString(Convert.FromBase64String(hashed));
            Console.WriteLine(dehased);
            string[] a = dehased.Split('|');
            Dictionary<string, string> ggg = new Dictionary<string, string>();
            foreach(var s in a)
            {
                if(s != "")
                {
                    Console.WriteLine(s);
                    string[] b = s.Split('=');
                    Console.WriteLine(b[0]);
                    Console.WriteLine(b[1]);
                    ggg.Add(b[0], b[1]);

                }

            }
            Console.WriteLine(ggg);
            Console.Read();
        }
    }
}
