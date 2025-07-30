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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Drawing.Text;

namespace xbd.ControlLib
{
    public partial class xbdGauge : UserControl
    {
        public xbdGauge()
        {
            InitializeComponent();

            sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //���ÿؼ���ʽ
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }


        #region Field

        private Graphics graphics;

        private StringFormat sf;

        #endregion

        #region Property

        private Color gaugeColor = Color.Green;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("���û��ȡ�Ǳ��̵������ɫ")]
        public Color GaugeColor
        {
            get { return gaugeColor; }
            set
            {
                gaugeColor = value;
                this.Invalidate();
            }
        }

        private Color pointerColor = Color.Green;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�������Ǳ���ָ����ɫ")]
        public Color PointerColor
        {
            get
            {
                return this.pointerColor;
            }
            set
            {
                this.pointerColor = value;
                this.Invalidate();
            }
        }

        private float rangeMin = 0.0f;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ������������Сֵ")]
        public float RangeMin
        {
            get
            {
                return rangeMin;
            }
            set
            {
                this.rangeMin = value;
                this.Invalidate();
            }
        }


        private float rangeMax = 100.0f;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�������������ֵ")]
        public float RangeMax
        {
            get
            {
                return rangeMax;
            }
            set
            {
                this.rangeMax = value;
                this.Invalidate();
            }
        }


        private float currentValue = 0.0f;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����õ�ǰֵ")]
        public float CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                this.currentValue = value;

                this.Invalidate();
            }
        }


        private float rangeAlarmMin = 20.0f;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����ñ�����Сֵ")]
        public float RangeAlarmMin
        {
            get
            {
                return rangeAlarmMin;
            }
            set
            {
                this.rangeAlarmMin = value;
                this.Invalidate();
            }
        }


        private float rangeAlarmMax = 80.0f;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����ñ������ֵ")]
        public float RangeAlarmMax
        {
            get
            {
                return rangeAlarmMax;
            }
            set
            {
                this.rangeAlarmMax = value;
                this.Invalidate();
            }
        }

        public string unitText = "";
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��λ")]
        public string UnitText
        {
            get
            {
                return this.unitText;
            }
            set
            {
                this.unitText = value;
                this.Invalidate();
            }
        }



        private int bigScaleCount = 4;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ�����ô�̶ȷֶ�������1-100֮��")]
        public int BigScaleCount
        {
            get
            {
                return bigScaleCount;
            }
            set
            {
                this.bigScaleCount = value;
                this.Invalidate();
            }
        }

        private int smallScaleCount = 10;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("��ȡ������С�̶ȷֶ�������1-100֮��")]
        public int SmallScaleCount
        {
            get
            {
                return smallScaleCount;
            }
            set
            {
                this.smallScaleCount = value;
                this.Invalidate();
            }
        }


        private bool isAllowFullCircle = false;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�Ƿ���������Բ��")]
        public bool IsAllowFullCircle
        {
            get
            {
                return this.isAllowFullCircle;
            }
            set
            {
                this.isAllowFullCircle = value;
                base.Invalidate();
            }
        }


        private Color textColor = Color.Black;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�ı���ɫ")]
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                base.Invalidate();
            }
        }


        private Color alarmColor = Color.Red;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("������ɫ")]
        public Color AlarmColor
        {
            get
            {
                return this.alarmColor;
            }
            set
            {
                this.alarmColor = value;
                this.Invalidate();
            }
        }


        private int pointerRadius = 5;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("ָ��뾶")]
        public int PointerRadius
        {
            get
            {
                return pointerRadius;
            }
            set
            {
                this.pointerRadius = value;
                this.Invalidate();
            }
        }


        private int topGap = 10;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�Ϸ���϶")]
        public int TopGap
        {
            get
            {
                return topGap;
            }
            set
            {
                this.topGap = value;
                this.Invalidate();
            }
        }

        private int leftGap = 10;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�󷽼�϶")]
        public int LeftGap
        {
            get
            {
                return leftGap;
            }
            set
            {
                this.leftGap = value;
                this.Invalidate();
            }
        }

        private int scaleGap = 30;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�̶�ֵ��϶")]
        public int ScaleGap
        {
            get
            {
                return scaleGap;
            }
            set
            {
                this.scaleGap = value;
                this.Invalidate();
            }
        }

        private int textGap = 50;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�ı���϶")]
        public int TextGap
        {
            get
            {
                return textGap;
            }
            set
            {
                this.textGap = value;
                this.Invalidate();
            }
        }

        private int scaleWidth = 200;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�̶�ֵ���")]
        public int ScaleWidth
        {
            get
            {
                return scaleWidth;
            }
            set
            {
                this.scaleWidth = value;
                this.Invalidate();
            }
        }


        private int scaleHeight = 80;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("�̶�ֵ�߶�")]
        public int ScaleHeight
        {
            get
            {
                return scaleHeight;
            }
            set
            {
                this.scaleHeight = value;
                this.Invalidate();
            }
        }

        public bool isTextVisible = false;
        [Browsable(true)]
        [Category("�Զ�������")]
        [Description("ָ��ֱ��")]
        public bool IsTextVisible
        {
            get
            {
                return this.isTextVisible;
            }
            set
            {
                this.isTextVisible = value;
                this.Invalidate();
            }
        }


        #endregion

        #region Method

        private bool IsDivExactly(float a, float b)
        {
            double result = a / b;

            return (result * 1000).ToString().Contains(".");
        }


        private Result GetResult()
        {
            //�ų��������
            if (this.Height <= 2 * this.topGap)
            {
                return null;
            }
            if (this.Width <= 2 * this.leftGap)
            {
                return null;
            }

            Result result = new Result();

            if (this.isAllowFullCircle == false)
            {
                //��ȡ��Բ�İ뾶
                result.Radius = this.Height - 2 * this.topGap;

                if (this.Width >= this.leftGap * 2 + result.Radius * 2)
                {
                    result.Angle = 0.0f;
                }
                else
                {
                    result.Angle = Convert.ToSingle(Math.Acos((this.Width - 2 * leftGap) * 0.5f / result.Radius) * 180.0 / Math.PI);
                }

                result.Center = new Point(this.Width / 2, this.Height - this.topGap);

            }
            else
            {
                result.Radius = (this.Width - 2 * leftGap) / 2;

                if (this.Height < result.Radius + 2 * topGap)
                {
                    result.Radius = this.Height - 2 * topGap;
                    result.Angle = 0.0f;
                    result.Center = new Point(this.Width / 2, this.Height - this.topGap);
                }
                else
                {
                    int num = this.Height - 2 * topGap - result.Radius;

                    if (num > result.Radius)
                    {
                        num = result.Radius;
                    }

                    result.Angle = Convert.ToSingle(Math.Asin(num / result.Radius) * 180.0 / Math.PI);
                    result.Center = new Point(this.Width / 2, result.Radius + topGap);
                }
            }
            return result;
        }



        #endregion

        #region Override

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            graphics = e.Graphics;

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Result result = GetResult();

            if (result != null)
            {
                //�任����
                graphics.TranslateTransform(result.Center.X, result.Center.Y);
                //��ת����
                graphics.RotateTransform(-90 + result.Angle);

                //�Ȼ���̶�

                float transangle = (180.0f - 2 * result.Angle) / this.bigScaleCount;

                for (int i = 0; i <= this.bigScaleCount; i++)
                {
                    int polygonWidth = result.Radius > 200 ? 6 : 4;
                    int polygonHeight = result.Radius > 200 ? 12 : 8;

                    PointF[] point = new PointF[] {
                    //��һ����
                    new PointF(polygonWidth*0.5f*(-1),result.Radius*(-1)),
                    new PointF(polygonWidth*0.5f,result.Radius*(-1)),
                    new PointF(polygonWidth*0.5f,result.Radius*(-1)+polygonHeight),
                    new PointF(0,result.Radius*(-1)+2*polygonHeight),
                    new PointF(polygonWidth*0.5f*(-1),result.Radius*(-1)+polygonHeight),
                    new PointF(polygonWidth*0.5f*(-1),result.Radius*(-1)),
                    };

                    graphics.FillPolygon(new SolidBrush(gaugeColor), point);

                    //�ٻ�С�̶�
                    for (int j = 0; j < this.smallScaleCount; j++)
                    {
                        graphics.RotateTransform(transangle / this.smallScaleCount);

                        using (Pen pen = new Pen(gaugeColor, (this.Width > 300) ? 3.0f : 2.0f))
                        {
                            if (i != this.bigScaleCount && j != this.smallScaleCount)
                            {
                                graphics.DrawLine(pen, 0, result.Radius * (-1), 0, result.Radius * (-1) + 6);
                            }
                        }
                    }
                }

                //�Ƕȹ���
                graphics.RotateTransform(transangle * (-1));
                graphics.RotateTransform(result.Angle);
                graphics.RotateTransform(-90.0f);

                //���ƿ̶�ֵ

                for (int k = 0; k <= this.bigScaleCount; k++)
                {
                    //��ֵ
                    double textvalue = (this.rangeMax - this.rangeMin) / this.bigScaleCount * k + this.rangeMin;

                    //�Ƕ�
                    double textangle = (-1) * (180.0 - 2 * result.Angle) / this.bigScaleCount * k + 180.0f - result.Angle;

                    ////����
                    PointF pointF = new PointF(Convert.ToSingle((result.Radius - this.scaleGap) * Math.Cos(textangle / 180.0 * Math.PI)), Convert.ToSingle((-1) * (result.Radius - this.scaleGap) * Math.Sin(textangle / 180.0 * Math.PI)));

                    if (IsDivExactly(this.rangeMax - this.rangeMin, this.bigScaleCount))
                    {
                        graphics.DrawString(textvalue.ToString("f1"), this.Font, new SolidBrush(this.textColor), new RectangleF(pointF.X - this.scaleWidth * 0.5f, pointF.Y - this.scaleHeight * 0.5f, (float)this.scaleWidth, (float)this.scaleHeight), this.sf);
                    }
                    else
                    {
                        graphics.DrawString(textvalue.ToString(), this.Font, new SolidBrush(this.textColor), new RectangleF(pointF.X - this.scaleWidth * 0.5f, pointF.Y - this.scaleHeight * 0.5f, (float)this.scaleWidth, (float)this.scaleHeight), this.sf);
                    }
                }

                Rectangle rec;
                float value = currentValue >= this.rangeMax ? this.rangeMax : currentValue <= this.rangeMin ? this.rangeMin : currentValue;
                if (isTextVisible)
                {
                    rec = new Rectangle(-100, (result.Radius - textGap) * (-1), 200, this.Font.Height);

                    graphics.DrawString(value.ToString() + " " + this.unitText, this.Font, new SolidBrush(textColor), rec, this.sf);
                }

                //����ԭ��

                graphics.FillEllipse(new SolidBrush(pointerColor), new Rectangle(pointerRadius * (-1), pointerRadius * (-1), 2 * pointerRadius, 2 * pointerRadius));

                //�Ƕ���ת
                graphics.RotateTransform(-90 + result.Angle);

                graphics.RotateTransform((180.0f - 2 * result.Angle) / (this.rangeMax - rangeMin) * (value - rangeMin));


                Point[] points = new Point[]
                    {
                    new Point(this.pointerRadius,0),
                    new Point( this.pointerRadius/2<1?1:  this.pointerRadius/2,-result.Radius+20),
                    new Point(0,result.Radius*(-1)),
                    new Point(( this.pointerRadius/2<1?1:  this.pointerRadius/2)*(-1),-result.Radius+20),
                    new Point(this.pointerRadius*(-1),0),
                    new Point(this.pointerRadius,0),
                    };

                graphics.FillPolygon(new SolidBrush(pointerColor), points);


                //���Ʊ���

                //�ǶȻָ�
                graphics.RotateTransform(90 - result.Angle);

                graphics.RotateTransform((180.0f - 2 * result.Angle) / (this.rangeMax - rangeMin) * (value - rangeMin) * (-1.0f));

                //ȷ������
                rec = new Rectangle(-result.Radius - 5, -result.Radius - 5, result.Radius * 2 + 10, result.Radius * 2 + 10);

                Pen alarmPen = new Pen(this.alarmColor, 3.0f);
                alarmPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                alarmPen.DashPattern = new float[] { 5.0f, 1.0f };


                //�Ȼ���ߵ�
                if (this.rangeAlarmMin > this.rangeMin && this.rangeAlarmMin < this.rangeMax)
                {
                    graphics.DrawArc(alarmPen, rec, result.Angle - 180.0f, (180.0f - 2 * result.Angle) / (this.rangeMax - rangeMin) * (this.rangeAlarmMin - rangeMin));
                }

                if (this.rangeAlarmMax > this.rangeMin && this.rangeAlarmMax < this.rangeMax)
                {
                    float end = -result.Angle;

                    float sweep = (this.rangeMax - this.rangeAlarmMax) / (this.rangeMax - this.rangeMin) * (180.0f - 2 * result.Angle);

                    graphics.DrawArc(alarmPen, rec, end, -sweep);

                }

                graphics.ResetTransform();


            }
        }
        #endregion

        #region Event

        #endregion

    }

    public class Result
    {
        //���ĵ�
        public Point Center { get; set; }

        //�뾶
        public int Radius { get; set; }

        //�Ƕ�
        public float Angle { get; set; }
    }
}
