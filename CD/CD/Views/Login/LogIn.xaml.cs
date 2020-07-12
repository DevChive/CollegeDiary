
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Autofac;
using CD.ViewModel.Auth;
using CD.Views.SignUp;
using CD.Views.ForgotPassword;
using System.Linq;

namespace CD.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{
		protected override bool OnBackButtonPressed() => false;
		public LogIn()
		{
			InitializeComponent();
			this.BindingContext = (Application.Current as App).Container.Resolve<LoginViewModel>();
		}

		private void Login(object sender, EventArgs e)
		{
			// TODO: change the login functionality
			Loading.IsRunning = true;
			Loading.Color = Color.Red;
		}

		private async void SignUpPage(object sender, EventArgs e)
		{
			sign_up_button.IsEnabled = false;
			await Navigation.PushAsync(new SignUpPage());
			sign_up_button.IsEnabled = true;
		}

		private async void ForgotPasswordPage(object sender, EventArgs e)
		{
			ForgotPasswordLabel.IsEnabled = false;
			await Navigation.PushAsync(new SimpleForgotPasswordPage());
			ForgotPasswordLabel.IsEnabled = true;
		}
	}
}