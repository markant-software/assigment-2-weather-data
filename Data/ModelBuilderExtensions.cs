using Microsoft.EntityFrameworkCore;
using Assigment2WeatherData.Models;

namespace Assigment2WeatherData.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEntinties(this ModelBuilder modelBuilder)
        {
            // For now nothing to configure.
        }
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Add Cities
            List<City> cities = new List<City>(){
                new City() { Id = 1, Country = "Macedonia", Name = "Skopje", Latitude = 42.00f, Longitude = 21.43f },
                new City() { Id = 2, Country = "Macedonia", Name = "Bitola", Latitude = 41.03f, Longitude = 21.34f },
                new City() { Id = 3, Country = "Denmark", Name = "Copenhagen", Latitude = 55.67f, Longitude = 12.58f },
                new City() { Id = 4, Country = "Denmark", Name = "Odense", Latitude = 55.40f, Longitude = 10.38f },
                new City() { Id = 5, Country = "Norway", Name = "Oslo", Latitude = 59.92f, Longitude = 10.75f },
                new City() { Id = 6, Country = "Norway", Name = "Bergen", Latitude = 60.39f, Longitude = 5.32f },
                new City() { Id = 7, Country = "United Kingdom", Name = "London", Latitude = 51.52f, Longitude = -0.11f },
                new City() { Id = 8, Country = "United Kingdom", Name = "Manchester", Latitude = 53.48f, Longitude = -2.25f },
                new City() { Id = 9, Country = "Spain", Name = "Madrid", Latitude = 40.40f, Longitude = -3.68f },
                new City() { Id = 10, Country = "Spain", Name = "Valencia", Latitude = 10.18f, Longitude = -68.00f }
                };
            modelBuilder.Entity<City>().HasData(cities);
        }
    }
}
