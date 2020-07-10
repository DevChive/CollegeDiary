using System;
using CD.Helper;
using System.Threading.Tasks;
using System.Windows.Input;
using CD.Validations;
using Xamarin.Forms;
using CD.Views.Login;

namespace CD.ViewModel.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        public ValidatableObject<string> Email { get; }
        public ValidatableObject<string> Password { get; }
        public ICommand LoginCmd { get; }
        public static IFirebaseAuthenticator auth;

        readonly IFirebaseAuthenticator firebaseAuthenticator;
        readonly NavigationService navigationService;

        Action propChangedCallBack => (LoginCmd as Command).ChangeCanExecute;

        public LoginViewModel(
            IFirebaseAuthenticator firebaseAuthenticator,
            NavigationService navigationService)
        {
            this.firebaseAuthenticator = firebaseAuthenticator;
            this.navigationService = navigationService;

            LoginCmd = new Command(async () => await Login(), () => Email.IsValid && Password.IsValid && !IsBusy);

            Email = new ValidatableObject<string>(propChangedCallBack, new EmailValidator()) { Value = "tataru.theodora@yahoo.com" };
            Password = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator()) { Value = "testing123" };
        }

        async Task Login()
        {
            try
            {
                IsBusy = true;
                propChangedCallBack();

                App.UserUID = (Application.Current as App).AuthToken = await firebaseAuthenticator.LoginWithEmailPassword(Email.Value, Password.Value);

                auth = firebaseAuthenticator;
                if (auth.IsSignedIn())
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else 
                {
                    App.UserUID = "";
                    App.Current.MainPage = new NavigationPage(new LogIn());
                }
                IsBusy = false;
                propChangedCallBack();
            }
            
            catch (Exception)
            {
                App.UserUID = "";
                await App.Current.MainPage.DisplayAlert("Error", "Invalid e-mail or password","OK");
                App.Current.MainPage = new NavigationPage(new LogIn());
            } 
        }
    }
}
