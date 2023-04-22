using Assigment2WeatherData.Models;
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.Models;

namespace Assigment2WeatherData.Services.WeatherDataGetter.DataProviders
{
    public interface IWeatherDataProvider
    {
        public Task<ProviderWeatherData?> GetDataForCity(City city);
    }
}
