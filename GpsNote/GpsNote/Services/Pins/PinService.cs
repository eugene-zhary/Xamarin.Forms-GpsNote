using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpsNote.Services.Map
{
    public class PinService : IPinService
    {
        private readonly ISettingsManager _settings;
        private readonly IRepository _repository;

        public PinService(ISettingsManager settingManager, IRepository repository)
        {
            _settings = settingManager;
            _repository = repository;
        }

        #region -- Public properties --

        public bool IsCollectionUpdated { get; set; }

        #endregion

        #region -- IPinManager implementation --

        public Task<IEnumerable<PinModel>> GetPinsAsync()
        {
            IsCollectionUpdated = false;

            return _repository.GetRowsAsync<PinModel>(pin => pin.UserId == _settings.UserId);
        }

        public async Task<IEnumerable<PinModel>> SearchPinsAsync(string searchQuery)
        {
            IsCollectionUpdated = false;

            searchQuery = searchQuery.ToLower();

            var pins = await _repository.GetRowsAsync<PinModel>(pin => pin.UserId == _settings.UserId);

            var result = pins?.Where(pin => pin?.UserId == _settings.UserId
                && pin.Label.ToLower().Contains(searchQuery)
                || pin.Address.ToLower().Contains(searchQuery)
                || pin.Latitude.ToString().Contains(searchQuery)
                || pin.Longitude.ToString().Contains(searchQuery));

            return result;
        }

        public Task RemovePinAsync(PinModel pin)
        {
            IsCollectionUpdated = true;

            return _repository.RemoveAsync(pin);
        }

        public async Task AddOrUpdatePinAsync(PinModel pin)
        {
            IsCollectionUpdated = true;

            if (pin.Id == 0)
            {
                pin.UserId = _settings.UserId;

                await _repository.AddAsync(pin);
            }
            else
            {
                await _repository.UpdateAsync(pin);
            }
        }

        #endregion
    }
}
