using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("api1","api application")
            };

        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //隐式模式
                new Client()
                {
                    ClientId = "mvc",
                    //匿名模式也叫隐式模式
                    AllowedGrantTypes = GrantTypes.Implicit,

                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "api1" }
                }
                //,//密码模式
                //new Client(){
                //    ClientId="pwdClient",
                //    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                //    ClientSecrets={
                //        new Secret("secret".Sha256())
                //    },
                //    RequireClientSecret=false,//请求时可以不传入Secret也可访问
                //    AllowedScopes={"api"}
                //}

            };


        }

        public static List<TestUser> GetTestUsers()
        {
            //获取的是测试用户数据，实际使用时获取的是生产数据
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId="10000",
                    Username="wy",
                    Password="123456",
                    
                    //Claims=new List<Claim>{
                    //    new Claim(JwtClaimTypes.Email,"wy@abc.com"),
                    //    new Claim(JwtClaimTypes.Role,"user")
                    //}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>{
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }



    }
}
