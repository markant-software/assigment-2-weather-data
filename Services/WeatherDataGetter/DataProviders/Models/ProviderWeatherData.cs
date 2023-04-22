namespace Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.Models
{
    public class ProviderWeatherData
    {
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public int Clouds { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
