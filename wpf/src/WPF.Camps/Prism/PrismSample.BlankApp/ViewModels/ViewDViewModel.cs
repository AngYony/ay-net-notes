using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.BlankApp.ViewModels
{
    public class ViewDViewModel : IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public ViewDViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));

        }

        private void Save()
        {
            OnDialogClosed();
        }

        public void OnDialogClosed()
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("Value", "这是沙和尚");
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
        }
    }
}
