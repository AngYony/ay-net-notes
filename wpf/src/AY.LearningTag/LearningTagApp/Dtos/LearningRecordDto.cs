using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    /// <summary>
    /// 学习记录表
    /// </summary>
    public partial class LearningRecordDto : BaseDto
    {
        /// <summary>
        /// 章节标题
        /// </summary>
        [ObservableProperty]
        private string chapterTitle;

        /// <summary>
        /// 小节标题
        /// </summary>
        [ObservableProperty]
        private string sectionTitle;

        /// <summary>
        /// 小节链接
        /// </summary>
        [ObservableProperty]
        private string sectionLinkUrl;

        /// <summary>
        /// 小节时长
        /// </summary>
        [ObservableProperty]
        private int sectionDuration;

        /// <summary>
        /// 学习开始时间
        /// </summary>
        [ObservableProperty]
        private DateTime learngingBeginTime;

        /// <summary>
        /// 学习结束时间
        /// </summary>
        [ObservableProperty]
        private DateTime learngingEndTime;


        /// <summary>
        /// 已学习小节时长，单位秒
        /// </summary>
        [ObservableProperty]
        private int takeDuration;

    }
}
