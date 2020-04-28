using System;
using System.Threading.Tasks;
using CD.Helper;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListViewSubjects : ContentPage
    {

        readonly FireBaseHelperSubject fireBaseHelper = new FireBaseHelperSubject();

        public ListViewSubjects()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchAllSubjects();
        }

        private async Task FetchAllSubjects()
        {
            var allSubjects = await fireBaseHelper.GetAllSubjects();
            LstSubjects.ItemsSource = allSubjects;
        }

        private Subject SelectedSubject => (Subject)LstSubjects.SelectedItem;

        private async void LstSubjects_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Subject subject = await fireBaseHelper.GetSubject(SelectedSubject.SubjectID);

            await Navigation.PushAsync(new SubjectSelected(subject));
        }
    }
}
