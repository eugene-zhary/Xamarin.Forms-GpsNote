using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Collections.Generic;
using System.Linq;
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
            var user_pins = await _repository.GetRowsAsync<UserPin>(pin=>pin.UserId == _settings.UserId);
            return user_pins;
        }

        public async Task RemovePinAsync(UserPin pin)
        {
            await _repository.RemoveAsync(pin);
        }

        public async Task RemovePinAsync(Pin pin)
        {
            await _repository.RemoveAsync(pin.AsUserPin());
        }

        public async Task SavePinAsync(UserPin pin)
        {
            await _repository.AddOrUpdataAsync(pin);
        }

        public async Task SavePinAsync(Pin pin)
        {
            await _repository.AddOrUpdataAsync(pin.AsUserPin(_settings.UserId));
        }
    }
}
