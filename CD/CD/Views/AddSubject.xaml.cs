using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using CD.Models;
using CD.Views;
using System.Text.RegularExpressions;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubject : ContentPage
    {
        readonly FireBaseHelperSubject fireBaseHelper = new FireBaseHelperSubject();

        public AddSubject() // So here is where the code will go for the validation?
        {
            InitializeComponent();// Loading this page
        }

        private async void Save_Subject(object sender, EventArgs e)
        {
            save_subject_button.IsEnabled = false;
            try
            {
                bool validate = true;
                string pattern = null;
                bool validateSubjectName = true;
                pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                // Checking if all the fields are filled
                if (string.IsNullOrEmpty(this.subjectName.Text) || string.IsNullOrEmpty(this.lecturerName.Text) || string.IsNullOrEmpty(this.CA.Text) ||
                    string.IsNullOrEmpty(this.finalExam.Text) || string.IsNullOrEmpty(this.lecturerEmail.Text) ||
                    string.IsNullOrWhiteSpace(this.subjectName.Text) || string.IsNullOrWhiteSpace(this.lecturerName.Text) || string.IsNullOrWhiteSpace(this.CA.Text) ||
                    string.IsNullOrWhiteSpace(this.finalExam.Text) || string.IsNullOrWhiteSpace(this.lecturerEmail.Text))
                {
                    await DisplayAlert("Subject not added", "All fields are required", "OK");
                    validate = false;
                }
                else
                {
                    // cheking if  the email is valid
                    if (Regex.IsMatch(this.lecturerEmail.Text, pattern) && !string.IsNullOrEmpty(this.lecturerEmail.Text) && !string.IsNullOrWhiteSpace(this.lecturerEmail.Text) && validate)
                    {
                        validate = true;
                    }
                    else
                    {
                        await DisplayAlert("Subject not added", "Invalid email address entered", "OK");
                        validate = false;
                    }
                }

                // cheking if the subject already exists in the database
                var allSubjects = await fireBaseHelper.GetAllSubjects();
                foreach (Subject listS in allSubjects)
                {
                    if (string.Equals(listS.SubjectName, this.subjectName.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        validateSubjectName = false;
                    }
                }
                if (!validateSubjectName)
                {
                    await DisplayAlert("Subject not added ", "Subject already exists", "OK");
                    validate = false;
                }

                if (validate)
                { 
                    int CA = Int32.Parse(this.CA.Text);
                    int FinalExam = Int32.Parse(this.finalExam.Text);

                    // checking id the weights of the exams add to 100
                    if (CA + FinalExam == 100 && CA >= 0 && FinalExam >= 0)
                    {
                        var subject = await fireBaseHelper.GetSubject(subjectName.Text);

                        await fireBaseHelper.AddSubject(subjectName.Text, lecturerName.Text, lecturerEmail.Text, CA, FinalExam);
                        await DisplayAlert("Subject Added", $"{this.subjectName.Text}\n{this.lecturerName.Text}", "OK");
                        await Navigation.PushAsync(new ListViewSubjects());
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Subject not added", "The Final Exam and Continuous Assessment need to add up to 100, and the numbers must be positive", "OK");
                    }
                }
            }
            catch(Exception)
                {
                    await DisplayAlert("Subject not added", "Use whole numbers for Continuous Assessment and Final Exam", "Ok");
                }
            save_subject_button.IsEnabled = true;
        }

        private async void Cancel_Subject(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            //MainPage.Instance.toFirstTab();
        }

        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void info(object sender, EventArgs e)
        {
            if (hiden.IsVisible)
            {
                hiden.IsVisible = false;
            }
            else
            {
                hiden.IsVisible = true;
            }
        }
    }
}