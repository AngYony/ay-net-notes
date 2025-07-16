using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Entity
{
    /// <summary>
    /// 学习集
    /// </summary>
    public class LearningWork
    {

        public int WorkId { get; set; }

        /// <summary>
        /// 学习的视频的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 视频的来源链接
        /// </summary>
        public string ResourceLink{get;set;}

    }
}
