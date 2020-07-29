using System;
using Firebase.Auth;
using CD.Helper;
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
                var token = await user.User.GetIdTokenAsync(false);
                await user.User.SendEmailVerificationAsync();
                UserID = user.User.Uid.ToString();
                return token;
            }
            catch (Exception) //TODO: check if the user has an account with that email or not
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