using SjtuNZApp.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SjtuNZApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            InitializeComponent();
            var url = DependencyService.Get<ISaveAndLoad>().LoadText("url.txt");
            if (url == null)
            {
                url = "http://sjtu.7nzgg.com/forum.php?mobile=yes";
                DependencyService.Get<ISaveAndLoad>().SaveText("url.txt", url);
            }

            WebView.Children.Add(new WebView
            {
                Source = url
            });
        }
    }

}
