using Android.Widget;
using CD.Droid;
using CD.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastMessage))]
namespace CD.Droid
{
    public class ToastMessage: IToastMessage
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}