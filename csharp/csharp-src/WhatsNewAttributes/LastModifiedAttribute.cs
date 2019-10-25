using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsNewAttributes
{

    /// <summary>
    /// 用于标记最后一次修改数据项的时间
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =true,Inherited =false )]
    public class LastModifiedAttribute:Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateModified">修改日期</param>
        /// <param name="changes">修改信息</param>
        public LastModifiedAttribute(string dateModified,string changes)
        {
            _dateModified = DateTime.Parse(dateModified);
            _changes = changes;
        }

        //只有get访问器，无set访问器，所以是只读的
        public DateTime DateModified => _dateModified;

        /// <summary>
        /// 获取修改信息，只读
        /// </summary>
        public string Changes => _changes;
        /// <summary>
        /// 可选参数，描述该数据项的任何重要问题
        /// </summary>
        public string Issues { get; set; }


    }



    
}
