using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace xbd.ControlLib
{
    /// <summary>
    /// xktLED
    /// </summary>
    public partial class xbdLed : UserControl
    {
        /// <summary>
        /// xktLED
        /// </summary>
        public xbdLed()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += timer_Tick;
        }


        private Timer timer;

        private int ledState = 0;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取指示灯颜色状态")]
        public int LedState
        {
            get { return ledState; }
            set
            {
                if (ledState != value)
                {
                    ledState = value;
                    this.Invalidate();
                }
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            ledState++;
            if (ledState >= colorList.Length)
                ledState = 0;
            Invalidate();
        }


        private Color[] colorList = new Color[] { Color.FromArgb(255, 77, 59), Color.FromArgb(0, 192, 0) };
        /// <summary>
        /// 设定或获取指示灯颜色集合
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取指示灯颜色集合")]
        public Color[] ColorList
        {
            get { return colorList; }
            set
            {
                if (value == null || value.Length <= 0)
                    return;
                colorList = value;
                Invalidate();
            }
        }

        private Color centerColor = Color.White;
        /// <summary>
        /// 设定或获取指示灯中心颜色
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取指示灯中心颜色")]
        public Color CenterColor
        {
            get { return centerColor; }
            set
            {
                centerColor = value;
                this.Invalidate();
            }
        }

        private bool isBorder = true;
        /// <summary>
        /// 设定或获取是否有边框
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取是否有边框")]
        public bool IsBorder
        {
            get { return isBorder; }
            set
            {
                isBorder = value;
                this.Invalidate();
            }
        }

        private int gapWidth = 5;

        /// <summary>
        /// 设定或获取外环间隙
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取外环间隙")]
        public int GapWidth
        {
            get { return gapWidth; }
            set
            {
                gapWidth = value;
                this.Invalidate();
            }
        }

        private int borderWidth = 5;

        /// <summary>
        /// 设定或获取外环宽度
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取外环宽度")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                this.Invalidate();
            }
        }

        private bool isHighLight = true;
        /// <summary>
        /// 设定或获取是否高亮显示
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取是否高亮显示")]
        public bool IsHighLight
        {
            get { return isHighLight; }
            set
            {
                isHighLight = value;
                this.Invalidate();
            }
        }

        private bool isBlink = false;

        /// <summary>
        /// 设定或获取是否闪烁
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取是否闪烁")]
        public bool IsBlink
        {
            get { return isBlink; }
            set
            {
                if (this.twinkleSpeed <= 0 || colorList.Length <= 1)
                {
                    return;
                }
                ledState = 0;
                timer.Enabled = value;
                isBlink = value;
                Invalidate();
            }
        }

        private int twinkleSpeed = 200;
        /// <summary>
        /// 设定或获取闪烁间隔
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设定或获取闪烁间隔")]
        public int TwinkleSpeed
        {
            get { return twinkleSpeed; }
            set
            {
                if (value <= 0)
                    return;
                twinkleSpeed = value;
                timer.Interval = value;
                Invalidate();
            }
        }

        private Graphics graphics;

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //绘制
            graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            PointF center = new PointF(Math.Min(this.Width, this.Height), Math.Min(this.Width, this.Height));
            SolidBrush sb = new SolidBrush(this.colorList[ledState]);
            RectangleF rec = new RectangleF(1, 1, center.X - 2, center.Y - 2);
            graphics.FillEllipse(sb, rec);
            //画个圆环
            if (isBorder)
            {
                Pen p = new Pen(this.BackColor, gapWidth);
                float f = 1 + gapWidth * 0.5f + borderWidth;
                rec = new RectangleF(f, f, center.X - 2 * f, center.Y - 2 * f);
                graphics.DrawEllipse(p, rec);
            }
            if (isHighLight)
            {
                if (isBorder)
                {
                    //画布路径
                    GraphicsPath gp = new GraphicsPath();
                    float f = 1 + borderWidth + gapWidth;
                    rec = new RectangleF(f, f, center.X - 2 * f, center.Y - 2 * f);
                    gp.AddEllipse(rec);
                    //渐变画刷
                    PathGradientBrush pb = new PathGradientBrush(gp);
                    pb.CenterColor = centerColor;
                    pb.SurroundColors = new Color[] { this.colorList[ledState] };
                    graphics.FillPath(pb, gp);
                }
                else
                {
                    //画布路径
                    GraphicsPath gp = new GraphicsPath();
                    rec = new RectangleF(1, 1, center.X - 2, center.Y - 2);
                    gp.AddEllipse(rec);
                    //渐变画刷
                    PathGradientBrush pb = new PathGradientBrush(gp);
                    pb.CenterColor = centerColor;
                    pb.SurroundColors = new Color[] { this.colorList[ledState] };
                    graphics.FillPath(pb, gp);
                }
            }
        }

    }
}
