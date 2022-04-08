using Filters.Sample.CusFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Sample.Controllers
{
    [CusActionFilterOnClass]
    //[CusResourceFilter] //资源过滤器
    [CusExceptionFilter] //异常筛选器
    [Route("api/[controller]")]
    [ApiController]
    [MyServiceFilter(typeof(CusResourceFilterAttribute))] //资源过滤器并注入，使用ServiceFilter，必须要在program中进行注册

    public class DoHomeworkController : ControllerBase
    {
        [HttpGet]
        //[CusActionFilterOnMethod]
        [TypeFilter(typeof(CusActionFilterOnMethodAttribute))] //依赖注入
        //如果使用ServiceFilter，CusActionFilterOnMethodAttribute必须在program中进行注册
        //[ServiceFilter(typeof(CusActionFilterOnMethodAttribute))]
        [CusAuthorizationFilter]
        public void DoHomework()
        {
            GetInCar();
        }
        [HttpGet("say")]
        public string Say()
        {
            return "sayyyyy";
        }
        [HttpGet("exception")]
        public void exp()
        {
            throw new Exception("测试异常筛选器");
        }
        private void GetInCar()
        {
            System.Console.WriteLine("乘车");
        }
    }
}
