﻿using Filters.Sample.CusFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Sample.Controllers
{
    [CusActionFilterOnClass]
    [CusResourceFilter] //资源过滤器
    [CusExceptionFilter] //异常筛选器
    [Route("api/[controller]")]
    [ApiController]

    public class DoHomeworkController : ControllerBase
    {
        [HttpGet]
        //[CusActionFilterOnMethod]
        [TypeFilter(typeof(CusActionFilterOnMethodAttribute))] //依赖注入
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
