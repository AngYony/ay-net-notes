using Microsoft.AspNetCore.Mvc;

namespace JwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WyController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult<string> Token(string value)
        {
            return value;
        }
    }
}