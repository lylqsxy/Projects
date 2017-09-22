using SjtuNZApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SjtuNZApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPageDetail : ContentPage
    {
        public WebPageDetail()
        {
            InitializeComponent();
            url_Txt.Text = DependencyService.Get<ISaveAndLoad>().LoadText("url.txt");
            save_Btn.Clicked += (sender, e) => {
                DependencyService.Get<ISaveAndLoad>().SaveText("url.txt", url_Txt.Text);
            };
            reset_Btn.Clicked += (sender, e) => {
                url_Txt.Text = "http://sjtu.7nzgg.com/forum.php?mobile=yes";
            };
        }
    }
}
