using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocDemo.Container
{
    public class MyServiceDiscriptor
    { 
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public MyServiceLife Life { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType{ get; set; }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementType { get; set; }

        //存放实现单例的对象
        public object ImplementInstance{ get; set; }


        
    }
}
