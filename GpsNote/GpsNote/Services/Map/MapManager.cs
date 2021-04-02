using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace GpsNote.Services.Map
{
    public class MapManager : IMapManager
    {
        private readonly ISettingManager _settingManager;
        private readonly IRepository _repository;

        public MapManager(ISettingManager settingManager, IRepository repository)
        {
            _settingManager = settingManager;
            _repository = repository;
        }

        public async Task<IEnumerable<Pin>> GetPins()
        {
            string sqlCommand = $"SELECT * FROM Pins WHERE UserId='{_settingManager.UserId}'";

            var user_pins = await _repository.GetAllWithCommand<UserPin>(sqlCommand);

            return user_pins.Select(p => p.ToPin());
        }

        public async Task SavePin(Pin pin)
        {
            await _repository.AddOrUpdata(pin.ToUserPin());
        }
    }
}
