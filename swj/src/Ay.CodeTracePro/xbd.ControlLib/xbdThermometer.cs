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
{    /// <summary>
     /// Enum TemperatureUnit
     /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>
        /// ����ʾ
        /// </summary>
        None,
        /// <summary>
        /// ���϶�
        /// </summary>
        C,
        /// <summary>
        /// ���϶�
        /// </summary>
        F,
        /// <summary>
        /// ���϶�
        /// </summary>
        K,
        /// <summary>
        /// ���϶�
        /// </summary>
        R,
        /// <summary>
        /// ���϶�
        /// </summary>
        Re
    }

    /// <summary>
    /// xktThermometer
    /// </summary>
    public partial class xbdThermometer : UserControl
    {
        /// <summary>
        /// xktThermometer
        /// </summary>
        public xbdThermometer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.SizeChanged += Thermometer_SizeChanged;
            this.Size = new Size(70, 200);
        }

        /// <summary>
        /// The glass tube color
        /// </summary>
        private Color glassTubeColor = Color.FromArgb(211, 211, 211);

        /// <summary>
        /// Gets or sets the color of the glass tube.
        /// </summary>
        /// <value>The color of the glass tube.</value>
        [Description("���û��ȡ��������ɫ"), Category("�Զ�������")]
        public Color GlassTubeColor
        {
            get { return glassTubeColor; }
            set
            {
                glassTubeColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The mercury color
        /// </summary>
        private Color mercuryColor = Color.FromArgb(255, 77, 59);

        /// <summary>
        /// Gets or sets the color of the mercury.
        /// </summary>
        /// <value>The color of the mercury.</value>
        [Description("���û��ȡˮ����ɫ"), Category("�Զ�������")]
        public Color MercuryColor
        {
            get { return mercuryColor; }
            set
            {
                mercuryColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The minimum value
        /// </summary>
        private float minValue = 0;
        /// <summary>
        /// ���̶���Сֵ
        /// </summary>
        /// <value>The minimum value.</value>
        [Description("���û��ȡ���̶���Сֵ"), Category("�Զ�������")]
        public float MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The maximum value
        /// </summary>
        private float maxValue = 100;
        /// <summary>
        /// ���̶����ֵ
        /// </summary>
        /// <value>The maximum value.</value>
        [Description("���û��ȡ���̶����ֵ"), Category("�Զ�������")]
        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The m value
        /// </summary>
        private float m_value = 10;
        /// <summary>
        /// ���̶�ֵ
        /// </summary>
        /// <value>The value.</value>
        [Description("���û��ȡ���̶�ֵ"), Category("�Զ�������")]
        public float Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The split count
        /// </summary>
        private int splitCount = 1;
        /// <summary>
        /// �̶ȷָ�����
        /// </summary>
        /// <value>The split count.</value>
        [Description("���û��ȡ�̶ȷָ�����"), Category("�Զ�������")]
        public int SplitCount
        {
            get { return splitCount; }
            set
            {
                if (value <= 0)
                    return;
                splitCount = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ʾ�����ֵ����塣
        /// </summary>
        /// <value>The font.</value>
        [Description("��ȡ�����ÿؼ���ʾ�����ֵ�����"), Category("�Զ�������")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ǰ��ɫ��
        /// </summary>
        /// <value>The color of the fore.</value>
        [Description("��ȡ�����ÿؼ������ּ��̶���ɫ"), Category("�Զ�������")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The left temperature unit
        /// </summary>
        private TemperatureUnit leftTemperatureUnit = TemperatureUnit.C;
        /// <summary>
        /// ���̶ȵ�λ
        /// </summary>
        /// <value>The left temperature unit.</value>
        [Description("���̶ȵ�λ������ΪNone"), Category("�Զ�������")]
        public TemperatureUnit LeftTemperatureUnit
        {
            get { return leftTemperatureUnit; }
            set
            {
                if (value == TemperatureUnit.None)
                    return;
                leftTemperatureUnit = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The right temperature unit
        /// </summary>
        private TemperatureUnit rightTemperatureUnit = TemperatureUnit.C;
        /// <summary>
        /// �Ҳ�̶ȵ�λ����Ϊnoneʱ������ʾ
        /// </summary>
        /// <value>The right temperature unit.</value>
        [Description("�Ҳ�̶ȵ�λ����ΪNoneʱ������ʾ"), Category("�Զ�������")]
        public TemperatureUnit RightTemperatureUnit
        {
            get { return rightTemperatureUnit; }
            set
            {
                rightTemperatureUnit = value;
                this.Invalidate();
            }
        }

        private bool isUnitVisiable = false;

        /// <summary>
        /// ��λ�Ƿ���ʾ
        /// </summary>
        [Description("��λ�Ƿ���ʾ"), Category("�Զ�������")]
        public bool IsUnitVisiable
        {
            get { return isUnitVisiable ; }
            set { isUnitVisiable  = value;
                this.Invalidate();
            }
        }


        /// <summary>
        /// The m rect working
        /// </summary>
        private Rectangle m_rectWorking;
        /// <summary>
        /// The m rect left
        /// </summary>
        private Rectangle m_rectLeft;
        /// <summary>
        /// The m rect right
        /// </summary>
        private Rectangle m_rectRight;

        /// <summary>
        /// Handles the SizeChanged event of the UCThermometer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void Thermometer_SizeChanged(object sender, EventArgs e)
        {
            m_rectWorking = new Rectangle(this.Width / 2 - this.Width / 8, this.Width / 4, this.Width / 4, this.Height - this.Width / 2);
            m_rectLeft = new Rectangle(0, m_rectWorking.Top + m_rectWorking.Width / 2, (this.Width - this.Width / 4) / 2 - 2, m_rectWorking.Height - m_rectWorking.Width * 2);
            m_rectRight = new Rectangle(this.Width - (this.Width - this.Width / 4) / 2 + 2, m_rectWorking.Top + m_rectWorking.Width / 2, (this.Width - this.Width / 4) / 2 - 2, m_rectWorking.Height - m_rectWorking.Width * 2);
        }

        /// <summary>
        /// ���� <see cref="E:System.Windows.Forms.Control.Paint" /> �¼���
        /// </summary>
        /// <param name="e">�����¼����ݵ� <see cref="T:System.Windows.Forms.PaintEventArgs" />��</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //ʹ��ͼ������ߣ����������
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //�����ܹ�
            GraphicsPath path = new GraphicsPath();
            path.AddLine(m_rectWorking.Left, m_rectWorking.Bottom, m_rectWorking.Left, m_rectWorking.Top + m_rectWorking.Width / 2);
            path.AddArc(new Rectangle(m_rectWorking.Left, m_rectWorking.Top, m_rectWorking.Width, m_rectWorking.Width), 180, 180);
            path.AddLine(m_rectWorking.Right, m_rectWorking.Top + m_rectWorking.Width / 2, m_rectWorking.Right, m_rectWorking.Bottom);
            path.CloseAllFigures();
            g.FillPath(new SolidBrush(glassTubeColor), path);

            //�ײ�
            var rectDi = new Rectangle(this.Width / 2 - m_rectWorking.Width, m_rectWorking.Bottom - m_rectWorking.Width - 2, m_rectWorking.Width * 2, m_rectWorking.Width * 2);
            g.FillEllipse(new SolidBrush(glassTubeColor), rectDi);
            g.FillEllipse(new SolidBrush(mercuryColor), new Rectangle(rectDi.Left + 4, rectDi.Top + 4, rectDi.Width - 8, rectDi.Height - 8));

            //�̶�
            float decSplit = (maxValue - minValue) / splitCount;
            float decSplitHeight = m_rectLeft.Height / splitCount;
            for (int i = 0; i <= splitCount; i++)
            {
                g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectLeft.Left + 2, (float)(m_rectLeft.Bottom - decSplitHeight * i)), new PointF(m_rectLeft.Right, (float)(m_rectLeft.Bottom - decSplitHeight * i)));

                var valueLeft = (minValue + decSplit * i).ToString("0.##");
                var sizeLeft = g.MeasureString(valueLeft, Font);
                g.DrawString(valueLeft, Font, new SolidBrush(ForeColor), new PointF(m_rectLeft.Left, m_rectLeft.Bottom - (float)decSplitHeight * i - sizeLeft.Height - 1));

                if (rightTemperatureUnit != TemperatureUnit.None)
                {
                    g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectRight.Left + 2, (float)(m_rectRight.Bottom - decSplitHeight * i)), new PointF(m_rectRight.Right, (float)(m_rectRight.Bottom - decSplitHeight * i)));
                    var valueRight = GetRightValue(minValue + decSplit * i).ToString("0.##");
                    var sizeRight = g.MeasureString(valueRight, Font);
                    g.DrawString(valueRight, Font, new SolidBrush(ForeColor), new PointF(m_rectRight.Right - sizeRight.Width - 1, m_rectRight.Bottom - (float)decSplitHeight * i - sizeRight.Height - 1));
                }
                if (i != splitCount)
                {
                    if (decSplitHeight > 40)
                    {
                        var decSp1 = decSplitHeight / 10;
                        for (int j = 1; j < 10; j++)
                        {
                            if (j == 5)
                            {
                                g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectLeft.Right - 10, (m_rectLeft.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))), new PointF(m_rectLeft.Right, (m_rectLeft.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))));
                                if (rightTemperatureUnit != TemperatureUnit.None)
                                {
                                    g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectRight.Left + 10, (m_rectRight.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))), new PointF(m_rectRight.Left, (m_rectRight.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))));
                                }
                            }
                            else
                            {
                                g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectLeft.Right - 5, (m_rectLeft.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))), new PointF(m_rectLeft.Right, (m_rectLeft.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))));
                                if (rightTemperatureUnit != TemperatureUnit.None)
                                {
                                    g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectRight.Left + 5, (m_rectRight.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))), new PointF(m_rectRight.Left, (m_rectRight.Bottom - (float)decSplitHeight * i - ((float)decSp1 * j))));
                                }
                            }
                        }
                    }
                    else if (decSplitHeight > 10)
                    {
                        g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectLeft.Right - 5, (m_rectLeft.Bottom - (float)decSplitHeight * i - (float)decSplitHeight / 2)), new PointF(m_rectLeft.Right, (m_rectLeft.Bottom - (float)decSplitHeight * i - (float)decSplitHeight / 2)));
                        if (rightTemperatureUnit != TemperatureUnit.None)
                        {
                            g.DrawLine(new Pen(new SolidBrush(ForeColor), 1), new PointF(m_rectRight.Left + 5, (m_rectRight.Bottom - (float)decSplitHeight * i - (float)decSplitHeight / 2)), new PointF(m_rectRight.Left, (m_rectRight.Bottom - (float)decSplitHeight * i - (float)decSplitHeight / 2)));
                        }
                    }
                }
            }

            if (isUnitVisiable)
            {
                //��λ
                string strLeftUnit = GetUnitChar(leftTemperatureUnit);
                g.DrawString(strLeftUnit, Font, new SolidBrush(ForeColor), new PointF(m_rectLeft.Left + 2, 2));
                if (rightTemperatureUnit != TemperatureUnit.None)
                {
                    string strRightUnit = GetUnitChar(rightTemperatureUnit);
                    var rightSize = g.MeasureString(strRightUnit, Font);
                    g.DrawString(strRightUnit, Font, new SolidBrush(ForeColor), new PointF(m_rectRight.Right - 2 - rightSize.Width, 2));
                }
            }
            //ֵ
            float fltHeightValue = (float)(Value / (maxValue - minValue) * m_rectLeft.Height);
            RectangleF rectValue = new RectangleF(m_rectWorking.Left + 4, m_rectLeft.Top + (m_rectLeft.Height - fltHeightValue), m_rectWorking.Width - 8, fltHeightValue + (m_rectWorking.Height - m_rectWorking.Width / 2 - m_rectLeft.Height));
            g.FillRectangle(new SolidBrush(mercuryColor), rectValue);


            var sizeValue = g.MeasureString(m_value.ToString("0.##"), Font);
            g.DrawString(m_value.ToString("0.##"), Font, new SolidBrush(Color.White), new PointF(rectDi.Left + (rectDi.Width - sizeValue.Width) / 2, rectDi.Top + (rectDi.Height - sizeValue.Height) / 2 + 1));
        }

        /// <summary>
        /// Gets the unit character.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>System.String.</returns>
        private string GetUnitChar(TemperatureUnit unit)
        {
            string strUnit = "��";
            switch (unit)
            {
                case TemperatureUnit.C:
                    strUnit = "��";
                    break;
                case TemperatureUnit.F:
                    strUnit = "�H";
                    break;
                case TemperatureUnit.K:
                    strUnit = "K";
                    break;
                case TemperatureUnit.R:
                    strUnit = "��R";
                    break;
                case TemperatureUnit.Re:
                    strUnit = "��Re";
                    break;
            }
            return strUnit;
        }

        /// <summary>
        /// Gets the right value.
        /// </summary>
        /// <param name="decValue">The decimal value.</param>
        /// <returns>System.Decimal.</returns>
        private float GetRightValue(float decValue)
        {
            //�Ƚ����Ļ���Ϊ���϶�
            var dec = decValue;
            switch (leftTemperatureUnit)
            {
                case TemperatureUnit.F:
                    dec = (decValue - 32) / (9.0f / 5.0f);
                    break;
                case TemperatureUnit.K:
                    dec = decValue - 273;
                    break;
                case TemperatureUnit.R:
                    dec = decValue / (5.0f / 9.0f) - 273.15f;
                    break;
                case TemperatureUnit.Re:
                    dec = decValue / 1.25f;
                    break;
                default:
                    break;
            }

            switch (rightTemperatureUnit)
            {
                case TemperatureUnit.C:
                    return dec;
                case TemperatureUnit.F:
                    return 9.0f / 5.0f * dec + 32;
                case TemperatureUnit.K:
                    return dec + 273;
                case TemperatureUnit.R:
                    return (dec + 273.15f) * (5.0f / 9.0f);
                case TemperatureUnit.Re:
                    return dec * 1.25f;
            }
            return decValue;
        }
    }
}
