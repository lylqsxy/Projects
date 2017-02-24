using System;

namespace Delegates
{
    public class PhotoProcessor
    {
        public void Process(string path, IPhotoFilter photoFilterHandler)
        {
            var photo = Photo.Load(path);
            Console.WriteLine("Processing photo {0}......", path);
            photoFilterHandler.PhotoFilterHandler(photo);
            photo.Save();
        }
    }
}