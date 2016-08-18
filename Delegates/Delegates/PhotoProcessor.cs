using System;

namespace Delegates
{
    public class PhotoProcessor
    {
        //public void Process(string path, Action<Photo> filterHandler)
        //{
        //    var photo = Photo.Load(path);

        //    filterHandler(photo);

        //    photo.Save();
        //}

        public delegate void PhotoFilterHandler(Photo photo);
        public void Process(string path, PhotoFilterHandler photoFilterHandler)
        {
            var photo = Photo.Load(path);
            Console.WriteLine("Processing photo {0}......", path);
            photoFilterHandler(photo);
            photo.Save();
        }
    }
}