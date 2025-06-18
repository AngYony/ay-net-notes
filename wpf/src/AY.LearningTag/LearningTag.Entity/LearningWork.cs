using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Entity
{
    public class LearningWork
    {
        public int Id { get; set; }

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
