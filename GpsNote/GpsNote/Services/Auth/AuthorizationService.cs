using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Threading.Tasks;

namespace GpsNote.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingManager;

        public AuthorizationService(
            IRepository repository, 
            ISettingsManager settingManager)
        {
            _repository = repository;
            _settingManager = settingManager;
        }

        #region -- IAuthorizationManager implementation --

        public bool IsAuthorized => _settingManager.UserId > 0;

        public async Task<bool> TrySignUpAsync(string name, string email, string password)
        {
            bool canSignUp = true;
            UserModel user = await _repository.FindAsync<UserModel>(u => u.Email.Equals(email));

            if(user != null)
            {
                canSignUp = false;
            }
            else
            {
                await _repository.AddAsync(new UserModel(name, email, password));
            }

            return canSignUp;
        }

        public async Task<bool> TrySignInAsync(string email, string password)
        {
            bool canSignIn = true;
            UserModel user = await _repository.FindAsync<UserModel>(u => u.Email.Equals(email));

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
    }
}
