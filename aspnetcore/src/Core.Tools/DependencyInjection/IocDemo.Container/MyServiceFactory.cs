using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocDemo.Container
{
    public static class MyServiceFactory
    {
        /// <summary>
        /// 创建根服务提供者
        /// </summary>
        /// <param name="myServices"></param>
        /// <returns></returns>
        public static MyServiceProvider BuildProvider(this MyServiceCollection myServices)
        {
            return new MyServiceProvider(myServices);
        }

        public static MyServiceProviderScoped CreateScoped(this MyServiceProvider provider)
        {
            return new MyServiceProviderScoped(provider);

        }
    }
}
