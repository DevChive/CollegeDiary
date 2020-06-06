﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Autofac;
using CD.ViewModel.Auth;
using CD.Helper;

namespace CD.Views.Login
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

		}
		//private async void Cancel_Login(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new LogIn());
		//}
		//private async void Gmail(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new LogIn());

		//}
		//private async void ForgotPasswordCommand(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new ForgotPassword());
		//}
		//public async void ShowError()
		//{
		//	await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
		//}

		//private void Facebook(object sender, EventArgs e)
		//{

		//}
	}
}