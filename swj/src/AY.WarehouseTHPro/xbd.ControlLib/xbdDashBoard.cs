using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    public partial class xbdDashBoard : UserControl
    {
        /// <summary>
        /// xktDashBoard
        /// </summary>
        public xbdDashBoard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        #region Public Member

        private Color leftColor = Color.FromArgb(113, 152, 54);
        /// <summary>
        /// 外环左边颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("外环左边颜色")]
        public Color LeftColor
        {
            get { return leftColor; }
            set
            {
                leftColor = value;

                this.Invalidate();
            }
        }


        private Color rightColor = Color.FromArgb(187, 187, 187);
        /// <summary>
        /// 外环右边颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("外环右边颜色")]
        public Color RightColor
        {
            get { return rightColor; }
            set
            {
                rightColor = value;
                this.Invalidate();
            }
        }

        private Color textColor = Color.FromArgb(187, 187, 187);
        /// <summary>
        /// 字体颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("字体颜色")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }


        private float leftAngle = 168.75f;
        /// <summary>
        /// 左半部分的角度
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("左半部分的角度")]
        public float LeftAngle
        {
            get { return leftAngle; }
            set
            {
                leftAngle = value;
                this.Invalidate();
            }
        }

        private float inScale = 0.5f;
        /// <summary>
        /// 内环圆占的比例
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("内环圆占的比例，设置要小于1.0")]
        public float InScale
        {
            get { return inScale; }
            set
            {
                if (value <= 1.0)
                {
                    inScale = value;
                    this.Invalidate();
                }
            }
        }

        private float textScale = 0.8f;
        /// <summary>
        /// 文本圆占的比例
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("文本圆占的比例，设置要小于1.0")]
        public float TextScale
        {
            get { return textScale; }
            set
            {
                if (value <= 1.0)
                {
                    textScale = value;
                    this.Invalidate();
                }
            }
        }

        private float textshowScale = 0.85f;
        /// <summary>
        /// 文本显示高度占的比例
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("文本显示高度占的比例，设置要小于1.0，值越小越靠上")]
        public float TextshowScale
        {
            get { return textshowScale; }
            set
            {
                if (value <= 1.0)
                {
                    textshowScale = value;
                    this.Invalidate();
                }
            }
        }



        private float range = 160.0f;
        /// <summary>
        /// 仪表对应量程
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("仪表对应量程")]
        public float Range
        {
            get { return range; }
            set
            {
                range = value;
                this.Invalidate();
            }
        }


        private int inThickness = 12;
        /// <summary>
        /// 内环宽度
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("内环宽度")]
        public int InThickness
        {
            get { return inThickness; }
            set
            {
                inThickness = value;
                this.Invalidate();
            }
        }

        private int outThickness = 5;
        /// <summary>
        /// 外环宽度
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("外环宽度")]
        public int OutThickness
        {
            get { return outThickness; }
            set
            {
                outThickness = value;
                this.Invalidate();
            }
        }

        private float currentValue = 100.0f;
        /// <summary>
        /// 显示数值
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("显示数值")]
        public float CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                this.Invalidate();
            }
        }

        private float centerRadius = 12.0f;
        /// <summary>
        /// 中心半径
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("中心半径")]
        public float CenterRadius
        {
            get { return centerRadius; }
            set
            {
                centerRadius = value;
                this.Invalidate();
            }
        }

        private string textPrefix = "实际温度：";
        /// <summary>
        /// 显示前缀
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("显示前缀")]
        public string TextPrefix
        {
            get { return textPrefix; }
            set
            {
                textPrefix = value;
                this.Invalidate();
            }
        }

        private string unit = "℃";
        /// <summary>
        /// 单位
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("单位")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                this.Invalidate();
            }
        }

        private Font scaleFont = new Font(new FontFamily("微软雅黑"), 8.0f);
        /// <summary>
        /// 刻度字体
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("刻度字体")]
        public Font ScaleFont
        {
            get { return scaleFont; }
            set
            {
                scaleFont = value;
                this.Invalidate();
            }
        }

        private Font showFont = new Font(new FontFamily("微软雅黑"), 10.0f, FontStyle.Bold);
        /// <summary>
        /// 显示字体
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("显示字体")]
        public Font ShowFont
        {
            get { return showFont; }
            set
            {
                showFont = value;
                this.Invalidate();
            }
        }

        private bool textShow = true;
        /// <summary>
        /// 是否显示实时数值
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("是否显示实时数值")]
        public bool TextShow
        {
            get { return textShow; }
            set
            {
                textShow = value;
                this.Invalidate();
            }
        }

        private Color textShowColor = Color.FromArgb(187, 187, 187);
        /// <summary>
        /// 实时数值显示字体颜色
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("实时数值显示字体颜色")]
        public Color TextShowColor
        {
            get { return textShowColor; }
            set
            {
                textShowColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Public Field

        Graphics g;

        int width;

        int height;

        Pen p;

        SolidBrush sb;

        #endregion

        #region Invalidate

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            width = this.Width;
            height = this.Height;

            if (this.width <= 20 || this.height <= 20)
            {
                return;
            }

            //画外环
            p = new Pen(leftColor, outThickness);

            g.DrawArc(p, new Rectangle(10, 10, this.width - 20, this.height - 20), -225.0f, leftAngle);

            p = new Pen(rightColor, outThickness);

            g.DrawArc(p, new Rectangle(10, 10, this.width - 20, this.height - 20), -225.0f + leftAngle, 270.0f - leftAngle);

            //画刻度
            g.TranslateTransform(this.width * 0.5f, this.height * 0.5f);

            g.RotateTransform(-135.0f);

            for (int i = 0; i < 9; i++)
            {
                if (33.75 * i > leftAngle)
                {
                    sb = new SolidBrush(rightColor);
                }
                else
                {
                    sb = new SolidBrush(leftColor);
                }
                g.FillRectangle(sb, new RectangleF(-2.0f, this.height * 0.5f * (-1) + 4.5f, 4.0f, 12.0f));
                g.RotateTransform(33.75f);
            }

            //画刻度值
            //33.75+270.0
            g.RotateTransform(-303.75f);

            g.RotateTransform(135.0f);

            for (int i = 0; i < 9; i++)
            {
                float angle = -225.0f + i * 33.75f;

                double x1 = Math.Cos(angle * Math.PI / 180);
                double y1 = Math.Sin(angle * Math.PI / 180);
                string val = (range / 8 * i).ToString();

                float x = Convert.ToSingle((this.width) * textScale * x1 * 0.5f);
                float y = Convert.ToSingle((this.height) * textScale * y1 * 0.5f);
                StringFormat sf = new StringFormat();
                if (i == 4)
                {
                    x = x - 30;
                    sf.Alignment = StringAlignment.Center; //居中
                }
                else if (i > 4)
                {
                    x = x - 60;
                    sf.Alignment = StringAlignment.Far; //居中
                }
                else if (i < 4)
                {
                    sf.Alignment = StringAlignment.Near; //居中
                }

                RectangleF rec = new RectangleF(x, y, 60, 20);

                if (range % 8 == 0)
                {
                    g.DrawString((range / 8 * i).ToString("f0"), scaleFont, new SolidBrush(textColor), rec, sf);
                }
                else
                {
                    g.DrawString((range / 8 * i).ToString("f1"), scaleFont, new SolidBrush(textColor),  rec, sf);
                }
            }
            //画内圆点
            g.FillEllipse(new SolidBrush(leftColor), new RectangleF(-centerRadius, -centerRadius, 2 * centerRadius, 2 * centerRadius));

            //画实际值
            p = new Pen(leftColor, inThickness);

            float sweepangle = currentValue / range * 270.0f;

            float xx = this.width * inScale * -0.5f;

            float yy = this.height * inScale * -0.5f;

            g.DrawArc(p, new RectangleF(xx, yy, this.width * inScale, this.height * inScale), -225.0f, sweepangle);

            g.RotateTransform(-135.0f);

            g.RotateTransform(sweepangle);

            p = new Pen(leftColor, 3.0f);

            float z = height / 2 * inScale * (-1.0f) - inThickness * 0.5f;
            g.DrawLine(p, new PointF(0, 0), new PointF((float)0, z));

            if (textShow)
            {
                g.RotateTransform(sweepangle * (-1.0f));

                g.RotateTransform(135.0f);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center; //居中

                g.DrawString(textPrefix + currentValue.ToString() + " " + unit, showFont, new SolidBrush(textShowColor), new RectangleF(this.width * (0.5f) * (-1.0f), (this.height * textshowScale - this.height * 0.5f), this.width, this.height *(1.0f- textshowScale)), sf);
            }
        }
        #endregion

    }
}
