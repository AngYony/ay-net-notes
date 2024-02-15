using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace S12_10
{
    public class Quadrilateral : Shape
    {
        #region 表示依赖项属性的字段
        public static readonly DependencyProperty Point1Property = null;
        public static readonly DependencyProperty Point2Property = null;
        public static readonly DependencyProperty Point3Property = null;
        public static readonly DependencyProperty Point4Property = null;
        #endregion

        #region 注册依赖项属性
        static Quadrilateral()
        {
            Point1Property = DependencyProperty.Register("Point1", typeof(Point), typeof(Quadrilateral), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
            Point2Property = DependencyProperty.Register("Point2", typeof(Point), typeof(Quadrilateral), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
            Point3Property = DependencyProperty.Register("Point3", typeof(Point), typeof(Quadrilateral), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
            Point4Property = DependencyProperty.Register("Point4", typeof(Point), typeof(Quadrilateral), new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
            // 重写Stretch依赖项属性的元数据
            StretchProperty.OverrideMetadata(typeof(Quadrilateral), new FrameworkPropertyMetadata(Stretch.Uniform));
        }
        #endregion

        #region 封装依赖项属性
        /// <summary>
        /// 四边形的第一个顶点
        /// </summary>
        public Point Point1
        {
            get { return (Point)GetValue(Point1Property); }
            set { SetValue(Point1Property, value); }
        }

        /// <summary>
        /// 四边形的第二个顶点
        /// </summary>
        public Point Point2
        {
            get { return (Point)GetValue(Point2Property); }
            set { SetValue(Point2Property, value); }
        }

        /// <summary>
        /// 四边形的第三个顶点
        /// </summary>
        public Point Point3
        {
            get { return (Point)GetValue(Point3Property); }
            set { SetValue(Point3Property, value); }
        }

        /// <summary>
        /// 四边形的第四个顶点
        /// </summary>
        public Point Point4
        {
            get { return (Point)GetValue(Point4Property); }
            set { SetValue(Point4Property, value); }
        }
        #endregion

        protected override Geometry DefiningGeometry
        {
            get
            {
                PathGeometry pg = new PathGeometry();
                // 确定路径的起点坐标
                // 左上角的顶点将作为路径起点
                PathFigure pf = new PathFigure();
                pf.StartPoint = Point1;
                pf.IsClosed = true;
                pf.IsFilled = true;
                // 将开始点与其余三个点连起来
                PolyLineSegment pline = new PolyLineSegment();
                pline.Points.Add(Point2);
                pline.Points.Add(Point3);
                pline.Points.Add(Point4);
                pf.Segments.Add(pline);
                // 将路径添加到PathGeometry中
                pg.Figures.Add(pf);
                // 返回PathGeometry对象实例
                return pg;
            }
        }
    }
}
