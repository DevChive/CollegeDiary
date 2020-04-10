using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Helper;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using System.Linq;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubject : ContentPage
    {

        readonly FireBaseHelper fireBaseHelper = new FireBaseHelper();

        public AddSubject() // So here is where the code will go for the validation?
        {
            InitializeComponent();// Loading this page
        }
        private async void Save_Subject(object sender, EventArgs e)
        {
            bool validate = true;


            if (string.IsNullOrEmpty(this.subjectName.Text) || string.IsNullOrEmpty(this.lecturerEmail.Text) || 
                string.IsNullOrEmpty(this.lecturerName.Text) || string.IsNullOrEmpty(this.CA.Text) || 
                string.IsNullOrEmpty(this.finalExam.Text))
                validate = false;

            if (validate)
            {
                int CA = Int32.Parse(this.CA.Text);
                int FinalExam = Int32.Parse(this.finalExam.Text);
                if (CA + FinalExam == 100)
                {   
                    var subject = await fireBaseHelper.GetSubject(subjectName.Text);

                    await fireBaseHelper.AddSubject(subjectName.Text, lecturerName.Text, lecturerEmail.Text, CA, FinalExam);
                    await DisplayAlert("Subject Added", $"{this.subjectName.Text}\n{this.lecturerName.Text}", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
            }
            else
                await DisplayAlert("Subject not added!","All fields are required and the weight of the module needs to add up to 100", "OK");
        }

        private async void Cancel_Subject(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}