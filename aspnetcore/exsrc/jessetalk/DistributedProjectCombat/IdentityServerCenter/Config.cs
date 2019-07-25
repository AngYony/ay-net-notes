using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource> {
                new ApiResource("api","my api")

            };

        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //客户端模式
                new Client()
                {
                    ClientId = "client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "api" }
                },

                //密码模式
                new Client(){
                    ClientId="pwdClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets={ 
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret=false,//请求时可以不传入Secret也可访问
                    AllowedScopes={"api"}
                }
            };


        }

        public static List<TestUser> GetTestUsers()
        {
            //获取的是测试用户数据，实际使用时获取的是生产数据
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId="1",
                    Username="wy",
                    Password="123456"
                }
            };
        }

    }
}
