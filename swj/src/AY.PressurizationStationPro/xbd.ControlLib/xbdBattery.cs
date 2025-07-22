using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Drawing.Text;

namespace xbd.ControlLib
{
    /// <summary>
    /// xktSingleBattery
    /// </summary>
    public partial class xbdBattery : UserControl
    {
        /// <summary>
        /// xktSingleBattery
        /// </summary>
        public xbdBattery()
        {
            InitializeComponent();
            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.hybirdLock = new object();
            this.m_UpdateAction = new Action(base.Invalidate);
        }

        private StringFormat sf = null;

        private Color batteryBackEdgeColor = Color.FromArgb(142, 196, 216);

        private Brush batteryBackBrush = new SolidBrush(Color.FromArgb(142, 196, 216));

        private Pen batteryBackPen = new Pen(Color.FromArgb(142, 196, 216));

        private DirectionStyle valvesStyle = DirectionStyle.Horizontal;

        private Color color_1 = Color.Green;

        private Color color_2 = Color.LimeGreen;

        private Color color_3 = Color.Orange;

        private Color color_4 = Color.Tomato;

        private Color color_5 = Color.Red;

        private float value_max = 100f;

        private float value_min = 0f;

        private float value_now = 0f;

        private float value_paint = 0f;

        private float value_color1 = 0.85f;

        private float value_color2 = 0.6f;

        private float value_color3 = 0.4f;

        private float value_color4 = 0.15f;

        private bool isTextRender = false;

        private bool isUseAnimation = true;

        private Action m_UpdateAction;

        private int m_version = 0;

        private object hybirdLock;

        private string batteryNum = "1";

        /// <summary>
        /// 获取或设置控件的背景色
        /// </summary>
        [Browsable(true), Category("自定义属性"),  Description("获取或设置控件的背景色")]
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
        /// 获取或设置电池的背景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的背景颜色")]
        public Color BatteryBackColor
        {
            get
            {
                return this.batteryBackEdgeColor;
            }
            set
            {
                this.batteryBackEdgeColor = value;
                this.batteryBackBrush.Dispose();
                this.batteryBackBrush = new SolidBrush(value);
                this.batteryBackPen.Dispose();
                this.batteryBackPen = new Pen(value);
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前的数值
        /// </summary>
        [Browsable(true), Category("自定义属性"),Description("获取或设置当前的数值")]
        public float Value
        {
            get
            {
                return this.value_now;
            }
            set
            {
                bool flag = value != this.value_now;
                if (flag)
                {
                    this.value_now = value;
                    if (this.isUseAnimation)
                    {
                        int num = Interlocked.Increment(ref this.m_version);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadPoolUpdateProgress), num);
                    }
                    else
                    {
                        this.value_paint = value;
                        base.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置当前数值的最大值
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置当前数值的最大值")]
        public float ValueMax
        {
            get
            {
                return this.value_max;
            }
            set
            {
                this.value_max = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前数值的最小值
        /// </summary>
        [Browsable(true), Category("自定义属性"),Description("获取或设置当前数值的最小值")]
        public float ValueMin
        {
            get
            {
                return this.value_min;
            }
            set
            {
                this.value_min = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池控件是否是横向的还是纵向的
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池控件是否是横向的还是纵向的")]
        public DirectionStyle BatteryStyle
        {
            get
            {
                return this.valvesStyle;
            }
            set
            {
                this.valvesStyle = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置是否显示文本提示信息的文本
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置是否显示文本提示信息的文本")]
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
        /// 获取或设置是否显示文本提示信息的文本
        /// </summary>
        [Browsable(true), Category("自定义属性"),Description("获取或设置是否显示文本提示信息的文本")]
        public string BatteryNum
        {
            get
            {
                return this.batteryNum;
            }
            set
            {
                this.batteryNum = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第一区间的前景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"),Description("获取或设置电池的第一区间的前景颜色")]
        public Color Color1
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第二区间的前景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的第二区间的前景颜色")]
        public Color Color2
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第三区间的前景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的第三区间的前景颜色")]
        public Color Color3
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第四区间的前景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的第四区间的前景颜色")]
        public Color Color4
        {
            get
            {
                return this.color_4;
            }
            set
            {
                this.color_4 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第五区间的前景颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的第五区间的前景颜色")]
        public Color Color5
        {
            get
            {
                return this.color_5;
            }
            set
            {
                this.color_5 = value;
                base.Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置电池的第一区间和第二区间的分割值的百分比
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置电池的第一区间和第二区间的分割值的百分比，默认0.85")]
        public float Separatrix1
        {
            get
            {
                return this.value_color1;
            }
            set
            {
                this.value_color1 = value;
                base.Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置电池的第二区间和第三区间的分割值的百分比
        /// </summary>
        [Browsable(true), Category("自定义属性"),  Description("获取或设置电池的第二区间和第三区间的分割值的百分比，默认0.60")]
        public float Separatrix2
        {
            get
            {
                return this.value_color2;
            }
            set
            {
                this.value_color2 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第三区间和第四区间的分割值的百分比
        /// </summary>
        [Browsable(true), Category("自定义属性"),  Description("获取或设置电池的第三区间和第四区间的分割值的百分比，默认0.40")]
        public float Separatrix3
        {
            get
            {
                return this.value_color3;
            }
            set
            {
                this.value_color3 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置电池的第四区间和第五区间的分割值的百分比
        /// </summary>
        [Browsable(true), Category("自定义属性"),  Description("获取或设置电池的第四区间和第五区间的分割值的百分比，默认0.15")]
        public float Separatrix4
        {
            get
            {
                return this.value_color4;
            }
            set
            {
                this.value_color4 = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前的控件是否启用动画信息
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("获取或设置当前的控件是否启用动画信息")]
        public bool UseAnimation
        {
            get
            {
                return this.isUseAnimation;
            }
            set
            {
                this.isUseAnimation = value;
            }
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            RectangleF layoutRectangle = default(RectangleF);
            bool flag2 = this.valvesStyle == DirectionStyle.Horizontal;
            if (flag2)
            {
                layoutRectangle = new RectangleF(0f, (float)base.Height * 0.1f, (float)base.Width, (float)base.Height * 0.9f);
                this.PaintMain(graphics, (float)base.Width, (float)base.Height);
            }
            else
            {
                layoutRectangle = new RectangleF(0f, 0f, (float)base.Width * 0.9f, (float)base.Height);
                graphics.TranslateTransform((float)base.Width, 0f);
                graphics.RotateTransform(90f);
                this.PaintMain(graphics, (float)base.Height, (float)base.Width);
                graphics.ResetTransform();
            }
            bool flag3 = this.isTextRender;
            if (flag3)
            {
                using (Brush brush = new SolidBrush(this.ForeColor))
                {
                    graphics.DrawString(this.batteryNum, this.Font, brush, layoutRectangle, this.sf);
                }
            }
        }

        /// <summary>
        /// PaintMain
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void PaintMain(Graphics g, float width, float height)
        {
            g.TranslateTransform(width / 2f, 0f);
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Positions = new float[]
            {
                0f,
                0.75f,
                1f
            };
            colorBlend.Colors = new Color[]
            {
                this.batteryBackEdgeColor,
                  this.batteryBackEdgeColor,
              //  Color.WhiteSmoke,
                this.batteryBackEdgeColor
            };
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF(-width / 2f, 0f), new PointF(width / 2f, 0f), Color.FromArgb(247, 252, 253), Color.WhiteSmoke);
            linearGradientBrush.InterpolationColors = colorBlend;
            g.FillEllipse(linearGradientBrush, -width * 0.5f - 1f, height * 0.93f, width + 1f, height * 0.06f);
            g.DrawEllipse(this.batteryBackPen, -width * 0.5f - 1f, height * 0.93f, width + 1f, height * 0.06f);
            g.FillRectangle(linearGradientBrush, -width * 0.5f - 1f, height * 0.1f, width + 1f, height * 0.86f);
            g.FillEllipse(this.batteryBackBrush, -width * 0.5f - 1f, height * 0.07f, width + 1f, height * 0.06f);
            linearGradientBrush.Dispose();
            linearGradientBrush = new LinearGradientBrush(new PointF(-width * 0.15f, 0f), new PointF(width * 0.15f, 0f), Color.FromArgb(247, 252, 253), Color.WhiteSmoke);
            linearGradientBrush.InterpolationColors = colorBlend;
            g.FillEllipse(linearGradientBrush, -width * 0.15f, height * 0.085f, width * 0.3f, height * 0.02f);
            g.FillRectangle(linearGradientBrush, -width * 0.15f, height * 0.03f, width * 0.3f, height * 0.065f);
            g.FillEllipse(this.batteryBackBrush, -width * 0.15f, height * 0.02f, width * 0.3f, height * 0.02f);
            linearGradientBrush.Dispose();
            Color colorFromValue = this.getColorFromValue(this.value_now);
            colorBlend.Colors = new Color[]
            {
                colorFromValue,
                 this.batteryBackEdgeColor,
                //Color.WhiteSmoke,
                colorFromValue
            };
            float num = height * 0.86f - (this.value_paint - this.value_min) / (this.value_max - this.value_min) * height * 0.86f + height * 0.1f;
            bool flag = num < height * 0.1f;
            if (flag)
            {
                num = height * 0.1f;
            }
            bool flag2 = num > height * 0.96f;
            if (flag2)
            {
                num = height * 0.96f;
            }
            linearGradientBrush = new LinearGradientBrush(new PointF(-width / 2f, 0f), new PointF(width / 2f, 0f), Color.FromArgb(247, 252, 253), Color.WhiteSmoke);
            linearGradientBrush.InterpolationColors = colorBlend;
            g.FillEllipse(linearGradientBrush, -width * 0.5f - 1f, height * 0.93f, width + 1f, height * 0.06f);
            using (Pen pen = new Pen(colorFromValue))
            {
                g.DrawEllipse(pen, -width * 0.5f - 1f, height * 0.93f, width + 1f, height * 0.06f);
            }
            g.FillRectangle(linearGradientBrush, -width * 0.5f - 1f, num, width + 1f, height * 0.86f - (num - height * 0.1f));
            using (Brush brush = new SolidBrush(colorFromValue))
            {
                g.FillEllipse(brush, -width * 0.5f - 1f, num - height * 0.03f, width + 1f, height * 0.06f);
            }
            linearGradientBrush.Dispose();
            g.TranslateTransform(-width / 2f, 0f);
        }

        private Color getColorFromValue(float value)
        {
            bool flag = value > (this.value_max - this.value_min) * this.value_color1 + this.value_min;
            Color result;
            if (flag)
            {
                result = this.color_1;
            }
            else
            {
                bool flag2 = value > (this.value_max - this.value_min) * this.value_color2 + this.value_min;
                if (flag2)
                {
                    result = this.color_2;
                }
                else
                {
                    bool flag3 = value > (this.value_max - this.value_min) * this.value_color3 + this.value_min;
                    if (flag3)
                    {
                        result = this.color_3;
                    }
                    else
                    {
                        bool flag4 = value > (this.value_max - this.value_min) * this.value_color4 + this.value_min;
                        if (flag4)
                        {
                            result = this.color_4;
                        }
                        else
                        {
                            result = this.color_5;
                        }
                    }
                }
            }
            return result;
        }

        private void ThreadPoolUpdateProgress(object obj)
        {
            try
            {
                int num = (int)obj;
                bool flag = this.value_paint == this.value_now;
                if (!flag)
                {
                    float num2 = Math.Abs(this.value_paint - this.value_now) / 10f;
                    bool flag2 = num2 == 0f;
                    if (flag2)
                    {
                        num2 = 1f;
                    }
                    while (this.value_paint != this.value_now)
                    {
                        Thread.Sleep(17);
                        bool flag3 = num != this.m_version;
                        if (flag3)
                        {
                            break;
                        }
                        lock (this.hybirdLock)
                        {
                            bool flag5 = this.value_paint > this.value_now;
                            float num4;
                            if (flag5)
                            {
                                float num3 = this.value_paint - this.value_now;
                                bool flag6 = num3 > num2;
                                if (flag6)
                                {
                                    num3 = num2;
                                }
                                num4 = this.value_paint - num3;
                            }
                            else
                            {
                                float num5 = this.value_now - this.value_paint;
                                bool flag7 = num5 > num2;
                                if (flag7)
                                {
                                    num5 = num2;
                                }
                                num4 = this.value_paint + num5;
                            }
                            this.value_paint = num4;
                        }
                        bool flag8 = num == this.m_version;
                        if (!flag8)
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
            }
            catch (Exception)
            {
            }
        }
    }
}
