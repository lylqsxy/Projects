using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SjtuNZApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPageMaster : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public WebPageMaster()
        {
            InitializeComponent();
            BindingContext = new WebPageMasterViewModel();
        }

        class WebPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<WebPageMenuItem> MenuItems { get; }
            public WebPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<WebPageMenuItem>(new[]
                {
                    new WebPageMenuItem { Id = 0, Title = "校友论坛", TargetType = typeof(WebViewPage)},
                    new WebPageMenuItem { Id = 1, Title = "设置", TargetType = typeof(WebPageDetail) },
                });
            }
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
