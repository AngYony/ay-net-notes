using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBlankApp.ViewModels
{
    public class MsgViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; SetProperty(ref title, value); }
        }

        public MsgViewModel()
        {
            OkCommand = new DelegateCommand(() =>
            {
                var param = new DialogParameters();
                param.Add("text", this.Title);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
            });

            CancelCommand = new DelegateCommand(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = parameters.GetValue<string>("p1");
        }
    }
}