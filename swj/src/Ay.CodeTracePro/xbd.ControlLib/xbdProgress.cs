//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    /// <summary>
    /// ProgressStyle
    /// </summary>
    public enum ProgressStyle
    {
        /// <summary>
        /// Horizontal
        /// </summary>
        Horizontal = 1,
        /// <summary>
        /// Vertical
        /// </summary>
        Vertical,
        /// <summary>
        /// Circular
        /// </summary>
        Circular
    }

    /// <summary>
    /// xktProgressRectangle
    /// </summary>
    public partial class xbdProgress : UserControl
    {
        private StringFormat sf = null;

        private Brush backBrush = new SolidBrush(Color.DimGray);

        private Color progressColor = Color.Tomato;

        private Brush progressBrush = new SolidBrush(Color.Tomato);

        private Color borderColor = Color.DimGray;

        private Pen borderPen = new Pen(Color.DimGray, 1f);

        private int max = 100;

        private int m_value = 50;

        private int m_actual = 50;

        private int m_speed = 1;

        private bool useAnimation = false;

        private int m_version = 0;

        private ProgressStyle m_progressStyle = ProgressStyle.Vertical;

        private bool isTextRender = true;

        private Action m_UpdateAction;

        /// <summary>
        /// ��ȡ�����ý������ı���ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"),Description("��ȡ�����ý������ı���ɫ")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                Brush brush = this.backBrush;
                if (brush != null)
                {
                    brush.Dispose();
                }
                this.backBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ý�������ǰ��ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"),  Description("��ȡ�����ý�������ǰ��ɫ")]
        public Color ProgressColor
        {
            get
            {
                return this.progressColor;
            }
            set
            {
                this.progressColor = value;
                Brush expr_0E = this.progressBrush;
                if (expr_0E != null)
                {
                    expr_0E.Dispose();
                }
                this.progressBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ý����������ֵ
        /// </summary>
        [Browsable(true), Category("�Զ�������"), Description("��ȡ�����ý����������ֵ��Ĭ��Ϊ100")]
        public int Max
        {
            get
            {
                return this.max;
            }
            set
            {
                bool flag = value > 1;
                if (flag)
                {
                    this.max = value;
                }
                bool flag2 = this.m_value > this.max;
                if (flag2)
                {
                    this.m_value = this.max;
                }
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ��������ֵ
        /// </summary>
        [Browsable(true), Category("�Զ�������"), Description("��ȡ�����õ�ǰ��������ֵ")]
        public int Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                bool flag = value >= 0 && value <= this.max;
                if (flag)
                {
                    bool flag2 = value != this.m_value;
                    if (flag2)
                    {
                        this.m_value = value;
                        bool flag3 = this.UseAnimation;
                        if (flag3)
                        {
                            int num = Interlocked.Increment(ref this.m_version);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadPoolUpdateProgress), num);
                        }
                        else
                        {
                            this.m_actual = value;
                            base.Invalidate();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�������Ƿ���ʾ�����ı�
        /// </summary>
        [Browsable(true), Category("�Զ�������"), Description("��ȡ�������Ƿ���ʾ�����ı�")]
        public bool IsTextRender
        {
            get
            {
                return this.isTextRender;
            }
            set
            {
                this.isTextRender = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ý������ı߿���ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"),Description("��ȡ�����ý������ı߿���ɫ")]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                Pen expr_0E = this.borderPen;
                if (expr_0E != null)
                {
                    expr_0E.Dispose();
                }
                this.borderPen = new Pen(value);
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ý������ı仯����
        /// </summary>
        [Browsable(true), Category("�Զ�������"),  Description("��ȡ�����ý������ı仯����")]
        public int ValueChangeSpeed
        {
            get
            {
                return this.m_speed;
            }
            set
            {
                bool flag = value >= 1;
                if (flag)
                {
                    this.m_speed = value;
                }
            }
        }

        /// <summary>
        /// �Ƿ���ö���Ч��
        /// </summary>
        [Browsable(true), Category("�Զ�������"),  Description("��ȡ�����ý������仯��ʱ���Ƿ���ö���Ч��")]
        public bool UseAnimation
        {
            get
            {
                return this.useAnimation;
            }
            set
            {
                this.useAnimation = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ý���������ʽ
        /// </summary>
        [Browsable(true), Category("�Զ�������"),  Description("��ȡ�����ý���������ʽ")]
        public ProgressStyle ProgressStyle
        {
            get
            {
                return this.m_progressStyle;
            }
            set
            {
                this.m_progressStyle = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// xktProgressRectangle
        /// </summary>
        public xbdProgress()
        {
            this.InitializeComponent();
            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.m_UpdateAction = new Action(this.UpdateRender);
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
                try
                {
                    Graphics graphics = e.Graphics;
                    Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
                    graphics.FillRectangle(this.backBrush, rectangle);
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    switch (this.m_progressStyle)
                    {
                        case ProgressStyle.Horizontal:
                            {
                                int num = (int)((long)this.m_actual * (long)(base.Width - 2) / (long)this.max);
                                rectangle = new Rectangle(0, 0, num + 1, base.Height - 1);
                                graphics.FillRectangle(this.progressBrush, rectangle);
                                break;
                            }
                        case ProgressStyle.Vertical:
                            {
                                int num2 = (int)((long)this.m_actual * (long)(base.Height - 2) / (long)this.max);
                                rectangle = new Rectangle(0, base.Height - 1 - num2, base.Width - 1, num2);
                                graphics.FillRectangle(this.progressBrush, rectangle);
                                break;
                            }
                    }
                    rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
                    bool flag2 = this.isTextRender;
                    if (flag2)
                    {
                        string s = (long)this.m_actual * 100L / (long)this.max + "%";
                        using (Brush brush = new SolidBrush(this.ForeColor))
                        {
                            bool flag3 = this.m_progressStyle != ProgressStyle.Circular;
                            if (flag3)
                            {
                                graphics.DrawString(s, this.Font, brush, rectangle, this.sf);
                            }
                            else
                            {
                                graphics.DrawString("Not supported", this.Font, brush, rectangle, this.sf);
                            }
                        }
                    }
                    bool flag4 = this.m_progressStyle != ProgressStyle.Circular;
                    if (flag4)
                    {
                        graphics.DrawRectangle(this.borderPen, rectangle);
                    }
                }
                catch (Exception)
                {
                }
                base.OnPaint(e);
            
        }

        private void ThreadPoolUpdateProgress(object obj)
        {
            try
            {
                int num = (int)obj;
                bool flag = this.m_speed < 1;
                if (flag)
                {
                    this.m_speed = 1;
                }
                while (this.m_actual != this.m_value)
                {
                    Thread.Sleep(17);
                    bool flag2 = num != this.m_version;
                    if (flag2)
                    {
                        break;
                    }
                    bool flag3 = this.m_actual > this.m_value;
                    int actual;
                    if (flag3)
                    {
                        int num2 = this.m_actual - this.m_value;
                        bool flag4 = num2 > this.m_speed;
                        if (flag4)
                        {
                            num2 = this.m_speed;
                        }
                        actual = this.m_actual - num2;
                    }
                    else
                    {
                        int num3 = this.m_value - this.m_actual;
                        bool flag5 = num3 > this.m_speed;
                        if (flag5)
                        {
                            num3 = this.m_speed;
                        }
                        actual = this.m_actual + num3;
                    }
                    this.m_actual = actual;
                    bool flag6 = num == this.m_version;
                    if (!flag6)
                    {
                        break;
                    }
                    bool isHandleCreated = base.IsHandleCreated;
                    if (isHandleCreated)
                    {
                        base.Invoke(this.m_UpdateAction);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateRender()
        {
            base.Invalidate();
        }

    }
}
