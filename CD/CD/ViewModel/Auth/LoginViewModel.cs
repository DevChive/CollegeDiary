using System;
using System.Collections.Generic;
using System.Text;
using CD.Helper;
using CD.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using CD.Validations;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using com.sun.xml.@internal.rngom.parse.compact;

namespace CD.ViewModel.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        public ValidatableObject<string> Email { get; }
        public ValidatableObject<string> Password { get; }
        public ICommand LoginCmd { get; }

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

            Email = new ValidatableObject<string>(propChangedCallBack, new EmailValidator()) { Value = "test@yahoo.com" };
            Password = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator()) { Value = "test123" };
        }

        async Task Login()
        {
            IsBusy = true;
            propChangedCallBack();
            try
                {
                    App.Token = (Application.Current as App).AuthToken = await firebaseAuthenticator.LoginWithEmailPassword(Email.Value, Password.Value);
                    IsBusy = false;
                    propChangedCallBack();
                }
                catch (Exception)
                {
                    App.Token = "";
                    App.Current.MainPage = new NavigationPage(new LogIn());
            }
            
        }
    }
}
