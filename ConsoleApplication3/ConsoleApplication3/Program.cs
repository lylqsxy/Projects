using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            const int doorNumber = 100;
            List<Door> doors = new List<Door>();
            for (int i = 0; i < doorNumber; i++)
            {
                doors.Add(new Door(false));
            }
            for (int i = 1; i <= doorNumber; i++)
            {
                for(int j = i - 1; j < doorNumber; j += i)
                {
                    doors[j].Toggle();
                }
            }
            while(true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Please input door number: ");
                int doorIndex = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Number {0} is {1}.", doorIndex, doors[doorIndex - 1].Status == true ? "Open" : "Close");
            }         
        }

    }

    class Door
    {
        public bool Status { get; set; }

        public Door(bool initStatus)
        {
            Status = initStatus;
        }

        public void Toggle()
        {
            Status = !Status;
        } 
    }
}
