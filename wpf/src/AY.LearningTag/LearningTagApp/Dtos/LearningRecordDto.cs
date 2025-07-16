using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public class LearningRecordDto : BaseDto
    {
        private int _recordId;

        public int RecordId
        {
            get { return _recordId; }
            set { SetProperty(ref _recordId, value); }
        }

        private int _workId;
        public int WorkId
        {
            get { return _workId; }
            set { SetProperty(ref _workId, value); }
        }



        private string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private DateTime _beginTime;
        /// <summary>
        /// 学习开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return _beginTime; }
            set { SetProperty(ref _beginTime, value); }
        }
        private DateTime _endTime;
        /// <summary>
        /// 学习结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
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

        private decimal _videoDuration;
        /// <summary>
        /// 视频时长
        /// </summary>
        public decimal VideoDuration
        {
            get { return _videoDuration; }
            set { SetProperty(ref _videoDuration, value); }
        }
        private decimal _studiedDuration;
        /// <summary>
        /// 已学时长
        /// </summary>
        public decimal StudiedDuration
        {
            get { return _studiedDuration; }
            set { SetProperty(ref _studiedDuration, value); }
        }

    }
}
