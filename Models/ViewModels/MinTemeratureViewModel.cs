using System.Text.Json.Serialization;

namespace Assigment2WeatherData.Models.ViewModels
{
    public class MinTemeratureViewModel
    {
        [JsonPropertyName("City")]
        public CityViewModel City { get; set; }
        [JsonPropertyName("Temperature")]
        public float Temperature { get; set; }
        [JsonPropertyName("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        public MinTemeratureViewModel(WeatherData weatherData)
        {
            City = new CityViewModel(weatherData.City!);
            Temperature = weatherData.Temperature;
            LastUpdateTime = weatherData.LastUpdateTime;
        }
    }
}
