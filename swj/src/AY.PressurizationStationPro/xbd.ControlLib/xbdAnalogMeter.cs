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

namespace xbd.ControlLib
{
    public partial class xbdAnalogMeter : UserControl
    {
		#region Properties variables
		//private AnalogMeterStyle meterStyle=AnalogMeterStyle.Circular;
		private Color bodyColor;
		private Color needleColor;
		private Color scaleColor;
		private bool viewGlass;
		private double currValue;
		private double minValue;
		private double maxValue;
		private int scaleDivisions;
		private int scaleSubDivisions;
		private LBAnalogMeterRenderer renderer;
		#endregion

		#region Class variables
		protected PointF needleCenter;
		protected RectangleF drawRect;
		protected RectangleF glossyRect;
		protected RectangleF needleCoverRect;
		protected float startAngle;
		protected float endAngle;
		protected float drawRatio;
		protected LBAnalogMeterRenderer defaultRenderer;
		#endregion

		#region Costructors
		public xbdAnalogMeter()
		{
			// Initialization
			InitializeComponent();

			// Properties initialization
			this.bodyColor = Color.FromArgb(9, 9, 45);
			this.needleColor = Color.FromArgb(192, 64, 0);
			this.scaleColor = Color.White;
			//this.meterStyle = AnalogMeterStyle.Circular;
			this.viewGlass = false;
			this.startAngle = 135;
			this.endAngle = 405;
			this.minValue = 0;
			this.maxValue = 10;
			this.currValue = 0;
			this.scaleDivisions = 11;
			this.scaleSubDivisions = 4;
			this.renderer = null;

			// Set the styles for drawing
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			// Create the default renderer
			this.defaultRenderer = new LBDefaultAnalogMeterRenderer();
			this.defaultRenderer.AnalogMeter = this;
		}
		#endregion

		#region Properties

		[Category("自定义属性")]
		[Description("设置或获取仪表盘主体颜色")]
		public Color BodyColor
		{
			get { return bodyColor; }
			set
			{
				bodyColor = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取仪表盘指针颜色")]
        public Color NeedleColor
		{
			get { return needleColor; }
			set
			{
				needleColor = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取是否有阴影")]
        public bool ViewGlass
		{
			get { return viewGlass; }
			set
			{
				viewGlass = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取刻度颜色")]
        public Color ScaleColor
		{
			get { return scaleColor; }
			set
			{
				scaleColor = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取当前值")]
        public double Value
		{
			get { return currValue; }
			set
			{
				double val = value;
				if (val > maxValue)
					val = maxValue;

				if (val < minValue)
					val = minValue;

				currValue = val;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取最小值")]
        public double MinValue
		{
			get { return minValue; }
			set
			{
				minValue = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取最大值")]
        public double MaxValue
		{
			get { return maxValue; }
			set
			{
				maxValue = value;
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取主刻度数量")]
        public int ScaleDivisions
		{
			get { return scaleDivisions; }
			set
			{
				scaleDivisions = value;
				CalculateDimensions();
				Invalidate();
			}
		}


        [Category("自定义属性")]
        [Description("设置或获取副刻度数量")]
        public int ScaleSubDivisions
		{
			get { return scaleSubDivisions; }
			set
			{
				scaleSubDivisions = value;
				CalculateDimensions();
				Invalidate();
			}
		}

		[Browsable(false)]
		public LBAnalogMeterRenderer Renderer
		{
			get { return this.renderer; }
			set
			{
				this.renderer = value;
				if (this.renderer != null)
					renderer.AnalogMeter = this;
				Invalidate();
			}
		}
		#endregion

		#region Public methods
		public float GetDrawRatio()
		{
			return this.drawRatio;
		}

		public float GetStartAngle()
		{
			return this.startAngle;
		}

		public float GetEndAngle()
		{
			return this.endAngle;
		}

		public PointF GetNeedleCenter()
		{
			return this.needleCenter;
		}
		#endregion

		#region Events delegates
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			// Calculate dimensions
			CalculateDimensions();

			this.Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			RectangleF _rc = new RectangleF(0, 0, this.Width, this.Height);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			if (this.renderer == null)
			{
				CalculateDimensions();
				this.defaultRenderer.DrawBackground(e.Graphics, _rc);
				this.defaultRenderer.DrawBody(e.Graphics, drawRect);
				this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);
            }
			else
			{
				if (this.Renderer.DrawBackground(e.Graphics, _rc) == false)
					this.defaultRenderer.DrawBackground(e.Graphics, _rc);
				if (this.Renderer.DrawBody(e.Graphics, drawRect) == false)
					this.defaultRenderer.DrawBody(e.Graphics, drawRect);
				if (this.Renderer.DrawThresholds(e.Graphics, drawRect) == false)
					this.defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                if (this.Renderer.DrawDivisions(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                if (this.Renderer.DrawUM(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawUM(e.Graphics, drawRect);
                if (this.Renderer.DrawValue(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawValue(e.Graphics, drawRect);
                if (this.Renderer.DrawNeedle(e.Graphics, drawRect) == false)
                    this.defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                if (this.Renderer.DrawNeedleCover(e.Graphics, this.needleCoverRect) == false)
                    this.defaultRenderer.DrawNeedleCover(e.Graphics, this.needleCoverRect);
                if (this.Renderer.DrawGlass(e.Graphics, this.glossyRect) == false)
                    this.defaultRenderer.DrawGlass(e.Graphics, this.glossyRect);
            }
        }
		#endregion

		#region Virtual functions		
		protected virtual void CalculateDimensions()
		{
			// Rectangle
			float x, y, w, h;
			x = 0;
			y = 0;
			w = this.Size.Width;
			h = this.Size.Height;

			// Calculate ratio
			drawRatio = (Math.Min(w, h)) / 200;
			if (drawRatio == 0.0)
				drawRatio = 1;

			// Draw rectangle
			drawRect.X = x;
			drawRect.Y = y;
			drawRect.Width = w - 2;
			drawRect.Height = h - 2;

			if (w < h)
				drawRect.Height = w;
			else if (w > h)
				drawRect.Width = h;

			if (drawRect.Width < 10)
				drawRect.Width = 10;
			if (drawRect.Height < 10)
				drawRect.Height = 10;

			// Calculate needle center
			needleCenter.X = drawRect.X + (drawRect.Width / 2);
			needleCenter.Y = drawRect.Y + (drawRect.Height / 2);

			// Needle cover rect
			needleCoverRect.X = needleCenter.X - (20 * drawRatio);
			needleCoverRect.Y = needleCenter.Y - (20 * drawRatio);
			needleCoverRect.Width = 40 * drawRatio;
			needleCoverRect.Height = 40 * drawRatio;

			// Glass effect rect
			glossyRect.X = drawRect.X + (20 * drawRatio);
			glossyRect.Y = drawRect.Y + (10 * drawRatio);
			glossyRect.Width = drawRect.Width - (40 * drawRatio);
			glossyRect.Height = needleCenter.Y + (30 * drawRatio);
		}
		#endregion
	}

    /// <summary>
    /// Base class for the renderers of the analog meter
    /// </summary>
    public class LBAnalogMeterRenderer
    {
        #region Variables
        /// <summary>
        /// Control to render
        /// </summary>
        private xbdAnalogMeter meter = null;
        #endregion

        #region Properies

        /// <summary>
        /// AnalogMeter
        /// </summary>
        public xbdAnalogMeter AnalogMeter
        {
            set { this.meter = value; }
            get { return this.meter; }
        }
        #endregion

        #region Virtual method
        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBody(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the thresholds 
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawThresholds(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Drawt the unit measure of the control
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawUM(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the current value in numerical form
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawValue(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the needle 
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawNeedle(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the needle cover at the center
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawNeedleCover(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the glass effect
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            return false;
        }
        #endregion
    }



    /// <summary>
    /// Default renderer class for the analog meter
    /// </summary>
    public class LBDefaultAnalogMeterRenderer : LBAnalogMeterRenderer
    {
        /// <summary>
        /// DrawBackground
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawBackground(Graphics gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            Color c = this.AnalogMeter.BackColor;
            SolidBrush br = new SolidBrush(c);
            Pen pen = new Pen(c);

            Rectangle _rcTmp = new Rectangle(0, 0, this.AnalogMeter.Width, this.AnalogMeter.Height);
            gr.DrawRectangle(pen, _rcTmp);
            gr.FillRectangle(br, rc);

            return true;
        }


        /// <summary>
        /// DrawBody
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawBody(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            Color bodyColor = this.AnalogMeter.BodyColor;
            Color cDark = LBColorManager.StepColor(bodyColor, 20);

            LinearGradientBrush br1 = new LinearGradientBrush(rc,
                                                               bodyColor,
                                                               cDark,
                                                               45);
            Gr.FillEllipse(br1, rc);

            float drawRatio = this.AnalogMeter.GetDrawRatio();

            RectangleF _rc = rc;
            _rc.X += 3 * drawRatio;
            _rc.Y += 3 * drawRatio;
            _rc.Width -= 6 * drawRatio;
            _rc.Height -= 6 * drawRatio;

            LinearGradientBrush br2 = new LinearGradientBrush(_rc,
                                                               cDark,
                                                               bodyColor,
                                                               45);
            Gr.FillEllipse(br2, _rc);

            return true;
        }

        /// <summary>
        /// DrawThresholds
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawThresholds(Graphics gr, RectangleF rc)
        {
            return false;
        }


        /// <summary>
        /// DrawDivisions
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            PointF needleCenter = this.AnalogMeter.GetNeedleCenter();
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            float scaleDivisions = this.AnalogMeter.ScaleDivisions;
            float scaleSubDivisions = this.AnalogMeter.ScaleSubDivisions;
            float drawRatio = this.AnalogMeter.GetDrawRatio();
            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            Color scaleColor = this.AnalogMeter.ScaleColor;

            float cx = needleCenter.X;
            float cy = needleCenter.Y;
            float w = rc.Width;
            float h = rc.Height;

            float incr = LBMath.GetRadian((endAngle - startAngle) / ((scaleDivisions - 1) * (scaleSubDivisions + 1)));
            float currentAngle = LBMath.GetRadian(startAngle);
            float radius = (float)(w / 2 - (w * 0.08));
            float rulerValue = (float)minValue;

            Pen pen = new Pen(scaleColor, (2 * drawRatio));
            SolidBrush br = new SolidBrush(scaleColor);

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);
            int n = 0;
            for (; n < scaleDivisions; n++)
            {
                //Draw Thick Line
                ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
                ptEnd.X = (float)(cx + (radius - w / 20) * Math.Cos(currentAngle));
                ptEnd.Y = (float)(cy + (radius - w / 20) * Math.Sin(currentAngle));
                Gr.DrawLine(pen, ptStart, ptEnd);

                //Draw Strings
                Font font = new Font(this.AnalogMeter.Font.FontFamily, (float)(10.0f * drawRatio));

                float tx = (float)(cx + (radius - (20 * drawRatio)) * Math.Cos(currentAngle));
                float ty = (float)(cy + (radius - (20 * drawRatio)) * Math.Sin(currentAngle));
                double val = Math.Round(rulerValue);
                String str = String.Format("{0,0:D}", (int)val);

                SizeF size = Gr.MeasureString(str, font);
                Gr.DrawString(str,
                                font,
                                br,
                                tx - (float)(size.Width * 0.5),
                                ty - (float)(size.Height * 0.5));

                rulerValue += (float)((maxValue - minValue) / (scaleDivisions - 1));

                if (n == scaleDivisions - 1)
                    break;

                if (scaleDivisions <= 0)
                    currentAngle += incr;
                else
                {
                    for (int j = 0; j <= scaleSubDivisions; j++)
                    {
                        currentAngle += incr;
                        ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                        ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
                        ptEnd.X = (float)(cx + (radius - w / 50) * Math.Cos(currentAngle));
                        ptEnd.Y = (float)(cy + (radius - w / 50) * Math.Sin(currentAngle));
                        Gr.DrawLine(pen, ptStart, ptEnd);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// DrawUM
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawUM(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// DrawValue
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>

        public override bool DrawValue(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// DrawNeedle
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>

        public override bool DrawNeedle(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            float w, h;
            w = rc.Width;
            h = rc.Height;

            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            double currValue = this.AnalogMeter.Value;
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            PointF needleCenter = this.AnalogMeter.GetNeedleCenter();

            float radius = (float)(w / 2 - (w * 0.12));
            float val = (float)(maxValue - minValue);

            val = (float)((100 * (currValue - minValue)) / val);
            val = ((endAngle - startAngle) * val) / 100;
            val += startAngle;

            float angle = LBMath.GetRadian(val);

            float cx = needleCenter.X;
            float cy = needleCenter.Y;

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);

            GraphicsPath pth1 = new GraphicsPath();

            ptStart.X = cx;
            ptStart.Y = cy;
            angle = LBMath.GetRadian(val + 10);
            ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
            ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            ptStart = ptEnd;
            angle = LBMath.GetRadian(val);
            ptEnd.X = (float)(cx + radius * Math.Cos(angle));
            ptEnd.Y = (float)(cy + radius * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            ptStart = ptEnd;
            angle = LBMath.GetRadian(val - 10);
            ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
            ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            pth1.CloseFigure();

            SolidBrush br = new SolidBrush(this.AnalogMeter.NeedleColor);
            Pen pen = new Pen(this.AnalogMeter.NeedleColor);
            Gr.DrawPath(pen, pth1);
            Gr.FillPath(br, pth1);

            return true;
        }

        /// <summary>
        /// DrawNeedleCover
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawNeedleCover(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            Color clr = this.AnalogMeter.NeedleColor;
            RectangleF _rc = rc;
            float drawRatio = this.AnalogMeter.GetDrawRatio();

            Color clr1 = Color.FromArgb(70, clr);

            _rc.Inflate(5 * drawRatio, 5 * drawRatio);

            SolidBrush brTransp = new SolidBrush(clr1);
            Gr.FillEllipse(brTransp, _rc);

            clr1 = clr;
            Color clr2 = LBColorManager.StepColor(clr, 75);
            LinearGradientBrush br1 = new LinearGradientBrush(rc,
                                                               clr1,
                                                               clr2,
                                                               45);
            Gr.FillEllipse(br1, rc);
            return true;
        }

        /// <summary>
        /// DrawGlass
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public override bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            if (this.AnalogMeter.ViewGlass == false)
                return true;

            Color clr1 = Color.FromArgb(40, 200, 200, 200);

            Color clr2 = Color.FromArgb(0, 200, 200, 200);
            LinearGradientBrush br1 = new LinearGradientBrush(rc,
                                                               clr1,
                                                               clr2,
                                                               45);
            Gr.FillEllipse(br1, rc);

            return true;
        }
    }

    /// <summary>
    /// LBMath
    /// </summary>
    public class LBMath : Object
    {

        /// <summary>
        /// GetRadian
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float GetRadian(float val)
        {
            return (float)(val * Math.PI / 180);
        }
    }

    /// <summary>
    /// Manager for color
    /// </summary>
    public class LBColorManager : Object
    {
        public static double BlendColour(double fg, double bg, double alpha)
        {
            double result = bg + (alpha * (fg - bg));
            if (result < 0.0)
                result = 0.0;
            if (result > 255)
                result = 255;
            return result;
        }

        public static Color StepColor(Color clr, int alpha)
        {
            if (alpha == 100)
                return clr;

            byte a = clr.A;
            byte r = clr.R;
            byte g = clr.G;
            byte b = clr.B;
            float bg = 0;

            int _alpha = Math.Min(alpha, 200);
            _alpha = Math.Max(alpha, 0);
            double ialpha = ((double)(_alpha - 100.0)) / 100.0;

            if (ialpha > 100)
            {
                // blend with white
                bg = 255.0F;
                ialpha = 1.0F - ialpha;  // 0 = transparent fg; 1 = opaque fg
            }
            else
            {
                // blend with black
                bg = 0.0F;
                ialpha = 1.0F + ialpha;  // 0 = transparent fg; 1 = opaque fg
            }

            r = (byte)(LBColorManager.BlendColour(r, bg, ialpha));
            g = (byte)(LBColorManager.BlendColour(g, bg, ialpha));
            b = (byte)(LBColorManager.BlendColour(b, bg, ialpha));

            return Color.FromArgb(a, r, g, b);
        }
    };
}
