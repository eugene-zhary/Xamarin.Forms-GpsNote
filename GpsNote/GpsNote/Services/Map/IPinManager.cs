using GpsNote.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace GpsNote.Services.Map
{
    public interface IPinManager
    {
        Task<IEnumerable<UserPin>> GetPins();
        Task SavePin(Pin pin);
    }
}
