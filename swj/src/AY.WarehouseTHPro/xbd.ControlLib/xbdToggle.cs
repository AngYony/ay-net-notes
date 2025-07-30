using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace xbd.ControlLib
{
    /// <summary>
    /// xktToggleAdvance
    /// </summary>
    [DefaultEvent("CheckedChanged")]
    public partial class xbdToggle : UserControl
    {
        /// <summary>
        /// 选中改变事件
        /// </summary>
        [Description("选中改变事件"), Category("自定义属性")]
        public event EventHandler CheckedChanged;
        private Color trueColor = Color.LimeGreen;

        /// <summary>
        /// 选中时颜色
        /// </summary>
        [Description("选中时颜色"), Category("自定义属性")]
        public Color TrueColor
        {
            get { return trueColor; }
            set
            {
                trueColor = value;
                Refresh();
            }
        }

        private Color falseColor = Color.FromArgb(255, 128, 0);

        /// <summary>
        /// 没有选中时颜色
        /// </summary>
        [Description("没有选中时颜色"), Category("自定义属性")]
        public Color FalseColor
        {
            get { return falseColor; }
            set
            {
                falseColor = value;
                Refresh();
            }
        }

        private bool m_checked;

        /// <summary>
        /// 是否选中
        /// </summary>
        [Description("是否选中"), Category("自定义属性")]
        public bool Checked
        {
            get { return m_checked; }
            set
            {
                if (m_checked != value)
                {
                    m_checked = value;
                    Refresh();
                }
            }
        }

        private string[] m_texts;

        /// <summary>
        /// Texts
        /// </summary>
        [Description("文本值，当选中或没有选中时显示，必须是长度为2的数组"), Category("自定义属性")]
        public string[] Texts
        {
            get { return m_texts; }
            set
            {
                m_texts = value;
                Refresh();
            }
        }
        private SwitchType m_switchType = SwitchType.Quadrilateral;

        /// <summary>
        /// SwitchType
        /// </summary>
        [Description("显示类型"), Category("自定义属性")]
        public SwitchType SwitchType
        {
            get { return m_switchType; }
            set
            {
                m_switchType = value;
                Refresh();
            }
        }

        /// <summary>
        /// Font字体
        /// </summary>
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                Refresh();
            }
        }

        /// <summary>
        /// xktToggleAdvance
        /// </summary>
        public xbdToggle()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MouseDown += UCSwitch_MouseDown;
        }

        void UCSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            Checked = !Checked;
            CheckedChanged?.Invoke(this, null);
        }

        /// <summary>
        /// OnPaint事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            if (m_switchType == SwitchType.Ellipse)
            {
                var fillColor = m_checked ? trueColor : falseColor;
                GraphicsPath path = new GraphicsPath();
                path.AddLine(new Point(this.Height / 2, 1), new Point(this.Width - this.Height / 2, 1));
                path.AddArc(new Rectangle(this.Width - this.Height - 1, 1, this.Height - 2, this.Height - 2), -90, 180);
                path.AddLine(new Point(this.Width - this.Height / 2, this.Height - 1), new Point(this.Height / 2, this.Height - 1));
                path.AddArc(new Rectangle(1, 1, this.Height - 2, this.Height - 2), 90, 180);
                g.FillPath(new SolidBrush(fillColor), path);

                string strText = string.Empty;
                if (m_texts != null && m_texts.Length == 2)
                {
                    if (m_checked)
                    {
                        strText = m_texts[0];
                    }
                    else
                    {
                        strText = m_texts[1];
                    }
                }

                if (m_checked)
                {
                    g.FillEllipse(Brushes.White, new Rectangle(this.Width - this.Height - 1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2), new Rectangle((this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                    }
                    else
                    {
                        System.Drawing.SizeF sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point((this.Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    g.FillEllipse(Brushes.White, new Rectangle(1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2), new Rectangle(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                    }
                    else
                    {
                        System.Drawing.SizeF sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - (int)sizeF.Width / 2, intTextY));
                    }
                }
            }
            else if (m_switchType == SwitchType.Quadrilateral)
            {
                var fillColor = m_checked ? trueColor : falseColor;
                GraphicsPath path = new GraphicsPath();
                int intRadius = 5;
                path.AddArc(0, 0, intRadius, intRadius, 180f, 90f);
                path.AddArc(this.Width - intRadius - 1, 0, intRadius, intRadius, 270f, 90f);
                path.AddArc(this.Width - intRadius - 1, this.Height - intRadius - 1, intRadius, intRadius, 0f, 90f);
                path.AddArc(0, this.Height - intRadius - 1, intRadius, intRadius, 90f, 90f);

                g.FillPath(new SolidBrush(fillColor), path);

                string strText = string.Empty;
                if (m_texts != null && m_texts.Length == 2)
                {
                    if (m_checked)
                    {
                        strText = m_texts[0];
                    }
                    else
                    {
                        strText = m_texts[1];
                    }
                }

                if (m_checked)
                {
                    GraphicsPath path2 = new GraphicsPath();
                    path2.AddArc(this.Width - this.Height - 1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(this.Width - 1 - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(this.Width - 1 - 2 - intRadius, this.Height - 2 - intRadius - 1, intRadius, intRadius, 0f, 90f);
                    path2.AddArc(this.Width - this.Height - 1 + 2, this.Height - 2 - intRadius - 1, intRadius, intRadius, 90f, 90f);
                    g.FillPath(Brushes.White, path2);

                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2), new Rectangle((this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                    }
                    else
                    {
                        System.Drawing.SizeF sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point((this.Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    GraphicsPath path2 = new GraphicsPath();
                    path2.AddArc(1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(this.Height - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(this.Height - 2 - intRadius, this.Height - 2 - intRadius - 1, intRadius, intRadius, 0f, 90f);
                    path2.AddArc(1 + 2, this.Height - 2 - intRadius - 1, intRadius, intRadius, 90f, 90f);
                    g.FillPath(Brushes.White, path2);

                    //g.FillEllipse(Brushes.White, new Rectangle(1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2), new Rectangle(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                    }
                    else
                    {
                        System.Drawing.SizeF sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - (int)sizeF.Width / 2, intTextY));
                    }
                }
            }
            else
            {
                var fillColor = m_checked ? trueColor : falseColor;
                int intLineHeight = (this.Height - 2 - 4) / 2;

                GraphicsPath path = new GraphicsPath();
                path.AddLine(new Point(this.Height / 2, (this.Height - intLineHeight) / 2), new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2));
                path.AddArc(new Rectangle(this.Width - this.Height / 2 - intLineHeight - 1, (this.Height - intLineHeight) / 2, intLineHeight, intLineHeight), -90, 180);
                path.AddLine(new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2 + intLineHeight), new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2 + intLineHeight));
                path.AddArc(new Rectangle(this.Height / 2, (this.Height - intLineHeight) / 2, intLineHeight, intLineHeight), 90, 180);
                g.FillPath(new SolidBrush(fillColor), path);

                if (m_checked)
                {
                    g.FillEllipse(new SolidBrush(fillColor), new Rectangle(this.Width - this.Height - 1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    g.FillEllipse(Brushes.White, new Rectangle(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - 4, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                }
                else
                {
                    g.FillEllipse(new SolidBrush(fillColor), new Rectangle(1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    g.FillEllipse(Brushes.White, new Rectangle((this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 + 4, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                }
            }
        }

    }

    /// <summary>
    /// SwitchType
    /// </summary>
    public enum SwitchType
    {
        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,
        /// <summary>
        /// 四边形
        /// </summary>
        Quadrilateral,
        /// <summary>
        /// 横线
        /// </summary>
        Line
    }
}
