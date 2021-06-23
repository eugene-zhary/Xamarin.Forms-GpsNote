using System.Threading.Tasks;

namespace GpsNote.Services
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }

        Task<bool> TrySignUpAsync(string name, string email, string password);

        Task<bool> TrySignInAsync(string email, string password);
    }
}
