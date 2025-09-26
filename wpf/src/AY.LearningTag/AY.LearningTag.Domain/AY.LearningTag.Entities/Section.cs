using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Entities
{
    /// <summary>
    /// 小节信息
    /// </summary>
    public class Section : BaseEntity
    {
        /// <summary>
        /// 所属课题Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 所属章节Id，0表示没有大的章节，直接属于课题
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// 小节标题
        /// </summary>
        public string SectionTitle { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 小节时长，单位秒
        /// </summary>
        public int Duration { get; set; }

    }
}
