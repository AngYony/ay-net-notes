using System;

namespace Generics_Sample
{
    public interface IDisplay<in T>
    {
        void Show(T item);
    }

    public interface IIndex<out T>
    {
        int Count { get; }

        //定义一个索引器
        T this[int index] { get; }
    }

    public class Rectangle : Shape
    {
    }

    public class RectangleCollection : IIndex<Rectangle>
    {
        private Rectangle[] data = new Rectangle[3] {
             new Rectangle{Height=2,Width=5},
             new Rectangle{ Height=3, Width=7},
             new Rectangle{ Height=4.5, Width=2.9}
        };
        public int Count => data.Length;

        public Rectangle this[int index]
        {
            get
            {
                if (index < 0 || index > data.Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return data[index];
            }
        }
    }

    public class Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        //重写Object的ToString方法
        public override string ToString() => $"Width:{Width},Height:{Height}";
    }
    public class ShapeDisplay : IDisplay<Shape>
    {
        public void Show(Shape item)
        {
            Console.WriteLine($"{item.GetType().Name}  Width:{item.Width},Height:{item.Height}");
        }
    }

    public class TestRun
    {
        //public Rectangle Display(Shape o)
        //{
        //    Rectangle r = o;
        //    return o;
        //}

        //public void Run()
        //{
        //    var r = new Rectangle { Width = 5, Height = 3.5 };
        //    Display(r);

        //    r.ToString()

        //}
    }
}

