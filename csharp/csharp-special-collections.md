# C# 特殊集合

C#中的特殊集合主要有：

- 不可变的集合
- 并发的集合
- 位数组合位矢量
- 可观察的集合



## 不变的集合

如果对象可以改变其状态，就很难在多个同时运行的任务中使用。这些集合必须同步。如果对象不能改变其状态，就很容易在多个线程中使用。不能改变的对象称为不变的对象；不能改变的集合称为不变的集合。

为了使用不可变的集合，需要添加NuGget包`System.Collections.Immutalbe`，关于此命名空间下的详细介绍，请[点击此处](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable?view=netcore-2.1)进行查看，本文只对其进行简单的示例说明。

#### [`ImmutableArray`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablearray?view=netcore-2.1) 

该类提供创建不可变数组的方法。例如：

```c#
ImmutableArray<string> a1= ImmutableArray.Create<string>();
```

上述语句用于创建一个string类型的不可变数组，注意，上述虽然都是`ImmutableArray`，但是却是两种不同的类型：非泛型类`ImmutableArray`调用`Create()`静态方法返回泛型`ImmutableArray`结构。其中，`Create`方法被重载，这个方法的其他变体允许传送任意数量的元素。

可以使用`Add()`方法添加新的元素，`Add()`方法不会改变不变集合本身，而是返回一个新的不变集合。

```c#
ImmutableArray<string> a2= a1.Add("java");
```

上述语句执行之后，`a1`仍然是一个空集合，`a2`是包含一个元素的不变集合。可以链式的重复调用`Add()`方法，最终返回一个集合：

```c#
ImmutableArray<string> a3 = a2.Add("c#").Add("python").Add("php");
```

> 在使用不变数组的每个阶段，都没有复制完整的集合。相反，不变类型使用了共享状态，仅在需要时复制集合。

通常，先填充集合，再将它变成不变的数组会更高效。当需要进行一些处理时，可以再次使用可变的集合。

#### [`ImmutableList<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablelist-1?view=netcore-2.1) 

表示不可变列表，它是可由索引访问的强类型对象列表。 

示例说明，先定义一个简单的类：

```c#
internal class Account
{
    public string Name { get; }
    public decimal Amount { get; }

    public Account(string name, decimal amount)
    {
        this.Name = name;
        this.Amount = amount;
    }
}
```

接着创建`List<Account>`集合，使用`ToImmutableList`方法将其转换为不变的集合。

```c#
var accounts = new List<Account>
{
    new Account("图书",424.2m),
    new Account("文具",1243.5m),
    new Account("篮球",243.3m)
};
//将List转换为不可变集合
ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();
//输出每一项的内容
immutableAccounts.ForEach(a => Console.WriteLine(a.Name + "--" + a.Amount));
```

如果需要更改不变集合的内容，可以使用不变集合的`Add、AddRange、Remove、RemoveAt、RemoveRange、Replace`以及`Sort`等方法，这些方法都不是直接改变了原来的不变集合，而是返回一个新的不可变集合。虽然上述这些方法可以创建新的不变集合，但是如果对集合频繁的进行多次修改和删除元素，这就不是非常高效。可以使用`ImmutableList<T>`的[`ToBuilder()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablelist-1.tobuilder?view=netcore-2.1#System_Collections_Immutable_ImmutableList_1_ToBuilder) 方法，创建一个构建器，该方法返回一个可以改变的集合。例如：

```c#
 var accounts = new List<Account>
 {
     new Account("图书",424.2m),
     new Account("文具",1243.5m),
     new Account("篮球",243.3m)
 };
 //先得到不可变集合
 ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();
 //调用ToBuilder()方法将不可变集合创建为可变集合
 ImmutableList<Account>.Builder builder = immutableAccounts.ToBuilder();
 for (int i = 0; i < builder.Count; i++)
 {
     Account a = builder[i];
     if (a.Amount > 1000)
     {
         builder.Remove(a);
     }
 }
 //将新创建的可变集合调用ToImmutable()方法得到不可变集合
 ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();
 overdrawnAccounts.ForEach(b => Console.WriteLine(b.Name + "=" + b.Amount));
```

除了`ImmutableArray`和`ImmutableList`之外，该命名空间下还提供了其他一些不变的集合类型。如：

- [`ImmutableArray<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablearray-1?view=netcore-2.1) ：`ImmutableArray<T>`是一个结构，它在内部使用数组类型，当不允许更改底层类型，这个结构实现了接口`IImmutableList<T>`。
- [`ImmutableList<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablelist-1?view=netcore-2.1)：`ImmutableList<T>`在内部使用一个二叉树来映射对象，以实现接口`IImmutableList<T>`。
- [`ImmutableQueue<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablequeue-1?view=netcore-2.1) ：`ImmutableQueue<T>` 实现了接口`IImmutableQueue<T>` ，允许使用`Enqueue`、`Dequeue`和`Peek`以先进先出的方式访问元素。
- [`ImmutableStack<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablestack-1?view=netcore-2.1) ：`ImmutableStack<T>` 实现了接口`IImmutableStack<T>`，允许使用`Push`、`Pop`和`Peek`以先进后出的方式访问元素。
- [`ImmutableDictionary<TKey,TValue>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutabledictionary-2?view=netcore-2.1) ：`ImmutableDictionary<TKey,TValue>` 是一个键和值不可变的集合，其无序的键/值对元素实现了接口`IImmutableDictionary<TKey,TValue>` 。
- [`ImmutableSortedDictionary<TKey,TValue>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablesorteddictionary-2?view=netcore-2.1) ：`ImmutableSortedDictionary<TKey,TValue>`是一个不可变的排序字典。其实现了接口`IImmutableDictionary<TKey,TValue>` 。
- [`ImmutableHashSet<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablehashset-1?view=netcore-2.1) ：表示不可变的无序哈希集 ，实现了接口`IImmutableSet<T>` 。
- [`ImmutableSortedSet<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable.immutablesortedset-1?view=netcore-2.1) ：表示不可变的有序集合，实现了接口`IImmutableSet<T>` 。

上述的这些不变的集合都实现了对应的接口，与正常集合相比，这些不变接口的最大区别是所有改变集合的方法都返回一个新的集合。



## 并发集合

在命名空间[`System.Collections.Concurrent`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent?view=netframework-4.7.2) 中，提供了几个线程安全的集合类，线程安全的集合可以防止多个线程以相互冲突的方式访问集合。下面列出了System.Collections.Concurrent命名空间中常用的类及其功能。

- [`ConcurrentQueue<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentqueue-1?view=netframework-4.7.2) :表示线程安全的先进先出（FIFO）集合。 这个集合类用一种免锁定的算法实现，使用在内部合并到一个链表中的32项数组。访问队列元素的方法有[`Enqueue(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentqueue-1.enqueue?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentQueue_1_Enqueue__0_) 、[`TryDequeue(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentqueue-1.trydequeue?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentQueue_1_TryDequeue__0__) 和[`TryPeek(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentqueue-1.trypeek?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentQueue_1_TryPeek__0__) 。这些方法的命名和前面的`Queue<T>`类的方法很像，只是给可能调用失败的方法加上了前缀`Try`。因为这个类实现了`IProducerConsumerCollection<T>`接口，所以`TryAdd()`和`TryTake()`方法仅调用`Enqueue()`和`TryDequeue()`方法。
- [`ConcurrentStack<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentstack-1?view=netframework-4.7.2) ：表示线程安全的后进先出（`LIFO`）集合。 和`ConcurrentQueue<T>`类似，只是访问元素的方法不同。`ConcurrentStack<T>`类定义了[`Push(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentstack-1.push?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentStack_1_Push__0_) 、`PushRange()`、[`TryPeek(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentstack-1.trypeek?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentStack_1_TryPeek__0__) 、[`TryPop(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentstack-1.trypop?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentStack_1_TryPop__0__) 和`TryPopRange(T[])`方法。该类也实现了`IProducerConsumerCollection<T> `接口。
- [`ConcurrentBag<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentbag-1?view=netframework-4.7.2) ：表示线程安全，无序的对象集合。 该类没有定义添加或提取项的任何顺序。这个类使用一个把线程映射到内部使用的数组上的概念，因此尝试减少锁定。访问元素的方法有[`Add(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentbag-1.add?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentBag_1_Add__0_) 、[`TryPeek(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentbag-1.trypeek?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentBag_1_TryPeek__0__) 和[`TryTake(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentbag-1.trytake?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentBag_1_TryTake__0__) 。该类也实现了`IProducerConsumerCollection<T>` 接口。
- [`ConcurrentDictionary<TKey,TValue>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.2) ：表示可以由多个线程同时访问的键/值对的线程安全集合。[`TryAdd(TKey, TValue)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentdictionary-2.tryadd?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentDictionary_2_TryAdd__0__1_) 、 [`TryGetValue(TKey, TValue)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentdictionary-2.trygetvalue?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentDictionary_2_TryGetValue__0__1__) 、[`TryRemove(TKey, TValue)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentdictionary-2.tryremove?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentDictionary_2_TryRemove__0__1__) 和[`TryUpdate(TKey, TValue, TValue)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.concurrentdictionary-2.tryupdate?view=netframework-4.7.2#System_Collections_Concurrent_ConcurrentDictionary_2_TryUpdate__0__1__1_) 方法以非阻塞的方式访问成员。因为元素基于键和值，所以`ConcurrentDictionary<TKey,TValue>` 类没有实现`IProducerConsumerCollection<T>` 接口。
- [`BlockingCollection<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1?view=netframework-4.7.2) ：为实现[`IProducerConsumerCollection` 的](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.iproducerconsumercollection-1?view=netframework-4.7.2)线程安全集合提供阻塞和[绑定功能](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.iproducerconsumercollection-1?view=netframework-4.7.2)。这个集合可以在添加或提取元素之前，会阻塞线程并一直等待。 `BlockingCollection<T>`集合提供了一个接口，以使用[`Add(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1.add?view=netframework-4.7.2#System_Collections_Concurrent_BlockingCollection_1_Add__0_) 和[`Take()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1.take?view=netframework-4.7.2#System_Collections_Concurrent_BlockingCollection_1_Take) 方法来添加和删除元素。这些方法会阻塞线程，一直等到任务可以执行为止。`Add()`方法有一个重载版本，其中可以给该重载版本传递一个`cancellationToken`令牌，这个令牌允许取消被阻塞的调用。如果不希望线程无限期的等待下去，且不希望从外部取消调用，就可以使用[`TryAdd(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1.tryadd?view=netframework-4.7.2#System_Collections_Concurrent_BlockingCollection_1_TryAdd__0_) 和[`TryTake(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.blockingcollection-1.trytake?view=netframework-4.7.2#System_Collections_Concurrent_BlockingCollection_1_TryTake__0__) 方法，在这些方法中，也可以指定一个超时值，它表示在调用失败之前应阻塞线程和等待的最长时间。

上述类中，有的实现了`IProducerConsumerCollection<T>`接口，`IProducerConsumerCollection<T>`接口提供了[`TryAdd(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.iproducerconsumercollection-1.tryadd?view=netframework-4.7.2#System_Collections_Concurrent_IProducerConsumerCollection_1_TryAdd__0_) 和[`TryTake(T)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent.iproducerconsumercollection-1.trytake?view=netframework-4.7.2#System_Collections_Concurrent_IProducerConsumerCollection_1_TryTake__0__) 方法。`TryAdd()`方法尝试给集合添加一项，返回布尔值；`TryTake()`方法尝试从集合中删除并返回一个项。

> 以ConcurrentXXX形式的集合是线程安全的，如果某个动作不适用于线程的当前状态，它们就返回`false`。在继续之前，总是需要确认添加或提取元素是否成功。不能相信集合 会完成任务。
>
> `BlockingCollection<T>`是对实现了`IProducerConsumerCollection<T>`接口的任意类的修饰器 ，它默认使用`ConcurrentQueue<T>`类。还可以给构造函数传递任何其他实现了`IProducerConsumerCollection<T>`接口的类，例如，`ConcurrentBag<T>`和`ConcurrentStack<T>`。



下面将使用一个完整的示例说明并发集合的应用。该示例基于管道，即一个任务向一个集合类写入一些内容，同时另一个任务从该集合中读取内容。首先定义一个基本类：

```c#
public class Info
{
    public string Word { get; set; }
    public int Count { get; set; }
    public string Color { get; set; }
    public override string ToString()
    {
        return $"Word:{Word},Count:{Count},Color:{Color}";
    }
}
```

定义向控制台输出的类，使用同步来避免返回颜色错误的输出：

```c#
public static class ColoredConsole
{
    private static object syncOutput = new object();

    public static void WriteLine(string message)
    {
        lock (syncOutput)
        {
            Console.WriteLine(message);
        }
    }

    public static void WriteLine(string message, string color)
    {
        lock (syncOutput)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(
                typeof(ConsoleColor), color);
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
```

接着定义具体的管道实现，详细说明请参加代码中的注释：

```c#
public static class PipeLineStages
{
    public static Task ReadFilenamesAsync(string path, BlockingCollection<string> output)
    {
        //第一个阶段
        return Task.Factory.StartNew(() =>
        {
            //读取文件名，并把它们添加到队列中
            foreach (string filename in Directory.EnumerateFiles(
                path, "*.cs", SearchOption.AllDirectories))
            {
                //添加到BlockingCollection<T>中
                output.Add(filename);
                ColoredConsole.WriteLine($"stage 1: added {filename}");
            }
            //通知所有读取器不应再等待集合中的任何额外项
            output.CompleteAdding(); //该方法必不可少
        },TaskCreationOptions.LongRunning);
    }

    public static async Task LoadContentAsync(BlockingCollection<string> input, 
        BlockingCollection<string> output)
    {
        //第二个阶段：从队列中读取文件名并加载它们的内容，并把内容写入到另一个队列
        //如果不调用GetConsumingEnumerable()方法，而是直接迭代集合，将不会迭代之后添加的项
        foreach (var filename in input.GetConsumingEnumerable())
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                var reader = new StreamReader(stream);
                string line = null;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    output.Add(line);
                    ColoredConsole.WriteLine("stage 2: added " + line);
                }
            }
        }
        output.CompleteAdding();
    }

    public static Task ProcessContentAsync(BlockingCollection<string> input, 
        ConcurrentDictionary<string, int> output)
    {
        return Task.Factory.StartNew(() =>
        {
            //第三个阶段：读取第二个阶段中写入内容的队列，并将结果写入到一个字典中
            foreach (var line in input.GetConsumingEnumerable())
            {
                string[] words = line?.Split(' ', ';', '\t', '{', '}', '(', ')', ':', ',', '"');
                if (words == null) continue;
                foreach (var word in words?.Where(w => !string.IsNullOrEmpty(w)))
                {
                    //如果键没有添加到字典中，第二个参数就定义应该设置的值
                    //如果 键已经存在于字典中，updateValueFactory就定义值的改变方式，++i
                    output.AddOrUpdate(key: word, addValue: 1, 
                                       updateValueFactory: (s, i) => ++i);
                    ColoredConsole.WriteLine("stage 3: added " + word);
                }
            }
        }, TaskCreationOptions.LongRunning);
    }

    public static Task transFerContentAsync(ConcurrentDictionary<string, int> input,
        BlockingCollection<Info> output)
    {
        //第四个阶段：从字典中读取内容，转换数据，将其写入队列中
        return Task.Factory.StartNew(() =>
        {
            foreach (var word in input.Keys)
            {
                int value;
                if (input.TryGetValue(word, out value))
                {
                    var info = new Info { Word = word, Count = value };
                    output.Add(info);
                    ColoredConsole.WriteLine("stage 4: added " + info);
                }
            }
            output.CompleteAdding();
        }, TaskCreationOptions.LongRunning);
    }

    public static Task AddColorAsync(BlockingCollection<Info> input,
        BlockingCollection<Info> output)
    {
        //第五个阶段：读取队列信息，并添加颜色信息，同时写入另一个队列
        return Task.Factory.StartNew(() =>
        {
            foreach (var item in input.GetConsumingEnumerable())
            {
                if (item.Count > 40)
                {
                    item.Color = "Red";
                }
                else if (item.Count > 20)
                {
                    item.Color = "Yellow";
                }
                else
                {
                    item.Color = "Green";
                }
                output.Add(item);
                ColoredConsole.WriteLine("Stage 5: added color " + item.Color + " to " + item);
            }
            output.CompleteAdding();
        }, TaskCreationOptions.LongRunning);
    }

    public static Task ShowContentAsync(BlockingCollection<Info> input)
    {
        //第六个阶段：显示最终的队列信息
        return Task.Factory.StartNew(() =>
        {
            foreach (var item in input.GetConsumingEnumerable())
            {
                ColoredConsole.WriteLine("Stage 6:" + item, item.Color);
            }
        }, TaskCreationOptions.LongRunning);
    }
}
```

最终的调用代码为：

```c#
public static async Task StartPipelineAsync()
{
    var fileNames = new BlockingCollection<string>();
    //启动第一个阶段任务，读取文件名，并写入到队列fileNames中
    Task t1 = PipeLineStages.ReadFilenamesAsync(@"../../", fileNames);
    ColoredConsole.WriteLine("started stage 1");

    var lines = new BlockingCollection<string>();
    //启动第二个阶段任务，将队列中的文件名进行读取，获取该文件的内容并写入到lines队列中
    Task t2 = PipeLineStages.LoadContentAsync(fileNames, lines);
    ColoredConsole.WriteLine("started stage 2");

    var words = new ConcurrentDictionary<string, int>();
    //启动第三个阶段任务，读取lines队列中内容并写入到words中
    Task t3 = PipeLineStages.ProcessContentAsync(lines, words);

    //同时启动1、2、3三个阶段的任务，并发执行
    await Task.WhenAll(t1, t2, t3);
    ColoredConsole.WriteLine("stages 1,2,3 completed");

    var items = new BlockingCollection<Info>();
    //启动第四个阶段任务，将words字典中的数据进行读取，写入到items中
    Task t4 = PipeLineStages.transFerContentAsync(words, items);

    var coloredItems = new BlockingCollection<Info>();
    //启动第五个阶段任务，将items的数据进行读取和修改，将结果写入到coloredItems中
    Task t5 = PipeLineStages.AddColorAsync(items, coloredItems);

    //启动第六个阶段任务，将最终的结果显示出来
    Task t6 = PipeLineStages.ShowContentAsync(coloredItems);

    ColoredConsole.WriteLine("stages 4,5,6 started");
    //同时启动4、5、6三个阶段的任务
    await Task.WhenAll(t4, t5, t6);
    ColoredConsole.WriteLine("all sages finished");
}
```



## 处理位的集合

如果需要处理的数字有许多位，可以使用 `BitArray`类和`BitVector32`结构。这两种类型最重要的区别是：`BitArray`类可以重新设置大小，如果事先不知道需要的位数，可以使用`BitArray`类，它可以包含非常多的位。`BitVector32`结构是基于栈的，因此比较快。`BitVector32`结构仅包含32位，它们存储在一个整数中。

#### `BitArray`类

[`BitArray`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.bitarray?view=netframework-4.7.2) 类是一个引用类型，当它的构造函数传入的是int[]时，每一个int类型的整数都将使用32个连续位进行表示。 

```c#
public static void Run()
{
    //创建一个包含8位的数组，其索引是0~7
    var bits1 = new BitArray(8);
    //把8位都设置为true
    bits1.SetAll(true);
    //把对应于1的位设置为false
    bits1.Set(1, false);
    bits1[5] = false;
    bits1[7] = false;
    DisplayBits(bits1); 
    Console.WriteLine(); 

    //Not()方法对所有的位取反
    bits1.Not();
    DisplayBits(bits1);
    Console.WriteLine();

    var bits2 = new BitArray(bits1);
    bits2[0] = true;
    bits2[1] = false;
    bits2[4] = true;
    DisplayBits(bits1);
    Console.Write (" Or ");
    DisplayBits(bits2);
    Console.Write (" = ");
    //比较两个数组上的同一个位置上的位，如果有一个为true，结果就为true
    bits1.Or(bits2);
    DisplayBits(bits1);
    Console.WriteLine();

    DisplayBits(bits2);
    Console.Write(" and ");
    DisplayBits(bits1);
    Console.Write (" = " );
    //如果两个数组上的同一个位置的位都为true，结果才为true
    bits2.And(bits1);
    DisplayBits(bits2);
    Console.WriteLine();

    DisplayBits(bits1);
    Console.Write (" xor ");
    DisplayBits(bits2);
    //比较两个数组上的同一个位置上的位，只有一个（不能是二个）设置为1，结果才是1
    bits1.Xor(bits2);
    Console.Write(" = ");
    DisplayBits(bits1);
    Console.WriteLine();
}

public static void DisplayBits(BitArray bits)
{
    foreach (bool bit in bits)
    {
        Console.Write(bit ? 1 : 0);
    }
}
```

#### `BitVector32`结构

如果事先知道需要的位数，留可以使用[`BitVector32`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized.bitvector32?view=netframework-4.7.2) 结构代替`BitArray`类。`BitVector32`结构效率较高，因为它是一个值类型，在整数栈上存储位。一个整数可以存储32位，如果需要更多的位，就可以使用多个`BitVector32`值或`BitArray`类。`BitArray`类可以根据需要增大，但`BitVector32`结构不能。

`BitVector32`结构中常用成员：

- [`Data`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized.bitvector32.data?view=netframework-4.7.2#System_Collections_Specialized_BitVector32_Data) ：以整数形式获取`BitVector32`的值。`Data`属性把`BitVector32`结构中的数据返回为整数。
- `Item[]`：`BitVector32`的值可以使用索引器设置，索引器是重载的——可以使用掩码或`BitVector32.Section`类型的片段来获取和设置值。
- [`CreateMask()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized.bitvector32.createmask?view=netframework-4.7.2#System_Collections_Specialized_BitVector32_CreateMask) ：该方法有多个重载版本，它是以静态方法，用于为访问`BitVector32`结构中的特定位创建掩码。
- [`CreateSection(Int16, BitVector32+Section)`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized.bitvector32.createsection?view=netframework-4.7.2#System_Collections_Specialized_BitVector32_CreateSection_System_Int16_System_Collections_Specialized_BitVector32_Section_) ：该方法有多个重载版本，也是一个静态方法，用于创建32位中的几个片段。

```c#
public static void Run()
{
    //使用默认构造函数创建一个BitVactor32结构，默认每一位都是false。
    var bits1 = new BitVector32();
    //调用CreateMask()方法创建用来访问第一位的一个掩码，bit1被设置为1
    int bit1 = BitVector32.CreateMask();
    //再次调用CreateMask()方法，并将一个掩码作为参数进行传递，返回第二位掩码 
    int bit2 = BitVector32.CreateMask(bit1);
    int bit3 = BitVector32.CreateMask(bit2);
    int bit4 = BitVector32.CreateMask(bit3);
    int bit5 = BitVector32.CreateMask(bit4);
    //使用掩码和索引器访问位矢量中的位，并设置值
    bits1[bit1] = true;
    bits1[bit2] = false;
    bits1[bit3] = true;
    bits1[bit4] = true;
    bits1[bit5] = true;
    Console.WriteLine(bits1);
    bits1[0xabcdef] = true;
    Console.WriteLine(bits1);
    
    int received = 0x79abcdef;
    //直接传入十六进制数来创建掩码
    BitVector32 bits2 = new BitVector32(received);
    Console.WriteLine(bits2);

    //分割片段
    BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
    BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
    BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
    BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
    BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
    BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);
    Console.WriteLine("Section A:" + IntToBinaryString(bits2[sectionA], true));
    Console.WriteLine("Section B:" + IntToBinaryString(bits2[sectionB], true));
    Console.WriteLine("Section C:" + IntToBinaryString(bits2[sectionC], true));
    Console.WriteLine("Section D:" + IntToBinaryString(bits2[sectionD], true));
    Console.WriteLine("Section E:" + IntToBinaryString(bits2[sectionE], true));
    Console.WriteLine("Section F:" + IntToBinaryString(bits2[sectionF], true));


}

public static string IntToBinaryString(int bits, bool removeTrailingZero)
{
    var sb = new StringBuilder(32);
    for (int i = 0; i < 32; i++)
    {
        if ((bits & 0x80000000) != 0)
        {
            sb.Append("1");
        }
        else
        {
            sb.Append("0");
        }
        bits = bits << 1;
    }
    string s = sb.ToString();
    if (removeTrailingZero)
    {
        return s.TrimStart('0');
    }
    else
    {
        return s;
    }
}
```



## 可观察的集合

使用[`ObservableCollection<T>`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.7.2) 集合类，可以在集合元素进行添加、删除、移动、修改等操作时，提供通知信息。它可以触发[`CollectionChanged`](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.objectmodel.observablecollection-1.collectionchanged?view=netframework-4.7.2) 事件，可以在该事件中，进行相关的操作。

```c#
public static void Run()
{
    var data = new ObservableCollection<string>();
    data.CollectionChanged += Data_CollectionChanged;
    data.Add("one");
    data.Add("tow");
    data.Insert(1, "Three");
    data.Remove("one");
}

private static void Data_CollectionChanged(object sender,
    System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
{
    Console.WriteLine("action" + e.Action.ToString());

    if (e.OldItems != null)
    {
        Console.WriteLine("OldStartingIndex:" + e.OldStartingIndex);
        Console.WriteLine("old item(s):");
        foreach (var item in e.OldItems)
        {
            Console.WriteLine(item);
        }
    }
    if (e.NewItems != null)
    {
        Console.WriteLine("NewStartingIndex:" + e.NewStartingIndex);
        Console.WriteLine("new items:");
        foreach (var item in e.NewItems)
        {
            Console.WriteLine(item);
        }
    }
    Console.WriteLine();
}
```





------



#### 参考资源

- 《C#高级编程（第10版）》
- [C#不变集合](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.immutable?view=netcore-2.1)
- [C#并发集合](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.concurrent?view=netframework-4.7.2)
- [C#可观察集合](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.7.2)
- [C#位数组](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.bitarray?view=netframework-4.7.2)
- [C# BitVector32结构](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.specialized.bitvector32?view=netframework-4.7.2)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

最后一次更新时间：2018-07-10

------



























