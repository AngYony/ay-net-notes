using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LearningProjectDto : BaseDto
    {
        /// <summary>
        /// 课题Id
        /// </summary>
        [ObservableProperty]
        private int projectId;

        /// <summary>
        /// 课题标题
        /// </summary>
        [ObservableProperty]
        private string projectTitle;

    }
}
