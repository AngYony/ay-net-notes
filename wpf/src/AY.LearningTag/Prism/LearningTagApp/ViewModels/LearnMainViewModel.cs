
using Dumpify;
using LearningTag.PrismShared.ViewModel;
using LearningTagApp.Dtos;


namespace LearningTagApp.ViewModels
{
    public class LearnMainViewModel : BaseNavigationViewModel
    {
        //public ObservableList<LearningWorkDto> LearningWorkDtos { get; set; }
        //public NotifyCollectionChangedSynchronizedViewList<LearningWorkDto> View { get; }

        public LearnMainViewModel()
        {
            //LearningWorkDtos = new ObservableList<LearningWorkDto>();
            //LearningWorkDtos.AddRange(GetLearningWorkDto());
            //View = LearningWorkDtos.ToNotifyCollectionChanged();
           
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