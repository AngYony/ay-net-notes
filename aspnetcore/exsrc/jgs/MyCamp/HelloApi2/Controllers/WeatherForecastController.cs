using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloApi2.Controllers
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
        private readonly ILogger _myLogger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerFactory loggerFactory)
        {
            //方式一
            _logger = logger;

            //方式二：泛型，等同于方式一，方式一更简洁一些。
            _logger = loggerFactory.CreateLogger<WeatherForecastController>();

            //方式三：非泛型
            _myLogger = loggerFactory.CreateLogger("MyLogger");

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogInformation(new EventId(1001, "Action"), "Get action executed");


            return result;
        }
    }
}
