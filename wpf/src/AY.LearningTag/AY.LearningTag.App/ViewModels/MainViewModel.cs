using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {
        [ObservableProperty]
        string title = "Learning Tag App";

        public MainViewModel(ILogger logger)
        {
            logger.Information(Title);
        }
    }
}
