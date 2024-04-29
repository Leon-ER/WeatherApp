using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    internal class WeatherInfo
    {
        public class weather
        {
            [JsonProperty("main")]
            public string main { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        public class main
        {
            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("humidity")]
            public double Humidity { get; set; }

            [JsonProperty("feels_like")]
            public double feelsLike { get; set; }

            [JsonProperty("pressure")]
            public double pressure {  get; set; }
            [JsonProperty("temp_min")]
            public double tempMin { get; set; }

            [JsonProperty("temp_max")]
            public double tempMax { get; set; }
        }
        public class wind
        {
            [JsonProperty("speed")]
            public double speed { get; set; }
        }
        public class Root
        {
            public List<weather> weather { get; set; }

            public main main { get; set; }

            public wind wind { get; set; }
        }
    }
}
