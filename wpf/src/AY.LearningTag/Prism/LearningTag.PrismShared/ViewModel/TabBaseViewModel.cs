namespace LearningTag.PrismShared.ViewModel
{
    public class TabBaseViewModel : Prism.Mvvm.BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}