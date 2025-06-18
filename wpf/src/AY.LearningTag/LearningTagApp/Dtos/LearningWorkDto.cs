using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public  class LearningWorkDto : BaseDto
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; SetProperty(ref id, value); }
        }

        private string name;
        /// <summary>
        /// 学习的视频的名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; SetProperty(ref name, value); }
        }

        private string resourceLink;
        /// <summary>
        /// 视频的来源链接
        /// </summary>
        public string ResourceLink
        {
            get { return resourceLink; }
            set { resourceLink = value; SetProperty(ref resourceLink, value); }
        }

    }
}
