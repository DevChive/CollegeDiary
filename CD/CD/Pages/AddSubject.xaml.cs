using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using CD.Models;
using System.Text.RegularExpressions;

namespace CD.Pages
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
            bool validate = true;
            string pattern = null;
            bool validateSubjectName = true;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            // Checking if all the fields are filled
            if (string.IsNullOrEmpty(this.subjectName.Text) || string.IsNullOrEmpty(this.lecturerName.Text) || string.IsNullOrEmpty(this.CA.Text) ||
                string.IsNullOrEmpty(this.finalExam.Text) || string.IsNullOrEmpty(this.lecturerEmail.Text))
            {
                await DisplayAlert("Subject not added", "All fields are required and the weight of the module needs to add up to 100", "OK");
                validate = false;
            }
            else
            {
                // cheking if  the email is valid
                if (Regex.IsMatch(this.lecturerEmail.Text, pattern) && !string.IsNullOrEmpty(this.lecturerEmail.Text) && validate)
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
                if (CA + FinalExam == 100)
                {
                    var subject = await fireBaseHelper.GetSubject(subjectName.Text);
              
                    await fireBaseHelper.AddSubject(subjectName.Text, lecturerName.Text, lecturerEmail.Text, CA, FinalExam);
                    await DisplayAlert("Subject Added", $"{this.subjectName.Text}\n{this.lecturerName.Text}", "OK");
                    //TODO: clear all entires!!!
                    //MainPage.Instance.toFirstTab();

                }
                else
                {
                    await DisplayAlert("Subject not added", "The Final Exam and CA need to add up to 100", "OK");
                }
            }           
        }

        private async void Cancel_Subject(object sender, EventArgs e)
        {
            //MainPage.Instance.toFirstTab();
        }
    }
}