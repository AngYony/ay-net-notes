using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZTApp
{
    public class VersionDetail
    {
        private string _time;

        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }


        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }


        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private string _group;
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
    }
}
