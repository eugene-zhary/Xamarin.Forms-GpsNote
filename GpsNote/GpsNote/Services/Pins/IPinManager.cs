using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpsNote.Services.Map
{
    public interface IPinManager
    {
        bool IsCollectionUpdated { get;set; }
        Task<IEnumerable<UserPin>> GetPinsAsync();
        Task<IEnumerable<UserPin>> SearchPinsByLabelAsync(string label);
        Task<IEnumerable<UserPin>> SearchPinsAsync(string searchQuery);
        Task AddOrUpdatePinAsync(UserPin pin);
        Task RemovePinAsync(UserPin pin);
    }
}
