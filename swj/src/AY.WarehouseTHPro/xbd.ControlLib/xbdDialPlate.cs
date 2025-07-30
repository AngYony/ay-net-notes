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
    public partial class xbdDialPlate : UserControl
    {
        public xbdDialPlate()
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

        private Color leftColor = Color.FromArgb(36, 184, 196);
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


        private float leftAngle = 120.0f;
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

        private float textScale = 0.85f;
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

        private float textshowScale = 0.8f;
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


        private int inThickness = 16;
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

        private int outThickness = 8;
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


        private Font scaleFont = new Font(new FontFamily("微软雅黑"), 8.0f);
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

        private Font showFont = new Font(new FontFamily("微软雅黑"), 12.0f, FontStyle.Bold);
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

        private Color textShowColor = Color.FromArgb(36, 184, 196);
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

            if (this.height < this.width * 0.5f)
            {
                return;
            }

            //画外环
            p = new Pen(leftColor, outThickness);

            g.DrawArc(p, new RectangleF(10, 10, this.width - 20, this.width - 20), -180.0f, leftAngle);

            p = new Pen(rightColor, outThickness);

            g.DrawArc(p, new RectangleF(10, 10, this.width - 20, this.width - 20), -180.0f + leftAngle, 180.0f - leftAngle);

            //画刻度
            g.TranslateTransform(this.width * 0.5f, this.width * 0.5f);

            g.RotateTransform(-90.0f);

            for (int i = 0; i < 7; i++)
            {
                if (30.0 * i > leftAngle)
                {
                    sb = new SolidBrush(rightColor);
                }
                else
                {
                    sb = new SolidBrush(leftColor);
                }
                g.FillRectangle(sb, new RectangleF(-3.0f, this.width * 0.5f * (-1) + 4.5f, 6.0f, 12.0f));
                g.RotateTransform(30.0f);
            }

            //画刻度值
            //30+180.0
            g.RotateTransform(-210.0f);

            g.RotateTransform(90.0f);

            for (int i = 0; i < 7; i++)
            {
                float angle = -180.0f + i * 30.0f;

                Double x1 = Math.Cos(angle * Math.PI / 180);
                Double y1 = Math.Sin(angle * Math.PI / 180);
                string val = (range / 6 * i).ToString();

                float x = Convert.ToSingle((this.width) * textScale * x1 * 0.5f);
                float y = Convert.ToSingle((this.width) * textScale * y1 * 0.5f);
                StringFormat sf = new StringFormat();
                if (i == 3)
                {
                    x = x - 30;
                    sf.Alignment = StringAlignment.Center; //居中
                }
                else if (i > 3)
                {
                    x = x - 60;
                    sf.Alignment = StringAlignment.Far; //居中
                }
                else if (i < 3)
                {
                    sf.Alignment = StringAlignment.Near; //居中
                }

                RectangleF rec = new RectangleF(x, y,60, 20);
              
                if (range % 6 == 0)
                {
                    g.DrawString((range / 6 * i).ToString("f0"), scaleFont, new SolidBrush(textColor), rec, sf);
                }
                else
                {
                    g.DrawString((range / 6 * i).ToString("f1"), scaleFont, new SolidBrush(textColor), rec, sf);
                }
            }

            //画实际值
            p = new Pen(leftColor, inThickness);

            float sweepangle = currentValue / range * 180.0f;

            float xx = this.width * inScale * -0.5f;

            float yy = this.width * inScale * -0.5f;

            g.DrawArc(p, new RectangleF(xx, yy, this.width * inScale, this.width * inScale), -180.0f, sweepangle);


            if (textShow)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center; //居中

                g.DrawString(currentValue.ToString(), showFont, new SolidBrush(textShowColor), new RectangleF(this.width * (0.5f) * (-1.0f), (this.height * textshowScale - this.width * 0.5f), this.width, this.height *(1.0f - textshowScale)), sf);

            }
        }
        #endregion
    }
}
