using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            LinkedListNode<int> thirdNode = linkedList.Last;
            linkedList.AddLast(4);
            DeleteNode(thirdNode);
            foreach (var value in linkedList)
            {
                Console.WriteLine(value);
            }
            Console.Read();
        }

        private static void DeleteNode(LinkedListNode<int> nodeToDelete)
        {
            nodeToDelete.List.Remove(nodeToDelete); 
        }
    }
}
