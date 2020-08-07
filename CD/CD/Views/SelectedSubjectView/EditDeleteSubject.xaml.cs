using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;
using CD.Models;
using CD.Helper;
using System.Text.RegularExpressions;

namespace CD.Views.SelectedSubjectView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDeleteSubject 
    {
        Subject subjectSelected;
        readonly FireBaseHelperSubject fireBaseHelperSubject = new FireBaseHelperSubject();
        readonly FireBaseHelperMark fireBaseHelperMark = new FireBaseHelperMark();
        public EditDeleteSubject(Subject subject)
        {
            InitializeComponent();
            subjectSelected = subject;
            subjectName.Text = subject.SubjectName;
            lecturerName.Text = subject.LecturerName;
            lecturerEmail.Text = subject.LecturerEmail;
            CA.Text = subject.CA.ToString() + "%";
            finalExam.Text = subject.FinalExam.ToString() + "%";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = System.Drawing.Color.FromArgb(200, 0, 0, 0);
        }
        private async void Save_Subject(object sender, EventArgs e)
        {
            save_subject_button.IsEnabled = false;
            bool validate = true;
            bool validateSubjectName = true;

            ErrorName.IsVisible = false;
            ErrorNameExists.IsVisible = false;

            //checking if the subject name is not empty
            if (string.IsNullOrEmpty(subjectName.Text.ToString()) || string.IsNullOrWhiteSpace(subjectName.Text.ToString()))
            {
                validate = false;
                ErrorName.IsVisible = true;
            }
            if (validate && !string.IsNullOrEmpty(lecturerEmail.Text))
            {
                string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                string lecEmail = lecturerEmail.Text.Trim();
                if (!Regex.IsMatch(lecEmail, pattern) && validate)
                {
                    EmailError.IsVisible = true;
                    validate = false;
                }
            }

            //checking if the subject alreasy exists in the database
            var allSubjects = await fireBaseHelperSubject.GetAllSubjects();
            foreach (Subject listS in allSubjects)
            {
                if (string.Equals(listS.SubjectName, this.subjectName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    if (listS.SubjectID == subjectSelected.SubjectID)
                    {
                        continue;
                    }
                    else
                    {
                        validateSubjectName = false;
                    }
                }
            }
            if (!validateSubjectName)
            {
                ErrorNameExists.IsVisible = true;
                validate = false;
            }


            if (validate)
            {
                try
                {
                    var subjectToEdit = await fireBaseHelperSubject.GetSubject(subjectSelected.SubjectID);
                    await fireBaseHelperSubject.UpdateSubject(subjectToEdit.SubjectID, subjectName.Text, lecturerName.Text, lecturerEmail.Text);
                    await Navigation.PushAsync(new SubjectSelected(subjectSelected), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    await PopupNavigation.RemovePageAsync(this);
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Please try again", "Ok");
                }

            }
            save_subject_button.IsEnabled = true; ;
        }

        [Obsolete]
        private async void Cancel_Update(object sender, EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}