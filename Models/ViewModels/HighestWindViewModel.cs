using System.Text.Json.Serialization;

namespace Assigment2WeatherData.Models.ViewModels
{
    public class HighestWindViewModel
    {
        [JsonPropertyName("City")]
        public CityViewModel City { get; set; }
        [JsonPropertyName("Wind")]
        public float Wind { get; set; }
        [JsonPropertyName("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        public HighestWindViewModel(WeatherData weatherData)
        {
            City = new CityViewModel(weatherData.City);
            Wind = weatherData.Wind;
            LastUpdateTime = weatherData.LastUpdateTime;
        }
    }
}
