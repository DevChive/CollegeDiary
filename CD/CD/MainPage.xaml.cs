using System;
using System.ComponentModel;
using Xamarin.Forms;
using Rg.Plugins.Popup;

namespace CD
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
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
