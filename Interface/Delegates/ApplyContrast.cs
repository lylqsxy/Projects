using System;

namespace Delegates
{
    public class ApplyContrast : IPhotoFilter
    {
        public void PhotoFilterHandler(Photo photo)
        {
            Console.WriteLine("Apply contrast");
        }
    }
}
