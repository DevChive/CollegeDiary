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

[assembly: Dependency(typeof(FirebaseRegister))]
namespace CD.iOS
{
    public class FirebaseRegister: IFirebaseRegister
    {
        public string UserID;
        public async Task<string> RegisterWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                var token = await user.GetIdTokenAsync(false);
                await user.SendEmailVerificationAsync();
                UserID = user.Uid.ToString();
                return token; 
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string UserUID()
        {
            return UserID;
        }
    }
}