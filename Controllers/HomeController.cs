using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assigment2WeatherData.Data;
using Assigment2WeatherData.Models;
using Assigment2WeatherData.Models.ViewModels;

namespace Assigment2WeatherData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMinTemperatures()
        {
            // Get latest minimum temerature for all cities and return view model for min temperatures
            List<MinTemeratureViewModel> minTemperatures = _context.WeatherData.Include(wd => wd.City)
                .GroupBy(wd => wd.City).Select(group => new MinTemeratureViewModel(group.OrderBy(group => group.Temperature)
                .ThenByDescending(group => group.LastUpdateTime).FirstOrDefault()!))
                .ToList();
            return Json(minTemperatures);
        }

        [HttpGet]
        public JsonResult GetHighestWinds()
        {
            // Get latest highes wind for all cities.
            List<HighestWindViewModel> highesWinds = _context.WeatherData.Include(wd => wd.City)
                .GroupBy(wd => wd.City).Select(group => new HighestWindViewModel(group.OrderByDescending(group => group.Wind)
                .ThenByDescending(group => group.LastUpdateTime).FirstOrDefault()!))
                .ToList();
            return Json(highesWinds);
        }
        [HttpGet]
        public JsonResult GetLastTwoHoursData(int? id)
        {
            // Get data from last two hours.
            // Note: We return Distinct by LastUpdateTime.
            List<WeatherDataViewModel> lastData = _context.WeatherData.Where(wd => wd.CityId == id && wd.LastUpdateTime >= DateTime.UtcNow.AddHours(-2))
                .GroupBy(wd=>wd.LastUpdateTime)
                .Select(wd=>new WeatherDataViewModel(wd.FirstOrDefault()!))
                .ToList();

            return Json(lastData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}