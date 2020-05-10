using System.Threading.Tasks;
using CD.Helper;
using Firebase.Auth;

namespace CD.iOS
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInAsync(email, password);
            return await user.GetIdTokenAsync();
        }
    }
}