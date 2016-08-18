
using System;

namespace LambdaExpressions
{
    class Program
    {
        static void Main(string[] args)
        {


            var books = new BookRepository().GetBooks();
            var cheapBooks = books.FindAll(x => x.Price < 10 && x.Title == "Title 1");
            foreach (var b in cheapBooks)
            {
                Console.WriteLine(b.Title + " Price: $" + b.Price);
            }
            Console.ReadKey();
        }
    }
}
