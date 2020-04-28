using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            InitializeSettings();

            base.OnAppearing();
        }

        private void InitializeSettings()
        {
            displayNameEntry.Text = "George";
            bioEditor.Text = "4th Year, Software Development, Institute of technology Carlow";
            articleCountSlider.Value = 10;
            categoryPicker.SelectedIndex = 1;
        }
    }
}