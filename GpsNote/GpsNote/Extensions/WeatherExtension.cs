using GpsNote.Models;
using System.Linq;

namespace GpsNote.Extensions
{
    public static class WeatherExtension
    {
        public static WeatherModel ToWeatherModel(this WeatherJson jsonModel)
        {
            return new WeatherModel
            {
                Name = jsonModel.Name,
                Icon = $"https://openweathermap.org/img/w/{jsonModel.Weathers.First().Icon}.png",
                Description = jsonModel.Weathers.FirstOrDefault()?.Description,
                Temperature = string.Format("{0:F2}° C", KelvinToCelsius(jsonModel.Main.Temperature))
            };
        }

        private static double KelvinToCelsius(double kelvin) => kelvin - 273.15;
    }
}
