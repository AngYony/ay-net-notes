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
    public partial class xbdValve : UserControl
    {
        public xbdValve()
        {
            InitializeComponent();
        }
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����÷�������")]
        public string ValveName { get; set; }

        private bool state = false;

        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����÷���״̬")]
        public bool State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    UpdateImage(this.isVertical, state);
                }
            }
        }

        private bool isVertical = false;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����÷��ŵ�ˮƽ��ֱ״̬")]
        public bool IsVertical
        {
            get { return isVertical; }
            set
            {
                isVertical = value;
                UpdateImage(isVertical, this.state);
            }
        }

        private void UpdateImage(bool isVertical, bool state)
        {
            if (isVertical)
            {
                this.BackgroundImage = state ? Properties.Resources.VvalveOn : Properties.Resources.VvalveOff;
            }
            else
            {
                this.BackgroundImage = state ? Properties.Resources.valveOn : Properties.Resources.valveOff;
            }
        }
    }
}
