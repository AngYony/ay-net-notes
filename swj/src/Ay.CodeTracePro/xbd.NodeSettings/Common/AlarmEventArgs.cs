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
    public  class AlarmEventArgs:EventArgs
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public string CurrentValue { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; }

        /// <summary>
        /// 报警说明
        /// </summary>
        public string AlarmNote { get; set; }

        /// <summary>
        /// 是否为触发
        /// </summary>
        public bool IsTriggered { get; set; }
    }
}
