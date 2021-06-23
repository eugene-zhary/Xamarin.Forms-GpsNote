using Newtonsoft.Json;
using System.Collections.Generic;

namespace GpsNote.Models
{
    public class Weather
    {
        [JsonProperty(Constants.WeatherRest.DESCRIPTION_PROPERTY)]
        public string Description { get; set; }


        [JsonProperty(Constants.WeatherRest.ICON_PROPERTY)]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty(Constants.WeatherRest.TEMP_PROPERTY)]
        public double Temperature { get; set; }
    }

    public class WeatherJson
    {
        [JsonProperty(Constants.WeatherRest.WEATHER_PROPERTY)]
        public IList<Weather> Weathers { get; set; }


        [JsonProperty(Constants.WeatherRest.MAIN_PROPERTY)]
        public Main Main { get; set; }


        [JsonProperty(Constants.WeatherRest.NAME_PROPERTY)]
        public string Name { get; set; }
    }
}
