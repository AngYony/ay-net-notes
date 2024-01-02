using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    //自定义ServiceFilter
    public class MyServiceFilterAttribute : Attribute, IFilterFactory, IFilterMetadata
    {
        private readonly Type type;

        public bool IsReusable => true;
        public MyServiceFilterAttribute(Type type)
        {
            //判断当前类型是否实现了某个接口
            var isFilter=type.IsAssignableTo(typeof(IFilterMetadata));
            if(isFilter ==false){
                throw new Exception("这不是一个过滤器");
            }
            this.type = type;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new Exception("serviceProvider没有值");
            }
            //GetService可以返回null，而GetRequiredService不存在就会报错
            //return serviceProvider.GetService<IFilterMetadata>();
            return (IFilterMetadata) serviceProvider.GetRequiredService(this.type);
             
        }


        ////TypeFilter的实现
        //public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        //{
        //    if (serviceProvider == null)
        //    {
        //        throw new Exception("serviceProvider没有值");
        //    }
        //    var objectFactory = ActivatorUtilities.CreateFactory(this.type, Type.EmptyTypes);
        //    return (IFilterMetadata)objectFactory(serviceProvider, null);
        //}
    }
}
