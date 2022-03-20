using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yang.API.Models;
using Yang.API.ViewModel;

namespace Yang.API.Controllers
{
    [EnableCors("myany")]   //针对单个控制器设置跨域规则
    [Route("api/[controller]")] // 一旦在控制器上指定了ApiController特性，就必须要指定Route特性
    [ApiController] //使用了ApiController，就不需要显式指定FromMBody/FromForm等
    // ApiController可以验证传入的参数是否符合规范
    public class WyController : ControllerBase
    {


        public readonly IUserService userService = null;
        public WyController(IServiceProvider serviceProvider, IUserService userService, IWebHostEnvironment environment)
        {
            //获取wwwroot的路径
            var res = environment.WebRootPath; //必须存在wwwroot目录才能获取到值，否则值为null

            userService = userService;
            var code = userService.GetCode();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 0)
            {
                return Ok();// 等同于 return new OkObjectResult(null);
            }
            else
            {
                return NotFound(); //NotFound便利方法作为return new NotFoundResult()的简写调用
            }
        }

        [HttpGet("name")]
        public ActionResult<WyViewModel> GetName(string? name) //.net6中，非必填项一定要加?
        {
            return new WyViewModel();
        }


        [HttpPost]
        public WyViewModel Save(WyViewModel wy)
        {
            // Post应该返回一个对象
            return wy;
        }

        [HttpPut] //整体更新
        public void Update(WyViewModel wy)
        {
            // update不返回数据
        }



        [HttpDelete]
        public void Delete()
        {

        }
    }
}
