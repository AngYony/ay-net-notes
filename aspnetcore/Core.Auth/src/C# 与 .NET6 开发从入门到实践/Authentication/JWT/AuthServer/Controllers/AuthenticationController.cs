using JwtLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace AuthServer.Controllers;

public class LoginModel
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost, Route("Login")]
    public ActionResult RequestToken([FromBody] LoginModel login, [FromServices] IOptions<JwtTokenOptions> options)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(login.UserName) || login.Password != "123456")
        {
            return BadRequest("Invalid Request");
        }

        var option = options.Value;

        // 准备令牌的声明
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login.UserName)
        };

        // 生成签名证书
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 生成令牌
        var jwtToken = new JwtSecurityToken(
            option.Issuer,
            option.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(option.AccessExpiration),
            signingCredentials: credentials);

        // 序列化令牌
        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return Ok(token);
    }
}
