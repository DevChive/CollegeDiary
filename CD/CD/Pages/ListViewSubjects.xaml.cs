using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CD.Helper;
using CD.Models;
using CD.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListViewSubjects : ContentPage
    {

        readonly FireBaseHelper fireBaseHelper = new FireBaseHelper();

        public ListViewSubjects()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchAllSubjects();

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async Task FetchAllSubjects()
        {
            var allSubjects = await fireBaseHelper.GetAllSubjects();
            LstSubjects.ItemsSource = allSubjects;
        }

        private Subject SelectedSubject => (Subject)LstSubjects.SelectedItem;
    }
}
