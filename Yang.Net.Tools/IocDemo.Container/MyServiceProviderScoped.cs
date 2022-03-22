using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocDemo.Container
{
    public class MyServiceProviderScoped
    {
        public Dictionary<Type,object> DicScopedService{ get; set; }
        public MyServiceProvider Root{ get; set; }
        public MyServiceProviderScoped(MyServiceProvider root){
        this.Root = root;
            DicScopedService = new();
            ResizeService(Root.dictionary);
        }

        /// <summary>
        /// 将根节点的容器字典的实现和服务类型转移过来，形成单次请求作用域
        /// </summary>
        /// <param name="dictionary"></param>
        public void ResizeService(ConcurrentDictionary<Type, MyServiceDiscriptor> dictionary)
        {
            foreach (var item in dictionary)
            {
                DicScopedService.TryAdd(item.Key, item.Value.ImplementInstance);
            }
        }

        /// <summary>
        /// 获取服务，主要还是通过根容器镜像
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object? GetService(Type serviceType) {
            return Root.GetService(serviceType, this);
        }
    }
}
