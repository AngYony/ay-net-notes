using Filters.Sample.CusFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoHomeworkController : ControllerBase
    {
    [HttpGet]
    [CusActionFilter]
        public void DoHomework(){
            GetInCar();
        }

        private void GetInCar()
        {
            System.Console.WriteLine("乘车");
        }
    }
}
