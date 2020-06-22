using System;
using CD.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Models;
using CD.Helper;


namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccount : ContentPage
    {
        string userID = "";
        readonly FireBaseHelperStudent fireBaseHelperStudent = new FireBaseHelperStudent();
        public MyAccount()
        {
            InitializeComponent();
            userID = App.UserUID;
        }
        protected override async void OnAppearing()
        {
            Student user = await fireBaseHelperStudent.GetStudent(userID);
            this.BindingContext = user;
            Console.WriteLine("--------------------------------" + user.Institute.ToString());
        }
        private void load_subject_list(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewSubjects());
        }

        private void load_add_subject(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddSubject());
        }
        private async void load_login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogIn());
        }

        private void edit_subject(object sender, EventArgs e)
        {

        }

        private void delete_subject(object sender, EventArgs e)
        {

        }

        private void tips(object sender, EventArgs e)
        {

        }

        private void uploadPicture(object sender, EventArgs e)
        {

        }

        private void CA_Changed(object sender, Syncfusion.XForms.ProgressBar.ProgressValueEventArgs e)
        {
            if (e.Progress < 40)
            {
                CA.ProgressColor = Color.Red;
            }
            else if (e.Progress >= 40 && e.Progress < 70)
            {
                CA.ProgressColor = Color.Orange;
            }
            else if (e.Progress > 70)
            {
                CA.ProgressColor = Color.Green;
            }
        }

        private void FE_Changed(object sender, Syncfusion.XForms.ProgressBar.ProgressValueEventArgs e)
        {
            if (e.Progress < 40)
            {
                FE.ProgressColor = Color.Red;
            }
            else if (e.Progress >= 40 && e.Progress < 70)
            {
                FE.ProgressColor = Color.Orange;
            }
            else if (e.Progress > 70)
            {
                FE.ProgressColor = Color.Green;
            }

        }
    }
}