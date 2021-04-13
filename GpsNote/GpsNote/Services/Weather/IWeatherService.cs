using GpsNote.Models;
using System.Threading.Tasks;

namespace GpsNote.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetForecast(double latitude, double longitude);
    }
}
