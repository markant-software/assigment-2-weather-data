using System.ComponentModel.DataAnnotations;

namespace Assigment2WeatherData.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public List<WeatherData> WeatherDatas { get; set; } = new List<WeatherData>();
    }
}
