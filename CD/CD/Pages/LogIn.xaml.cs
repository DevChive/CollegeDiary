using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
namespace CD.Pages
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
			ProcessStartInfo startInfo = new ProcessStartInfo("iexplore.exe", "http://www.google.com/");
			_ = Process.Start(startInfo);

		}

	}
}