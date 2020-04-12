using System;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using CD.ViewModel;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SubjectSelected : ContentPage
    {
        private Subject _subject;
        private SubjectMark subjectMark;
        readonly FireBaseHelperSubject fireBaseHelperSubject = new FireBaseHelperSubject();
        readonly FireBaseHelperMark fireBaseHelperMark = new FireBaseHelperMark();

        public SubjectSelected(Subject subject)
        {
            _subject = subject;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Mark> listMarks = await fireBaseHelperMark.GetMarksForSubject(_subject.SubjectID);
            subjectMark = new SubjectMark(_subject, listMarks);

            this.BindingContext = subjectMark; //!!!

            statusCA.ProgressColor = Color.Green; // depending on student's progree
            await statusCA.ProgressTo(0.2, 500, Easing.Linear);
            await statusFinalExam.ProgressTo(0.7, 500, Easing.Linear);

            // update button
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            await fireBaseHelperSubject.DeleteSubject(_subject.SubjectID);
            await fireBaseHelperMark.DeleteMarks(_subject.SubjectID);
            await DisplayAlert("Success", "Subject Deleted", "OK"); // add a toast message
            await Navigation.PushAsync(new MainPage());
        }

        [Obsolete]
        private void add_new_mark(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddMarkToSubject(_subject));
        }

        private void LstMarks_Selected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}