using System.Timers;
using Assigment2WeatherData.Data;
using Assigment2WeatherData.Models;
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders;
using Assigment2WeatherData.Services.WeatherDataGetter.DataProviders.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Assigment2WeatherData.Services.WeatherDataGetter
{
    public class WeatherDataGetterService : IWeatherDataGetterService
    {
        private IServiceScopeFactory _scopeFactory;
        private IWeatherDataProvider? _weatherDataProvider = null;
        private System.Timers.Timer _dataGetterTimer = new System.Timers.Timer();
        

        public WeatherDataGetterService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            // Create Data provider from Factory.
            _weatherDataProvider = WeatherDataProviderFactory.createWeatherDataProvider(configuration);
            
            // Get Data Refresh timer from configuration.
            // Note: Timer interval is in msec!
            _dataGetterTimer.Interval = configuration.GetValue<double>("WeatherDataProviders:DataRefreshTimeSec", 60) * 1000; 
            _dataGetterTimer.AutoReset = true;
            _dataGetterTimer.Enabled = false;
            _dataGetterTimer.Elapsed += OnTimedEvent;

            // Get data on startup.
            _ = GettherData();


        }

        private void OnTimedEvent(Object? source, ElapsedEventArgs args)
        {
            _ = GettherData();
        }

        private async Task GettherData()
        {
            if (_weatherDataProvider == null)
            {
                Console.WriteLine("Can not gether data because no supported data provider!");
                return; 
            }
            
            // Store scheduleTime in UTC.
            DateTime scheduleTime = DateTime.UtcNow;

            // Step 0: Get Applicaton DB Context.
            var scope = _scopeFactory.CreateScope();
            ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Step 1: Get all cities from DB
            List<City> cities = applicationDbContext.Cities.ToList();

            // Step 2: Get weather data for all cities.
            foreach(City city in cities) {

                ProviderWeatherData? providerWeatherData = await _weatherDataProvider.GetDataForCity(city);
                if (providerWeatherData == null)
                {
                    // Problem getting data.
                    Console.WriteLine("Problem gettering data from data provider!");
                    continue;
                }

                // Step 3: Store getthered data in DB.
                WeatherData weatherData = new WeatherData();
                weatherData.CityId = city.Id;
                weatherData.ScheduleTime = scheduleTime;

                weatherData.Temperature = providerWeatherData.Temperature;
                weatherData.Wind = providerWeatherData.Wind;
                weatherData.Clouds = providerWeatherData.Clouds;
                weatherData.LastUpdateTime = providerWeatherData.LastUpdateTime;

                applicationDbContext.WeatherData.Add(weatherData);
            }
            applicationDbContext.SaveChanges();
        }

        public void StartGetteringData()
        {
            if (_weatherDataProvider == null) {
                Console.WriteLine("Data gethering not started because no supported data provider!");
                return;
            }
            _dataGetterTimer.Enabled = true;
        }

        public void StopGetteringData()
        {
            _dataGetterTimer.Enabled = false;
        }
    }
}
