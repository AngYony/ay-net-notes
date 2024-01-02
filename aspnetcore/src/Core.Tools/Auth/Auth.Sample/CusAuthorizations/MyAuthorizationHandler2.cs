using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.CusAuthorizations
{
    public class MyAuthorizationHandler2 : AuthorizationHandler<MyAuthorizationHandler2>, IAuthorizationRequirement
    {
        private readonly string userName;

        public MyAuthorizationHandler2(string userName)
        {
            this.userName = userName;
        }
        //重写父类中的抽象方法
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAuthorizationHandler2 requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Name) &&
            context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Name)).Value == userName)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
