using Microsoft.EntityFrameworkCore;
using Assigment2WeatherData.Data;
using Assigment2WeatherData.Services.WeatherDataGetter;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Getter data service.
builder.Services.AddSingleton<IWeatherDataGetterService, WeatherDataGetterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Start Wether data service.
IWeatherDataGetterService? weatherDataGetterService = app.Services.GetService<IWeatherDataGetterService>();
if (weatherDataGetterService != null)
{
    weatherDataGetterService.StartGetteringData();
}

// Ensure DB is create
var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.Migrate();



app.Run();
