using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocDemo.Container
{
    public class MyServiceCollection : List<MyServiceDiscriptor>
    {
        public void AddTransient<TService, TImplement>()
        where TService : class
        where TImplement : class
        {
            AddTransient(typeof(TService), typeof(TImplement));
        }

        public void AddTransient(Type serviceType, Type implementType)
        {
            var discriptor = new MyServiceDiscriptor()
            {
                Life = MyServiceLife.Transient,
                ServiceType = serviceType,
                ImplementType = implementType,

            };

            AddIfNotContent(discriptor);
        }

        public void AddScoped<TService, TImplement>()
        where TService : class
        where TImplement : class
        {
            AddScoped(typeof(TService), typeof(TImplement));
        }

        public void AddScoped(Type serviceType, Type implementType)
        {
            var discriptor = new MyServiceDiscriptor()
            {
                Life = MyServiceLife.Scoped,
                ServiceType = serviceType,
                ImplementType = implementType,

            };

            AddIfNotContent(discriptor);
        }



        public void AddSingleton<TService, TImplement>() where TService : class where TImplement : class
        {
            AddSingleton(typeof(TService), typeof(TImplement));
        }

        public void AddSingleton(Type serviceType, Type implementType)
        {
            AddSingleton(serviceType, implementType, null);
        }

        public void AddSingleton(Type serviceType, Type implementType, object instance)
        {
            var discriptor = new MyServiceDiscriptor()
            {
                Life = MyServiceLife.Singleton,
                ServiceType = serviceType,
                ImplementType = implementType,
                ImplementInstance = instance

            };
            AddIfNotContent(discriptor);
        }





        /// <summary>
        /// 向集合添加类型
        /// </summary>
        /// <param name="discriptor"></param>
        private void AddIfNotContent(MyServiceDiscriptor discriptor)
        {
            if (!this.Any(m => m.ServiceType == discriptor.ServiceType && m.ImplementType == discriptor.ImplementType))
            {
                this.Add(discriptor);
            }
        }
    }
}
