using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public class LearningWorkDto : BaseDto
    {
        private int _workId;

        public int WorkId
        {
            get { return _workId; }
            set { SetProperty(ref _workId, value); }
        }

        private string _name;
        /// <summary>
        /// 学习的视频的名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _resourceLink;
        /// <summary>
        /// 视频的来源链接
        /// </summary>
        public string ResourceLink
        {
            get { return _resourceLink; }
            set { SetProperty(ref _resourceLink, value); }
        }

    }
}
