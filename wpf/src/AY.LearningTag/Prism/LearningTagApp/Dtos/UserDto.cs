using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public class UserDto : BaseDto
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private string  _mail;

        public string Mail
        {
            get { return _mail; }
            set { SetProperty(ref  _mail , value); }
        }
          
        private int _sex;
        /// <summary>
        /// 1:男；2：女；其他未知；
        /// </summary>
        public int Sex
        {
            get { return _sex; }
            set {  SetProperty(ref _sex , value); }
        }
            
    }
}
