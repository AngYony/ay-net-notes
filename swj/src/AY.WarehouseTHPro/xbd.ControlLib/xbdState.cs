using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    public partial class xbdState : UserControl
    {
        public xbdState()
        {
            InitializeComponent();
        }

        private bool state = false;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("获取或设置状态")]
        public bool State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    this.BackgroundImage = state ? Properties.Resources.Run : Properties.Resources.Stop;
                }
            }
        }
    }
}
