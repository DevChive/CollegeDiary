using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using CD.Views.Login;
using CD.Helper;
using Xamarin.Forms;
using System;
using System.Text.RegularExpressions;


namespace CD.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage: ContentPage
    {
        IFirebaseRegister auth;
        readonly FireBaseHelperStudent firebaseStudent = new FireBaseHelperStudent();

        public SignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseRegister>();
        }
        private void LoginPage(object sender, System.EventArgs e)
        {
            // not allowing the user to use the back button from the phone
            App.Current.MainPage = new NavigationPage(new LogIn());
            //await Navigation.PopToRootAsync(true);
        }
        private async void RegiterNewUser(object sender, EventArgs e)
        {
            signup_button.IsEnabled = false;
            bool validate = true;
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            
            NameError.IsVisible = false;
            InstituteError.IsVisible = false;
            EmailError.IsVisible = false;
            PasswordErrorNotMatching.IsVisible = false;
            PasswordErrorTooShort.IsVisible = false;
            PasswordEmpty.IsVisible = false;

            if (string.IsNullOrEmpty(NameEntry.Text) && validate)
            {
                NameError.IsVisible = true;
                validate = false;
            }
            if (string.IsNullOrEmpty(College_University.Text) && validate)
            {
                InstituteError.IsVisible = true;
                validate = false;
            }
            // cheking if  the email is valid
            string userEmail = "";
            if (validate)
            {
                if (!string.IsNullOrEmpty(SignUpEmailEntry.Text) && !string.IsNullOrWhiteSpace(SignUpEmailEntry.Text))
                {
                    userEmail = SignUpEmailEntry.Text.Trim();
                }
                else
                {
                    validate = false;
                    EmailError.IsVisible = true;
                }
            }

            if (validate)
            {
                if (!Regex.IsMatch(userEmail, pattern))
                {
                    EmailError.IsVisible = true;
                    validate = false;
                }
            }
            if  (validate)
            {
                if (string.IsNullOrEmpty(PasswordEntry.Text) && string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
                {
                    PasswordEmpty.IsVisible = true;
                    validate = false;
                }
                if (!passwordMatch(PasswordEntry.Text, ConfirmPasswordEntry.Text) && validate)
                {
                    PasswordErrorNotMatching.IsVisible = true;
                    validate = false;
                }
                if (validate && !string.IsNullOrEmpty(PasswordEntry.Text) && PasswordEntry.Text.Length < 6)
                {
                    PasswordErrorTooShort.IsVisible = true;
                    validate = false;
                }
            }
            
            if (validate)
            {
                //System.Console.WriteLine("=====================================" + SignUpEmailEntry.Text + " " + PasswordEntry.Text);
                string Token = await auth.RegisterWithEmailAndPassword(userEmail, PasswordEntry.Text);
                if (!string.IsNullOrEmpty(Token) && Token != "existing")
                {
                    DependencyService.Get<IToastMessage>().Show("Account created");
                    //App.UserUID = authDeleteAccount.UserUID();
                    AddUserDetails(NameEntry.Text, College_University.Text, SignUpEmailEntry.Text);
                    App.UserUID = "";
                    App.Current.Properties["App.UserUID"] = "";
                    await App.Current.SavePropertiesAsync();
                    App.Current.MainPage = new NavigationPage(new LogIn());
                }
                else if (Token == "existing")
                {
                    DependencyService.Get<IToastMessage>().Show("An account using this email already exists");
                    App.Current.MainPage = new NavigationPage(new LogIn());
                }
                else
                {
                    await DisplayAlert("Error", "Please try again", "ok");
                }
            }
            signup_button.IsEnabled = true;
        }

        private bool passwordMatch(string password1, string password2)
        {
            return password1 == password2;
        }

        private async void AddUserDetails(string Name, string UC, string Email)
        {
            await firebaseStudent.AddStudent(App.UserUID, Name, UC, Email);
        }
    }
}