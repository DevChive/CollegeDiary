
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Autofac;
using CD.ViewModel.Auth;

namespace CD.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{

		public LogIn()
		{
			InitializeComponent();
			this.BindingContext = (Application.Current as App).Container.Resolve<LoginViewModel>();
		}

		private async void Login(object sender, EventArgs e)
		{
			App.Current.MainPage = new NavigationPage(new MainPage());
			//TODO: Put a wrong password message and clear the password field
		}
		private async void Cancel_Login(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LogIn());
		}
		private async void Gmail(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LogIn());

		}
		private async void ForgotPasswordCommand(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ForgotPassword());
		}
		public async void ShowError()
		{
			await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
		}
	}
}