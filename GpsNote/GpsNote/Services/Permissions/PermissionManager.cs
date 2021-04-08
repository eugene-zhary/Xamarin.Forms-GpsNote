using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace GpsNote.Services.Permissions
{
    public class PermissionManager : IPermissionManager
    {
        public async Task<bool> RequestLocationPermissionAsync()
        {
            var status = await RequestPermissionAsync<LocationPermission>();

            return (status == PermissionStatus.Granted);
        }

        private async Task<PermissionStatus> RequestPermissionAsync<T>() where T : BasePermission, new()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<T>();

            if(status == PermissionStatus.Denied)
            {
                CrossPermissions.Current.OpenAppSettings();
            }

            if(status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<T>();
            }

            return status;
        }
    }
}
