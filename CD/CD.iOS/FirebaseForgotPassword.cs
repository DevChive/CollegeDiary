using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Auth;
using CD.Helper;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using Xamarin.Forms;
using CD.iOS;

[assembly: Dependency(typeof(FirebaseForgotPassword))]
namespace CD.iOS
{
    class FirebaseForgotPassword: IFirebaseForgotPassword
    {
        public async Task ForgotPassword(string email)
        {
            try
            {
                await Auth.DefaultInstance.SendPasswordResetAsync(email);
            }
            catch (Exception)
            { 
            }
        }
    }
}