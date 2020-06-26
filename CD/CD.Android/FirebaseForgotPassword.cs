using System;
using Firebase.Auth;
using CD.Helper;
using System.Threading.Tasks;
using Xamarin.Forms;
using CD.Droid;

[assembly: Dependency(typeof(FirebaseForgotPassword))]
namespace CD.Droid
{
    class FirebaseForgotPassword: IFirebaseForgotPassword
    {
        public async Task ForgotPassword(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
            }
            catch (Exception)
            { 
            }
        }
    }
}