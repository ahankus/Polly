using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        private static int _errorRequestCount = 0;

        [HttpGet("error", Name = "GetWeatherForecastError")]
        public IActionResult GetError()
        {
            _errorRequestCount++;

            // Return 500 Internal Server Error for the first 3 requests
            if (_errorRequestCount % 3 == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, "OK");
            }

            // After the 3rd request, return 200 OK
            return StatusCode((int)HttpStatusCode.InternalServerError, "Simulated 500 error");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
