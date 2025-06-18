using LearningTagApp.Dtos;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace LearningTagApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public string Title  { get; set; }= "标题列";

        private ObservableCollection<LearningWorkDto> _learningWorks;

        public ObservableCollection<LearningWorkDto> LearningWorks
        {
            get { return _learningWorks; }
            set { _learningWorks = value; RaisePropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            this.LearningWorks = new ObservableCollection<LearningWorkDto>();
            this.LearningWorks.AddRange(GetAllLearningWorks());

        }


        private IEnumerable<LearningWorkDto> GetAllLearningWorks()
        {
            return new List<LearningWorkDto>() {
                new LearningWorkDto{ Id=1, Name="ASP.NET Core3", },
                new LearningWorkDto{ Id=2, Name="ASP.NET Core2", },
                new LearningWorkDto{ Id=3, Name="ASP.NET Core4", },
            };
        }
    }
}
