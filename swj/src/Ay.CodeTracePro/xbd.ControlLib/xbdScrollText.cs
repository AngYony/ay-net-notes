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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum ScrollDirection
    {
        RightToLeft,
        LeftToRight
    }

    /// <summary>
    /// �ı���ֱ����Ķ��뷽ʽ
    /// </summary>
    public enum TextVerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    public partial class xbdScrollText : UserControl
    {
        public xbdScrollText()
        {
            InitializeComponent();

            //���ÿؼ���ʽ
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.DoubleBuffered = true;
            this.updateTimer.Interval = 50;
            this.updateTimer.Tick += UpdateTimer_Tick;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            //��������
            lastRectangle.Inflate(10, 10);
            //�ֲ�����
            this.Invalidate();
        }

        private Timer updateTimer = new Timer();

        private Graphics graphics;

        private float xPos, yPos;

        private RectangleF lastRectangle;

        private string textScoll = "�űش���λ��";
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ��ʾ���ı�����")]
        public string TextScroll
        {
            get { return textScoll; }
            set
            {
                textScoll = value;
                this.Invalidate();
            }
        }


        private bool isScoll = false;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ��ʾ���ı�����")]
        public bool IsScoll
        {
            get { return isScoll; }
            set
            {
                isScoll = value;
                updateTimer.Enabled = isScoll;
                this.Invalidate();
            }
        }


        private int scrollDistance = 5;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡÿ�ι����ľ���")]
        public int ScrollDistance
        {
            get { return scrollDistance; }
            set
            {
                scrollDistance = value;

                this.Invalidate();
            }
        }


        private int scrollInterval = 50;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ�����ļ��ʱ��")]
        public int ScrollInterval
        {
            get { return scrollInterval; }
            set
            {
                if (value <= 0) return;
                scrollInterval = value;
                this.updateTimer.Interval = scrollInterval;
            }
        }

        private ScrollDirection scrollDirection = ScrollDirection.RightToLeft;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ��������")]
        public ScrollDirection ScrollDirection
        {
            get { return scrollDirection; }
            set
            {
                scrollDirection = value;

                this.Invalidate();
            }
        }


        private TextVerticalAlignment verticalAlignment = TextVerticalAlignment.Center;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ��ֱ������뷽ʽ")]
        public TextVerticalAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set
            {
                verticalAlignment = value;
                this.Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            //��Ҫ��˼·���ǻ�ȡ�����ı���Point,X�����ꡢY������

            SizeF stringSize = graphics.MeasureString(this.textScoll, this.Font);

            //��ȡY������
            CaculateYPos(stringSize);

            if (isScoll)
            {
                //��ȡX������
                CaculateXPos(stringSize);

                graphics.DrawString(this.textScoll, this.Font, new SolidBrush(this.ForeColor), xPos, yPos);

                lastRectangle = new RectangleF(xPos, yPos, stringSize.Width, stringSize.Height);

            }
            else
            {
                graphics.DrawString(this.textScoll, this.Font, new SolidBrush(this.ForeColor), this.ClientSize.Width / 2 - stringSize.Width / 2, yPos);
            }

        }

        private void CaculateYPos(SizeF size)
        {
            switch (this.verticalAlignment)
            {
                case TextVerticalAlignment.Top:
                    yPos = 2;
                    break;
                case TextVerticalAlignment.Center:
                    yPos = this.ClientSize.Height / 2 - size.Height / 2;
                    break;
                case TextVerticalAlignment.Bottom:
                    yPos = this.ClientSize.Height - size.Height;
                    break;
                default:
                    break;
            }

        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void CaculateXPos(SizeF size)
        {
            switch (scrollDirection)
            {
                case ScrollDirection.RightToLeft:

                    if (xPos < size.Width * (-1))
                    {
                        xPos = this.ClientSize.Width - 1;
                    }
                    else
                    {
                        xPos -= scrollDistance;
                    }

                    break;
                case ScrollDirection.LeftToRight:

                    if (xPos > this.ClientSize.Width)
                    {
                        xPos = size.Width * (-1);
                    }
                    else
                    {
                        xPos += scrollDistance;
                    }

                    break;
                default:
                    break;
            }

        }
    }
}
