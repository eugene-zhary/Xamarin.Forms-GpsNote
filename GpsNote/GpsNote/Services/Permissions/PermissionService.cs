using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace GpsNote.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        #region -- IPermissionService implementation --

        public Task<bool> RequestLocationPermissionAsync()
        {
            return RequestPermissionAsync<LocationPermission>();
        }

        public void GoToAppSettings()
        {
            CrossPermissions.Current.OpenAppSettings();
        }

        #endregion

        #region -- Private helpers --

        private async Task<bool> RequestPermissionAsync<T>() 
            where T : BasePermission, new()
        {
            bool result = false;

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<T>();

            if (status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<T>();
            }

            if (status == PermissionStatus.Granted)
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
