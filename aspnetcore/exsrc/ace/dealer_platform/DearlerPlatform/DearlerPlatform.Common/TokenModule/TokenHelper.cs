using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DearlerPlatform.Common.TokenModule
{
    public static class TokenHelper
    {
        public static string CreateToken(JwtTokenModel jwtTokenModel)
        {
            var claims = new[]
            {
                new Claim("Id",jwtTokenModel.Id.ToString()),
                new Claim("CustomerNo",jwtTokenModel.CustomerNo),
                new Claim("CustomerName",jwtTokenModel.CustomerName),
            };

            //生成密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenModel.Security));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: jwtTokenModel.Issuer,
                 audience: jwtTokenModel.Audience,
                 expires: DateTime.Now.AddMinutes(jwtTokenModel.Expires),
                 signingCredentials: creds,
                 claims: claims);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}