using Autofac.Sample.IocServices;
using Microsoft.AspNetCore.Mvc;

namespace Autofac.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IIocService iocService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IIocService iocService, ILogger<WeatherForecastController> logger)
        {
            this.iocService = iocService;
            _logger = logger;
        }

        [HttpGet("str")]
        public string GetStr()
        {
            return iocService.GetStr();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
