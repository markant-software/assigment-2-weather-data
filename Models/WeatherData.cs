using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigment2WeatherData.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
       
        [ForeignKey("CityId")]
        public City City { get; set; }
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public int Clouds { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime ScheduleTime { get; set; }

    }
}
