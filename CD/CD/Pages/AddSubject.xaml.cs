using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Models;
using CD.Helper;
using CD.Pages;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubject : ContentPage
    {

        readonly FireBaseHelper fireBaseHelper = new FireBaseHelper();

        public AddSubject()
        {
            InitializeComponent();
        }

        private async void Save_Subject(object sender, EventArgs e)
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
            else
                await DisplayAlert("Subject not added!", $"Continuous Assessment = {this.CA.Text}%\n" +
                    $"Final Exam = {this.finalExam.Text}%\nThey should add up to 100", "OK");
        }
    }
}