using System.Threading.Tasks;

namespace CD.Helper
{
    public interface IFirebaseRegister
    {
        Task RegisterWithEmailAndPassword(string email, string password);
        bool Registered();
        bool NotRegistered();
    }
}
