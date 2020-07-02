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
            details();
        }
        private async void details()
        {
            var subject = await fireBaseHelperSubject.GetSubject(_subject.SubjectID);
            subjectName.Text = subject.SubjectName;
            lecturerName.Text = subject.LecturerName;
            lecturerEmail.Text = subject.LecturerEmail;
            double remCA = await fireBaseHelperSubject.remainigCA(_subject.SubjectID);
            remainingCA.Text = remCA.ToString("F2") + "%";
            double remFE = await fireBaseHelperSubject.remainigFE(_subject.SubjectID);
            remainingFE.Text = remFE.ToString("F2") + "%";
            Title = subject.SubjectName;
            
            // refrashing the selected subject
            _subject = subject;
        }
        private async void status_bars()
        {
            double CAProgress = await fireBaseHelperSubject.getTotalCA(_subject.SubjectID);
            double FinalExamProgress = await fireBaseHelperSubject.Final_Exam_Progress(_subject.SubjectID);

            double CA = CAProgress;
            double FE = FinalExamProgress;
            double pass = 0.4;
            double distinction = 0.7;
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
        [Obsolete]
        private async void add_new_mark(object sender, EventArgs e)
        {
            add_ca_button.IsEnabled = false;
            await PopupNavigation.PushAsync(new AddMarkToSubject(_subject));
            add_ca_button.IsEnabled = true;
        }

        [Obsolete]
        private async void add_final_exam(object sender, EventArgs e)
        {
            add_fe_button.IsEnabled = false;
            await PopupNavigation.PushAsync(new AddFinalExamToSubject(_subject));
            add_fe_button.IsEnabled = true;
        }

        [Obsolete]
        private async void edit_subject(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new EditDeleteSubject(_subject));
        }

        private async void delete_subject(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Are you sure you want to delete", _subject.SubjectName, "Yes", "No");
            if (result) // YES
            {
                try
                {
                    await fireBaseHelperSubject.DeleteSubject(_subject.SubjectID);
                    await fireBaseHelperMark.DeleteMarks(_subject.SubjectID);
                    await DisplayAlert("Success", "Subject Deleted", "OK"); //TODO: add a toast message
                    await Navigation.PopAsync();
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Please try again", "Ok");
                }
            }
        }

        private async void delete_mark(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            var thisMark = e.ItemData as Mark;
            var result = await DisplayAlert("Are you sure you want to delete this mark?", "Name "  + thisMark.MarkName + "\nResult " + thisMark.Result, "Yes", "No");
            if (result)
            {
                try
                {
                    await fireBaseHelperMark.DeleteMark(thisMark.MarkID);
                    await Navigation.PushAsync(new SubjectSelected(_subject), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);

                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Please try again", "Ok");
                }
            }
        }

        private void tips(object sender, EventArgs e)
        {
            if (hidenSubjectDetailsHelp.IsVisible)
            {
                hidenSubjectDetailsHelp.IsVisible = false;
            }
            else
            {
                hidenSubjectDetailsHelp.IsVisible = true;
                if (hidenYourResultsHelp.IsVisible)
                {
                    hidenYourResultsHelp.IsVisible = false;
                }
            }
        }

        private void tipsYourResults(object sender, EventArgs e)
        {
            if (hidenYourResultsHelp.IsVisible)
            {
                hidenYourResultsHelp.IsVisible = false;
            }
            else
            {
                hidenYourResultsHelp.IsVisible = true;
                if (hidenSubjectDetailsHelp.IsVisible)
                {
                    hidenSubjectDetailsHelp.IsVisible = false;
                }
            }
        }
    }
}