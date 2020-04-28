
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace CD.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{
		public LogIn()
		{
			InitializeComponent();

		}

		private async void Login(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MainPage());

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
	}
}