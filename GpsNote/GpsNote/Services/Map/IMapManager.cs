using GpsNote.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace GpsNote.Services.Map
{
    public interface IMapManager
    {
        Task<IEnumerable<Pin>> GetPins();
        Task SavePin(Pin pin);
    }
}
