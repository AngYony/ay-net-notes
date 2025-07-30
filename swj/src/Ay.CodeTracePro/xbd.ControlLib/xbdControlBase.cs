//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
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
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class xbdControlBase : UserControl, IContainerControl
    {
        /// <summary>
        /// xktControlBase
        /// </summary>
        public xbdControlBase()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }


        /// <summary>
        /// The is radius
        /// </summary>
        private bool _isRadius = false;

        /// <summary>
        /// The corner radius
        /// </summary>
        private int _cornerRadius = 24;


        /// <summary>
        /// The is show rect
        /// </summary>
        private bool _isShowRect = false;

        /// <summary>
        /// The rect color
        /// </summary>
        private Color _rectColor = Color.FromArgb(220, 220, 220);

        /// <summary>
        /// The rect width
        /// </summary>
        private int _rectWidth = 1;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _fillColor = Color.Transparent;
        /// <summary>
        /// �Ƿ�Բ��
        /// </summary>
        /// <value><c>true</c> if this instance is radius; otherwise, <c>false</c>.</value>
        [Description("�Ƿ�Բ��"), Category("�Զ���")]
        public virtual bool IsRadius
        {
            get
            {
                return this._isRadius;
            }
            set
            {
                this._isRadius = value;
                Refresh();
            }
        }
        /// <summary>
        /// Բ�ǽǶ�
        /// </summary>
        /// <value>The coner radius.</value>
        [Description("Բ�ǽǶ�"), Category("�Զ���")]
        public virtual int ConerRadius
        {
            get
            {
                return this._cornerRadius;
            }
            set
            {
                this._cornerRadius = Math.Max(value, 1);
                Refresh();
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�߿�
        /// </summary>
        /// <value><c>true</c> if this instance is show rect; otherwise, <c>false</c>.</value>
        [Description("�Ƿ���ʾ�߿�"), Category("�Զ���")]
        public virtual bool IsShowRect
        {
            get
            {
                return this._isShowRect;
            }
            set
            {
                this._isShowRect = value;
                Refresh();
            }
        }
        /// <summary>
        /// �߿���ɫ
        /// </summary>
        /// <value>The color of the rect.</value>
        [Description("�߿���ɫ"), Category("�Զ���")]
        public virtual Color RectColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// �߿���
        /// </summary>
        /// <value>The width of the rect.</value>
        [Description("�߿���"), Category("�Զ���")]
        public virtual int RectWidth
        {
            get
            {
                return this._rectWidth;
            }
            set
            {
                this._rectWidth = value;
                Refresh();
            }
        }
        /// <summary>
        /// ��ʹ�ñ߿�ʱ�����ɫ����ֵΪ����ɫ��͸��ɫ���ֵ�����
        /// </summary>
        /// <value>The color of the fill.</value>
        [Description("��ʹ�ñ߿�ʱ�����ɫ����ֵΪ����ɫ��͸��ɫ���ֵ�����"), Category("�Զ���")]
        public virtual Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// ���� <see cref="E:System.Windows.Forms.Control.Paint" /> �¼���
        /// </summary>
        /// <param name="e">�����¼����ݵ� <see cref="T:System.Windows.Forms.PaintEventArgs" />��</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Visible)
            {
                if (this._isRadius)
                {
                    this.SetWindowRegion();
                }
                else
                {
                    //�ر�Բ�Ǻ���ʾΪԭ����
                    GraphicsPath g = new GraphicsPath();
                    g.AddRectangle(base.ClientRectangle);
                    g.CloseFigure();
                    base.Region = new Region(g);
                }

                GraphicsPath graphicsPath = new GraphicsPath();
                if (this._isShowRect || (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor))
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (_isRadius)
                    {
                        graphicsPath.AddArc(0, 0, _cornerRadius, _cornerRadius, 180f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, 0, _cornerRadius, _cornerRadius, 270f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 0f, 90f);
                        graphicsPath.AddArc(0, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 90f, 90f);
                        graphicsPath.CloseFigure();
                    }
                    else
                    {
                        graphicsPath.AddRectangle(clientRectangle);
                    }
                }
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //ʹ��ͼ������ߣ����������
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                if (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor)
                    e.Graphics.FillPath(new SolidBrush(this._fillColor), graphicsPath);
                if (this._isShowRect)
                {
                    Color rectColor = this._rectColor;
                    Pen pen = new Pen(rectColor, (float)this._rectWidth);
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Sets the window region.
        /// </summary>
        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this._cornerRadius);
            base.Region = new Region(path);
        }

        /// <summary>
        /// Gets the rounded rect path.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//���Ͻ�
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);//���Ͻ�
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//���½�           
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//���½�
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">Ҫ����� Windows <see cref="T:System.Windows.Forms.Message" />��</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }

    }
}
