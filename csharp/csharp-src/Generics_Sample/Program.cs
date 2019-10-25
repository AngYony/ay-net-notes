using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 
            //var list1 = new LinkedList();
            //list1.AddLast(2);
            //list1.AddLast(3);
            //list1.AddLast("4");
            //foreach (var i in list1)
            //{
            //    Console.WriteLine(i);
            //}
            //Console.Read();
            #endregion

            #region
            //var list2 = new LinkedList<string>();
            //list2.AddLast("java");
            //list2.AddLast("c#");
            //list2.AddLast("python");
            //foreach (string i in list2)
            //{
            //    Console.WriteLine(i);
            //}
            //Console.Read();
            #endregion

            #region 
            ////DocumentManager的调用
            //var dm = new DocumentManager<Document>();
            //dm.AddDocument(new Document("title A", "sample A"));
            //dm.AddDocument(new Document("title B", "sample B"));
            //dm.DisplayAllDocuments();
            //if (dm.IsDocumentAvailable)
            //{
            //    Document d = dm.GetDocument();
            //    Console.WriteLine(d.Content);
            //}
            //Console.Read();
            #endregion

            #region 泛型静态成员调用
            //StaticDemo<string>.x = "abc";
            //StaticDemo<int>.x = 13;
            //StaticDemo<string>.y = 2;
            //StaticDemo<int>.y = 10;

            //Console.WriteLine(StaticDemo<string>.x); //结果：abc
            //Console.WriteLine(StaticDemo<int>.x);    //结果：13
            //Console.WriteLine(StaticDemo<string>.y); //结果：2
            //Console.WriteLine(StaticDemo<int>.y);    //结果：10
            //Console.Read();
            #endregion

            #region 协变调用
            //IIndex<Rectangle> rectangles = new RectangleCollection();
            ////由于采用了协变，此处可以直接使用父类Shape引用子类Rectangle相关对象
            //IIndex<Shape> shapes = rectangles;
            //IIndex<Shape> shapes2 = new RectangleCollection();

            //for (int i = 0; i < shapes.Count; i++)
            //{
            //    Console.WriteLine(shapes[i]);
            //}

            #endregion

            #region 逆变调用
            //IDisplay<Shape> sd = new ShapeDisplay();
            //IDisplay<Rectangle> rectangleDisplay = sd;
            //rectangleDisplay.Show(rectangles[0]);

            //Console.Read();
            #endregion


            #region 协变逆变测试
            //List<string> listA = new List<string>();
            //IList<string> iListA = listA;
            //IEnumerable<string> iEnumerableA = listA;
            //IEnumerable<string> iEnumerableA2 = iListA;


            //List<object> listB = new List<string>(); //报错，不会通过编译
            //IList<object> iListB = new List<string>(); //报错，不会通过编译

            //IEnumerable<object> iEnumerableB = new List<string>();
            //IEnumerable<object> iEnumerableB2 = iListA;


            //IEnumerable<object> integers = new List<int>();



            //IEnumerable<object> c = a;

            //IEnumerable<string> a2 = new List<string>();
            //IEnumerable<object> b2 = new List<string>();
            #endregion

            #region 泛型方法调用
            //var accounts = new List<Account> {
            //    new Account("书籍",234),
            //    new Account("文具",56),
            //    new Account("手机",2300)
            //};
            ////decimal amount= Algorithms.AccumulateSimple(accounts);
            ////Console.WriteLine(amount);

            //////该语句简化自decimal amount2 = Algorithms.Accumulate<Account>(accounts);
            ////decimal amount2 = Algorithms.Accumulate(accounts);
            ////Console.WriteLine(amount2);

            //decimal amount = Algorithms.Accumulate<Account, decimal>(
            //     accounts, (item, sum) => sum += item.Balance);
            //Console.WriteLine(amount);

            //Console.Read();
            #endregion

            #region 泛型方法重载调用
            var test = new MethodOverloads();
            test.Foo(33);
            test.Foo("abc");
            test.Foo("abc", 42);
            test.Foo(11, "abc");
            test.Bar(44);
            Console.Read();

            #endregion
        }
    }
}
