
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.WeatherAPI;

namespace Assigment2WeatherData.Services.WeatherDataGetter.DataProviders
{
    public static class WeatherDataProviderFactory
    {
        // We implement factory pattern for Weather data providers inorder easy to extend for other providers.
        // For now we only support WeatherAPI data provider.
        static public IWeatherDataProvider? createWeatherDataProvider(IConfiguration configuration)
        {
            IWeatherDataProvider? weatherDataProvider = null;
            // Get current data provider name from configuration.
            string dataProvider = configuration.GetValue("WeatherDataProviders:WeatherDataProvider", "");

            // For now we just support "WeatherAPI"
            if (dataProvider == "WeatherAPI")
            {
                weatherDataProvider = new WeatherAPIDataProvider(configuration);
            }
            else
            {
                // Not supported WeatherAPI return null.
                Console.WriteLine($"Not supported Weater Data Provider: {dataProvider}");
                weatherDataProvider = null;
            }

            return weatherDataProvider;
        }
    }
}
