using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                string a = "dsfdsfs";
                string b = new string(a.ToCharArray().Reverse().ToArray());
                Console.WriteLine(b);
                Console.WriteLine("Please input door number: ");
                var doorNumber = Convert.ToInt32(Console.ReadLine());
                var result = DoorQuiz.isOpen(DoorQuiz.CalculateDoorActiveNumbers(), doorNumber) ? "Open" : "Closed";
                Console.WriteLine("Number {0} door is {1}.", doorNumber, result);
                

            }   
        }
    }


    public class DoorQuiz
    {
        public static bool isOpen(List<int> doorActiveInts, int doorNumber)
        {
            var activeNumber = doorActiveInts[doorNumber - 1];
            if ((activeNumber % 2) != 0 || activeNumber == 1)
                return true;
            return false;
        }

        public static List<int> CalculateDoorActiveNumbers()
        {
            int[,] doorMatrix = new int[10, 10];

            for (int iterTime = 0; iterTime < 10; iterTime++)
            {
                for (int doorNumber = iterTime; doorNumber < 10; doorNumber += iterTime + 1)
                {
                    doorMatrix[doorNumber, iterTime] = 1;
                }
            }

            var doorActiveNumber = new List<int>();

            for (int i = 0; i < doorMatrix.GetLength(0); i++)
            {
                var sum = 0;
                for (int j = 0; j < doorMatrix.GetLength(1); j++)
                {
                    sum += doorMatrix[i, j];
                }
                doorActiveNumber.Add(sum);
            }
            return doorActiveNumber;
        }
    }
}


