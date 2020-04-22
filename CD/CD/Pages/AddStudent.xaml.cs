using CD.Helper;
using CD.Models;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddStudent : ContentPage
	{
		readonly FireBaseHelperStudent fireBaseHelper = new FireBaseHelperStudent();
		public AddStudent()
		{
			InitializeComponent();
		}

		private async void Save_Student(object sender, EventArgs e)
		{
			bool validate = true;
			string pattern = null;
			bool validateStudentName = true;
			pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            // Checking if all the fields are filled
            if (string.IsNullOrEmpty(this.studentName.Text) || string.IsNullOrEmpty(this.studentAddress.Text) || string.IsNullOrEmpty(this.studentPhone.Text) ||
                string.IsNullOrEmpty(this.studentEmail.Text))
            {
                await DisplayAlert("Student not added", "All fields are required ", "OK");
                validate = false;
            }
            else
            {
                // cheking if  the email is valid
                if (Regex.IsMatch(this.studentEmail.Text, pattern) && !string.IsNullOrEmpty(this.studentEmail.Text) && validate)
                {
                    validate = true;
                }
                else
                {
                    await DisplayAlert("Student not added", "Invalid email address entered", "OK");
                    validate = false;
                }
            }

            // cheking if the subject already exists in the database
            var allStudents = await fireBaseHelper.GetAllStudents();
            foreach (Student listS in allStudents)
            {
                if (string.Equals(listS.StudentName, this.studentName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    validateStudentName = false;
                }
            }
            if (!validateStudentName)
            {
                await DisplayAlert("Student not added ", "Student already exists", "OK");
                validate = false;
            }

            if (validate)
            {
                var student = await fireBaseHelper.GetStudent(studentName.Text);
                int studentPhone = Int32.Parse(this.studentPhone.Text);

                await fireBaseHelper.AddStudent(studentName.Text, studentAddress.Text, studentEmail.Text, studentPhone);
                await DisplayAlert("Student Added", $"{this.studentName.Text}", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Student not added", "There was an issue", "OK");
            }
            
        }

        private async void Cancel_Student(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}