
using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new PhotoProcessor();
            var filters = new PhotoFilters();
            PhotoProcessor.PhotoFilterHandler photoFilter = filters.ApplyBrightness;
            photoFilter += filters.RemoveRedEye;
            photoFilter += MyCustomFilter;
            //PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyContrast;
            //filterHandler += filters.ApplyBrightness;
            processor.Process("photo.jpg", photoFilter);
            Console.ReadKey();
        }

        static void MyCustomFilter(Photo photo)
        {
            Console.WriteLine("Apply my custom filter");
        }
    }
}
