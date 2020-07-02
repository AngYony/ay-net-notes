using System;
using Microsoft.AspNetCore.Mvc;

namespace My.ApplicationPart.Wy.Study
{
    public class WyController:Controller
    {
        public IActionResult Index(){
            return Ok("WyWyWy");
        }
    }
}
