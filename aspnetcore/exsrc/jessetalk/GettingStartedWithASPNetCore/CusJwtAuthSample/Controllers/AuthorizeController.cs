﻿using CusJwtAuthSample.Models;
using CusJwtAuthSample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CusJwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        public AuthorizeController(IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
        }

        [HttpPost("token")] //
        public IActionResult Token([FromForm] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!(loginViewModel.UserName == "smallz" && loginViewModel.Password == "123456"))
                {
                    return BadRequest();
                }

                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,"smallz"),
                    new Claim(ClaimTypes.Role,"user"),
                    new Claim("SuperAdminOnly","true")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddHours(30),
                creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }



    }
}
