using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    /// <summary>
    /// xktWave
    /// </summary>
    public partial class xbdWave : xbdControlBase
    {
        /// <summary>
        /// xktWave
        /// </summary>
        public xbdWave()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.waveBase = new xbd.ControlLib.xbdWaveBase();
            this.waveBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.waveBase.WaveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.waveBase.WaveHeight = 15;
            this.waveBase.WaveSleep = 100;
            this.waveBase.WaveWidth = 100;
            this.Controls.Add(this.waveBase);

            base.IsRadius = true;
            base.IsShowRect = false;
            RectWidth = 4;
            RectColor = Color.White;
            ForeColor = Color.White;
            waveBase.Height = (int)((double)m_value / (double)m_maxValue * this.Height) + waveBase.WaveHeight;
            this.SizeChanged += ProcessWave_SizeChanged;
            this.waveBase.OnPainted += Wave1_Painted;
            base.ConerRadius = 10;
        }

        private xbdWaveBase waveBase;

        private bool m_isRectangle = true;
        [Description("设置或获取是否矩形"), Category("自定义属性")]
        public bool IsRectangle
        {
            get { return m_isRectangle; }
            set
            {
                m_isRectangle = value;
                if (value)
                {
                    base.ConerRadius = 10;
                }
                else
                {
                    base.ConerRadius = Math.Min(this.Width, this.Height);
                }
                this.Invalidate();
            }
        }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        [Description("值变更事件"), Category("自定义事件")]
        public event EventHandler ValueChanged;

        private int m_value = 0;
        [Description("设置或获取当前值"), Category("自定义属性")]
        public int Value
        {
            set
            {
                if (value > m_maxValue)
                    m_value = m_maxValue;
                else if (value < 0)
                    m_value = 0;
                else
                    m_value = value;
                if (ValueChanged != null)
                    ValueChanged(this, null);
                waveBase.Height = (int)((double)m_value / (double)m_maxValue * this.Height) + waveBase.WaveHeight;
                Refresh();
            }
            get
            {
                return m_value;
            }
        }

        private int m_maxValue = 100;

        [Description("设置或获取最大值"), Category("自定义属性")]
        public int MaxValue
        {
            get { return m_maxValue; }
            set
            {
                if (value < m_value)
                    m_maxValue = m_value;
                else
                    m_maxValue = value;
                Refresh();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的文字的字体。
        /// </summary>
        [Description("设置或获取显示字体"), Category("自定义属性")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        /// <summary>
        /// 获取或设置控件的前景色。
        /// </summary>
        [Description("设置或获取前景色"), Category("自定义属性")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the value.
        /// </summary>
        /// <value>The color of the value.</value>
        [Description("设置或获取值颜色"), Category("自定义属性")]
        public Color ValueColor
        {
            get { return this.waveBase.WaveColor; }
            set
            {
                this.waveBase.WaveColor = value;
            }
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        [Description("设置或获取边框宽度"), Category("自定义属性")]
        public override int RectWidth
        {
            get
            {
                return base.RectWidth;
            }
            set
            {
                if (value < 4)
                    base.RectWidth = 4;
                else
                    base.RectWidth = value;
            }
        }

        /// <summary>
        /// Handles the Painted event of the waveBase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        void Wave1_Painted(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (IsShowRect)
            {
                if (m_isRectangle)
                {
                    Color rectColor = RectColor;
                    Pen pen = new Pen(rectColor, (float)RectWidth);
                    Rectangle clientRectangle = new Rectangle(0, this.waveBase.Height - this.Height, this.Width, this.Height);
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddArc(clientRectangle.X, clientRectangle.Y, 10, 10, 180f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - 10 - 1, clientRectangle.Y, 10, 10, 270f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - 10 - 1, clientRectangle.Bottom - 10 - 1, 10, 10, 0f, 90f);
                    graphicsPath.AddArc(clientRectangle.X, clientRectangle.Bottom - 10 - 1, 10, 10, 90f, 90f);
                    graphicsPath.CloseFigure();
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
                else
                {
                    SolidBrush solidBrush = new SolidBrush(RectColor);
                    e.Graphics.DrawEllipse(new Pen(solidBrush, RectWidth), new Rectangle(0, this.waveBase.Height - this.Height, this.Width, this.Height));
                }
            }

            if (!m_isRectangle)
            {
                //这里曲线救国，因为设置了控件区域导致的毛边，通过画一个没有毛边的圆遮挡
                SolidBrush solidBrush1 = new SolidBrush(RectColor);
                e.Graphics.DrawEllipse(new Pen(solidBrush1, 2), new Rectangle(-1, this.waveBase.Height - this.Height - 1, this.Width + 2, this.Height + 2));
            }
            string strValue = ((double)m_value / (double)m_maxValue).ToString("0.%");
            SizeF sizeF = e.Graphics.MeasureString(strValue, Font);
            e.Graphics.DrawString(strValue, Font, new SolidBrush(ForeColor), new PointF((this.Width - sizeF.Width) / 2, (this.waveBase.Height - this.Height) + (this.Height - sizeF.Height) / 2));
        }

        /// <summary>
        /// Handles the SizeChanged event of the UCProcessWave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void ProcessWave_SizeChanged(object sender, EventArgs e)
        {
            if (!m_isRectangle)
            {
                base.ConerRadius = Math.Min(this.Width, this.Height);
                if (this.Width != this.Height)
                {
                    this.Size = new Size(Math.Min(this.Width, this.Height), Math.Min(this.Width, this.Height));
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (!m_isRectangle)
            {
                //这里曲线救国，因为设置了控件区域导致的毛边，通过画一个没有毛边的圆遮挡
                SolidBrush solidBrush = new SolidBrush(RectColor);
                e.Graphics.DrawEllipse(new Pen(solidBrush, 2), new Rectangle(-1, -1, this.Width + 2, this.Height + 2));
            }
            string strValue = ((double)m_value / (double)m_maxValue).ToString("0.%");
            System.Drawing.SizeF sizeF = e.Graphics.MeasureString(strValue, Font);
            e.Graphics.DrawString(strValue, Font, new SolidBrush(ForeColor), new PointF((this.Width - sizeF.Width) / 2, (this.Height - sizeF.Height) / 2 + 1));

        }
    }

    /// <summary>
    /// xktWaveBase
    /// </summary>
    public class xbdWaveBase : Control
    {
        /// <summary>
        /// xktWaveBase
        /// </summary>
        public xbdWaveBase()
        {
            this.Size = new Size(600, 100);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            timer.Interval = m_waveSleep;
            timer.Tick += timer_Tick;
            this.VisibleChanged += UCWave_VisibleChanged;
        }

        /// <summary>
        /// Occurs when [on painted].
        /// </summary>
        public event PaintEventHandler OnPainted;

        /// <summary>
        /// The m wave color
        /// </summary>
        private Color m_waveColor = Color.FromArgb(255, 77, 59);

        /// <summary>
        /// Gets or sets the color of the wave.
        /// </summary>
        /// <value>The color of the wave.</value>
        [Description("波纹颜色"), Category("自定义属性")]
        public Color WaveColor
        {
            get { return m_waveColor; }
            set { m_waveColor = value; }
        }

        /// <summary>
        /// The m wave width
        /// </summary>
        private int m_waveWidth = 200;
        /// <summary>
        /// 为方便计算，强制使用10的倍数
        /// </summary>
        /// <value>The width of the wave.</value>
        [Description("波纹宽度（为方便计算，强制使用10的倍数）"), Category("自定义属性")]
        public int WaveWidth
        {
            get { return m_waveWidth; }
            set
            {
                m_waveWidth = value;
                m_waveWidth = m_waveWidth / 10 * 10;
                intLeftX = value * -1;
            }
        }

        /// <summary>
        /// The m wave height
        /// </summary>
        private int m_waveHeight = 30;
        /// <summary>
        /// 波高
        /// </summary>
        /// <value>The height of the wave.</value>
        [Description("波高"), Category("自定义属性")]
        public int WaveHeight
        {
            get { return m_waveHeight; }
            set { m_waveHeight = value; }
        }

        /// <summary>
        /// The m wave sleep
        /// </summary>
        private int m_waveSleep = 50;
        /// <summary>
        /// 波运行速度（运行时间间隔，毫秒）
        /// </summary>
        /// <value>The wave sleep.</value>
        [Description("波运行速度（运行时间间隔，毫秒）"), Category("自定义属性")]
        public int WaveSleep
        {
            get { return m_waveSleep; }
            set
            {
                if (value <= 0)
                    return;
                m_waveSleep = value;
                if (timer != null)
                {
                    timer.Enabled = false;
                    timer.Interval = value;
                    timer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// The timer
        /// </summary>
        Timer timer = new Timer();
        /// <summary>
        /// The int left x
        /// </summary>
        int intLeftX = -200;

        /// <summary>
        /// Handles the VisibleChanged event of the UCWave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void UCWave_VisibleChanged(object sender, EventArgs e)
        {
            timer.Enabled = this.Visible;
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
        {
            intLeftX -= 10;
            if (intLeftX == m_waveWidth * -2)
                intLeftX = m_waveWidth * -1;
            this.Refresh();
        }
        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            List<Point> lst1 = new List<Point>();
            List<Point> lst2 = new List<Point>();
            int m_intX = intLeftX;
            while (true)
            {
                lst1.Add(new Point(m_intX, 1));
                lst1.Add(new Point(m_intX + m_waveWidth / 2, m_waveHeight));

                lst2.Add(new Point(m_intX + m_waveWidth / 2, 1));
                lst2.Add(new Point(m_intX + m_waveWidth / 2 + m_waveWidth / 2, m_waveHeight));
                m_intX += m_waveWidth;
                if (m_intX > this.Width + m_waveWidth)
                    break;
            }

            GraphicsPath path1 = new GraphicsPath();
            path1.AddCurve(lst1.ToArray(), 0.5F);
            path1.AddLine(this.Width + 1, -1, this.Width + 1, this.Height);
            path1.AddLine(this.Width + 1, this.Height, -1, this.Height);
            path1.AddLine(-1, this.Height, -1, -1);

            GraphicsPath path2 = new GraphicsPath();
            path2.AddCurve(lst2.ToArray(), 0.5F);
            path2.AddLine(this.Width + 1, -1, this.Width + 1, this.Height);
            path2.AddLine(this.Width + 1, this.Height, -1, this.Height);
            path2.AddLine(-1, this.Height, -1, -1);

            g.FillPath(new SolidBrush(Color.FromArgb(220, m_waveColor.R, m_waveColor.G, m_waveColor.B)), path1);
            g.FillPath(new SolidBrush(Color.FromArgb(220, m_waveColor.R, m_waveColor.G, m_waveColor.B)), path2);

            if (OnPainted != null)
            {
                OnPainted(this, e);
            }
        }
    }
}
