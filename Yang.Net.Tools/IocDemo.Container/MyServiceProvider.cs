using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocDemo.Container
{
    /// <summary>
    /// 根节点的服务提供者
    /// </summary>
    public class MyServiceProvider
    {
        //把容器中的东西放入到字典中
        public ConcurrentDictionary<Type, MyServiceDiscriptor> dictionary { get; set; }

        public MyServiceProvider(MyServiceCollection myServices)
        {
            dictionary = new();
            ResizeService(myServices);
        }


        public void ResizeService(MyServiceCollection serviceCollection)
        {
            foreach (var service in serviceCollection)
            {
                dictionary.TryAdd(service.ServiceType, service);
            }
        }

        public object? GetService(Type serviceType, MyServiceProviderScoped scopedProvider)
        {
            var hasValue = dictionary.TryGetValue(serviceType, out MyServiceDiscriptor discriptor);
            if (hasValue)
            {
                switch (discriptor.Life)
                {
                    default:
                    case MyServiceLife.Transient:
                        {
                            return Activator.CreateInstance(discriptor.ImplementType);
                        }
                    case MyServiceLife.Singleton:
                        {
                            if (discriptor.ImplementInstance == null)
                            {
                                discriptor.ImplementInstance = Activator.CreateInstance(discriptor.ImplementType);
                            }
                            return discriptor.ImplementInstance;
                        }
                    case MyServiceLife.Scoped:
                        {
                            if (scopedProvider.DicScopedService.TryGetValue(serviceType, out object instance))
                            {
                                if (instance == null)
                                {
                                    instance = Activator.CreateInstance(discriptor.ImplementType);
                                    scopedProvider.DicScopedService[serviceType] = instance;
                                }
                            }
                            return instance;
                        }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
