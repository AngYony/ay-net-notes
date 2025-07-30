//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// GroupBase基类
    /// </summary>
    public  class GroupBase
    {
        /// <summary>
        /// 通信组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 通信组状态
        /// </summary>
        public bool IsOK { get; set; } = true;

        /// <summary>
        /// 读取次数，控制在读取失败时，最多可以尝试读几次
        /// </summary>
        public int ReadTimes { get; set; } = 1;

        /// <summary>
        /// 延时时间
        /// </summary>
        public int DelayTime { get; set; } = 100;

    }
}
