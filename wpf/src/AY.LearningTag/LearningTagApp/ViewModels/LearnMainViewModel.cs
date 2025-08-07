using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Dumpify;
using LearningTag.Shared.ViewModel;
using LearningTagApp.Dtos;
using LearningTagApp.Messages;
using ObservableCollections;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningTagApp.ViewModels
{
    public class LearnMainViewModel : BaseNavigationViewModel
    {


        public ObservableList<LearningWorkDto> LearningWorkDtos { get; set; }
        public NotifyCollectionChangedSynchronizedViewList<LearningWorkDto> View { get; }

        public LearnMainViewModel()
        {
            LearningWorkDtos = new ObservableList<LearningWorkDto>();
            LearningWorkDtos.AddRange(GetLearningWorkDto());
            View = LearningWorkDtos.ToNotifyCollectionChanged();
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<RecordMessage>>(this,Receive);

        }

        private void Receive(object recipient, ValueChangedMessage<RecordMessage> message)
        {
            message.Value.RecordTitle.Dump();
        }

        private LearningWorkDto[] GetLearningWorkDto()
        {
            return new LearningWorkDto[]
            {
                new LearningWorkDto
                {
                    WorkId = 1,
                    Name = "C#基础教程",
                    ResourceLink = "https://example.com/csharp-basics"
                },
                new LearningWorkDto
                {
                    WorkId = 2,
                    Name = "Prism框架入门",
                    ResourceLink = "https://example.com/prism-intro"
                },
                new LearningWorkDto
                {
                    WorkId = 3,
                    Name = "MVVM设计模式",
                    ResourceLink = "https://example.com/mvvm-pattern"
                }
            };
        }
    }
}
