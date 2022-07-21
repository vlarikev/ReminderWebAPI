using Microsoft.AspNetCore.Mvc;

namespace ReminderWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Mild", "Hot"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            int tempC = Random.Shared.Next(-35, 35);
            return Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = tempC,
                Summary = GetStringSummary(tempC)
            })
            .ToArray();
        }
        private string GetStringSummary(int tempC)
        {
            switch (tempC)
            {
                case <= -15:
                    return Summaries[0];
                case <= 20:
                    return Summaries[1];
                default:
                    return Summaries[2];
            }
        }
    }
}