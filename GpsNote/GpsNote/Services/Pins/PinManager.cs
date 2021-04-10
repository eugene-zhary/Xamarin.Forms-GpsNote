using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpsNote.Services.Map
{
    public class PinManager : IPinManager
    {
        private readonly ISettingsManager _settings;
        private readonly IRepository _repository;

        public PinManager(ISettingsManager settingManager, IRepository repository)
        {
            _settings = settingManager;
            _repository = repository;
        }

        #region -- Public properties --

        public bool IsCollectionUpdated { get; set; }

        #endregion

        #region -- IPinManager implementation --

        public async Task<IEnumerable<UserPin>> GetPinsAsync()
        {
            IsCollectionUpdated = false;
            return await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId);
        }

        public async Task<IEnumerable<UserPin>> SearchPinsByLabelAsync(string label)
        {
            IsCollectionUpdated = false;

            label = label.ToLower();

            var pins = await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId);
            return pins.Where(pin => pin.UserId == _settings.UserId &&
                                     pin.Label.ToLower().Contains(label));
        }

        public async Task<IEnumerable<UserPin>> SearchPinsAsync(string searchQuery)
        {
            IsCollectionUpdated = false;

            searchQuery = searchQuery.ToLower();

            var pins = await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId);
            return pins.Where(pin => pin.UserId == _settings.UserId &&
                                     pin.Label.ToLower().Contains(searchQuery) ||
                                     pin.Address.ToLower().Contains(searchQuery) || 
                                     pin.Latitude.ToString().Contains(searchQuery) ||
                                     pin.Longitude.ToString().Contains(searchQuery));
        }

        public async Task RemovePinAsync(UserPin pin)
        {
            IsCollectionUpdated = true;

            await _repository.RemoveAsync(pin);
        }

        public async Task AddOrUpdatePinAsync(UserPin pin)
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
