using System;
using CD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using CD.ViewModel;
using CD.Views;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;

namespace CD.Views
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
            status_bars();
        }

        private async void status_bars()
        {
            decimal CAProgress = await fireBaseHelperSubject.getTotalCA(_subject.SubjectID);
            decimal FinalExamProgress = await fireBaseHelperSubject.Final_Exam_Progress(_subject.SubjectID);

            double CA = Decimal.ToDouble(CAProgress);
            double FE = Decimal.ToDouble(FinalExamProgress);
            decimal pass = 0.4m;
            decimal distinction = 0.7m;
            //Console.WriteLine(CAProgress < pass);

            if (CAProgress < pass) { statusCA.ProgressColor = Color.Red; }
            else if (CAProgress >= pass && CAProgress < distinction) { statusCA.ProgressColor = Color.Orange; }
            else if (CAProgress >= distinction){ statusCA.ProgressColor = Color.LightGreen; }

            if (FinalExamProgress < pass) { statusFinalExam.ProgressColor = Color.Red; }
            else if (FinalExamProgress >= pass && FinalExamProgress < distinction) { statusFinalExam.ProgressColor = Color.Orange; }
            else if(FinalExamProgress >= distinction) { statusFinalExam.ProgressColor = Color.LightGreen; }

            Ca_StatusBar.Text = (CA*100).ToString("F2") + "%";
            await statusCA.ProgressTo(CA, 100, Easing.Linear);

            Fe_StatusBar.Text = (FE*100).ToString("F2") + "%";
            await statusFinalExam.ProgressTo(FE, 100, Easing.Linear);

        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            await fireBaseHelperSubject.DeleteSubject(_subject.SubjectID);
            await fireBaseHelperMark.DeleteMarks(_subject.SubjectID);
            await DisplayAlert("Success", "Subject Deleted", "OK"); //TODO: add a toast message

            await Navigation.PopAsync();
        }

        [Obsolete]
        private void add_new_mark(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddMarkToSubject(_subject));
        }
        private void add_final_exam(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddFinalExamToSubject(_subject));
        }
        private void LstMarks_Selected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        private void markMenu(object sender, EventArgs e)
        {
            
        }
    }
}