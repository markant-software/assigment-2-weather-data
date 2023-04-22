using System.Text.Json.Serialization;

namespace Assigment2WeatherData.Models.ViewModels
{
    public class WeatherDataViewModel
    {
        [JsonPropertyName("Temperature")]
        public float Temperature { get; set; }
        [JsonPropertyName("Wind")]
        public float Wind { get; set; }
        [JsonPropertyName("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        public WeatherDataViewModel(WeatherData weatherData)
        {
            Temperature= weatherData.Temperature;
            Wind= weatherData.Wind; 
            LastUpdateTime= weatherData.LastUpdateTime;
        }
    }
}
