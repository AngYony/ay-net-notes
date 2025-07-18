﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.About.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public AboutViewModel()
        {
            Message = "关于软件的介绍，用来验证目录加载模块";
        }
    }
}
