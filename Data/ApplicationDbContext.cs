using Microsoft.EntityFrameworkCore;
using Assigment2WeatherData.Models;

namespace Assigment2WeatherData.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelBuilderExtensions.ConfigureEntinties(builder);
            ModelBuilderExtensions.SeedData(builder);
        }
    }
}
