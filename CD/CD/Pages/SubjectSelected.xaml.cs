using System;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using Rg.Plugins.Popup.Services;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SubjectSelected : ContentPage
    {
        public Subject _subject;
        readonly FireBaseHelperSubject fireBaseHelper = new FireBaseHelperSubject();

        public SubjectSelected(Subject subject)
        {
            _subject = subject;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = _subject;
            statusCA.ProgressColor = Color.Green; // depending on student's progree
            await statusCA.ProgressTo(0.2, 500, Easing.Linear);
            await statusFinalExam.ProgressTo(0.7, 500, Easing.Linear);

            // add delete button and update button
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            await fireBaseHelper.DeleteSubject(_subject.SubjectID);
            await DisplayAlert("Success", "Subject Deleted", "OK"); // add a toast message
            await Navigation.PushAsync(new MainPage());
        }

        [Obsolete]
        private void add_new_mark(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddMarkToSubject());
        }
    }
}