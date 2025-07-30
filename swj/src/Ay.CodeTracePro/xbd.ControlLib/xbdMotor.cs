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
using System.Windows.Forms;

namespace xbd.ControlLib
{
   public  enum PumpState
    {
        ֹͣ,
        ����,
        ����,
        ����
    }
    public partial class xbdMotor : UserControl
    {
        public xbdMotor()
        {
            InitializeComponent();
        }

        private PumpState pumpState = 0;

        [Browsable(true), Category("�Զ�������"), Description("��ȡ�����ñõ��豸״̬")]
        /// <summary>
        /// 0��ֹͣ  1������  2������  3������
        /// </summary>
        public PumpState PumpState
        {
            get { return pumpState; }

            set
            {
                if (value != pumpState)
                {
                    pumpState = value;

                    switch (pumpState)
                    {
                        case PumpState.ֹͣ:
                            this.MainPic.Image = Properties.Resources.PumpStop;
                            break;
                        case PumpState.����:
                            this.MainPic.Image = Properties.Resources.PumpRun;
                            break;
                        case PumpState.����:
                            this.MainPic.Image = Properties.Resources.PumpFault;
                            break;
                        case PumpState.����:
                            this.MainPic.Image = Properties.Resources.PumpSpare;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public event EventHandler PumpClickEvent;

        private void MainPic_DoubleClick(object sender, EventArgs e)
        {
            PumpClickEvent?.Invoke(this, e);
        }
    }
}
