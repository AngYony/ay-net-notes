//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
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
        [Category("�Զ�������")]
        [Description("��ȡ������״̬")]
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
