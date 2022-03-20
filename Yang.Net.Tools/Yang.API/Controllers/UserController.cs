using Microsoft.AspNetCore.Mvc;
using Yang.API.Models;

namespace Yang.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public IUserService UserService { get; set; }   // 通过Autofac属性注入，而不是构造函数注入
       [HttpGet]
        public IActionResult Index()
        {
            return Ok(UserService.GetCode());

        }
    }
}
