using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Services.Map
{
    public interface IPinManager
    {
        Task<IEnumerable<UserPin>> GetPinsAsync();
        Task AddOrUpdatePinAsync(UserPin pin);
        Task RemovePinAsync(UserPin pin);
    }
}
