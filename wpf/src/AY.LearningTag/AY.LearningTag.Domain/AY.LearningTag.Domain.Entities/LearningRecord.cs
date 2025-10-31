using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Entities
{
    /// <summary>
    /// 学习记录（学习任务）
    /// </summary>
    public class LearningRecord : BaseEntity
    {
        /// <summary>
        /// 小节信息Id
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// 学习开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 学习结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// 已学时长
        /// </summary>
        public int TakeDuration { get; set; }


    }
}
