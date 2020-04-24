using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Pages;

namespace CD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public static MainPage Instance;
        public MainPage()
        {
            Instance = this;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.Settings());
        }
        public void toFirstTab()
        {
            this.CurrentPage = this.Children[0];
        }
    }
}