using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.NodeSettings
{
    /// <summary>
    /// 一个设备对应多个组
    /// </summary>
    public class GroupBase
    {
        /// <summary>
        /// 通信组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 通信组状态
        /// </summary>
        public bool IsOk { get; set; } = true;

        /// <summary>
        /// 读取的次数
        /// </summary>
        public int ReadTimes { get; set; } = 1;

        /// <summary>
        /// 延时时间
        /// </summary>
        public int DelayTime { get; set; } = 100;
    }
}
