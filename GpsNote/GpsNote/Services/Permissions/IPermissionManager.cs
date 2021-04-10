using System.Threading.Tasks;

namespace GpsNote.Services.Permissions
{
    public interface IPermissionManager
    {
        Task<bool> RequestLocationPermissionAsync();
    }
}
