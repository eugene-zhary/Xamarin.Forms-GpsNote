using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Services.Map
{
    public interface IPinManager
    {
        Task<IEnumerable<UserPin>> GetPinsAsync();
        Task<IEnumerable<UserPin>> GetPinsWithLabelAsync(string label);
        Task<IEnumerable<UserPin>> GetPinsWithQueryAsync(string searchQuery);
        Task AddOrUpdatePinAsync(UserPin pin);
        Task RemovePinAsync(UserPin pin);
    }
}
