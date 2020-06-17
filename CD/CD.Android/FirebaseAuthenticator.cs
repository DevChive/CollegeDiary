using System;
using CD.Helper;
using Firebase.Auth;
using System.Threading.Tasks;
using Android.Gms.Extensions;

namespace CD.Droid
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = await user.User.GetIdTokenAsync(false);
            return user.User.Uid;
        }

        public bool IsSignedIn()
        {
            string UID = Firebase.Auth.FirebaseAuth.Instance.Uid;
            //var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return UID != "";
        }
        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class FirebaseRegister : IFirebaseRegister
    {
        public bool NotRegistered()
        {
            throw new NotImplementedException();
        }

        public bool Registered()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterWithEmailAndPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
        }
    }
}