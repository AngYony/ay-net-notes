using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WyController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult<string> Token(string value){
            return value;
        }

    }
}