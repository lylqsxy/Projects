using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new PhotoProcessor();
            var ab = new ApplyBrightness();
            var ac = new ApplyContrast();
            processor.Process("photo.jpg", ab);
            processor.Process("photo.jpg", ac);
            Console.ReadKey();
        }

        static void MyCustomFilter(Photo photo)
        {
            Console.WriteLine("Apply my custom filter");
        }
    }
}
