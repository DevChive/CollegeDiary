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
        }

        private async void Save_Account(object sender, EventArgs e)
        {
            bool validate = true;
            if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(userInstitute.Text) && validate)
            {
                validate = true;
            }
            else
            {
                validate = false;
                await DisplayAlert("Error", "All fields are required", "Ok");
            }
            if (validate)
            {
                try
                {
                    var studentToEdit = await fireBaseHelperStudent.GetStudent(user.StudentID);
                    await fireBaseHelperStudent.UpdateAccount(user.StudentID, userName.Text, userInstitute.Text);
                    Navigation.PushAsync(new MyAccount(), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    await PopupNavigation.RemovePageAsync(this);
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Please try again", "Ok");
                }

            }
        }

        private void Cancel_Update(object sender, EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }
    }
}