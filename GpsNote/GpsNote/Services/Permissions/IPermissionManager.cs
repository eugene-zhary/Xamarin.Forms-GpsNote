using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services.Permissions
{
    public interface IPermissionManager
    {
        Task<bool> RequestLocationPermissionAsync();
    }
}
