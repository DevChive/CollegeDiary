
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
			Loading.IsRunning = true;
			Loading.Color = Color.Red;
		}
	}
}