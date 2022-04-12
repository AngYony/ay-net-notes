using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
    [HttpGet]
        public IActionResult NoLogin()
        {
            
            return Content("未登录");
        }

        [HttpPost]
        public async Task<IActionResult> LoginSucess(string userName,string password){
            if (userName == "孙悟空" && password == "666")
            {
                ClaimsIdentity identity = new ClaimsIdentity("Ctm");
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                //不配置过期时间，默认是会话级的
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
                //写入到cookie中
               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return Ok("登录成功");
            }
            else{
                return BadRequest("登录失败");
            }
            
        }
    }
}
