using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Safety_Sample
{
    class Demo01
    {
        public static void Run()
        {
            WindowsIdentity identity = ShowIdentityInformation();
            WindowsPrincipal principal = ShowPrincipal(identity);
            //添加声称
            identity.AddClaim(new Claim("Age", "24"));
            ShowClaims(principal.Claims);

            //使用HasClaim测试声明是否可用
            identity.HasClaim(c => c.Type == ClaimTypes.Name);
            //检索特定的声明
            var gropuClaims = identity.FindAll(c => c.Type == ClaimTypes.GroupSid);

        }

        /// <summary>
        /// 写入声明信息
        /// </summary>
        /// <param name="claims"></param>
        private static void ShowClaims(IEnumerable<Claim> claims)
        {
            Console.WriteLine("Claims");
            foreach(var claim in claims)
            {
                //获取声明的主题
                Console.WriteLine("Subject:"+claim.Subject);
                //获取声明的颁发者
                Console.WriteLine("Issuer:"+claim.Issuer);
                //获取声明的声明类型
                Console.WriteLine("Type:"+claim.Type);
                //获取声明的值类型
                Console.WriteLine("Value type:"+claim.ValueType);
                //获取声明的值
                Console.WriteLine("Value:"+claim.Value);
                //获取跟此声明关联的其他属性值
                foreach (var prop in claim.Properties)
                {
                    Console.WriteLine( $"\tProperty:{prop.Key} {prop.Value}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 输出Principal的额外信息
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private static WindowsPrincipal ShowPrincipal(WindowsIdentity identity)
        {
            Console.WriteLine("Show principal information");
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            
            if(principal==null)
            {
                Console.WriteLine("not a windows Principal");
                return null;
            }
            //当前用户是否属于内置的角色User
            Console.WriteLine("Users?"+principal.IsInRole(WindowsBuiltInRole.User));
            //当前用户是否属于内置的角色Administrator
            Console.WriteLine("Administrators?"+principal.IsInRole(WindowsBuiltInRole.Administrator));
            Console.WriteLine();
            return principal;
        }

        /// <summary>
        /// 输出WindowsIdentity的信息
        /// </summary>
        /// <returns></returns>
        private static WindowsIdentity ShowIdentityInformation()
        {
            // 返回表示当前 Windows 用户的WindowsIdentity 对象。
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                Console.WriteLine("not a windows identity");
                return null;
            }
            //省份类型
            Console.WriteLine("IdentityType:"+identity);
            //windows登录名
            Console.WriteLine("Name:"+identity.Name);
            //是否对用户进行了身份验证
            Console.WriteLine("Authenticated:"+identity.IsAuthenticated);
            //省份验证类型
            Console.WriteLine("Authentication Type:"+identity.AuthenticationType);
            //该用户是否为匿名账户
            Console.WriteLine("Anonymous?"+identity.IsAnonymous);
            Console.WriteLine("Access Token:"+identity.AccessToken.DangerousGetHandle());
            Console.WriteLine();
            return identity;
        }
    }
}
