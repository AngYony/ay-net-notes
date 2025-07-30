using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.ControlLib
{
    public partial class xbdHeadPanel : Panel
    {
        public xbdHeadPanel()
        {
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }
        private StringFormat sf = new StringFormat();
        private Graphics graphics = null;



        private string titleText = "信必达";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本")]
        public string TitleText
        {
            get
            {
                return titleText;
            }
            set
            {
                titleText = value;
                Invalidate();
            }
        }

        private Color themeColor = Color.LimeGreen;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本背景色")]
        public Color ThemeColor
        {
            get
            {
                return this.themeColor;
            }
            set
            {
                this.themeColor = value;
                Invalidate();
            }
        }

        private Color themeForeColor = Color.Black;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本前景色")]
        public Color ThemeForeColor
        {
            get
            {
                return this.themeForeColor;
            }
            set
            {
                this.themeForeColor = value;
                Invalidate();
            }
        }

        private int headHeight = 30;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题高度")]
        public int HeadHeight
        {
            get
            {
                return this.headHeight;
            }
            set
            {
                this.headHeight = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.Gray;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取边框颜色")]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                Invalidate();
            }
        }



        private float linearGradientRate = 0.4f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题背景颜色的渐变系数")]
        public float LinearGradientRate
        {
            get
            {
                return this.linearGradientRate;
            }
            set
            {
                this.linearGradientRate = value;
                Invalidate();
            }
        }

        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本位置")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;

                switch (textAlign)
                {
                    case ContentAlignment.TopLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    default:
                        break;
                }
                Invalidate();
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.headHeight), GetColorLight(this.themeColor, this.linearGradientRate), this.themeColor, LinearGradientMode.Horizontal);
            graphics.FillRectangle(linearGradientBrush, 0, 0, this.Width, this.headHeight);
            Brush brush = new SolidBrush(this.themeForeColor);
            graphics.DrawString(this.titleText, this.Font, brush, new Rectangle(0, 0, this.Width, this.headHeight), this.sf);

            Pen pen = new Pen(this.BorderColor);
            graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
            graphics.DrawLine(pen, 0, this.headHeight, base.Width, this.headHeight);
        }
        public static Color GetColorLight(Color color, float rate)
        {
            return Color.FromArgb(Convert.ToInt32(color.R + (255 - color.R) * rate), Convert.ToInt32(color.G + (255 - color.G) * rate), Convert.ToInt32(color.B + (255 - color.B) * rate));
        }
    }
}
