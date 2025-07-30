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

    public enum PipeTurnDirection
    {
        Up = 1,
        Down,
        Left,
        Right,
        None
    }

    public enum DirectionStyle
    {
        Horizontal = 1,
        Vertical
    }
    public partial class xbdFlowControl : UserControl
    {
        public xbdFlowControl()
        {
            InitializeComponent();

            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += new EventHandler(this.Timer_Tick);
        }

        private Timer timer = null;


        private void Timer_Tick(object sender, EventArgs e)
        {
            this.startOffset -= this.moveSpeed;
            if (this.startOffset <= (this.pipeLineLength + this.pipeLineGap )* (-1.0f) || this.startOffset >= (this.pipeLineLength+this.pipeLineGap))
            {
                this.startOffset = 0f;
            }
            this.Invalidate();
        }

        private DirectionStyle pipeLineStyle = DirectionStyle.Horizontal;

        private Pen edgePen = new Pen(Color.DimGray, 1f);

        private float startOffset = 0f;


        private int pipeLineLength =2;
        [Browsable(true), Category("自定义属性"), Description("获取或设置流动条长度")]
        public int PipeLineLength
        {
            get { return pipeLineLength; }
            set
            {
                pipeLineLength = value;
                this.Invalidate();
            }
        }

        private int pipeLineGap = 2;
        [Browsable(true), Category("自定义属性"), Description("获取或设置流动条间隙长度")]
        public int PipeLineGap
        {
            get { return pipeLineGap; }
            set
            {
                pipeLineGap = value;
                this.Invalidate();
            }
        }

        private int pipeLineWidth = 5;
        [Browsable(true), Category("自定义属性"), Description("获取或设置流动条宽度")]
        public int PipeLineWidth
        {
            get { return pipeLineWidth; }
            set
            {
                pipeLineWidth = value;
                this.Invalidate();
            }
        }

        private Color colorPipeLineCenter = Color.DodgerBlue;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件的流动颜色")]
        public Color ColorPipeLineCenter
        {
            get { return colorPipeLineCenter; }
            set
            {
                colorPipeLineCenter = value;
                this.Invalidate();
            }
        }


        [Browsable(true), Category("自定义属性"), Description("获取或设置控件的背景色"), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                base.Invalidate();
            }
        }

        private Color edgeColor = Color.DimGray;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件的边缘颜色")]
        public Color EdgeColor
        {
            get
            {
                return this.edgeColor;
            }
            set
            {
                this.edgeColor = value;
                this.edgePen.Dispose();
                this.edgePen = new Pen(value, 1f);
                base.Invalidate();
            }
        }

        private Color centerColor = Color.LightGray;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件的中心颜色")]
        public Color LineCenterColor
        {
            get
            {
                return this.centerColor;
            }
            set
            {
                this.centerColor = value;
                base.Invalidate();
            }
        }

        private PipeTurnDirection pipeTurnLeft = PipeTurnDirection.None;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件左侧或是上方的端点朝向")]
        public PipeTurnDirection PipeTurnLeft
        {
            get
            {
                return this.pipeTurnLeft;
            }
            set
            {
                this.pipeTurnLeft = value;
                base.Invalidate();
            }
        }

        private PipeTurnDirection pipeTurnRight = PipeTurnDirection.None;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件左侧或是上方的端点朝向")]
        public PipeTurnDirection PipeTurnRight
        {
            get
            {
                return this.pipeTurnRight;
            }
            set
            {
                this.pipeTurnRight = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件是否是横向的还是纵向的")]
        public DirectionStyle PipeLineStyle
        {
            get
            {
                return this.pipeLineStyle;
            }
            set
            {
                this.pipeLineStyle = value;
                base.Invalidate();
            }
        }

        private bool isPipeLineActive = false;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道线是否激活液体显示")]
        public bool PipeLineActive
        {
            get
            {
                return this.isPipeLineActive;
            }
            set
            {
                this.isPipeLineActive = value;
                this.timer.Enabled = value;
                base.Invalidate();

            }
        }

        private float moveSpeed = 0.2f;
        [Browsable(true), Category("自定义属性"), Description("获取或设置管道线液体流动的速度，0为静止，正数为正向流动，负数为反向流动")]
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Positions = new float[]
            {
                    0f,
                    0.5f,
                    1f
            };
            colorBlend.Colors = new Color[]
            {
                    this.edgeColor,
                    this.centerColor,
                    this.edgeColor
            };
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Alternate);

            if (this.PipeLineStyle == DirectionStyle.Horizontal)
            {
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, base.Height - 1), this.edgeColor, this.centerColor);
                linearGradientBrush.InterpolationColors = colorBlend;
                if (this.PipeTurnLeft == PipeTurnDirection.Up)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(0, -base.Height - 1, 2 * base.Height, 2 * base.Height), 90f, 90f);
                }
                else if (this.PipeTurnLeft == PipeTurnDirection.Down)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(0, 0, 2 * base.Height, 2 * base.Height), 180f, 90f);
                }
                else
                {
                    this.PaintRectangleBorderUpDown(e.Graphics, linearGradientBrush, this.edgePen, new Rectangle(0, 0, base.Height, base.Height));
                }

                if (this.PipeTurnRight == PipeTurnDirection.Up)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(base.Width - 1 - base.Height * 2, -base.Height - 1, 2 * base.Height, 2 * base.Height), 0f, 90f);
                }
                else if (this.PipeTurnRight == PipeTurnDirection.Down)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(base.Width - 1 - base.Height * 2, 0, 2 * base.Height, 2 * base.Height), 270f, 90f);
                }
                else
                {
                    this.PaintRectangleBorderUpDown(e.Graphics, linearGradientBrush, this.edgePen, new Rectangle(base.Width - base.Height, 0, base.Height, base.Height));
                }

                if (base.Width - base.Height * 2 >= 0)
                {
                    this.PaintRectangleBorderUpDown(e.Graphics, linearGradientBrush, this.edgePen, new Rectangle(base.Height - 1, 0, base.Width - 2 * base.Height + 2, base.Height));
                }
                linearGradientBrush.Dispose();
                if (this.isPipeLineActive)
                {
                    if (base.Width < base.Height)
                    {
                        graphicsPath.AddLine(0, base.Height / 2, base.Height, base.Height / 2);
                    }
                    else
                    {
                        if (this.pipeTurnLeft == PipeTurnDirection.Up)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Height / 2, -base.Height / 2 - 1, base.Height, base.Height), 180f, -90f);
                        }
                        else if (this.pipeTurnLeft == PipeTurnDirection.Down)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Height / 2, base.Height / 2, base.Height, base.Height), 180f, 90f);
                        }
                        else
                        {
                            graphicsPath.AddLine(0, base.Height / 2, base.Height, base.Height / 2);
                        }

                        if (base.Width - base.Height * 2 > 0)
                        {
                            graphicsPath.AddLine(base.Height, base.Height / 2, base.Width - base.Height - 1, base.Height / 2);
                        }
                        if (this.pipeTurnRight == PipeTurnDirection.Up)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Width - 1 - base.Height * 3 / 2, -base.Height / 2 - 1, base.Height, base.Height), 90f, -90f);
                        }
                        else if (this.pipeTurnRight == PipeTurnDirection.Down)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Width - 1 - base.Height * 3 / 2, base.Height / 2, base.Height, base.Height), 270f, 90f);
                        }
                        else
                        {
                            graphicsPath.AddLine(base.Width - base.Height, base.Height / 2, base.Width - 1, base.Height / 2);
                        }
                    }
                }
            }
            else
            {
                LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Point(0, 0), new Point(base.Width - 1, 0), this.edgeColor, this.centerColor);
                linearGradientBrush2.InterpolationColors = colorBlend;
                if (this.PipeTurnLeft == PipeTurnDirection.Left)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(-base.Width - 1, 0, 2 * base.Width, 2 * base.Width), 270f, 90f);
                }
                else if (this.PipeTurnLeft == PipeTurnDirection.Right)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(0, 0, 2 * base.Width, 2 * base.Width), 180f, 90f);
                }
                else
                {
                    this.PaintRectangleBorderLeftRight(e.Graphics, linearGradientBrush2, this.edgePen, new Rectangle(0, 0, base.Width, base.Width));
                }

                if (this.PipeTurnRight == PipeTurnDirection.Left)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(-base.Width - 1, base.Height - base.Width * 2, 2 * base.Width, 2 * base.Width), 0f, 90f);
                }
                else if (this.PipeTurnRight == PipeTurnDirection.Right)
                {
                    this.PaintEllipse(e.Graphics, colorBlend, new Rectangle(0, base.Height - base.Width * 2, 2 * base.Width, 2 * base.Width), 90f, 90f);
                }
                else
                {
                    this.PaintRectangleBorderLeftRight(e.Graphics, linearGradientBrush2, this.edgePen, new Rectangle(0, base.Height - base.Width, base.Width, base.Width));
                }
                if (base.Height - base.Width * 2 >= 0)
                {
                    this.PaintRectangleBorderLeftRight(e.Graphics, linearGradientBrush2, this.edgePen, new Rectangle(0, base.Width - 1, base.Width, base.Height - base.Width * 2 + 2));
                }
                linearGradientBrush2.Dispose();
                if (this.isPipeLineActive)
                {
                    if (base.Width > base.Height)
                    {
                        graphicsPath.AddLine(0, base.Height / 2, base.Height, base.Height / 2);
                    }
                    else
                    {
                        if (this.PipeTurnLeft == PipeTurnDirection.Left)
                        {
                            graphicsPath.AddArc(new Rectangle(-base.Width / 2, base.Width / 2 - 1, base.Width, base.Width), 270f, 90f);
                        }
                        else if (this.PipeTurnLeft == PipeTurnDirection.Right)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Width / 2, base.Width / 2 - 1, base.Width, base.Width), 270f, -90f);
                        }
                        else
                        {
                            graphicsPath.AddLine(base.Width / 2, 0, base.Width / 2, base.Width);
                        }

                        if (base.Height - base.Width * 2 >= 0)
                        {
                            graphicsPath.AddLine(base.Width / 2, base.Width, base.Width / 2, base.Height - base.Width - 1);
                        }
                        if (this.PipeTurnRight == PipeTurnDirection.Left)
                        {
                            graphicsPath.AddArc(new Rectangle(-base.Width / 2, base.Height - 1 - base.Width * 3 / 2, base.Width, base.Width), 0f, 90f);
                        }
                        else if (this.PipeTurnRight == PipeTurnDirection.Right)
                        {
                            graphicsPath.AddArc(new Rectangle(base.Width / 2, base.Height - 1 - base.Width * 3 / 2, base.Width, base.Width), -180f, -90f);
                        }
                        else
                        {
                            graphicsPath.AddLine(base.Width / 2, base.Height - base.Width, base.Width / 2, base.Height - 1);
                        }

                    }
                }
            }
            using (Pen pen = new Pen(this.ColorPipeLineCenter, (float)this.pipeLineWidth))
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = new float[]
                {
                        this.pipeLineLength,
                            this.pipeLineGap,
                };
                pen.DashOffset = this.startOffset;
                e.Graphics.DrawPath(pen, graphicsPath);
            }
            base.OnPaint(e);

        }


        private void PaintEllipse(Graphics g, ColorBlend colorBlend, Rectangle rect, float startAngle, float sweepAngle)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rect);
            PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
            pathGradientBrush.CenterPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            pathGradientBrush.InterpolationColors = colorBlend;
            g.FillPie(pathGradientBrush, rect, startAngle, sweepAngle);
            g.DrawArc(this.edgePen, rect, startAngle, sweepAngle);
            pathGradientBrush.Dispose();
            graphicsPath.Dispose();
        }

        private void PaintRectangleBorder(Graphics g, Brush brush, Pen pen, Rectangle rectangle, bool left, bool right, bool up, bool down)
        {
            g.FillRectangle(brush, rectangle);
            if (left)
            {
                g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height - 1);
            }
            if (up)
            {
                g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width - 1, rectangle.Y);
            }
            if (right)
            {
                g.DrawLine(pen, rectangle.X + rectangle.Width - 1, rectangle.Y, rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 1);
            }
            if (down)
            {
                g.DrawLine(pen, rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 1);
            }
        }

        private void PaintRectangleBorderLeftRight(Graphics g, Brush brush, Pen pen, Rectangle rectangle)
        {
            this.PaintRectangleBorder(g, brush, pen, rectangle, true, true, false, false);
        }

        private void PaintRectangleBorderUpDown(Graphics g, Brush brush, Pen pen, Rectangle rectangle)
        {
            this.PaintRectangleBorder(g, brush, pen, rectangle, false, false, true, true);
        }

        private void PaintRectangleBorder(Graphics g, Brush brush, Pen pen, Rectangle rectangle)
        {
            this.PaintRectangleBorder(g, brush, pen, rectangle, true, true, true, true);
        }
    }
}
