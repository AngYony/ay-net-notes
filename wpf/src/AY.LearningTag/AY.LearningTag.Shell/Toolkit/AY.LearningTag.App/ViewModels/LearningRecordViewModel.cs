using AY.LearningTag.App.Dtos;
using AY.LearningTag.App.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AY.LearningTag.App.ViewModels
{

    public partial class LearningRecordViewModel : ObservableRecipient
    {


        /// <summary>
        /// 课题标题
        /// </summary>
        [ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(LoadLearningRecordsCommand))]
        [NotifyPropertyChangedFor(nameof(ProjectTitleName))] //关联通知属性
        private string projectTitle;


        public string ProjectTitleName => "当前名称：" + this.ProjectTitle;

        [ObservableProperty]
        private bool showLoading;
        /// <summary>
        /// 进行保存的学习记录
        /// </summary>
        [ObservableProperty]
        private LearningRecordDto learningRecordSaveData;

        /// <summary>
        /// 学习记录
        /// </summary>
        private ObservableCollection<LearningRecordDto> LearningRecords { get; set; }


        public LearningRecordViewModel()
        {
            this.ProjectTitle = "找不到课题标题";
        }



        [RelayCommand(CanExecute = nameof(CanSaveLearningRecord))]
        private async Task SaveLearningRecordAsync()
        {
            WeakReferenceMessenger.Default.Send(
                new ValueChangedMessage<RecordMessage>(
                    new RecordMessage
                    {
                        RecordTitle = "发送的消息"
                    }));
            this.ProjectTitle = "AAAA";
            await Task.Delay(4000);
        }

        private bool CanSaveLearningRecord()
        {
            return true;
            //return !string.IsNullOrWhiteSpace(learningRecordSaveData?.SectionTitle);
        }
    }
}
