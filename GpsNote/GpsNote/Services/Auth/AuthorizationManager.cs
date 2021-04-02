using GpsNote.Helpers;
using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GpsNote.Services
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IRepository _repository;
        private readonly ISettingManager _settingManager;

        public AuthorizationManager(IRepository repo, ISettingManager settingManager)
        {
            _repository = repo;
            _settingManager = settingManager;
        }

        #region -- IAuthorizationManager implementation --

        public bool IsAuthorized => _settingManager.UserId > 0;

        public async Task<bool> TrySignUp(string name, string email, string password)
        {
            bool CanSignUp = true;
            User user = await _repository.FindWithCommand<User>($"SELECT * FROM Users WHERE Email='{email}'");

            if (user != null)
            {
                CanSignUp = false;
            }
            else
            {
                await _repository.Add(new User(name, email, password));
            }

            return CanSignUp;
        }

        public async Task<bool> TrySignIn(string email, string password)
        {
            bool output = UserValidator.IsValid(email, password);

            if (output)
            {
                output = await IsUserExists(email, password);
            }

            return  output;
        }

        #endregion

        #region -- Private helpers --

        private async Task<bool> IsUserExists(string email, string password)
        {
            bool isExists = true;
            User user = await _repository.FindWithCommand<User>($"SELECT * FROM Users WHERE Email='{email}' AND Password='{password}'");

            if (user == null)
            {
                isExists = false;
            }
            else
            {
                _settingManager.UserId = user.Id;
            }

            return isExists;
        }

        #endregion
    }
}
