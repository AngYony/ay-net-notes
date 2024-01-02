using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.CusAuthorizations
{
    public class MyAuthorizationHandler : AuthorizationHandler<MyAuthorizationHandler>,IAuthorizationRequirement
    {
        private readonly string userId;

        public MyAuthorizationHandler(string userId)
        {
            this.userId = userId;
        }
        //重写父类中的抽象方法
        protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, MyAuthorizationHandler requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) &&
            context.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value == userId)
            {
                context.Succeed(requirement);
            }
            else{
                context.Fail();
            }
        }
    }
}
