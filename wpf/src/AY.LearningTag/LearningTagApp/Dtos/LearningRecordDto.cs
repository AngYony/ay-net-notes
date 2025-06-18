using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public class LearningRecordDto : BaseDto
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; SetProperty(ref id, value); }
        }

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; SetProperty(ref title, value); }
        }
        private DateTime beginTime;
        /// <summary>
        /// 学习开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
        private DateTime endTime;
        /// <summary>
        /// 学习结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

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

        private decimal videoDuration;
        /// <summary>
        /// 视频时长
        /// </summary>
        public decimal VideoDuration
        {
            get { return videoDuration; }
            set { videoDuration = value; }
        }
        private decimal studiedDuration;
        /// <summary>
        /// 已学时长
        /// </summary>
        public decimal StudiedDuration
        {
            get { return studiedDuration; }
            set { studiedDuration = value; }
        }

    }
}
