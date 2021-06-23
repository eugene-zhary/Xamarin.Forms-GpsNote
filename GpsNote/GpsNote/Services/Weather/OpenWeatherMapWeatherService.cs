using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Resources;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace GpsNote.Services.Weather
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private readonly string _language;

        public OpenWeatherMapWeatherService()
        {
            _language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }

        #region -- IWeatherService implementation --

        public async Task<WeatherModel> GetForecast(double latitude, double longitude)
        {
            WeatherModel forecast;

            try
            {
                string json = await GetJsonWithWeather(latitude, longitude);

                forecast = JsonConvert.DeserializeObject<WeatherJson>(json).ToWeatherModel();
            }
            catch (HttpRequestException)
            {
                forecast = new WeatherModel
                {
                    Name = Strings.NoInternet
                };
            }

            return forecast;
        }

        #endregion

        #region -- Private helpers --

        private async Task<string> GetJsonWithWeather(double latitude, double longitude)
        {
            string json = string.Empty;

            using (var httpClient = new HttpClient())
            {
                string uri = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&lang={_language}&appid={Constants.WeatherRest.API_KEY}";

                json = await httpClient.GetStringAsync(uri);
            }

            return json;
        }

        #endregion
    }
}
