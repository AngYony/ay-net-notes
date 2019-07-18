using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CusJwtAuthSample
{
    public class MyTokenValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);



            if (securityToken=="abcdefg")
            {
                identity.AddClaim(new Claim("name", "smallz"));
                identity.AddClaim(new Claim("SuperAdminOnly", "true"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"));
            }

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
