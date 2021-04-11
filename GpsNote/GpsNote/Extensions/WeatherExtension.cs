using GpsNote.Models;
using System;
using System.Linq;

namespace GpsNote.Extensions
{
    public static class WeatherExtension
    {
        public static WeatherModel ToWeatherModel(this WeatherJson jsonModel)
        {
            WeatherModel output = null;

            if(jsonModel != null)
            {
                output = new WeatherModel
                {
                    Name = jsonModel.Name,
                    Icon = $"https://openweathermap.org/img/w/{jsonModel.Weathers.First().Icon}.png",
                    Description = jsonModel.Weathers.First().Description,
                    Temperature = String.Format("{0:F2}° C", KelvinToCelsius(jsonModel.Main.Temperature))
                };
            }

            return output;
        }

        private static double KelvinToCelsius(double kelvin) => (kelvin - 273.15);
    }
}
