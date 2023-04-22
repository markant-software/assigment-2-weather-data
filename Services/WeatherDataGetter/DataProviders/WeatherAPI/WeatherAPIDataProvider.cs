using Assigment2WeatherData.Models;
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.Models;
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.WeatherAPI.Models;

namespace Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.WeatherAPI
{
    public class WeatherAPIDataProvider : IWeatherDataProvider
    {
        private string _webAPIUrl;
        private int requestTimeOut = 30;
        public WeatherAPIDataProvider(IConfiguration configuration)
        {
            // Get WebAPI URL and APP key from configuration.
            string url = configuration.GetValue("WeatherDataProviders:DataProviders:WeatherAPI:URL", "http://api.weatherapi.com/v1/current.json");
            string apiKey = configuration.GetValue("WeatherDataProviders:DataProviders:WeatherAPI:APIKey", "");
            // Web API request for WeatherAPI service is:
            // http://api.weatherapi.com/v1/current.json?key=[ApiKey]&q=[CityName]

            _webAPIUrl = $"{url}?key={apiKey}&q=";

            // Get Timeout from configuration.
            requestTimeOut = configuration.GetValue("WeatherDataProviders:RequestTimeOutSec", 30);
        }
        public async Task<ProviderWeatherData?> GetDataForCity(City city)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(requestTimeOut);
            // The WebApi
            HttpResponseMessage responseMessage = await httpClient.GetAsync(_webAPIUrl + city.Name);

            if (!responseMessage.IsSuccessStatusCode)
            {
                // Problem getting data from WeatherAPI.
                Console.WriteLine("Problem getting data from WeatherAPI.");
                return null;
            }

            // Convert content to JSON.
            // TODO: Add try/catch for robustnest.
            WeatherAPIResponse? response = await responseMessage.Content.ReadFromJsonAsync<WeatherAPIResponse>(); ;


            if (response == null || response.current == null) 
            {
                // Problem getting data from WeatherAPI.
                Console.WriteLine("Not expected response content received from WeatherAPI.");
                return null;
            }

            // Return data
            // Note: We store last_updated_epoch to UTC time.
            return new ProviderWeatherData {
                Temperature = response.current.temp_c,
                Wind = response.current.wind_kph,
                Clouds = response.current.cloud,
                LastUpdateTime = DateTimeOffset.FromUnixTimeSeconds(response.current.last_updated_epoch).DateTime
            };
        }
    }
}
