using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Resources;
using GpsNote.Services.Rest;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace GpsNote.Services.Weather
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private readonly IRestService _restService;

        public OpenWeatherMapWeatherService(IRestService restService)
        {
            _restService = restService;
        }

        #region -- IWeatherService implementation --

        public async Task<WeatherModel> GetForecastAsync(double lat, double lon)
        {
            WeatherModel result;

            try
            {
                string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                string uri = $"{Constants.WeatherRest.BASE_URL}?lat={lat}&lon={lon}&lang={lang}&appid={Constants.WeatherRest.API_KEY}";

                var forecast = await _restService.GetAsync<WeatherJson>(uri);

                result = forecast.ToWeatherModel();
            }
            catch (HttpRequestException)
            {
                result = new WeatherModel
                {
                    Name = Strings.NoInternet
                };
            }

            return result;
        }

        #endregion
    }
}
