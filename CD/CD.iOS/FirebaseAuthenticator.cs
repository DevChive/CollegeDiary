using System.Threading.Tasks;
using CD.Helper;
using Firebase.Auth;
using System;
using Foundation;
using Xamarin.Forms;
using CD.iOS;

[assembly: Dependency(typeof(FirebaseAuthenticator))]
namespace CD.iOS
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync();
                return user.User.Uid;
            }
            catch(Exception ex)
            {
                Console.WriteLine("----------------------------------" + ex.ToString());
                return "";
            }
        }
        public bool IsSignedIn()
        {
            //var user = Auth.DefaultInstance.CurrentUser;
            //return user != null;
            string UID = Auth.DefaultInstance.CurrentUser.Uid;

            return UID != "";
        }
        public bool SignOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}