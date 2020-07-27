using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;
using CD.Helper;
using CD.Models;

namespace CD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountUpdate 
    {
        Student user;
        readonly FireBaseHelperStudent fireBaseHelperStudent = new FireBaseHelperStudent();
        public MyAccountUpdate(Student User)
        {
            InitializeComponent();
            user = User;
            userName.Text = user.StudentName;
            userInstitute.Text = user.Institute;
            userEmail.Text = user.StudentEmail;
        }

        [Obsolete]
        private async void Save_Account(object sender, EventArgs e)
        {
            save_profile_button.IsEnabled = false;
            bool validate = true;

            ErrorName.IsVisible = false;
            ErrorInstite.IsVisible = false;

            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrWhiteSpace(userName.Text))
            {
                validate = false;
                ErrorName.IsVisible = true;
            }
            if (validate)
            {
                if(string.IsNullOrWhiteSpace(userInstitute.Text) || string.IsNullOrEmpty(userInstitute.Text))
                {
                    validate = false;
                    ErrorInstite.IsVisible = true;
                }
            }
            if (validate)
            {
                try
                {
                    var studentToEdit = await fireBaseHelperStudent.GetStudent(user.StudentID);
                    await fireBaseHelperStudent.UpdateAccount(user.StudentID, userName.Text, userInstitute.Text);
                    await Navigation.PushAsync(new MyAccount(), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    await PopupNavigation.RemovePageAsync(this);
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Please try again", "Ok");
                }

            }
            save_profile_button.IsEnabled = true;
        }

        [Obsolete]
        private async void Cancel_Update(object sender, EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}