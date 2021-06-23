using GpsNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpsNote.Services.Map
{
    public interface IPinService
    {
        bool IsCollectionUpdated { get;set; }

        Task<IEnumerable<PinModel>> GetPinsAsync();

        Task<IEnumerable<PinModel>> SearchPinsAsync(string searchQuery);

        Task AddOrUpdatePinAsync(PinModel pin);

        Task RemovePinAsync(PinModel pin);
    }
}
