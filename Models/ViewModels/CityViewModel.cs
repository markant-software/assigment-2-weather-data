using System.Text.Json.Serialization;

namespace Assigment2WeatherData.Models.ViewModels
{
    public class CityViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Country")]
        public string Country { get; set; }
        public CityViewModel(City city)
        {
            Id = city.Id;
            Name = city.Name;
            Country = city.Country;
        }

    }
}
