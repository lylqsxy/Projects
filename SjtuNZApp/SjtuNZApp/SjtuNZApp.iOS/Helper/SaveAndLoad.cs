using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SjtuNZApp.Interface;
using System.IO;
using Xamarin.Forms;
using SjtuNZApp.iOS.Helper;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SjtuNZApp.iOS.Helper
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (!File.Exists(filePath))
            {
                return null;
            }
            return System.IO.File.ReadAllText(filePath);
        }
    }
}