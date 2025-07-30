using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    /// <summary>
    /// xktPumpBasic
    /// </summary>
    public partial class xbdPump : UserControl
    {
        private StringFormat sf = null;

        private DirectionStyle ValvesStyle = DirectionStyle.Horizontal;

        private int entrance = 1;

        private int export = 6;

        private Pen pen_1 = new Pen(Color.FromArgb(92, 100, 111), 3f);

        private Color color1 = Color.FromArgb(209, 218, 227);
        private Color color2 = Color.LightGray;
       private Color color3 = Color.FromArgb(212, 135, 69);
        private Color color4 = Color.FromArgb(204, 208, 214);
        private Color color5 = Color.FromArgb(208, 213, 220);
        private Color color6 = Color.FromArgb(153, 160, 169);
        private Color color7 = Color.FromArgb(92, 100, 111);

        private Brush brush1, brush2, brush3, brush4, brush5, brush6, brush7;

        private float moveSpeed = 1.0f;

        private float startAngle = 0f;

        private Timer timer = null;

        /// <summary>
        /// 控件的背景色
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "Transparent"), Description("获取或设置控件的背景色"), EditorBrowsable(EditorBrowsableState.Always)]
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
        /// 控件的文本
        /// </summary>
        [Bindable(true), Browsable(true), Category("自定义属性"), Description("获取或设置当前控件的文本"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
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
        /// 横向还是竖向
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(DirectionStyle), "Horizontal"), Description("获取或设置泵控件是否是横向的还是纵向的")]
        public DirectionStyle PumpStyle
        {
            get
            {
                return this.ValvesStyle;
            }
            set
            {
                this.ValvesStyle = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 传送带流动的速度
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(0.3f), Description("获取或设置传送带流动的速度，0为静止，正数为正向流动，负数为反向流动")]
        public float MoveSpeed
        {
            get
            {
                return this.moveSpeed;
            }
            set
            {
                this.moveSpeed = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色1
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[199, 205, 211]"), Description("获取或设置颜色1")]
        public Color Color1
        {
            get
            {
                return this.color1;
            }
            set
            {
                this.color1 = value;
                this.brush1.Dispose();
                this.brush1 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色2
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[135, 144, 156]"), Description("获取或设置颜色2")]
        public Color Color2
        {
            get
            {
                return this.color2;
            }
            set
            {
                this.color2 = value;
                this.brush2.Dispose();
                this.brush2 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色3
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[208, 213, 220]"), Description("获取或设置颜色3")]
        public Color Color3
        {
            get
            {
                return this.color3;
            }
            set
            {
                this.color3 = value;
                this.brush3.Dispose();
                this.brush3 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色4
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[153, 160, 169]"), Description("获取或设置颜色4")]
        public Color Color4
        {
            get
            {
                return this.color4;
            }
            set
            {
                this.color4 = value;
                this.brush4.Dispose();
                this.brush4 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色5
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[92, 100, 111]"), Description("获取或设置颜色5")]
        public Color Color5
        {
            get
            {
                return this.color5;
            }
            set
            {
                this.color5 = value;
                this.brush5.Dispose();
                this.brush5 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置颜色6
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[108, 114, 121]"), Description("获取或设置颜色6")]
        public Color Color6
        {
            get
            {
                return this.color6;
            }
            set
            {
                this.color6 = value;
                this.brush6.Dispose();
                this.brush6 = new SolidBrush(value);
                base.Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置颜色7
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(typeof(Color), "[158, 165, 173]"), Description("获取或设置颜色7")]
        public Color Color7
        {
            get
            {
                return this.color7;
            }
            set
            {
                this.color7 = value;
                this.brush7.Dispose();
                this.brush7 = new SolidBrush(value);
                base.Invalidate();
            }
        }
        /// <summary>
        /// 入口管道的位置
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(1), Description("入口管道的位置")]
        public int Entrance
        {
            get
            {
                return this.entrance;
            }
            set
            {
                if (value > 0 && value < 7)
                {
                    this.entrance = value;
                    base.Invalidate();
                }
            }
        }
        /// <summary>
        /// 出口管道的位置
        /// </summary>
        [Browsable(true), Category("自定义属性"), DefaultValue(4), Description("出口管道的位置")]
        public int Export
        {
            get
            {
                return this.export;
            }
            set
            {
                if (value > 0 && value < 7)
                {
                    this.export = value;
                    base.Invalidate();
                }
            }
        }

        private bool isRun = false;
        [Browsable(true), Category("自定义属性"), DefaultValue(1), Description("是否运行")]
        public bool IsRun
        {
            get { return isRun; }
            set
            {
                if (isRun != value)
                {
                    isRun = value;
                    this.timer.Enabled = isRun;
                }
            }
        }


        /// <summary>
        /// xktPumpBasic
        /// </summary>
        public xbdPump()
        {
            this.InitializeComponent();
            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += new EventHandler(this.Timer_Tick);

            brush1 = new SolidBrush(color1);
            brush2 = new SolidBrush(color2);
            brush3 = new SolidBrush(color3);
            brush4 = new SolidBrush(color4);
            brush5 = new SolidBrush(color5);
            brush6 = new SolidBrush(color6);
            brush7 = new SolidBrush(color7);
        }




        /// <summary>
        /// OnPaint事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            float num = (float)Math.Min(base.Width, base.Height);
            bool flag2 = this.ValvesStyle == DirectionStyle.Horizontal;
            if (flag2)
            {
                this.PaintMain(graphics, num, num);
            }
            else
            {
                graphics.TranslateTransform((float)base.Width, 0f);
                graphics.RotateTransform(90f);
                this.PaintMain(graphics, num, num);
                graphics.ResetTransform();
            }
            base.OnPaint(e);
        }

        private void PaintMain(Graphics g, float width, float height)
        {
            g.TranslateTransform(width / 2f, height / 2f);
            this.DrawPipe(g, width, height, this.entrance);
            this.DrawPipe(g, width, height, this.export);
            PointF[] points = new PointF[]
            {
                new PointF(0f, height * 0.1f),
                new PointF(-width * 0.25f, height * 0.5f - 1f),
                new PointF(width * 0.25f, height * 0.5f - 1f),
                new PointF(0f, height * 0.1f)
            };
            g.FillPolygon(this.brush1, points);
            g.FillRectangle(this.brush2, new RectangleF(-width * 0.28f, height * 0.46f, width * 0.56f, height * 0.04f));
            g.FillEllipse(this.brush3, new RectangleF(-width * 0.3f, -height * 0.3f, width * 0.6f, height * 0.6f));
            g.FillEllipse(this.brush4, new RectangleF(-width * 0.24f, -height * 0.24f, width * 0.48f, height * 0.48f));
            g.RotateTransform(this.startAngle);
            bool flag = width < 50f;
            if (flag)
            {
                this.pen_1.Width = 1f;
            }
            else
            {
                bool flag2 = width < 100f;
                if (flag2)
                {
                    this.pen_1.Width = 2f;
                }
                else
                {
                    this.pen_1.Width = 3f;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(this.pen_1, -width * 0.2f, 0f, width * 0.2f, 0f);
                g.RotateTransform(45f);
            }
            g.RotateTransform(-180f);
            g.RotateTransform(-this.startAngle);
            g.FillEllipse(this.brush5, -width * 0.09f, -width * 0.09f, width * 0.18f, width * 0.18f);
            g.FillEllipse(this.brush6, -width * 0.08f, -width * 0.08f, width * 0.16f, width * 0.16f);
            g.FillEllipse(this.brush5, -width * 0.02f, -width * 0.02f, width * 0.04f, width * 0.04f);
            g.FillEllipse(this.brush7, -width * 0.01f, -width * 0.01f, width * 0.02f, width * 0.02f);
            using (Brush brush = new SolidBrush(this.ForeColor))
            {
                g.DrawString(this.Text, this.Font, brush, new RectangleF(-width / 2f, height * 0.38f, width, height * 0.55f), this.sf);
            }
            g.TranslateTransform(-width / 2f, -height / 2f);
        }

        private void DrawPipe(Graphics g, float width, float height, int direction)
        {
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Positions = new float[]
            {
                0f,
                0.5f,
                1f
            };
            colorBlend.Colors = new Color[]
            {
                this.color6,
                Color.WhiteSmoke,
                this.color6
            };
            float num = width * 0.02f;
            if (num < 3f)
            {
                num = 3f;
            }
            float num2 = height * 0.25f + 10f;
            bool flag2 = direction == 1 || direction == 2 || direction == 5 || direction == 6;
            if (flag2)
            {
                RectangleF rect = default(RectangleF);
                RectangleF rect2 = default(RectangleF);
                switch (direction)
                {
                    case 1:
                        rect = new RectangleF(-width / 2f + 1f, height * 0.05f, width * 0.5f, height * 0.25f);
                        rect2 = new RectangleF(-width / 2f, height * 0.05f - 5f, num, num2);
                        break;
                    case 2:
                        rect = new RectangleF(-width / 2f + 1f, -height * 0.3f, width * 0.5f, height * 0.25f);
                        rect2 = new RectangleF(-width / 2f, -height * 0.3f - 5f, num, num2);
                        break;
                    case 5:
                        rect = new RectangleF(0f, -height * 0.3f, width * 0.5f, height * 0.25f);
                        rect2 = new RectangleF(width * 0.5f - num, -height * 0.3f - 5f, num, num2);
                        break;
                    case 6:
                        rect = new RectangleF(0f, height * 0.05f, width * 0.5f, height * 0.25f);
                        rect2 = new RectangleF(width * 0.5f - num, height * 0.05f - 5f, num, num2);
                        break;
                }
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF(rect.Location.X, rect.Location.Y + rect.Height), new PointF(rect.Location.X, rect.Location.Y), Color.Wheat, Color.White);
                linearGradientBrush.InterpolationColors = colorBlend;
                g.FillRectangle(linearGradientBrush, rect);
                linearGradientBrush.Dispose();
                g.FillRectangle(Brushes.DimGray, rect2);
            }
            else
            {
                bool flag3 = direction == 3 || direction == 4;
                if (flag3)
                {
                    RectangleF rect3 = default(RectangleF);
                    RectangleF rect4 = default(RectangleF);
                    if (direction != 3)
                    {
                        if (direction == 4)
                        {
                            rect3 = new RectangleF(width * 0.05f, -height * 0.5f, width * 0.25f, height * 0.5f);
                            rect4 = new RectangleF(width * 0.05f - 5f, -height * 0.5f, num2, num);
                        }
                    }
                    else
                    {
                        rect3 = new RectangleF(-width * 0.3f, -height * 0.5f, width * 0.25f, height * 0.5f);
                        rect4 = new RectangleF(-width * 0.3f - 5f, -height * 0.5f, num2, num);
                    }
                    LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new PointF(rect3.Location.X, rect3.Location.Y), new PointF(rect3.Location.X + rect3.Width, rect3.Location.Y), Color.Wheat, Color.White);
                    linearGradientBrush2.InterpolationColors = colorBlend;
                    g.FillRectangle(linearGradientBrush2, rect3);
                    linearGradientBrush2.Dispose();
                    g.FillRectangle(Brushes.DimGray, rect4);
                }
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.moveSpeed != 0f)
            {
                this.startAngle = (float)((double)this.startAngle + (double)(this.moveSpeed * 180f) / 3.1415926535897931 / 10.0);
                if (this.startAngle <= -360f)
                {
                    this.startAngle += 360f;
                }
                else
                {
                    if (this.startAngle >= 360f)
                    {
                        this.startAngle -= 360f;
                    }
                }
                base.Invalidate();
            }
        }

    }
}
