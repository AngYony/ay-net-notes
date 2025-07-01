using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningTagApp.ViewModels
{
    public class MessageDialogViewModel : BindableBase,IDialogAware
    {
        public MessageDialogViewModel()
        {

        }

        public string Title => throw new NotImplementedException();

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
           var parms = parameters.GetValue<string>("message");

        }
    }
}
