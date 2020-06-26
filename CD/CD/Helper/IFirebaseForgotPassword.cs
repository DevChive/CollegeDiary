using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CD.Helper
{
    public interface IFirebaseForgotPassword
    {
        Task ForgotPassword(string email);
    }
}
