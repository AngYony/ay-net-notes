using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Entity
{
    /// <summary>
    /// 章节信息
    /// </summary>
    public class Chapter : BaseEntity
    {
        /// <summary>
        /// 所属课题Id
        /// </summary>
        public int ProjectId { get; set; }


        /// <summary>
        /// 章节标题
        /// </summary>
        public string ChapterTitle { get; set; }
    }


}
