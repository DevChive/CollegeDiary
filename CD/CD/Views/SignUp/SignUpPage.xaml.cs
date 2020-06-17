using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using CD.Views.Login;

namespace CD.Views.SignUp
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void LoginPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LogIn());
        }

        private void RegiterNewUser(object sender, System.EventArgs e)
        {
            if (!passwordMatch(PasswordEntry.Text, ConfirmPasswordEntry.Text))
            {
                DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }

        private bool passwordMatch(string password1, string password2)
        {
            return password1 == password2;
        }
    }
}