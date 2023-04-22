using System.Text.Json.Serialization;

namespace Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.WeatherAPI.Models
{
    public class Current
    {
        [JsonPropertyName("last_updated_epoch")]
        public long last_updated_epoch { get; set; }

        [JsonPropertyName("temp_c")]
        public float temp_c { get; set; }
        [JsonPropertyName("wind_kph")]
        public float wind_kph { get; set; }
        [JsonPropertyName("cloud")]
        public int cloud { get; set; }
    }

    public class WeatherAPIResponse
    {
        [JsonPropertyName("current")]

        public Current? current { get; set; }

    }
}
