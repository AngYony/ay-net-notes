using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTagApp.Dtos
{
    public class MenuInfoDto:BaseDto
    {
		private string _title;

		public string Title
		{
			get { return _title; }
			set { _title = value; SetProperty(ref _title, value); }
		}


        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; SetProperty(ref _icon, value); }
        }


		private string _color;

		public string Color
		{
			get { return _color; }
			set { _color = value; }
		}


	}
}
