//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace xbd.WarehouseTHPro
{
    public partial class xbdCheckBox : CheckBox
    {
        private Color baseColor = Color.FromArgb(3, 25, 66);

        public xbdCheckBox() : base()
        {
            sf.Alignment = StringAlignment.Center; //居中
            sf.LineAlignment = StringAlignment.Center;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }


        private StringFormat sf = new StringFormat();
        public Color BaseColor
        {
            get { return baseColor; }
            set
            {
                baseColor = value;
                base.Invalidate();
            }
        }

        private int defaultCheckButtonWidth = 20;

        public int DefaultCheckButtonWidth
        {
            get { return defaultCheckButtonWidth; }
            set
            {
                defaultCheckButtonWidth = value;
                this.Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle checkRect;
            Rectangle textRect;

            CalculateRect(out checkRect, out textRect);


            SolidBrush brush = new SolidBrush(Color.White);
            graphics.FillRectangle(brush, checkRect);

            if (this.CheckState == CheckState.Checked)
            {
                DrawCheckedFlag(graphics, checkRect, baseColor);
            }

            Pen pen = new Pen(Color.LightGray);
            graphics.DrawRectangle(pen, checkRect);

            graphics.DrawString(Text, this.Font, new SolidBrush(this.ForeColor), textRect, this.sf);
        }

        private void CalculateRect(out Rectangle checkButtonRect, out Rectangle textRect)
        {
            checkButtonRect = new Rectangle(1, (Height - DefaultCheckButtonWidth) / 2, DefaultCheckButtonWidth, DefaultCheckButtonWidth);

            textRect = new Rectangle(checkButtonRect.Right + 2, 0, Width - checkButtonRect.Right - 4, Height);
        }

        private void DrawCheckedFlag(Graphics graphics, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[3];
            points[0] = new PointF(rect.X + rect.Width / 4.5f, rect.Y + rect.Height / 2.5f);
            points[1] = new PointF(rect.X + rect.Width / 2.5f, rect.Bottom - rect.Height / 3f);
            points[2] = new PointF(rect.Right - rect.Width / 4.0f, rect.Y + rect.Height / 4.5f);

            Pen pen = new Pen(color, 2F);
            graphics.DrawLines(pen, points);
        }


    }
}
