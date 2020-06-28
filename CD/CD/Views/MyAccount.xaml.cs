using System;
using CD.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CD.Models;
using CD.Helper;
using Rg.Plugins.Popup.Services;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccount : ContentPage
    {
        string userID = "";
        readonly FireBaseHelperStudent fireBaseHelperStudent = new FireBaseHelperStudent();
        IFirebaseDeleteAccount auth;
        protected override bool OnBackButtonPressed() => false;
        public MyAccount()
        {
            InitializeComponent();
            userID = App.UserUID;
            auth = DependencyService.Get<IFirebaseDeleteAccount>();
        }
        protected override async void OnAppearing()
        {
            await fireBaseHelperStudent.AddGPA(userID);
            Student user = await fireBaseHelperStudent.GetStudent(userID);
            this.BindingContext = user;
            FE.Progress = Convert.ToDouble(user.FinalExam);
            CA.Progress = Convert.ToDouble(user.CA);
            institute.Text = user.Institute;
            studentName.Text = user.StudentName;
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
        private async void tips(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new MyAccountHelp());
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
        private async void edit_account(object sender, EventArgs e)
        {
            Student student = await fireBaseHelperStudent.GetStudent(userID);
            await PopupNavigation.PushAsync(new MyAccountUpdate(student));
        }

        private async void delete_account(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Are you sure you want to delete your account?",
                "If you delete your account all your information will be permanently deleted.", "Yes", "No");
            if (result) // if it's equal to Yes
            {
                try
                {
                    await fireBaseHelperStudent.DeleteStudent(App.UserUID);
                    await auth.DeleteAccount();
                    App.UserUID = "";
                    App.Current.MainPage = new NavigationPage(new LogIn());
                    await DisplayAlert("Account deleted", "To use the application again please sign up", "ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Failed", "Please try again later", "ok");
                }
            }
        }

        private void sign_out(object sender, EventArgs e)
        {
            App.UserUID = "";
            App.Current.MainPage = new NavigationPage(new LogIn());
            // back button disabled
            OnBackButtonPressed();
        }        
    }
}