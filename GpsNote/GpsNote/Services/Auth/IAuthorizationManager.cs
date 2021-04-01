using GpsNote.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services.Auth
{
    public interface IAuthorizationManager
    {
        Task<bool> TrySignUp(string name, string email, string password);
        Task<bool> TrySignIn(string email, string password);
    }
}
