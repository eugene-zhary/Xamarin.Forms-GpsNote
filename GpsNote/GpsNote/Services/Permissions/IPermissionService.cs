using System.Threading.Tasks;

namespace GpsNote.Services.Permissions
{
    public interface IPermissionService
    {
        Task<bool> RequestLocationPermissionAsync();

        void GoToAppSettings();
    }
}
