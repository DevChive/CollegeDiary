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
                bool validateSubjectName = true;

                NameEntryError.IsVisible = false;
                CAError.IsVisible = false;
                FEError.IsVisible = false;
                CA_FE_Error.IsVisible = false;
                CA_FE_Decimal.IsVisible = false;

                // Checking if all the fields are filled
                if (string.IsNullOrEmpty(this.subjectName.Text) || string.IsNullOrWhiteSpace(this.subjectName.Text))
                {
                    NameEntryError.IsVisible = true;
                    validate = false;

                }
                if (validate)
                {
                    if (string.IsNullOrEmpty(this.CA.Text) || string.IsNullOrWhiteSpace(this.CA.Text) && validate)
                    {
                        validate = false;
                        CAError.IsVisible = true;
                    }
                }
                if (validate)
                {
                    if (string.IsNullOrEmpty(this.finalExam.Text) || string.IsNullOrWhiteSpace(this.finalExam.Text) && validate)
                    {
                        FEError.IsVisible = true;
                        validate = false;
                    }
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
                        MainPage.Instance.toListSubjects();                     
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        CA_FE_Error.IsVisible = true; ;
                    }
                }
            }
            catch(Exception)
                {
                    CA_FE_Decimal.IsVisible = true;
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