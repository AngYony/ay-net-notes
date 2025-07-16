using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Entity
{
    /// <summary>
    /// 学习记录（学习任务）
    /// </summary>
    public class LearningRecord
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 学习集Id
        /// </summary>
        public int WorkId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 学习开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 学习结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 学习用时
        /// </summary>
        public TimeSpan UseTime
        {
            get
            {
                return EndTime - BeginTime;
            }
        }

        /// <summary>
        /// 视频时长
        /// </summary>
        public decimal VideoDuration { get; set; }

        /// <summary>
        /// 已学时长
        /// </summary>
        public decimal StudiedDuration { get; set; }
    }
}