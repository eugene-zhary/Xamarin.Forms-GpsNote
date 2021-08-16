using Newtonsoft.Json;
using System.Collections.Generic;

namespace GpsNote.Models
{
    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }

    public class WeatherJson
    {
        [JsonProperty("weather")]
        public IList<Weather> Weathers { get; set; }


        [JsonProperty("main")]
        public Main Main { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
