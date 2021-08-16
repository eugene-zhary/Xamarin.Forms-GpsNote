using GpsNote.Models;
using System.Threading.Tasks;

namespace GpsNote.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetForecastAsync(double latitude, double longitude);
    }
}
