using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Services.Map
{
    public interface IPinManager
    {
        Task<IEnumerable<UserPin>> GetPinsAsync();
        Task SavePinAsync(UserPin pin);
        Task SavePinAsync(Pin pin);
        Task RemovePinAsync(UserPin pin);
        Task RemovePinAsync(Pin pin);
    }
}
