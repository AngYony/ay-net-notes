using LearningTagApp.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LearningTagApp.ViewModels
{
    public class UserRegistWindowViewModel : BindableBase
    {

        private UserDto _userDto;

        public UserDto User
        {
            get { return _userDto ?? (_userDto = new UserDto()); }
            set
            {
                SetProperty(ref _userDto, value);
                SaveUserRegistCommand.RaiseCanExecuteChanged();
            }
        }

        public Window View { get; set; }

        private bool _isInvalid;

        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool IsInvalid
        {
            get { return _isInvalid; }
            set { SetProperty(ref _isInvalid, value); }
        }

        public DelegateCommand SaveUserRegistCommand { get; }
        public UserRegistWindowViewModel()
        {
            SaveUserRegistCommand = new DelegateCommand(SaveUserRegist, CanSaveUserRegist)
                .ObservesProperty(() => User);
        }

        private void SaveUserRegist()
        {

            if (IsInvalid) return;
            //insert data
            this.View.Close();
        }

        private bool CanSaveUserRegist()
        {
            //var u = this.User;
            if (string.IsNullOrEmpty(User.UserName))
                return false;
            return !IsInvalid;
        }
    }
}
