using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.CusAuthentication
{
    /// <summary>
    /// 自定义认证方案（此处只是为了讲述实现原理），一般使用第三方包工具
    /// </summary>
    public class TokenAuthenticationHandler : IAuthenticationHandler
    {

        /*
         不需要自定义认证方案，此处只是为了讲述实现原理，实际使用中，可以直接引入JwtBearer包
         */


        AuthenticationScheme _scheme;
        HttpContext _context;
        /// <summary>
        /// 认证初始化
        /// </summary>
        /// <param name="scheme">鉴权架构名称</param>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 认证操作
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            string token = _context.Request.Headers["Authorization"];
            if (token == "wenyi")
            {
                ClaimsIdentity claimsIdentity = new("Ctm");
                claimsIdentity.AddClaims(new Claim[]{
                    new Claim(ClaimTypes.Name, "孙悟空"),
                    new Claim(ClaimTypes.NameIdentifier, "6") //只对6做了鉴权，因此在验证授权时，也必须满足6
                });
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, null, _scheme.Name)));
            }
            return Task.FromResult(AuthenticateResult.Fail("token错误，请重新登录"));
        }

        /// <summary>
        /// 未登录操作
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
             
            _context.Response.Redirect("/api/Login/NoLogin");
            return Task.CompletedTask;
            
        }

        /// <summary>
        /// 没有权限访问
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            _context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

    }
}
