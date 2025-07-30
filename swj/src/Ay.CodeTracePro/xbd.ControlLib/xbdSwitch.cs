//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    [DefaultEvent("OnSwitchChanged")]


    public partial class xbdSwitch : UserControl
    {
        private Color textForeColor = Color.Black;

        private Brush textForeBrush = new SolidBrush(Color.Black);

        private Color color_switch_background = Color.DimGray;

        private Brush brush_switch_background = new SolidBrush(Color.DimGray);

        private Pen pen_switch_background = new Pen(Color.DimGray, 2f);

        private bool switch_status = false;

        private Color color_switch_foreground = Color.FromArgb(36, 36, 36);

        private Brush brush_switch_foreground = new SolidBrush(Color.FromArgb(36, 36, 36));

        private StringFormat sf = null;

        private string description = "Off;On";

        /// <summary>
        /// ����˰�ť�����󴥷�
        /// </summary>
        [Category("Action"), Description("����˰�ť�����󴥷�")]
        [method: CompilerGenerated]
        public event Action<object, bool> OnSwitchChanged;

        /// <summary>
        /// ��ȡ�����ÿؼ��ı���ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue(typeof(Color), "Transparent"), Description("��ȡ�����ÿؼ��ı���ɫ"), EditorBrowsable(EditorBrowsableState.Always)]
        
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ÿ��ذ�ť�ı���ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue(typeof(Color), "DimGray"), Description("��ȡ�����ÿ��ذ�ť�ı���ɫ")]
        public Color SwitchBackground
        {
            get
            {
                return this.color_switch_background;
            }
            set
            {
                this.color_switch_background = value;
                Brush expr_0E = this.brush_switch_background;
                if (expr_0E != null)
                {
                    expr_0E.Dispose();
                }
                Pen expr_20 = this.pen_switch_background;
                if (expr_20 != null)
                {
                    expr_20.Dispose();
                }
                this.brush_switch_background = new SolidBrush(this.color_switch_background);
                this.pen_switch_background = new Pen(this.color_switch_background, 2f);
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ÿ��ذ�ť��ǰ��ɫ
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue(typeof(Color), "[36, 36, 36]"), Description("��ȡ�����ÿ��ذ�ť��ǰ��ɫ")]
        public Color SwitchForeground
        {
            get
            {
                return this.color_switch_foreground;
            }
            set
            {
                this.color_switch_foreground = value;
                this.brush_switch_foreground = new SolidBrush(this.color_switch_foreground);
                base.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ÿ��ذ�ť�Ŀ���״̬
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue(false), Description("��ȡ�����ÿ��ذ�ť�Ŀ���״̬")]
        public bool SwitchStatus
        {
            get
            {
                return this.switch_status;
            }
            set
            {
                bool flag = value != this.switch_status;
                if (flag)
                {
                    this.switch_status = value;
                    base.Invalidate();
                    Action<object, bool> expr_26 = this.OnSwitchChanged;
                    if (expr_26 != null)
                    {
                        expr_26(this, this.switch_status);
                    }
                }
            }
        }

        /// <summary>
        /// SwitchStatusDescription
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue("Off;On"), Description("��ȡ���������ֿ���״̬���ı����������磺Off;On")]
        public string SwitchStatusDescription
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Text
        /// </summary>
        [Bindable(true), Browsable(true), Category("�Զ�������"), Description("��ȡ�����õ�ǰ�ؼ����ı�"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// ForeColor
        /// </summary>
        [Browsable(true), Category("�Զ�������"), DefaultValue(typeof(Color), "Black"), Description("��ȡ�����õ�ǰ�ؼ����ı�����ɫ"), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get
            {
                return this.textForeColor;
            }
            set
            {
                this.textForeColor = value;
                this.textForeBrush.Dispose();
                this.textForeBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        /// <summary>
        /// xktSwitch
        /// </summary>
        public xbdSwitch()
        {
            this.InitializeComponent();
            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private Point GetCenterPoint()
        {
            bool flag = base.Height > base.Width;
            Point result;
            if (flag)
            {
                result = new Point(base.Width / 2, base.Width / 2);
            }
            else
            {
                result = new Point(base.Height / 2, base.Height / 2);
            }
            return result;
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
                string[] array = this.description.Split(new char[]
                {
                    ';'
                }, StringSplitOptions.RemoveEmptyEntries);
                string text = string.Empty;
                string text2 = string.Empty;
                bool flag2 = array.Length != 0;
                if (flag2)
                {
                    text = array[0];
                }
                bool flag3 = array.Length > 1;
                if (flag3)
                {
                    text2 = array[1];
                }
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Point centerPoint = this.GetCenterPoint();
                e.Graphics.TranslateTransform((float)centerPoint.X, (float)centerPoint.Y);
                int num = 45 * (centerPoint.X * 2 - 30) / 130;
                bool flag4 = num < 5;
                if (!flag4)
                {
                    Rectangle rect = new Rectangle(-num - 4, -num - 4, 2 * num + 8, 2 * num + 8);
                    Rectangle rect2 = new Rectangle(-num, -num, 2 * num, 2 * num);
                    e.Graphics.DrawEllipse(this.pen_switch_background, rect);
                    e.Graphics.FillEllipse(this.brush_switch_background, rect2);
                    float angle = -36f;
                    bool flag5 = this.switch_status;
                    if (flag5)
                    {
                        angle = 36f;
                    }
                    e.Graphics.RotateTransform(angle);
                    int num2 = 20 * (centerPoint.X * 2 - 30) / 130;
                    Rectangle rect3 = new Rectangle(-centerPoint.X / 8, -num - num2, centerPoint.X / 4, num * 2 + num2 * 2);
                    e.Graphics.FillRectangle(this.brush_switch_foreground, rect3);
                    Rectangle rect4 = new Rectangle(-centerPoint.X / 16, -num - 10, centerPoint.X / 8, centerPoint.X * 3 / 8);
                    e.Graphics.FillEllipse(this.switch_status ? Brushes.LimeGreen : Brushes.Tomato, rect4);
                    Rectangle r = new Rectangle(-50, -num - num2 - 15, 100, 15);
                    e.Graphics.DrawString(this.switch_status ? text2 : text, this.Font, this.switch_status ? Brushes.LimeGreen : Brushes.Tomato, r, this.sf);
                    e.Graphics.ResetTransform();
                    bool flag6 = base.Height - Math.Min(base.Width, base.Height) > 0;
                    if (flag6)
                    {
                        bool flag7 = !string.IsNullOrEmpty(this.Text);
                        if (flag7)
                        {
                            e.Graphics.DrawString(this.Text, this.Font, this.textForeBrush, new Rectangle(0, Math.Min(base.Width, base.Height) - 15, Math.Min(base.Width, base.Height), base.Height - Math.Min(base.Width, base.Height) + 15), this.sf);
                        }
                    }
                    base.OnPaint(e);
                }
        }

        /// <summary>
        /// OnMouseClick
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            bool flag = e.Button == MouseButtons.Left;
            if (flag)
            {
                this.SwitchStatus = !this.SwitchStatus;
            }
            base.OnClick(e);
        }

    }
}
