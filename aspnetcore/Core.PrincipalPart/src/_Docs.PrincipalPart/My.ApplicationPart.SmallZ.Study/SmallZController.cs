using System;
using Microsoft.AspNetCore.Mvc;

namespace My.ApplicationPart.SmallZ.Study
{
    public class SmallZController:Controller
    {
        public IActionResult Index(){
            return Ok("SmalllZ");
        }
    }
}
