using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services.Auth
{
    public interface IAuthorizationManager
    {
        Task<bool> RegUser(string name,string email, string password);
        Task<bool> SignIn(string email, string password);
    }
}
