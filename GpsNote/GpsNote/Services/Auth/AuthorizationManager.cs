using GpsNote.Helpers;
using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GpsNote.Services.Auth
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IRepository<User> repository;

        public AuthorizationManager(IRepository<User> repo)
        {
            repository = repo;
        }

        #region -- IAuthorizationManager implementation --

        public async Task<bool> TrySignUp(string name, string email, string password)
        {
            // check for the unique email
            if (await repository.FindWithCommand($"SELECT * FROM Users WHERE Email='{email}'") != null)
                return false;

            // add new user to database
            await repository.Add(new User(name, email, password));
            return true;
        }

        public async Task<bool> TrySignIn(string email, string password)
        {
            return UserValidator.IsValid(email, password) &&
                await isUserExists(email, password);
        }

        #endregion

        #region -- Private helpers --

        private async Task<bool> isUserExists(string email, string password)
        {
            User user = await repository.FindWithCommand($"SELECT * FROM Users WHERE Email='{email}' AND Password='{password}'");

            if (user != null) {
                Preferences.Set("UserId", user.Id);
                return true;
            }

            return false;
        }

        #endregion
    }
}
