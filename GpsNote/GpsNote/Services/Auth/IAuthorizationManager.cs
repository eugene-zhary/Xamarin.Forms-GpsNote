using GpsNote.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services
{
    public interface IAuthorizationManager
    {
        bool IsAuthorized { get; }

        Task<bool> TrySignUp(string name, string email, string password);
        Task<bool> TrySignIn(string email, string password);
    }
}
