using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Threading.Tasks;

namespace GpsNote.Services
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingManager;

        public AuthorizationManager(IRepository repo, ISettingsManager settingManager)
        {
            _repository = repo;
            _settingManager = settingManager;
        }

        #region -- IAuthorizationManager implementation --

        public bool IsAuthorized => _settingManager.UserId > 0;

        public async Task<bool> TrySignUpAsync(string name, string email, string password)
        {
            bool canSignUp = true;
            User user = await _repository.FindAsync<User>(u => u.Email.Equals(email));

            if(user != null)
            {
                canSignUp = false;
            }
            else
            {
                await _repository.AddAsync(new User(name, email, password));
            }

            return canSignUp;
        }

        public async Task<bool> TrySignInAsync(string email, string password)
        {
            bool canSignIn = true;
            User user = await _repository.FindAsync<User>(u => u.Email.Equals(email));

            if(user != null && user.Password.Equals(password))
            {
                _settingManager.UserId = user.Id;
            }
            else
            {
                canSignIn = false;
            }

            return canSignIn;
        }

        #endregion

        #region -- Private helpers --


        #endregion
    }
}
