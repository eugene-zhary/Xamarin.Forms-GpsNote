using GpsNote.Models;
using GpsNote.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace GpsNote.Services.Map
{
    public class MapManager : IMapManager
    {
        private IRepository<UsersPin> _repository;

        public MapManager(IRepository<UsersPin> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsersPin>> GetPins(int user_id)
        {
            string sqlCommand = $"SELECT * FROM Pins WHERE UserId='{user_id}'";
            return await _repository.GetAllWithCommand(sqlCommand);
        }

        public async Task SavePin(UsersPin pin)
        {
            await _repository.AddOrUpdata(pin);
        }
    }
}
