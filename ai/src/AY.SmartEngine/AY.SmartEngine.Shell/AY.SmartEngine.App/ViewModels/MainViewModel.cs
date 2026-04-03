using AY.SmartEngine.ApplicatonServices.Users;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IUserService userService;

        public MainViewModel(ILogger<MainViewModel> logger, IUserService userService)
        {
            logger.LogInformation("测试日志");
            this.userService = userService;
            userService.GetUserAsync();
        }
    }
}
