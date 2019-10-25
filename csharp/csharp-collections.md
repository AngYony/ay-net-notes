# C#集合

有两种主要的集合类型：泛型集合和非泛型集合。 泛型集合被添加在 .NET Framework 2.0 中，并提供编译时类型安全的集合。 因此，泛型集合通常能提供更好的性能。 构造泛型集合时，它们接受类型形参；并在向该集合添加项或从该集合删除项时无需在Object类型间来回转换。 



## 集合接口和类型

- [System.Array](https://docs.microsoft.com/zh-cn/dotnet/api/system.array) ：用于数组，提供创建，操作，搜索和排序数组的方法，是所有数组的基类。 
- [System.Collections](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections) ：是`ArrayList`、`Queue`、`Hashtable`等基类，包含定义对象的各种集合。
- [System.Collections.Concurrent](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent) ：提供了线程安全的集合类。
- [System.Collections.Generic](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic) ：包含接口和定义泛型集合，它允许用户创建强类型集合能提供比非泛型强类型集合更好的类型安全和性能等级。 
- [System.Collections.Specialized](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized)：包含强类型集合。专用于特定类型的集合类。
- [System.Collections.Immutable](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable) ：包含了定义不可变的集合接口和类 。



## 列表`List<T>`

该类派生自如下接口和类：

```c#
public class List<T> : System.Collections.Generic.ICollection<T>, 
System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IList<T>, 
System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.Generic.IReadOnlyList<T>, 
System.Collections.IList,System.Collections.IEnumerable,System.Collections.ICollection
```

#### 创建列表

可以调用默认的构造函数创建列表对象。

```c#
List<int> intList = new List<int>();
```

使用构造函数创建一个空列表，当元素添加到列表中后，列表的容量就会扩大为可接纳4个元素，当添加第5个元素时，列表的容量大小就会被重新设置为包含8个元素，如果8个元素还不够 ，列表的容量大小就会被设置为16个元素，每次超出已有的容量大小后，都会将列表的容量重新设置为原来的2倍。

使用`Capacity`属性可以获取该列表的容量大小。下面将使用一个示例来说明添加元素后，`Capacity`的值是如何变化的。

```c#
List<int> intList = new List<int>();
//获取初始容量大小
Console.WriteLine("初始容量大小：" + intList.Capacity);
intList.Add(1);
Console.WriteLine($"添加了一个元素后，容量大小为：{intList.Capacity}");
//获取或设置该内部数据结构在不调整大小的情况下能够容纳的元素总数
intList.Capacity = 5;
Console.WriteLine("设置了指定的容量大小为5后：" + intList.Capacity);
intList.AddRange(new[] { 2, 3, 4, 5, 6 });
Console.WriteLine($"添加了{intList.Count}个元素后，容量大小为：{intList.Capacity}");
```

上述的输出结果依次为：

```
初始容量大小：0
添加了一个元素后，容量大小为：4
设置了指定的容量大小为5后：5
添加了6个元素后，容量大小为：10
>
```

如果元素添加到列表后，还有多余的容量大小，可以调用`TrimExcess()`方法，去除不需要的容量。

注意：如果未使用的容量小于总容量的10％，则列表不会调整大小 。

接着上述示例执行下述代码：

```C#
Console.WriteLine($"原来的元素个数为：{intList.Count} 容量大小为：" + intList.Capacity);
intList.TrimExcess();
Console.WriteLine("调用了TrimExcess()方法后，容量大小为：" + intList.Capacity);
//重新调整容量大小，未使用容量小于总容量10%
intList.Capacity = 7;
intList.TrimExcess();
Console.WriteLine($"最终元素个数为：{intList.Count} 容量大小为：" + intList.Capacity);
```

输出结果为：

```
原来的元素个数为：6 容量大小为：10
调用了TrimExcess()方法后，容量大小为：6
最终元素个数为：6 容量大小为：7
```

#### 初始化集合并设定值

```c#
intList = new List<int>() { 1, 2, 3 };
intList = new List<int> { 4, 5, 6 };
```

#### 添加或插入元素

使用`Add()`方法可以给列表添加一个元素，使用`AddRange()`方法可以一次给集合添加多个元素。使用`Insert()`方法可以在指定位置插入元素：

```c#
intList.Add(7);
intList.AddRange(new int [] { 7, 8, 9 });
intList.Insert(2, 0);
//4	 5	0	6	7	7	8	9	
```

#### 访问元素

实现了`IList`和`IList<T>`接口的所有类都提供了一个索引器，因此可以使用索引下标的形式进行访问指定索引位置的元素。索引下标从0开始。

```c#
> Console.Write(intList[2]);
0
```

因为`List<T>`集合类实现了`IEnumerate`接口，所以可以使用`foreach`语句进行遍历集合中的元素。

#### 删除元素

使用`RemoveAt()`方法移除指定索引位置的元素，使用`Remove()`方法移除指定元素。使用`RemoveRange()`方法可以从集合中删除多个元素。使用`RemoveAll()`方法可以删除集合中的所有的元素。

注意：推荐使用`RemoveAt()`方法按索引删除元素，因为它比`Remove()`方法执行的要快，`Remove()`方法会先在集合中搜索元素，搜索的过程中会调用`Equals()`方法，然后使用`IndexOf()`方法获取元素的索引，再使用该索引删除元素。

```c#
intList.RemoveAt(2);//删除索引2的元素
intList.Remove(7);//删除元素7
intList.RemoveRange(4, 2);//删除索引为4及之后的2个元素
intList.RemoveAll(a => a > 5); //删除值大于5的元素
```

#### 搜索元素

可以通过索引或元素本身搜索元素。可以使用的方法有：`IndexOf()、LastIndexOf()、FindIndex()、FindLastIndex()、Find()、FindLast()`等。判断元素是否存在可以使用Exists()方法。除了这些方法，实际应用中还包括Linq可以使用的方法。具体使用，请查看[官方文档](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.list-1?view=netframework-4.7.2#methods)。

#### 排序

`List<T>`类可以使用`Sort()`方法对元素进行排序。`Sort()`方法有如下几个重载方法：

```c#
public void Sort(int index, int count, IComparer<T> comparer);
public void Sort(Comparison<T> comparison);
public void Sort();
public void Sort(IComparer<T> comparer);
```

只有集合中的元素实现了`IComparable`接口，才能使用不带参数的`Sort()`方法。

如果需要按照元素类型 不 默认支持的方式排序，就需要使用其他重载方法，比如传递一个实现了`IComparer<T>`接口的对象。

下面将用一个具体的示例进行说明：

```c#
public class Racer : IComparable<Racer>
{
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public int Wins { get; set; }
	//实现接口中的方法
    public int CompareTo(Racer other)
    {
        int compare = LastName?.CompareTo(other?.LastName) ?? -1;
        if (compare == 0)
        {
            return FirstName?.CompareTo(other?.FirstName) ?? -1;
        }
        return compare;
    }
	//定义构造函数
    public Racer(int id, string firstName, string lastName, string country, int wins)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Country = country;
        this.Wins = wins;
    }
	//定义另一个构造函数，并调用上述构造函数
    public Racer(int id, string firstName, string lastName, string country)
        : this(id, firstName, lastName, country, wins: 0) { }
	//重写object的Tostring()方法
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
```

上述的类直接实现了`IComparable<T>`泛型接口，所以可以直接使用不带参数的`Sort()`方法进行排序，排序的依据基于重写的`CompareTo()`方法。

```c#
var racers = new List<Racer> {
    new Racer(1,"zhang","bsan","中国"),
    new Racer(3,"li","asi","中国"),
    new Racer(2,"wang","dwu","中国")
};
racers.Sort();
```

执行上述语句，将会按照`LastName`进行排序后输出，依次为`li asi、zhang bsan、wang dwu`。

对上述示例进行扩展，使用传递一个实现了`IComparer<T>`接口的对象进行排序。如下：

```c#
public class RacerComparer : IComparer<Racer>
{
	//定义一个枚举，可以直接通过类名.枚举名进行访问
    public enum CompareType
    {
        FirstName,
        LastName,
        Country,
        Wins
    }
	//定义枚举变量
    private CompareType _compareType;
    //定义构造函数，通过外部指定枚举类型
    public RacerComparer(CompareType compareType)
    {
        _compareType = compareType;
    }
	//重写接口方法
    public int Compare(Racer x, Racer y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        int result;
        switch (_compareType)
        {
            case CompareType.FirstName:
                return string.Compare(x.FirstName, y.FirstName);
            case CompareType.LastName:
                return string.Compare(x.LastName, y.LastName);
            case CompareType.Country:
                result = string.Compare(x.Country, y.Country);
                if (result == 0)
                    return string.Compare(x.LastName, y.LastName);
                else return result;
            case CompareType.Wins:
                return x.Wins.CompareTo(y.Wins);
            default:
                throw new ArgumentException("Invalid Compare Type");
        }
    }
}
```

上述类`RacerComparer` 实现了泛型接口`IComparer<T>`，其中泛型类型为`Racer`（实现`IComparer<T>`接口中的泛型类型` T`应该是将要进行排序的元素的类型），并重写了`Compare()`方法，因此可以调用`Srot(IComparer><T>`)方法进行排序。

```c#
var racers = new List<Racer> {
    new Racer(1,"zhang","bsan","中国"),
    new Racer(3,"li","asi","中国"),
    new Racer(2,"wang","dwu","中国")
};
racers.Sort(new RacerComparer(RacerComparer.CompareType.FirstName));
//将会按照FirstName进行排序
```

还可以调用`Sort(Comparison<T> comparison)`进行排序，`Comparison<T>`是一个泛型委托，它的定义如下：

```c#
public delegate int Comparison<in T>(T x, T y);
```

它需要传入两个T类型的参数，返回类型为int。如果参数值相等，该方法返回0；如果第一个参数比第二个小，返回一个小于0的值；否则，返回一个大于0的值。

比如示例中如果按照Id进行排序，可以使用如下方法调用：

```c#
//由于是委托，此处可以使用lambda表达式
racers.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
```

上述将会按照Id升序排序。

如果使用了`Sort()`进行排序后，可以调用`Reverse()`方法，逆转整个集合的排序。

#### 只读集合

> 可以调用`List<T>`集合的`AsReadOnly()`方法返回`ReadOnlyCollection<T>`类型的对象。`ReadOnlyCollection<T>`类实现的接口与`List<T>`集合相同，除此之外还实现了`IReadOnlyCollection<T>`和`IReadOnlyList`接口。因为这些接口的成员，集合不能修改。所有修改集合的方法和属性都抛出`NotSupportedException`异常。

```c#
public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, 
IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
```



## 队列`Queue<T>`

队列是其元素以**先进先出**（`Firstin，Firstout，FIFO`）的方式来处理的集合，先放入队列中的元素会先读取。队列使用`System.Collections.Generic`命名空间中的泛型类`Queue<T>`实现，它的声明如下：

```c#
[System.Runtime.InteropServices.ComVisible(false)]
public class Queue<T> : System.Collections.Generic.IEnumerable<T>, 
System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection
```

由于`Queue<T>`没有实现`ICollection<T>`泛型接口，所以不能使用这个接口中定义的`Add()`和`Remove()`方法操作元素。也因为`Queue<T>`没有实现`IList<T>`泛型接口，所以也不能使用索引下标的方式访问队列。

`Queue<T>`常用方法和属性说明：

[`Dequeue()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.queue-1.dequeue?view=netframework-4.7.2#System_Collections_Generic_Queue_1_Dequeue) ：删除并返回队列开头的元素。如果队列中没有元素，在调用该方法时，将会抛出一个`InvalidOperationException`类型的异常。

[`Enqueue(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.queue-1.enqueue?view=netframework-4.7.2#System_Collections_Generic_Queue_1_Enqueue__0_) ：将元素添加到队列的末尾。

[`Peek()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.queue-1.peek?view=netframework-4.7.2#System_Collections_Generic_Queue_1_Peek) ：返回但不删除队列开头的元素。

[`TrimExcess()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.queue-1.trimexcess?view=netframework-4.7.2#System_Collections_Generic_Queue_1_TrimExcess) ：如果该数量小于当前容量的90％，则将容量设置为队列的实际元素数。 

[`Count`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.queue-1.count?view=netframework-4.7.2#System_Collections_Generic_Queue_1_Count) ：获取队列中的元素的个数

可以使用默认的构造函数创建一个空队列，也可以使用构造函数指定容量。在把元素添加到队列中时，如果没有指定容量，将会类似于`List<T>`类，队列的容量也总是根据需要成倍增加，从而包含4、8、16和32个元素等。

下面将使用一个复杂的示例说明队列是如何使用的。首先定义一个简单的类：

```c#
public class Document
{
    public string Title { get; }
    public string Content { get; }

    public Document(string title, string content)
    {
        this.Title = title;
        this.Content = content;
    }
}
```

接着为这个类进行队列写入和读取操作：

```c#
public class DocumentManager
{
    private readonly Queue<Document> _documentQueue = new Queue<Document>();
	//向队列中添加元素
    public void AddDocument(Document doc)
    {
        //因为后面将要使用多线程，为了避免死锁，进行lock语句限制
        lock (this)
        {
            _documentQueue.Enqueue(doc);
        }
    }
	//获取队列中的元素
    public Document GetDocument()
    {
        Document doc = null;
        lock (this)
        {
            doc = _documentQueue.Dequeue();
        }
        return doc;
    }
	//队列中是否还有元素未读出
    public bool IsDocumentAvailable => _documentQueue.Count > 0;
}
```

然后定义一个操作此类的对外开放的类：

```c#
public class ProcessDocuments
{
    private DocumentManager _documentManager;

    protected ProcessDocuments(DocumentManager dm)
    {
        _documentManager = dm ?? throw new ArgumentNullException(nameof(dm));
    }

    protected async Task Run()
    {
        while (true)
        {
            if (_documentManager.IsDocumentAvailable)
            {
                Document doc = _documentManager.GetDocument();
                Console.WriteLine(doc.Title + ":" + doc.Content);
            }
            //显式的指定间隔时间，将会在此等待，从而执行该方法之外的代码部分
            await Task.Delay(new Random().Next(1000));
        }
    }
	//对外开放的调用方法
    public static void Start(DocumentManager dm)
    {
        //启动一个新的任务
        Task.Run(new ProcessDocuments(dm).Run);
    }
}
```

调用代码如下：

```c#
public static void Run()
{
    var dm = new DocumentManager();
    ProcessDocuments.Start(dm);

    for (int i = 0; i < 1000; i++)
    {
        var doc = new Document("Doc_" + i, "Content_" + i);
        dm.AddDocument(doc);
        Console.WriteLine("添加了document:Doc_" + i);
        System.Threading.Thread.Sleep(new Random().Next(1000));
    }
}
```



## 栈`Stack<T>`

栈是一个**后进先出**（`Lastin，Firstout，LIFO`）的集合，最后添加到栈中的元素会最先读取。它和队列非常的类似，只是读取元素的方法不同。栈在.NET中的声明如下：

```c#
[System.Runtime.InteropServices.ComVisible(false)]
public class Stack<T> : System.Collections.Generic.IEnumerable<T>, 
System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.ICollection
```

常用的方法和属性有 ：

[`Pop()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.stack-1.pop?view=netframework-4.7.2#System_Collections_Generic_Stack_1_Pop) ：删除并返回栈的顶部的一元素。如果栈没有元素，调用该方法将会抛出`InvalidOperationException`异常。

[`Push(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.stack-1.push?view=netframework-4.7.2#System_Collections_Generic_Stack_1_Push__0_) :在栈的顶部插入一个元素。

[`Peek()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.stack-1.peek?view=netframework-4.7.2#System_Collections_Generic_Stack_1_Peek) ：返回但不删除栈的顶部的一个元素。

[`Contains(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.stack-1.contains?view=netframework-4.7.2#System_Collections_Generic_Stack_1_Contains__0_) ：判断一个元素是否在栈中。

[`Count`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.stack-1.count?view=netframework-4.7.2#System_Collections_Generic_Stack_1_Count) ：返回栈中元素的个数。

下面使用一个简单的示例来说明栈的相关操作：

```c#
var mystacks = new Stack<int>();
mystacks.Push(1);
mystacks.Push(2);
mystacks.Push(3);
foreach(var num in mystacks)
{
    Console.Write(num+"\t"); //将会输出：3	2	1
}
Console.WriteLine();
while (mystacks.Count > 0)
{
    Console.Write(mystacks.Pop() + "\t");
}
```



## 链表`LinkedList<T>`

`LinkedList<T>`是一个双向链表，其元素指向它前面和后面的元素。

~~链表的优点是，如果将元素插入列表的中间位置，使用链表就会非常快。在往链表插入一个新的元素时，只需要修改上一个元素的Next引用和下一个元素的Previous引用，使它们的引用指向新插入的元素。~~【删除原因：表述不准确，Next和Previous都是获取值不能设置值】

~~链表的缺点是，链表的元素只能一个接一个地访问，这需要较长的时间来查找位于链表中间或尾部的元素。~~

`LinkedList<T> `在.NET中的定义：

```c#
[System.Runtime.InteropServices.ComVisible(false)]
public class LinkedList<T> : System.Collections.Generic.ICollection<T>,
System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IReadOnlyCollection<T>,
System.Collections.ICollection, System.Runtime.Serialization.IDeserializationCallback,
System.Runtime.Serialization.ISerializable
```

操作链表时，离不开泛型类`LinkedListNode<T>`，它表示`LinkedList<T>`中的节点。`LinkedListNode<T>`是一个独立定义的类，并不继承自`LinkedList<T>`，但是链表`LinkedList<T>`包含的元素节点均来自于`LinkedListNode<T>`，以下为链表`LinkedList<T>`部分常用方法和属性：

```c#
public LinkedListNode<T> Last { get; }
public LinkedListNode<T> First { get; }
public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value);
public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode);
public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value);
public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode);
public LinkedListNode<T> AddFirst(T value);
public void AddFirst(LinkedListNode<T> node);
public LinkedListNode<T> AddLast(T value);
public void AddLast(LinkedListNode<T> node);
public LinkedListNode<T> Find(T value);
public LinkedListNode<T> FindLast(T value);
```

上述大多数都与`LinkedListNode<T>`紧密相关：

```c#
[System.Runtime.InteropServices.ComVisible(false)]
public sealed class LinkedListNode<T>
{
	public LinkedListNode(T value);
	public LinkedList<T> List { get; }
	public LinkedListNode<T> Next { get; }
	public LinkedListNode<T> Previous { get; }
	public T Value { get; set; }
}
```

通过定义可以知道，使用`LinkedListNode<T>`类，可以获得列表中的下一个元素和上一个元素。`LinkedListNode<T>`定义了属性`List`（返回对应的`LinkedList<T>`对象）、`Next`、`Previous`和`Value`（返回与节点相关的元素，其类型是T）。并且它提供的属性大多数都是可读不可写。

注：`LinkedListNode<T>`的成员很少，几乎提供的全是读取的操作，因此实际操作元素比如添加、删除等，还是通过`LinkedList<T>`的成员方法（如上述展示的方法）进行调用。

下面将使用一个完整的示例说明如何使用链表进行操作，该示例使用链表让文档按照优先级进行排序显示，并且在链表中添加新文档时，新添加的文档应该放在优先级相同的最后一个文档的后面。

首先定义一个简单的类`Document_V2`，它包括基本的文档信息已经优先级：

```c#
public class Document_V2
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public byte Priority { get; private set; }

    public Document_V2(string title,string content,byte priority)
    {
        this.Title = title;
        this.Content = content;
        this.Priority = priority;
    }
}
```

接着定义一个操作该类链表的类：

```c#
public class PriorityDocumentManager
{
    //集合LinkedList<Document_V2>包含多个LinkedListNode<Document_V2>类型的元素
    private readonly LinkedList<Document_V2> _documentList;
	//定义包含LinkedListNode<Document_V2>类型的List集合，便于后续使用Next和Previous属性进行遍历
    private readonly List<LinkedListNode<Document_V2>> _priorityNodes;

    public PriorityDocumentManager()
    {
        _documentList = new LinkedList<Document_V2>();
        _priorityNodes = new List<LinkedListNode<Document_V2>>(10);
        for (int i = 0; i < 10; i++)
        {
            //添加10个类型为Document_V2的空节点（节点的Value及其他属性均为null）
            _priorityNodes.Add(new LinkedListNode<Document_V2>(null));
        }
    }

    public void AddDocument(Document_V2 d)
    {
        if (d == null) throw new ArgumentNullException("d");
        AddDocumentToPriorityNode(d, d.Priority);
    }
    private void AddDocumentToPriorityNode(Document_V2 doc, int priority)
    {
        if (priority > 9 || priority < 0)
        {
            throw new ArgumentException("等级必须为0~9");
        }
        if (_priorityNodes[priority].Value == null)
        {
            --priority;
            if (priority >= 0)
            {
                //递归调用该方法
                AddDocumentToPriorityNode(doc, priority);
            }
            else
            {
                _documentList.AddLast(doc);
                _priorityNodes[doc.Priority] = _documentList.Last;
            }
            return;
        }
        else
        {
            LinkedListNode<Document_V2> prioNode = _priorityNodes[priority];
            //判断优先级是否相同
            if (priority == doc.Priority)
            {
                _documentList.AddAfter(prioNode, doc);
                _priorityNodes[doc.Priority] = prioNode.Next;
            }
            else
            {
                LinkedListNode<Document_V2> firstPrioNode = prioNode;
                //循环遍历所有链接节点
                while (firstPrioNode.Previous != null 
                    && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                {
                    firstPrioNode = prioNode.Previous;
                    prioNode = firstPrioNode;
                }
                _documentList.AddBefore(firstPrioNode, doc);
                _priorityNodes[doc.Priority] = firstPrioNode.Previous;
            }
        }
    }
    public void DisplayAllNodes()
    {
        foreach (Document_V2 doc in _documentList)
        {
            Console.WriteLine($"priority:{doc.Priority},tilte:{doc.Title}");
        }
    }
    public Document_V2 GetDocument()
    {
        Document_V2 doc = _documentList.First.Value;
        _documentList.RemoveFirst();
        return doc;
    }
}
```

调用上述执行：

```c#
public static void Run()
{
    var pdm = new PriorityDocumentManager();

    pdm.AddDocument(new Document_V2("one", "示例一", 8));
    pdm.AddDocument(new Document_V2("two", "示例二", 3));
    pdm.AddDocument(new Document_V2("three", "示例三", 4));
    pdm.AddDocument(new Document_V2("for", "示例四", 8));
    pdm.AddDocument(new Document_V2("five", "示例五", 1));
    pdm.AddDocument(new Document_V2("six", "示例六", 9));
    pdm.AddDocument(new Document_V2("seven", "示例七", 1));
    pdm.AddDocument(new Document_V2("eight", "示例八", 1));
    pdm.DisplayAllNodes();
}
```

输出结果：

```
priority:9,title:six
priority:8,title:one
priority:8,title:for
priority:4,title:three
priority:3,title:two
priority:1,title:five
priority:1,title:seven
priority:1,title:eight
```



## 有序列表`SortedList<TKey, TValue>`

使用`SortedList<TKey, TValue>`类可以基于键对集合排序。

使用一个简单的示例对其进行操作说明：

```c#
var mysortedlist = new SortedList<string, string>();
mysortedlist.Add("one", "一");
mysortedlist.Add("two", "二");
mysortedlist.Add("three", "三");
mysortedlist.Add("four", "四");
//还可以使用索引的形式添加元素，索引参数是键
mysortedlist["five"] = "五";
//修改值
mysortedlist["three"] = "3";

foreach (var item in mysortedlist)
{
    Console.WriteLine($"{item.Key}:{item.Value}");
}
```

上述将会按照键自动的进行排序显示，显示结果：

```
five:五
four:四
one:一
three:3
two:二
```





## 字典`Dictionary<TKey, TValue>`

字典表示一种非常复杂的数据结构，由键和值组成，这种数据结构允许按照某个键来访问元素。字典也被称为映射或散列表。

#### 字典初始化

之前只能先实例一个字典对象，然后使用`Add()`方法添加元素，在C#6定义了一个新的语法，可以在声明的同时初始化字典，例如：

```c#
var dic = new Dictionary<int, string>()
{
    //第一元素的键是100
    [100] = "第一个元素",
    [200] = "第二个元素"
};
```

#### 键的类型

字典类要确定元素的位置，它就要调用`GetHashCode()`方法，`GetHashCode()`方法返回的int由字典用于计算在对应位置放置元素的索引，因此用作字典中的键的类型必须重写`Object`类的`GetHashCode()`方法。`GetHashCode()`方法的实现代码必须满足如下要求：

- 相同的对象应该总是返回相同的值。
- 不同的对象可以返回相同的值。
- 不能抛出异常。
- 至少使用一个实例字段。
- 散列代码（调用`GetHashCode`方法得到的值）在对象的生存期中不发生变化。

除了必须要满足的要求外，最好还满足如下要求：

- 它应该执行的比较快，计算开销不大。
- 散列代码值应平均分布在`int`可以存储的整个数字范围内。

注意：字典的性能 取决于`GetHashCode()`方法的实现代码。

通过`GetHashCode`得到的散列代码值的范围应该尽可能的分布在int可以存储的整个数字范围内，避免两个键返回的散列代码值得到相同的索引（字典中的索引包含一个到值的链接，一个索引项可以关联多个值，此处的索引不是指索引下标），这会降低性能，因为字典类需要寻找最近的可用空闲位置来存储第二个数据项，这需要进行一定的搜索，如果在排序时许多键都有相同的索引，这类冲突就更可能出现，所以，当计算出来的散列代码值平均分布在`int.MinValue`和`int.MaxValue`之间时，这种风险会降低到最小。

> 除了实现`GetHashCode()`方法之外，键类型还必须实现`IEquatable<T>.EQuals()`方法，或重写`Object`类的`Equals()`方法。因为不同的键对象可能返回相同的散列代码，所以字典使用`Equals()`方法来比较键。字典检查两个键A和B是否相等，并调用`A.Equals(B)`方法。这说明必须确保下述条件总是成立：
>
> 如果`A.Equals(B)`方法返回`true`，则`A.GetHashCode()`和`B.GetHashCode()`方法必须总是返回相同的散列代码。
>
> 注意：如果为`Equals()`方法提供了重写版本，但没有提供`GetHashCode()`方法的重写版本，C#编译器就会显示一个编译警告。

综上所述，应用在字典中的键，必须实现或重写`GetHashCode()`和`IEquatable<T>.EQuals()`方法。如果这两个方法都没有实现，

> 可以创建一个实现`IEqualityComparer<T>`接口的比较器，`IEqualityComparer<T>`接口定义了`GetHashCode()`和`Equals()`方法，并将传递的对象作为参数，因此可以提供与对象类型不同的实现方式。`Dictionary<TKey,TValue>`构造函数的一个重载版本允许传递一个实现了`IEqualityComparer<T>`接口的对象。如果把这个对象赋予字典，该类就用于生成散列代码并比较键。

下面通过一个示例进行说明。首先创建字典中的键将要使用到的类型：

```c#
public struct EmployeeId : IEquatable<EmployeeId>
{
    private readonly char prefix;
    private readonly int number;

    public EmployeeId(string id)
    {
        //System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(id != null);
        prefix = (id.ToUpper())[0];
        int numLength = id.Length - 1;
        try
        {
            number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
        }
        catch (Exception)
        {
            throw new Exception("EmployeeId格式错误");
        }
    }
    
    public override string ToString()
    {
        return prefix.ToString() + $"{number,6:000000}";
    }
    //重写GetHashCode()方法
    public override int GetHashCode()
    {
        //此条语句只是为了使得到的值能够尽可能的平均到int范围
        //将数字向左移动16位，再与原数字进行异或操作，得到的结果乘以16进制数15051505
        return (number ^ number << 16) * 0x15051505;
    }
    //必须实现Equals()方法
    public bool Equals(EmployeeId other)
    {
        //return (_prefix == other?._prefix && _number == other?._number);
        return (prefix == other.prefix && number == other.number);
    }

    public override bool Equals(object obj)
    {
        return Equals((EmployeeId)obj);
    }
    //使用 operator 关键字重载内置运算符==
    public static bool operator ==(EmployeeId left, EmployeeId right)
    {
        return left.Equals(right);
    }
    //使用 operator 关键字重载内置运算符!=
    public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
}
```

接着创建字典中的值对应的类型：

```c#
public class Employee
{
    private string name;
    private decimal salary;
    private readonly EmployeeId id;

    public Employee(EmployeeId id, string name, decimal salary)
    {
        this.id = id;
        this.name = name;
        this.salary = salary;
    }

    public override string ToString()
    {
        return $"{id.ToString()}:{name,-20} {salary:C}";
    }
}
```

定义字典，并调用：

```c#
public static void Run()
{
    var idTony = new EmployeeId("C3755");
    var tony = new Employee(idTony, "Tony Stewart", 379025.00m);

    var idCarl = new EmployeeId("F3547");
    var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);

    var idKevin = new EmployeeId("C3386");
    var kevin = new Employee(idKevin, "kevin Harwick", 415261.00m);

    //字典使用EmployeeId对象来索引
    var employees = new Dictionary<EmployeeId, Employee>(5)
    {
        [idTony] = tony,
        [idCarl] = carl,
        [idKevin] = kevin
    };

    foreach (var employee in employees.Values)
    {
        Console.WriteLine(employee);
    }
}
```

#### `Lookup<TKey,TElement>`

`Lookup<TKey,TElement>`类非常类似于`Dictionary<TKey,TValue>`类，但是`Lookup<TKey,TElement>`表示每个映射到一个或多个值的键集合，也就是它的键可以映射到一个或多个值，`Lookup<TKey,TElement>`中的`TElement`表示的是`Lookup<TKey,TElement>`中每个`IEnumerable<T>`值的元素类型。所以要获取其中的每个元素，可以使用循环进行遍历：

```c#
var racers = new List<Racer>();
racers.Add(new Racer(1, "zhang", "san", "zhongguo"));
racers.Add(new Racer(2, "li", "si", "riben"));
racers.Add(new Racer(3, "wang", "wu", "zhongguo"));
racers.Add(new Racer(4, "zhao", "liu", "meiguo"));

var lookupRacers= racers.ToLookup(r => r.Country);
foreach (var item in lookupRacers)
{
    foreach(Racer r  in lookupRacers[item.Key])
    {
        Console.WriteLine($"{item.Key}:{r.ToString()}");
    }
}
```

#### 有序字典`SortedDictionary<Tkey,TValue>`

`SortedDictionary<TKey,TValue>`和`SortedList<TKey,TValue>`功能类似，但因为`SortedList<Tkey,TValue>`实现为一个基于数组的列表，而`SortedDictionary<TKey,TValue>`类实现为一个字典，所以它们有不同的特征。

- `SortedList<TKey,TValue>`使用的内存比`SortedDictionary<TKey,TValue>`少。
- `SortedDictionary<TKey,TValue>`的元素插入和删除操作比较快。
- 在用已排好序的数据填充集合时，若不需要修改容量，`SortedList<TKey,TValue>`就比较快。

注意：`SortedList`使用的内存比`SortedDictionary`少，但`SortedDictionary`在插入和删除未排序的数据时比较快。

`SortedDictionary<TKey,TValue>`是一个二叉搜索树，其中的元素根据键来排序。该键类型必须实现`IComparable<Tkey>`接口。如果键的类型不能排序，则可以创建一个实现了`IComparer<Tkey>`接口的比较器，将比较器用作有序字典的构造函数的一个参数。



## 集`Set`

集（`set`）是包含**不重复的元素**的集合。主要有两个集，`HashSet<T>`和`SortedSet<T>`，它们都实现`ISet<T>`接口，其中，`HashSet<T>`集包含不重复元素的无序列表，`SortedSet<T>`集包含不重复元素的有序列表。

`ISet<T>`常用方法：

- `bool Add(T item)`：向当前集添加元素并返回一个值以指示元素是否已成功添加。 
- `void ExceptWith(IEnumerable<T> other)`：从当前集中删除指定集合中的所有元素。 
- `bool IsSubsetOf(IEnumerable<T> other)`：确定集合是否是指定集合的子集。
- `bool IsSupersetOf(IEnumerable<T> other)`：确定当前集是否是指定集合的超集。
- `bool Overlaps(IEnumerable<T> other)`：确定当前集是否与指定集合重叠。 
- `void UnionWith(IEnumerable<T> other)`：修改当前集，使其包含当前集，指定集合或两者中存在的所有元素。 

```c#
var hsA = new HashSet<string>() { "one", "two", "three" };
var hsB = new HashSet<string>() { "two", "three", "four" };
if (hsA.Add("five"))
{
    Console.WriteLine("添加了five");
}
if (!hsA.Add("two"))
{
    Console.WriteLine("已经存在了two");
}
var hsM = new HashSet<string>() { "one", "two", "three", "four", "five", "six" };

//hsA的每个元素是否都包含在hsB中
if (hsA.IsSubsetOf(hsB))//false
{
    Console.WriteLine("hsA是hsB的子集");
}
if (hsA.IsSubsetOf(hsM))//true
{
    Console.WriteLine("hsA是hsM的子集");
}
//hsA是否是hsB的超集
if (hsA.IsSupersetOf(hsB))//false
{
    Console.WriteLine("hsA是hsB的超集");
}
if (hsM.IsSupersetOf(hsB))//true
{
    Console.WriteLine("hsM是hsB的超集");
}
//判断hsA是否与hsB有公共元素
if (hsA.Overlaps(hsB))//true
{
    Console.WriteLine("hsA与hsB包含共同元素");
}

var allhs = new SortedSet<string>(hsA);
allhs.UnionWith(hsB);
allhs.UnionWith(hsM);
foreach (var n in allhs)
{
    Console.Write(n + "\t");
}
var ex = new HashSet<string>() { "five", "three" };
//删除ex包含的元素
allhs.ExceptWith(ex);
Console.WriteLine();
Console.WriteLine("删除后：");
foreach (var n in allhs)
{
    Console.Write(n + "\t");
}
```



## 性能

集合的性能决定了操作时应该选择哪种集合。在MSDN文档中，集合的方法常常有性能提示，给出了以大写O记号表示的操作时间：

- O(1)：表示无论集合中有多少数据项，这个操作需要的时间都不变。
- O(n)：表示对于集合执行一个操作需要的时间在最坏情况下是N。
- O(log n)：表示操作需要的时间随着集合中元素的增加而增加，但每个元素需要增加的时间不是线性的，而是成对数曲线。

下表列出了集合类执行不同操作的性能，如果单元格中有多个大O值，表示若集合需要重置大小，该操作就需要一定的时间。一般重置大小出现在集合的容量不足以满足需要添加的元素总个数，因此最好避免重置集合的大小，而应把集合的容量设置为一个可以包含所有元素的值。

如果表单元格的内容是n/a（代表not applicable），就表示这个操作不能应用于这个集合类型。

| 集合                             | Add                                                          | Insert           | Remove          | Item                                                         | Sort                           | Find |
| -------------------------------- | ------------------------------------------------------------ | ---------------- | --------------- | ------------------------------------------------------------ | ------------------------------ | ---- |
| `List<T>`                        | 如果集合必须重置大小，就是O(1)或O(n)                         | O(n)             | O(n)            | O(1)                                                         | O(n log n)，最坏的情况是O(n^2) | O(n) |
| `Stack<T>`                       | `Push()`，如果栈必须重置大小，就是O(1)或O(n)                 | n/a              | Pop，O(1)       | n/a                                                          | n/a                            | n/a  |
| `Queue<T>`                       | `Enqueue()`,如果队列必须重置大小，就是O(1)或O(n)             | n/a              | `Dequeue`, O(1) | n/a                                                          | n/a                            | n/a  |
| `HashSet<T>`                     | 如果集必须重置大小，就是O(1)或O(n)                           | `Add`,O(1)或O(n) | O(1)            | n/a                                                          | n/a                            | n/a  |
| `SortedSet<T>`                   | 如果集必须重置大小，就是O(1)或O(n)                           | `Add`,O(1)或O(n) | O(1)            | n/a                                                          | n/a                            | n/a  |
| `LinkedList<T>`                  | `AddLast`，O(1)                                              | `AddAfter`, O(1) | O(1)            | n/a                                                          | n/a                            | O(n) |
| `Dictionary <TKey,TValue>`       | O(1)或O(n)                                                   | n/a              | O(1)            | O(1)                                                         | n/a                            | n/a  |
| `SortedDictionary <Tkey,TValue>` | O(log n)                                                     | n/a              | O(log n)        | O(log n)                                                     | n/a                            | n/a  |
| `SortedList <TKey,TValue>`       | 无序数据为O(n)；如果必须重置大小，就是O(n)；到列表的尾部，就是O(log n) | n/a              | O(n)            | 读/写是O(log n);如果键在列表中，就是O(log n)；如果键不在列表中，就是O(n) | n/a                            | n/a  |





------



### 参考资源

- 《C#高级编程（第10版）》
- [C#集合和数据结构](https://docs.microsoft.com/zh-cn/dotnet/standard/collections/)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。  

最后一次更新时间 ：2018-07-10

------



















