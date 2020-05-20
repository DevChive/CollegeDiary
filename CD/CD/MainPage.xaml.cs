using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;

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
            await Navigation.PushAsync(new Views.Settings());
        }
        public void toFirstTab()
        {
            this.CurrentPage = this.Children[0];
        }
    }
}