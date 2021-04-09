using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

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

        public async Task<IEnumerable<UserPin>> GetPinsAsync()
        {
            return await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId); ;
        }

        public async Task<IEnumerable<UserPin>> GetPinsWithLabelAsync(string label)
        {
            label = label.ToLower();
            return await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId &&
                                                                  pin.Label.ToLower().Contains(label));
        }

        public async Task<IEnumerable<UserPin>> GetPinsWithQueryAsync(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();
            return await _repository.GetRowsAsync<UserPin>(pin => pin.UserId == _settings.UserId &&
                                                                  pin.Label.ToLower().Contains(searchQuery) ||
                                                                  pin.Address.ToLower().Contains(searchQuery) ||
                                                                  pin.Latitude.ToString().Contains(searchQuery) ||
                                                                  pin.Longitude.ToString().Contains(searchQuery));
        }

        public async Task RemovePinAsync(UserPin pin)
        {
            await _repository.RemoveAsync(pin);
        }

        public async Task AddOrUpdatePinAsync(UserPin pin)
        {
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
    }
}
