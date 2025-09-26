
using LearningTag.PrismShared.ViewModel;
using LearningTagApp.Dtos;
using ObservableCollections;


namespace LearningTagApp.ViewModels
{
    public class LearnMainViewModel : BaseNavigationViewModel
    {
        private ObservableList<LearningWorkDto> _learningWorkDtos;
        public NotifyCollectionChangedSynchronizedViewList<LearningWorkDto> View { get; }



        public LearnMainViewModel()
        {

            _learningWorkDtos = new ObservableList<LearningWorkDto>();
            _learningWorkDtos.AddRange(GetLearningWorkDto());
            View = _learningWorkDtos.ToNotifyCollectionChanged();

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