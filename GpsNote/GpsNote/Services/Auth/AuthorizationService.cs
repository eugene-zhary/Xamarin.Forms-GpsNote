using GpsNote.Models;
using GpsNote.Services.Rest;
using System;
using System.Threading.Tasks;

namespace GpsNote.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRestService _restService;
        private readonly ISettingsManager _settingManager;

        public AuthorizationService(
            IRestService restService,
            ISettingsManager settingManager)
        {
            _restService = restService;
            _settingManager = settingManager;
        }

        #region -- IAuthorizationManager implementation --

        public bool IsAuthorized => _settingManager.UserId > 0;

        public async Task<bool> TrySignUpAsync(string name, string email, string password)
        {
            bool result = false;

            try
            {
                var user = await _restService.GetAsync<UserModel>($"{Constants.GpsRest.BASE_URL}/users/{email}");

                if (user == null)
                {
                    user = new UserModel
                    {
                        Name = name,
                        Email = email,
                        Password = password
                    };

                    await _restService.PostAsync<UserModel>($"{Constants.GpsRest.BASE_URL}/users", user);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<bool> TrySignInAsync(string email, string password)
        {
            bool result = false;

            try
            {
                var user = await _restService.GetAsync<UserModel>($"{Constants.GpsRest.BASE_URL}/users/{email}");

                if (user != null && user.Password.Equals(password))
                {
                    _settingManager.UserId = user.Id;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion
    }
}
