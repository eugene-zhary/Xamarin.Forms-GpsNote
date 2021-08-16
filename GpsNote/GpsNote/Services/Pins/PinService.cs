using GpsNote.Models;
using GpsNote.Services.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpsNote.Services.Map
{
    public class PinService : IPinService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRestService _restService;

        public PinService(ISettingsManager settingsManager, IRestService restService)
        {
            _settingsManager = settingsManager;
            _restService = restService;
        }

        #region -- Public properties --

        public bool IsCollectionUpdated { get; set; }

        #endregion

        #region -- IPinManager implementation --

        public async Task<IEnumerable<PinModel>> GetPinsAsync()
        {
            IEnumerable<PinModel> result;

            try
            {
                result = await _restService.GetAsync<IEnumerable<PinModel>>($"{Constants.GpsRest.BASE_URL}/pins/{_settingsManager.UserId}");

                IsCollectionUpdated = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IEnumerable<PinModel>> SearchPinsAsync(string searchQuery)
        {
            IEnumerable<PinModel> result;

            try
            {
                result = await _restService.GetAsync<IEnumerable<PinModel>>($"{Constants.GpsRest.BASE_URL}/pins/{_settingsManager.UserId}/{searchQuery}");

                IsCollectionUpdated = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task RemovePinAsync(PinModel pin)
        {
            try
            {
                await _restService.DeleteAsync<PinModel>($"{Constants.GpsRest.BASE_URL}/pins", pin);

                IsCollectionUpdated = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddOrUpdatePinAsync(PinModel pin)
        {
            try
            {
                if (pin.Id == 0)
                {
                    pin.UserId = _settingsManager.UserId;

                    await _restService.PostAsync<PinModel>($"{Constants.GpsRest.BASE_URL}/pins", pin);
                }
                else
                {
                    await _restService.PutAsync<PinModel>($"{Constants.GpsRest.BASE_URL}/pins", pin);
                }

                IsCollectionUpdated = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
