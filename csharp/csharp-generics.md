# C# 泛型(Generics)



## 泛型概述

泛型是C#编程语言的一部分，它与程序集中的`IL`(`Intermediate Language`，中间语言)代码紧密的集成。通过泛型，我们不必给不同的类型编写功能相同的许多方法和类，而是可以创建独立于被包含类型的一个类或方法。 例如，通过使用泛型类型参数 T，可以编写其他客户端代码能够使用的单个类，而不会产生运行时转换或装箱操作的成本或风险。使用泛型类型可以最大限度地重用代码、保护类型安全性以及提高性能。 

#### 泛型性能

泛型的一个主要优点是性能。值类型存储在栈上，引用类型存储在堆上。从值类型转换为引用类型称为装箱；从引用类型转换为值类型称为拆箱。对值类型使用非泛型集合类，常常需要将值类型和引用类型互相转换，进行装箱和拆箱操作，性能损失比较大。而使用了泛型，可以很好的解决这一问题，泛型可以不再进行装箱和拆箱操作。

#### 泛型类型安全

泛型的另一个特性是类型安全。例如，在泛型类`List<T>`中，泛型类型`T`定义了允许使用的类型。假设有一个泛型实例为`List<int>`，它在添加元素时，就只会添加类型为`int`的数值到集合中。

#### 泛型允许二进制代码重用

泛型允许更好的重用二进制代码，泛型类可以定义一次，使用许多不同的类型实例化。例如，泛型类`List<T>`可以实例化为`List<int>`、`List<string>`、`List<MyClass>`等。

#### 泛型实例化时代码生成

> 泛型类的定义会放在程序集 中，所以用特定类型实例化泛型类不会在中间语言（IL）代码中复制这些类。但是，在JIT编译器把泛型类编译为本地代码时，会给每个值类型创建一个新类。而引用类型共享同一个本地类的所有相同的实现代码。这是因为引用类型在实例化泛型类中只需要4个字节的内存地址（32位系统），就可以引用一个引用类型。值类型包含在实例化的泛型类的内存中，同时因为每个值类型对内存的要求都不同，所以要为每个值类型实例化一个新类。
>
> 注：【本段文字来自于《C#高级编程（第10版）》中的”不同的特定类型实例化泛型时创建了多少代码“相关描述】

#### 泛型类型命名约定

- 泛型类型的名称用字母`T`作为前缀。
- 如果没有特殊的要求，泛型类型允许用任意类替代，且只使用了一个泛型类型，就可以用字符T作为泛型类型的名称。例如：`public class List<T>{}`
- 如果泛型类型有特定的要求（例如，它必须实现一个接口或派生自基类），或者使用了两个或多个泛型类型，就应给泛型类型使用描述性的 名称。例如：`public class SortedList<Tkey,TValue>{}`



## 泛型类

泛型类型：也被称为泛型类型参数，它是在实例化泛型类的一个变量时，泛型声明中指定的特定类型的占位符，即泛型类中指定的T。

泛型类：定义泛型类型的类，例如`List<T>`，它无法按原样使用，因为它不是真正的类型；它更像是类型的蓝图。 若要使用 `List<T>`，客户端代码必须通过指定尖括号内的类型参数来声明并实例化构造类型。  

#### 创建泛型类

通常，创建泛型类是从现有具体类开始，然后每次逐个将类型更改为类型参数，直到泛化和可用性达到最佳平衡。 在创建泛型类之前，先建立一个简单的普通类，然后再把这个类转化为泛型类。

定义一个一般的、非泛型的简化链表类：

```c#
public class LinkedListNode
{
    public object Value { get; private set; }
    public LinkedListNode(object value)
    {
        Value = value;
    }
    public LinkedListNode Prev { get; internal set; }
    public LinkedListNode Next { get; internal set; }
}
public class LinkedList : IEnumerable
{
    public LinkedListNode First { get; private set; }
    public LinkedListNode Last { get; private set; }

    //在链表尾部添加一个新元素
    public LinkedListNode AddLast(object node)
    {
        var newNode = new LinkedListNode(node);
        if (First == null)
        {
            First = newNode;
            Last = First;
        }
        else
        {
            LinkedListNode previous = Last;
            Last.Next = newNode;
            Last = newNode;
            Last.Prev = previous;
        }
        return newNode;
    }

    //实现GetEnumerator()方法
    public IEnumerator GetEnumerator()
    {
        LinkedListNode current = First;
        while (current != null)
        {
            //使用yield语句创建一个枚举器类型
            yield return current.Value;
            current = current.Next;
        }
    }
}
```

当调用上述`LinkedList`类的`AddLast()`方法传入任意类型的值时，会进行一系列的装箱和拆箱的操作

```c#
var list1 = new LinkedList();
list1.AddLast(2);
list1.AddLast(3);
list1.AddLast("4");
foreach (var i in list1)
{
    Console.WriteLine(i);
}
```

使用泛型定义上述类

```c#
public class LinkedListNode<T>
{
    public LinkedListNode(T value)
    {
        Value = value;
    }

    public LinkedListNode<T> Next { get; internal set; }
    public LinkedListNode<T> Prev { get; internal set; }
    public T Value { get; private set; }
}

public class LinkedList<T> : IEnumerable<T>
{
    public LinkedListNode<T> First { get; private set; }
    public LinkedListNode<T> Last { get; private set; }

    public LinkedListNode<T> AddLast(T node)
    {
        var newNode = new LinkedListNode<T>(node);
        if (First == null)
        {
            First = newNode;
            Last = First;
        }
        else
        {
            LinkedListNode<T> previous = Last;
            Last.Next = newNode;
            Last = newNode;
            Last.Prev = previous;
        }
        return newNode;
    }

    public IEnumerator<T> GetEnumerator()
    {
        LinkedListNode<T> current = First;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

泛型类的定义与一般类类似，只是要使用泛型类型声明。声明后的泛型类型可以在类中用作方法或字段成员的参数类型。

调用上述中声明的方法用例如下，此时添加元素和遍历元素时都不用频繁的装箱和拆箱：

```c#
var list2 = new LinkedList<string>();
list2.AddLast("java");
list2.AddLast("c#");
list2.AddLast("python");
foreach (string i in list2)
{
    Console.WriteLine(i);
}
```

#### 泛型类功能

在创建泛型类时，可以为泛型类型指定默认值、约束、继承和静态成员等。

创建如下一个简单泛型类 ，用于从队列中读写文档。

```c#
public class DocumentManager<T>
{
    private readonly Queue<T> documentQueue = new Queue<T>();

    public bool IsDocumentAvailable => documentQueue.Count > 0;

    public void AddDocument(T doc)
    {
        lock (this)
        {
            documentQueue.Enqueue(doc);
        }
    }
}
//定义一个简单的接口
public interface IDocument
{
    string Title { get; set; }
    string Content { get; set; }
}
//实现该接口
public class Document : IDocument
{
    public Document(string title, string content)
    {
        this.Title = title;
        this.Content = content;
    }
    public string Content { get; set; }
    public string Title { get; set; }
}
```

**泛型类型默认值**

在上述类`DocumentManager<T>`中添加如下方法：

```c#
public T GetDocument()
{
    //default将泛型类型的值初始化为null或者0，取决于泛型类型是引用类型还是值类型。
    T doc = default(T);
    lock (this)
    {
        doc = documentQueue.Dequeue();
    }
    return doc;
}
```

该方法直接返回类型`T`的值，由于不能把`null`赋予泛型类型，原因是泛型类型可以实例化为值类型，而`null`只能用于引用类型，因此为了解决这个问题，使用了`default`关键字来代替`T doc=null;` 通过`default`关键字，可以自动的将`null`赋予引用类型，将`0`赋予值类型，而不用管`T`具体是哪种类型。

**泛型类型约束**

如果泛型类（定义泛型类型的类，如`DocumentManager`）需要调用泛型类型（T）中的方法，就必须添加约束。

例如，在泛型类`DocumentManager<T>`中添加`DisplayAllDocuments()`方法用于显示泛型类型T对应的Title值，需要强制进行类型转换，如下：

```c#
public void DisplayAllDocuments()
{
    foreach (T doc in documentQueue)
    {
        Console.WriteLine(((IDocument)doc).Title);
    }
}

```

一旦类型`T`没有实现`IDocument`接口，上述类型转换就会存在错误，此时最好的做法就是为泛型类添加一个约束：`T `必须实现`IDocument`接口。

```c#
public class DocumentManager<TDocument> where TDocument : IDocument
{
    private readonly Queue<TDocument> documentQueue = new Queue<TDocument>();

    public bool IsDocumentAvailable => documentQueue.Count > 0;

    public void AddDocument(TDocument doc)
    {
        lock (this)
        {
            documentQueue.Enqueue(doc);
        }
    }

    public TDocument GetDocument()
    {
        //default将泛型类型的值初始化为null或者0，取决于泛型类型是引用类型还是值类型。
        TDocument doc = default(TDocument);
        lock (this)
        {
            doc = documentQueue.Dequeue();
        }
        return doc;
    }
    public void DisplayAllDocuments()
    {
        foreach (TDocument doc in documentQueue)
        {
            Console.WriteLine(doc.Title);
        }
    }
}
```

注意：给泛型类型 添加约束时，最好包含泛型参数名称的一些信息，此示例使用`TDocument`来代替`T`。调用上述代码如下：

```c#
var dm = new DocumentManager<Document>();
dm.AddDocument(new Document("title A", "sample A"));
dm.AddDocument(new Document("title B", "sample B"));
dm.DisplayAllDocuments();
if (dm.IsDocumentAvailable)
{
    Document d = dm.GetDocument();
    Console.WriteLine(d.Content);
}
```

泛型类型支持的约束类型

| 约束                    | 说明                                                         |
| ----------------------- | ------------------------------------------------------------ |
| `where T:struct`        | 类型参数必须是值类型。 可以指定除`Nullable`以外的任何值类型。 |
| ` where T : unmanaged ` | `unmanaged` 约束指定类型参数必须为“非托管类型”。 “非托管类型”不是引用类型，所以该约束指定类型参数不能是引用类型，并且任何嵌套级别均不能包含任何引用类型成员。 |
| `where T:class`         | 类约束指定类型`T`必须是引用类型 。此约束还应用于任何类、接口、委托或数组类型。 |
| `where T:IFoo`          | 指定类型`T`必须实现接口`IFoo`                                |
| `where T:Foo`           | 指定类型`T`必须派生自基类`Foo`                               |
| `where T:new()`         | 这是一个构造函数约束，指定类型T必须有一个默认构造函数。当与其他约束一起使用时，`new()` 约束必须最后指定。 |
| `where T1:T2`           | 这个约束也可以指定，类型`T1`派生自泛型类型`T2`               |

某些约束是互斥的。 所有值类型必须具有可访问的无参数构造函数。 `struct` 约束包含 `new()` 约束，且 `new()` 约束不能与 `struct` 约束结合使用。 `unmanaged` 约束包含 `struct` 约束。 `unmanaged` 约束不能与 `struct` 或 `new()` 约束结合使用。 

从 C# 7.3 开始，可使用 `unmanaged` 约束来指定类型参数必须为“非托管类型”。 “非托管类型”不是引用类型，且任何嵌套级别都不包含引用类型字段。 

注意：只能为默认构造函数定义构造函数约束，不能为其他构造函数定义构造函数约束。

使用泛型类型可以合并多个约束：

```c#
  public class MyClass<T>
        where T : IFoo, new(){    }
```

上述声明表示类型T必须实现`IFoo`接口，且必须有一个默认构造函数。

注意：在C#中，`where`子句不能定义必须由泛型类型实现的运算符。运算符不能在接口中定义。在`where`子句中，只能定义基类、接口、和默认构造函数。

**泛型类型继承**

泛型类型可以实现泛型接口，也可以派生自一个泛型基类，其要求是必须重复基类的泛型类型，或者必须指定基类的类型。

```c#
public class Base<T> { }
public class Derived<T> : Base<T> { }
public class Derived_2<T> : Base<string> { }
```

派生类（子类）可以是泛型类或非泛型类，例如，可以定义一个抽象的泛型基类，它在派生类中用一个具体的类实现：

```c#
public abstract class Calc<T>
{
    public abstract T Add(T x, T y);
    public abstract T Sub(T x, T y);
}

public class IntCalc : Calc<int>
{
    public override int Add(int x, int y) => x + y;
    public override int Sub(int x, int y) => x - y;
}
```

还可以创建一个部分的特殊操作，如下：

```c#
public class Query<TRequest, TResult> { }
public class StringQuery<TRequest> : Query<TRequest, string> { }
```

上述中`StringQuery`继承自`Query`，只定义了一个泛型参数，为基类的`TResult`指定为`string`，要实例化`StringQuery`，只需要提供`TRequest`的类型。

**泛型静态成员**

应该减少泛型静态成员的使用，泛型类的静态成员只能在对应的同一个类实例中共享。

```c#
public class StaticDemo<T>
{
    public static T x; //此处变量x为T类型
    public static int y;
    //泛型静态成员调用
    StaticDemo<string>.x = "abc";
    StaticDemo<int>.x = 13;
    StaticDemo<string>.y = 2;
    StaticDemo<int>.y = 10;

	Console.WriteLine(StaticDemo<string>.x); //结果：abc
	Console.WriteLine(StaticDemo<int>.x);    //结果：13
	Console.WriteLine(StaticDemo<string>.y); //结果：2
	Console.WriteLine(StaticDemo<int>.y);    //结果：10
}
```



## 泛型接口

使用泛型可以定义接口，在接口中定义的方法可以带泛型参数。.NET提供了许多泛型接口，同一个接口常常存在比较老的非泛型版本，建议在实际使用中，优先采用泛型版本去解决问题。

#### 泛型接口中的协变和逆变

为了更好的解释协变和逆变的概念，我们使用`List`、`IList`、`IEnumerable`三者做一个简单的测验。首先我们定义一个`List<string>` 实例变量`listA`，并将`listA`的值指向`IList<string>`的变量`iListA`，同时分别使用`IEnumerable<string>`去引用这两个变量。

```c#
List<string> listA = new List<string>();
IList<string> iListA = listA;
IEnumerable<string> iEnumerableA = listA;
IEnumerable<string> iEnumerableB = iListA;
```

此时代码不会产生错误，能够正常编译。因为`List<T>`派生自`IList<T>`和`IEnumerable<T>`，`IList<T>`派生自`IEnumerable<T>`，父类引用指向子类对象，所以代码可以通过编译。

注意：`IEnumerable<T>`实际上是一个变体，查看定义的源码如下：

```c#
public interface IEnumerable<out T> : IEnumerable
{
    IEnumerator<T> GetEnumerator();
}
```

特别需要注意的是泛型类型T前面的`out`关键字，它代表的就是协变。它的作用是什么？

C#中的`string`派生自`Object`类型，假设我们也想通过`object`的集合直接去引用`string`的`List`，类似于如下代码：

```c#
List<object> listB = new List<string>(); //报错，不会通过编译
IList<object> iListB = new List<string>(); //报错，不会通过编译
```

上述的两条语句均会编译失败，因为`List<T>`和`IList<T>`在泛型定义时均没有指定`out`关键字。而使用`IEnumerable<T>`可以通过编译：

```c#
IEnumerable<object> iEnumerableB = new List<string>();//代码可以正常编译
```

上述语句可以通过编译。

注意：只有引用类型才支持使用泛型接口中的变体。 值类型不支持变体。 如下语句将会编译报错：

```c#
IEnumerable<object> integers = new List<int>();//编译错误，值类型不支持变体
```

下面将用具体的示例对协变和逆变做详细说明。首先定义两个简单的类，其中`Rectangle`继承自父类`Shape`。

```c#
public class Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    //重写Object的ToString方法
    public override string ToString() => $"Width:{Width},Height:{Height}";
}
//定义子类Rectangle
public class Rectangle : Shape { }
```

**泛型接口的协变**

如果泛型类型使用`out`关键字标注，该泛型接口就是协变的。

```c#
public interface IIndex<out T>
{
    //定义一个索引器
    T this[int index] { get; }
    int Count { get; }
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
```

上述定义了一个泛型接口`IIndex`，并使用`out`标注为协变，接着定义类`RectangleCollection`实现该接口，调用上述代码如下：

```c#
IIndex<Rectangle> rectangles = new RectangleCollection();
//由于采用了协变，此处可以直接使用父类Shape相关的引用指向子类Rectangle相关的对象
IIndex<Shape> shapes = rectangles;
IIndex<Shape> shapes2 = new RectangleCollection();
for (int i = 0; i < shapes.Count; i++)
{
    Console.WriteLine(shapes[i]);
}
```

**泛型接口的逆变**

使用in关键字标注泛型类型的接口就是逆变的。

```c#
public interface IDisplay<in T>
{
    void Show(T item);
}
public class ShapeDisplay : IDisplay<Shape>
{
    public void Show(Shape item)
    {
        Console.WriteLine($"{item.GetType().Name}  Width:{item.Width},Height:{item.Height}");
    }
}
```

上述定义了一个逆变的泛型接口`IDisplay`，并使用`ShapeDisplay`实现它，注意实现时指定的类型是`Shape`，并且定义了`Show`方法，显示对应`Type`名。调用代码如下：

```c#
IDisplay<Shape> sd = new ShapeDisplay();
//由于采用了逆变，可以使用Rectangle相关的引用指向父类Shape相关的对象
IDisplay<Rectangle> rectangleDisplay = sd;
rectangleDisplay.Show(rectangles[0]); //Type将会输出为Rectangle
```

下面是我自己的理解做的一个总结

协变：使用`out`关键字标注，协助变换，既然是协助就说明是客观存在的，也就是顺应"父类引用指向子类对象"这一原则所做的转换，协变会保留分配兼容性。协变允许方法具有的返回类型比接口的泛型类型参数定义的返回类型的派生程度更大。 在.net中，大多数的参数类型类似于协变 ，比如定义了一个方法，参数为`object`，调用该方法时，可以为参数传入所有`object`派生出的子类对象。

逆变：逆反变换，违背”父类引用指向子类对象“这一原则所做的转换，和协变相反，类似于“子类引用父类相关的对象”。逆变允许方法具有的实参类型比接口的泛型形参定义的类型的派生程度更小。比如定义一个方法，方法的参数为`object`，返回的类型为`object`的子类，此时不能直接返回传入的参数，必须进行类型转换，而逆变可以很好的解决此类问题。

变体：如果泛型接口或委托的泛型参数被声明为协变或逆变，该泛型接口或委托则被称为“变体”。 



## 泛型方法

在泛型方法中，泛型类型用方法声明来定义。泛型方法可以在非泛型类中定义。如下，定义一个简单的泛型方法：

```c#
void Swap<T>(ref T x,ref T y)
{
    T temp;
    temp = x;
    x = y;
    y = temp;
}
```

注意定义的形式，泛型类型`T`需要在方法声明中（方法名的后面）指定。调用上述方法代码：

```c#
int a = 1, b = 2;
Swap<int>(ref a, ref b);
//C#编译器会通过调用该方法来获取参数的类型，所以不需要把泛型类型赋予方法调用，可简化为下述语句
Swap(ref a, ref b); //上述语句的简化写法
```

在调用泛型方法时，C#编译器会根据传入的参数自动获取类型，因此不需要把泛型类型赋予方法调用，即`Swap<int>`中的`<int>`可以不用指定（实际编码中，可以借助VS智能编码助手进行简化，使用`ctrl+.`快捷键进行调用）

#### 带约束的泛型方法

在泛型类中，泛型类型可以用where子句来限制，同样，在泛型方法，也可以使用`where`子句来限制泛型类型。

```c#
public interface IAccount
{
    decimal Balance { get; }
    string Name { get; }
}
public class Account : IAccount
{
    public Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }

    public decimal Balance { get; private set; }
    public string Name { get; }
}
```

上述定义一个简单的接口和实现的类，接着定义一个泛型方法，并且添加where子句约束，让泛型类型`TAccount`对应的传入参数必须实现接口`IAccount`。

```c#
//静态类不能被实例化
public static class Algorithms
{
    public static decimal Accumulate<TAccount>(IEnumerable<TAccount> source)
        where TAccount : IAccount
    {
        decimal sum = 0;
        foreach (TAccount a in source)
        {
            sum += a.Balance;
        }
        return sum;
    }
}
```

调用上述方法的代码：

```c#
var accounts = new List<Account> {
    new Account("书籍",234),
    new Account("文具",56),
    new Account("手机",2300)
};
//decimal amount = Algorithms.Accumulate<Account>(accounts);
//编译器会从方法的参数类型中自动推断出泛型类型参数，可以简化为下述代码进行调用
decimal amount = Algorithms.Accumulate(accounts);
```

注意：并不是所有的方法调用都不需要指定泛型参数类型，当编译器无法自动推断出类型时，需要显式的进行指定，比如带委托的泛型方法。

#### 泛型委托

这里我们用一个简单的例子来说明一下泛型委托的调用。关于委托，后续我会单独进行总结。

```c#
public static TSum Accumulate<TAccount, TSum>(
    IEnumerable<TAccount> source, //方法第一个参数
    Func<TAccount, TSum, TSum> action //方法第二个参数是一个委托
    ) where TAccount : IAccount where TSum : struct
{
    TSum sum = default(TSum);
    foreach (TAccount item in source)
    {
        sum = action(item, sum);
    }
    return sum;
}
```

该方法在声明时，指定了两个泛型类型`TSum`和`TAccount`，其中一个约束是值类型，一个约束是实现接口`IAccount`，传入的第一个参数是`IEnumerable<TAccount>`类型的，第二个参数是一个委托。在调用该方法时，编译器不能自动推断出参数类型，需要显式的指定泛型参数类型，调用该方法代码如下：

```c#
var accounts = new List<Account> {
    new Account("书籍",234),
    new Account("文具",56),
    new Account("手机",2300)
};
decimal amount = Algorithms.Accumulate<Account, decimal>(
     accounts,  //传入的参数1
     (item, sum) => sum += item.Balance //传入的参数2
     );
```



#### 参考资源

- 《C#高级编程（第10版）》
- [C#编程指南——泛型](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/generics/index)
- [C#泛型中的协变和逆变](https://docs.microsoft.com/zh-cn/dotnet/standard/generics/covariance-and-contravariance)
- [.NET中的泛型](https://docs.microsoft.com/zh-cn/dotnet/standard/generics/)



本文最后一次更改时间：2018-07-04



