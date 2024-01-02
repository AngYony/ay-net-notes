using System;
using Microsoft.AspNetCore.Mvc;

namespace My.ApplicationPart.Test.Study
{
    public class TestController:Controller
    {
        public IActionResult Index(){
            return Ok("Test");
        }
    }
}
