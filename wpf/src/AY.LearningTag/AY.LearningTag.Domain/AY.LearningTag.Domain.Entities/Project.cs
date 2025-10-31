using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Entities
{
    /// <summary>
    /// 课题信息
    /// </summary>
    public class Project : BaseEntity
    {
        /// <summary>
        /// 课题标题
        /// </summary>
        public string ProjectTitle { get; set; }

        /// <summary>
        /// 课题链接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
