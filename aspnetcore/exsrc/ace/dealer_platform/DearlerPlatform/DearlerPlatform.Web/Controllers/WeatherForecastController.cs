using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Web.Controllers
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
        private readonly IRepository<Customer> repository;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IRepository<Customer> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public async  Task<IEnumerable<Customer>> Get()
        {
            return await repository.GetListAsync(a=>a.Id==1);
        }
    }
}
