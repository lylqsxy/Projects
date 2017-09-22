using System;

namespace Delegates
{
    public class ApplyBrightness : IPhotoFilter
    {
        public void PhotoFilterHandler(Photo photo)
        {
            Console.WriteLine("Apply brightness");
        }
    }
}
