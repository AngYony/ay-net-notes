# C#任务和并行编程

我们在处理有些需要等待的操作时，例如，文件读取、数据库或网络访问等，这些都需要一定的时间，我们可以使用多线程，不需要让用户一直等待这些任务的完成，就可以同时执行其他的一些操作。即使是处理密集型的任务，线程也能起到帮助作用， 一个进程的多个线程可以同时运行在不同的CPU上，或多核CPU的不同内核上。

在.NET 4.0之后，提供了线程的一个抽象机制：任务。任务允许建立任务之间的关系（例如，一个任务完成之后可以继续下一个任务），也可以建立一个层级结构，其中包含多个任务。

可以是使用`Task`或`Parallel`类实现并行活动。

- 数据并行：在不同的任务之间同时处理一些数据。
- 任务并行：同时执行不同的功能。

在创建并行程序时，如果需要管理任务之间的关系，或定义返回任务的方法，就要使用`Task`类；如果需要更多的控制并行性，如设置优先级，就需要使用`Thread`类；除此之外就可以使用`Parallel`类，它提供了非常简单的并行性。

## `Parallel`类

`Parallel`类是对线程的一个很好的抽象。该类提供了数据和任务并行性。如果能用`Parallel`类解决的话，优先使用`Parallel`类进行处理。

`Parallel`类定义了并行的`for`和`foreach`的静态方法，分别为`Parallel.For()`和`Parallel.ForEach()`，它们在每次迭代中调用相同的代码，对于C#的`for`和`foreach`语句而言，循环从一个线程中运行。`Parallel`类使用多个任务，因此使用多个线程来完成这个作业。

`Parallel`类还有一个静态方法`Parallel.Invoke()`，它允许同时调用不同的方法，用于任务并行性，而`Parallel.ForEach()`用于数据并行性。

#### `Parallel.For()`

`Parallel.For()`方法类似于C#的`for`循环语句，也是多次执行一个任务（代码块），使用`Parallel.For()`方法，可以并行运行迭代。

首先，定义一个方法，用来记录当前任务和线程的标识符。

```c#
public static void log(string prefix)
{
    Console.WriteLine($"{prefix} \t任务ID: {Task.CurrentId}\t线程ID: {Thread.CurrentThread.ManagedThreadId}");
}
```

`Parallel.For()`方法有许多的重载版本，最简单的定义如下：

```c#
public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
```

该方法的前两个参数定义了循环的开头和结束。第3个参数是一个`Action<int>`委托，整数参数是循环的迭代索引。使用示例如下：

```c#
public static void ParallelFor()
{
    ParallelLoopResult result = Parallel.For(0, 10,
        i =>
        {
            log("开始：" + i);
            //这行代码为了创建更多的线程，可以调大时间
            Task.Delay(1000).Wait();
            log("结束：" + i);
        });
    Console.WriteLine("循环是否结束:" + result.IsCompleted);
}
```

`Parallel.For()`方法的返回类型是`ParallelLoopResult`结构，它提供了循环是否结束的信息。上述示例中的`Task.Delay(1000).Wait()`是为了延长遍历的代码块执行时间，以便创建更多的线程。

上述执行的结果如下：

```
开始：6         任务ID: 2       线程ID: 4
开始：4         任务ID: 1       线程ID: 5
开始：2         任务ID: 4       线程ID: 3
开始：8         任务ID: 5       线程ID: 6
开始：0         任务ID: 3       线程ID: 1
结束：0         任务ID: 3       线程ID: 1
开始：3         任务ID: 3       线程ID: 1
结束：4         任务ID: 1       线程ID: 5
开始：5         任务ID: 12      线程ID: 5
开始：1         任务ID: 11      线程ID: 8
结束：6         任务ID: 2       线程ID: 4
开始：7         任务ID: 13      线程ID: 4
结束：8         任务ID: 5       线程ID: 6
开始：9         任务ID: 14      线程ID: 6
结束：2         任务ID: 4       线程ID: 3
结束：5         任务ID: 12      线程ID: 5
结束：9         任务ID: 14      线程ID: 6
结束：1         任务ID: 11      线程ID: 8
结束：7         任务ID: 13      线程ID: 4
结束：3         任务ID: 3       线程ID: 1
循环是否结束:True
-----程序执行完毕-----
```

通过结果我们可以看到：

- 并行执行的顺序是不能保证的（如果再次运行会看到不同的结果）。
- 不同的任务会映射到不同的线程上，`Task.Delay(1000)`设置的时间越大，创建的不同的线程和任务就越多，结果就越明显。
- 线程可以被不同的任务重用，`Task.Delay(1000)`设置的时间越小，创建的不同的线程和任务就越少，就越会被重用。
- 当一个任务被结束后，如果还在并行运行其他任务，有可能使用同一个任务ID，例如上述中任务ID为3的任务。
- 遍历中的每一个开始和结束都使用相同的线程和任务。这是因为`Task.Delay()`和`Wait()`方法会阻塞当前线程，直到延迟结束，所以任务和线程不会发生改变。

为了说明`await`关键字和`Task.Wait()`的不同，将上述方法使用`await`关键字进行改写，如下：

```c#
//使用await关键字进行改写
public static void ParallelFor2()
{
    ParallelLoopResult result = Parallel.For(0, 10,
        async i =>
        {
            log("开始：" + i);
            await Task.Delay(1000);
            log("结束：" + i);
        });
    Console.WriteLine("循环是否结束:" + result.IsCompleted);
}
```

该示例使用了`await`改写了之前的`Wait()`方法，并且使用`async`关键字进行了修饰，执行结果如下：

```
开始：0         任务ID: 1       线程ID: 1
开始：4         任务ID: 2       线程ID: 5
开始：6         任务ID: 4       线程ID: 4
开始：2         任务ID: 3       线程ID: 3
开始：8         任务ID: 5       线程ID: 6
开始：1         任务ID: 1       线程ID: 1
开始：9         任务ID: 5       线程ID: 6
开始：3         任务ID: 3       线程ID: 3
开始：7         任务ID: 4       线程ID: 4
开始：5         任务ID: 2       线程ID: 5
循环是否结束:True
-----程序执行完毕-----
结束：7         任务ID:         线程ID: 5
结束：3         任务ID:         线程ID: 5
结束：5         任务ID:         线程ID: 4
结束：1         任务ID:         线程ID: 6
结束：9         任务ID:         线程ID: 5
结束：6         任务ID:         线程ID: 3
结束：0         任务ID:         线程ID: 3
结束：2         任务ID:         线程ID: 6
结束：4         任务ID:         线程ID: 5
结束：8         任务ID:         线程ID: 4
```

通过这个结果，可以看出：

- 在使用`await Task.Delay(1000)`语句之后，线程发生了变化，例如，开始为0的线程ID为1，在延迟后结束的线程ID变为了3。并且，结束中没有线程ID为1的线程。
- 在使用`await Task.Delay(1000)`语句延迟后，任务不再存在，只剩下线程，并且剩下的线程重用了前面的线程。
- 使用了`await`后，`Parallel.For()`方法并没有等待延迟，而是直接完成。因为委托参数使用了`async`修饰，`Parallel`类只等待它创建的任务，而不等待其他后台活动。

通过这两个不同的对比可以发现，无论使用哪种形式，分配任务和线程的多少取决于CPU的资源和调用的代码块执行的时长，虽然指定了迭代次数，但是可能因为CPU资源和执行的时长重复使用同一个线程和任务。为了提高执行的效率，应该采用`await`关键字的形式去处理代码块。

`await`是异步的，而`Wait()`是阻塞当前线程，直到结束，是同步的。

#### 提前停止`Parallel.For()`

`Parallel.For()`方法提供了可以在完成所有迭代之前，提前中断迭代的重载版本，定义如下：

```c#
public static ParallelLoopResult For(
    int fromInclusive, 
    int toExclusive, 
    Action<int, ParallelLoopState> body);
```

该方法的第三个委托参数`Action<int, ParallelLoopState>`中的`ParallelLoopState`提供了的`Break()`和`Stop()`方法，以影响循环的结果。

使用[`ParallelLoopState.Break()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.parallelloopstate.break?view=netframework-4.7.2)方法的示例如下：

```c#
public static void StopParallelForEarly()
{
    ParallelLoopResult result = Parallel.For(0, 10,
        (int i, ParallelLoopState pls) =>
        {
            log("开始：" + i);
            if (i > 5)
            {
                pls.Break();
                log("中断：" + i);
            }
            Task.Delay(5000).Wait();
            log("结束：" + i);
        });
    Console.WriteLine("循环是否完成运行：" + result.IsCompleted);
    Console.WriteLine("最小迭代索引：" + result.LowestBreakIteration);
}
```

上述执行结果如下：

```
开始：0         任务ID: 1       线程ID: 1
开始：2         任务ID: 2       线程ID: 3
开始：4         任务ID: 3       线程ID: 5
开始：6         任务ID: 4       线程ID: 4
中断：6         任务ID: 4       线程ID: 4
开始：8         任务ID: 5       线程ID: 6
中断：8         任务ID: 5       线程ID: 6
开始：1         任务ID: 6       线程ID: 8
开始：3         任务ID: 7       线程ID: 9
开始：5         任务ID: 8       线程ID: 10
结束：2         任务ID: 2       线程ID: 3
结束：4         任务ID: 3       线程ID: 5
结束：0         任务ID: 1       线程ID: 1
结束：8         任务ID: 5       线程ID: 6
结束：6         任务ID: 4       线程ID: 4
结束：1         任务ID: 6       线程ID: 8
结束：3         任务ID: 7       线程ID: 9
结束：5         任务ID: 8       线程ID: 10
循环是否完成运行：False
最小迭代索引：6
-----程序执行完毕-----
```

通过结果可以说明：

迭代虽然调用了`Break()`方法，但是其他迭代仍然可以同时运行。并且在中断前开始的所有任务都可以继续运行，直到结束。**注意：`Break()`方法不会停止任何已经开始执行的迭代。**

注：使用`Break()`方法时，常常需要结合[`LowestBreakIteration`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.parallelloopstate.lowestbreakiteration?view=netframework-4.7.2) 和[`ShouldExitCurrentIteration`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.parallelloopstate.shouldexitcurrentiteration?view=netframework-4.7.2) 属性，关于具体用法，可以参见官方示例和说明。

#### `Parallel.For()`对线程初始化

`Parallel.For()`方法使用多个线程来执行循环，如果需要对每个线程进行初始化，可以使用`Parallel.For()`的另一个重载方法，该方法定义如下：

```c#
public static System.Threading.Tasks.ParallelLoopResult For<TLocal> (
    int fromInclusive, 
    int toExclusive, 
    Func<TLocal> localInit, 
    Func<int,System.Threading.Tasks.ParallelLoopState,TLocal,TLocal> body, 
    Action<TLocal> localFinally);
```

除了前两个参数外，第三个委托参数`Func<TLocal>`，返回每个任务的本地数据的初始状态，这个方法仅用于执行迭代的每个线程调用一次。

`Parallel.For()`方法的第四个参数`Func<int,System.Threading.Tasks.ParallelLoopState,TLocal,TLocal>`为循环体定义委托，该`Func`的第一个参数是迭代索引，第二个参数`ParallelLoopState`允许停止循环，第三个参数`TLocal`接收从`Func<TLocal> localInit`方法返回的值，最后一个参数为返回的类型，该类型和`Func<TLocal> localInit`中的`TLocal`要一致。

`Parallel.For()`方法的第五个参数`Action<TLocal>`为线程退出调用的方法，该方法接收一个`TLocal`类型的值，该值来自于第四个参数返回的结果。

通过`Parallel.For()`方法的定义可以看出，它的后三个参数都是基于同一个泛型类型（`Tlocal`）进行操作的，因此在实际使用时，这三个委托所传入的方法的参数和返回值的类型要保持一致。

示例代码如下：

```c#
public static void ParallelForWithInit()
{
    Parallel.For<string>(0, 10, () =>
    {
        log("初始化线程");
        return "线程ID：" + Thread.CurrentThread.ManagedThreadId;
    },
    (i, pls, str1) =>
    {
        log($"迭代调用：{i} ，字符串：{str1}");
        Task.Delay(5000).Wait();
        return "i " + i;
    },
    (str1) =>
    {
        log("finally " + str1);
    });
}
```

在上述方法中，第一个委托的结果，将会作为参数传递给第二个委托的第三个参数，第二个委托的结果，又会作为参数传递给第三个委托。

该方法的执行结果如下：

```
初始化线程      任务ID: 2       线程ID: 3
初始化线程      任务ID: 3       线程ID: 4
初始化线程      任务ID: 4       线程ID: 5
迭代调用：6 ，字符串：线程ID：5         任务ID: 4       线程ID: 5
初始化线程      任务ID: 1       线程ID: 1
迭代调用：2 ，字符串：线程ID：3         任务ID: 2       线程ID: 3
迭代调用：4 ，字符串：线程ID：4         任务ID: 3       线程ID: 4
迭代调用：0 ，字符串：线程ID：1         任务ID: 1       线程ID: 1
初始化线程      任务ID: 5       线程ID: 6
迭代调用：8 ，字符串：线程ID：6         任务ID: 5       线程ID: 6
初始化线程      任务ID: 6       线程ID: 8
迭代调用：1 ，字符串：线程ID：8         任务ID: 6       线程ID: 8
初始化线程      任务ID: 7       线程ID: 9
迭代调用：3 ，字符串：线程ID：9         任务ID: 7       线程ID: 9
初始化线程      任务ID: 8       线程ID: 10
迭代调用：5 ，字符串：线程ID：10        任务ID: 8       线程ID: 10
初始化线程      任务ID: 9       线程ID: 11
迭代调用：7 ，字符串：线程ID：11        任务ID: 9       线程ID: 11
初始化线程      任务ID: 10      线程ID: 12
迭代调用：9 ，字符串：线程ID：12        任务ID: 10      线程ID: 12
finally i 8     任务ID: 5       线程ID: 6
finally i 0     任务ID: 1       线程ID: 1
finally i 2     任务ID: 2       线程ID: 3
finally i 4     任务ID: 3       线程ID: 4
finally i 6     任务ID: 4       线程ID: 5
finally i 1     任务ID: 6       线程ID: 8
finally i 3     任务ID: 7       线程ID: 9
finally i 5     任务ID: 8       线程ID: 10
finally i 7     任务ID: 9       线程ID: 11
finally i 9     任务ID: 10      线程ID: 12
-----程序执行完毕-----
```

#### `Parallel.ForEach()`

`Parallel.ForEach()`方法遍历实现了`IEnumerable`的集合，其方式类似于`foreach`语句，但以异步方式遍历。

```c#
public static void ParallelForEach()
{
    string[] data = { "zero", "one", "two", "three", "four",
        "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve" };

    ParallelLoopResult result = Parallel.ForEach<string>(
        data, s => { Console.WriteLine(s); });
    
    //如果需要终端循环，可以使用重载版本和ParallelLoopState参数
    ParallelLoopResult result2 = Parallel.ForEach<string>(
        data, (s, pls, l) => { Console.WriteLine(s); });
}
```

如果需要中断循环，可以使用`ForEach()`方法的重载版本和`ParallelLoopState`参数，其方式和`For()`方法相同。

#### 使用`Parallel.Invoke()`同时调用多个方法

如果多个任务将并行运行，就可以使用`Parallel.Invoke()`方法，它提供了任务并行性模式。`Parallel.Invoke()`方法允许传递一个`Action`委托的数组，在其中可以指定将运行的方法。

```c#
public static void ParallelInvoke()
{
    Parallel.Invoke(Foo, Bar);
}
public static void Foo()
{
    Console.WriteLine("Foo");
}
public static void Bar()
{
    Console.WriteLine("Bar");
}
```



#### `Parallel`总结

一般来说，优先使用`Parallel`类进行并行处理，它既可以用于任务，也可以用于数据并行性。如果`Parallel`类不能很好的解决，或者需要细致的控制，或者不想等到`Parallel`类结束后再开始其他操作动作，就可以使用`Task`类。也可以结合使用`Task`类和`Parallel`类，具体根据情况而定。



## 任务（Task）

任务表示将完成的某个工作单元。这个工作单元可以在单独的线程中运行，也可以以同步方式启动一个任务，这需要等待**主调线程**。使用任务不仅可以获得一个抽象层，还可以对底层线程进行很多控制。因此，对于复杂的并行处理，为了更好的控制并行操作，优先使用`Task`类。

`Task`可以定义连续的任务，也可以父任务创建子任务，提供了非常大的灵活性。

#### 启动任务

可以使用`TaskFactory`类或`Task`类来启动任务。

首先定义如下两个方法，用来记录任务和线程信息：

```c#
private static object s_logLock = new object();
public static void Log(string title)
{
    //使用lock可以让其他方法并行调用Log()，避免lock内的代码被多个线程或任务交叉调用
    lock (s_logLock)
    {
        Console.WriteLine(title);
        Console.WriteLine($"Task ID:{Task.CurrentId?.ToString() ?? "没有Task存在"}，Thread ID：{ Thread.CurrentThread.ManagedThreadId}");
        //如果不是.NET Core 1.0运行库就执行该语句
        #if (!DNXCORE)
            Console.WriteLine("是否是线程池线程：" + Thread.CurrentThread.IsThreadPoolThread);
        #endif
        Console.WriteLine("是否是后台线程：" + Thread.CurrentThread.IsBackground);
        Console.WriteLine();
    }
}
//使用Task需要传入Action<object>委托，所以此处定义的方法的参数为Object类型
public static void TaskMethod(object obj)
{
    Log(obj?.ToString());
}
```

##### 使用线程池的任务

线程池提供了一个后台线程的池，它独自管理线程，可以根据需要增加或减少线程池中的线程数。线程池中的线程用于实现一些操作，之后仍然返回线程池中。

创建任务的4种方式：

1. 实例化`TaskFactory`类，并调用该实例的`StartNew()`方法。
2. 使用`Task`类的静态属性`Factory`来访问`TaskFactory`，以及调用`StartNew()`方法。它和第一种方式类似，但是没有第一种控制起来那么全面。
3. 使用`Task`类的构造函数，实例化Task对象。实例化时，任务不会立即运行，而是指定`Created`状态，接着调用`Task`类的`Start()`方法，来启动任务。
4. 调用`Task`类的`Run()`静态方法，立即启动任务。虽然`Run()`方法没有可以传递`Action<object>`委托的重载版本，但是可以通过传递`Action`类型的lambda表达式并在其实现中使用参数，可以模拟这种行为。

下面分别对这四种方式进行实现，示例如下：

```c#
public static void TasksUsingThreadPool()
{
    Task t1 = new TaskFactory().StartNew(TaskMethod, "使用TaskFactory实例化形式创建任务");
    Task t2 = Task.Factory.StartNew(TaskMethod, "使用Task的Factory属性形式创建任务");
    Task t3 = new Task(TaskMethod, "使用Task构造函数并调用实例的Start()形式创建任务");
    t3.Start();
    Task t4 = Task.Run(() => { TaskMethod("使用Task.Run()形式创建任务"); });
}
```

调用上述方法代码如下：

```c#
TaskDemo.TasksUsingThreadPool();
Console.WriteLine("-----程序执行完毕-----");
Console.Read();
```

执行结果如下所示：

```
-----程序执行完毕-----
使用Task的Factory属性形式创建任务
Task ID：1，Thread ID：3
是否是线程池线程：True
是否是后台线程：True

使用Task构造函数并调用实例的Start()形式创建任务
Task ID：2，Thread ID：4
是否是线程池线程：True
是否是后台线程：True

使用TaskFactory实例化形式创建任务
Task ID：3，Thread ID：6
是否是线程池线程：True
是否是后台线程：True

使用Task.Run()形式创建任务
Task ID：4，Thread ID：5
是否是线程池线程：True
是否是后台线程：True

```

##### 同步任务

上述通过结果可以看到，在输出了“程序执行完毕”之后，才执行创建的任务，并且使用了不同的线程。而任务不一定要使用线程池中的线程，也可以使用其他线程。任务也可以同步运行，以相同的线程作为主调线程。

```c#
public static void RunSynchronousTask()
{
    TaskMethod("主线程运行");
    var t1 = new Task(TaskMethod, "同步调用");
    //同步运行Task
    t1.RunSynchronously();
}
```

这里调用了`Task`的`RunSynchronously()`方法，输出结果如下：

```
主线程运行
Task ID：没有Task存在，Thread ID：1
是否是线程池线程：False
是否是后台线程：False

同步调用
Task ID：1，Thread ID：1
是否是线程池线程：False
是否是后台线程：False

-----程序执行完毕-----
```

在代码中，`TaskMethod()`方法首先在主线程上直接调用，然后在新创建的`Task`上调用，通过输出结果可以看出，主线程没有任务ID，也不是线程池中的线程。当调用`RunSynchronously()`方法后，会使用相同的线程作为主调线程，如果以前没有创建任务，就会创建一个新任务。

> 注意：在新的.net core运行库中，主线程是一个后台线程？？？

##### 使用单独线程的任务

如果任务的代码将长时间运行，就应该使用`TaskCreationOptions.LongRunning`告诉任务调度器创建一个新线程，而不是使用线程池中的线程。此时，线程可以不由线程池管理。当线程来自线程池时，任务调度器可以决定等待已经运行的任务完成，然后使用这个线程，而不是在线程池中创建一个新线程。

```c#
public static void LongRunningTask()
{
    var t1 = new Task(TaskMethod, "long runing", TaskCreationOptions.LongRunning);
    t1.Start();
}
```

执行结果如下：

```
-----程序执行完毕-----
long runing
Task ID：1，Thread ID：3
是否是线程池线程：False
是否是后台线程：True
```

使用`TaskCreationOptions.LongRunning`选项时，不会使用线程池中的线程，而是会创建一个新线程

#### 返回任务的结果

如果使用返回某个结果的任务，该结果可以是任何类型，可以通过`Task.Result`得到。示例如下：

首先定义一个方法，用来返回一个元组。

```c#
//为了给Task构造函数传递Func<object, TResult>参数，所以此处方法参数定义为Object类型
private static Tuple<int, int> TaskWithResult(object division)
{
    Tuple<int, int> div = (Tuple<int, int>)division;
    int result = div.Item1 / div.Item2;
    int reminder = div.Item1 % div.Item2;
    Console.WriteLine("任务创建了一个结果...");
    return Tuple.Create(result, reminder);
}
```

接着创建一个方法，使用`Task`调用`TaskWithResult()`方法，代码如下：

```c#
public static void TaskWithResultDemo()
{
    var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(7, 3));
    t1.Start();
    Console.WriteLine(t1.Result);
    t1.Wait();
    Console.WriteLine($"result from task:{t1.Result.Item1} {t1.Result.Item2}");
}
```

执行结果：

```
任务创建了一个结果...
(2, 1)
result from task:2 1
-----程序执行完毕-----
```

#### 连续的任务

通过任务，可以指定在任务完成后，应开始运行另一个特定任务。连续任务通过在任务上调用`ContinueWith()`方法来定义，该方法需要传递一个Task类型参数的委托`Action<Task>`，该委托的`Task`类型代表着上一个任务，连续任务也可以使用`TaskFactory`类来定义。

示例如下：

定义两个方法，模拟不同的任务，其中第二个方法的参数为`Task`类型，用来作为`ContinueWith()`方法的参数来使用。

```c#
private static void DoOnFirst()
{
    Console.WriteLine("处理多个连续的任务，TaskID：" + Task.CurrentId)
    Task.Delay(3000).Wait();
}

private static void DoOnSecond(Task t)
{
    Console.WriteLine($"该任务已经完成，TaskID： {t.Id} ");
    Console.WriteLine("当前TaskID： " + Task.CurrentId);
    Console.WriteLine("模拟清理");
    Task.Delay(3000).Wait();
}
```

下面进行任务的调用，首先进行简单的连续调用：

```c#
Task t1 = new Task(DoOnFirst);
Task t2 = t1.ContinueWith(DoOnSecond);
t1.Start();
```

使用`t1.ContinueWith(DoOnSecond)`方法，将会在任务`t1`结束时，立即启动调用`DoOnSecond()`方法的新任务`t2`。执行结果如下：

```
-----程序执行完毕-----
处理多个连续的任务，TaskID：1
该任务已经完成，TaskID： 1
当前TaskID： 3
模拟清理
```

在一个任务结束时，可以启动多个任务，连续任务也可以有另一个连续任务，例如：

```c#
Task t1 = new Task(DoOnFirst);
Task t2 = t1.ContinueWith(DoOnSecond);
//模拟启动多个任务
Task t3 = t1.ContinueWith(DoOnSecond);
//连续任务启动另一个连续任务
Task t4 = t2.ContinueWith(DoOnSecond);
t1.Start();
```

执行结果：

```
-----程序执行完毕-----
处理多个连续的任务，TaskID：1
该任务已经完成，TaskID： 1
当前TaskID： 3
该任务已经完成，TaskID： 1
当前TaskID： 4
模拟清理
模拟清理
该任务已经完成，TaskID： 3
当前TaskID： 7
模拟清理
```

上述代码中，`t1`同时启动了多个连续任务`t2`和`t3`，通过结果可以看到`t2`和`t3`是并行运行的（输出的“模拟清理”是连续的，该现象是随机的），它们都是在`t1`结束时才启动。`t4`是`t2`的连续任务，所以它必须在`t2`结束时才启动。

无论前一个任务如何结束，连续任务总是在它前一个任务结束时才启动。使用`TaskContinuationOptions`枚举中的值可以指定，连续任务只有在起始任务成功或失败结束时启动。该枚举常用的值有`OnlyOnFaulted`、`NotOnCanceled`、`OnlyOnCanceled`、`NotOnCanceled`和`OnlyOnRanToCompletion`等。

```c#
//任务如果出现异常，需要执行的任务
private static void DoOnError(Task obj)
{
    throw new NotImplementedException();
}
//指定只有在延续任务前面的任务引发了未处理异常的情况下才应安排延续任务。
Task t5 = t1.ContinueWith(DoOnError, TaskContinuationOptions.OnlyOnFaulted);
```

#### 任务层次结构

利用任务连续性，可以在一个任务结束后启动另一个任务。任务可以构成一个层次结构，一个任务启动另一个新任务时，就启动了一个父/子层次结构。

示例如下：

定义一个子任务将要调用的方法：

```c#
private static void ChildTask()
{
    Console.WriteLine("开始执行子任务方法");
    Task.Delay(5000).Wait();
    Console.WriteLine("子任务方法执行结束");
}
```

定义一个启动子任务的父任务方法：

```c#
private static void ParentTask()
{
    Console.WriteLine("task id " + Task.CurrentId);
    var child = new Task(ChildTask);
    child.Start();
    Task.Delay(1000).Wait();
    Console.WriteLine("父任务执行结束");
}
```

调用代码：

```c#
var parent = new Task(ParentTask);
parent.Start();
Task.Delay(2000).Wait();
Console.WriteLine(parent.Status);
Task.Delay(4000).Wait();
Console.WriteLine(parent.Status);
```

执行结果：

```
task id 1
开始执行子任务方法
父任务执行结束
RanToCompletion
子任务方法执行结束
RanToCompletion
-----程序执行完毕-----
```

> ~~如果父任务在子任务之前结束，父任务的状态就显示为`WaitingForChildrenToComplete`。所有的子任务也结束时，父任务的状态就变成了`RanToCompletion`。~~

上述引用的内容和实际执行代码输出的结果不一致，执行结果中，已经输出了“父任务执行结束”（父任务只需要1秒，子任务需要5秒），此时子任务还在执行当中，父任务在子任务之前结束了，但是父任务的状态输出的不是`WaitingForChildrenToComplete`，而是`RanToCompletion`。【此处可能是一个描述错误】

#### 从方法中返回任务

返回任务和结果的方法声明为返回`Task<T>`。如果需要实现一个用同步代码定义的接口，就不需要为结果的值创建一个任务，可以使用`Task`类的`FromResult()`方法创建一个结果与完成的任务。该任务用状态`RanToCompletion`表示。

```c#
public Task<IEnumerable<string>> TaskMethodAsync()
{
    return Task.FromResult<IEnumerable<string>>(
        new List<string> { "one", "two" });
}
```

#### 等待任务

- `WhenAll()`：返回一个任务，从而允许使用`async`关键字等待结果，它不会阻塞等待的任务。
- `WaitAll()`：阻塞调用任务，直到等待的所有任务完成为止。
- [`WhenAny()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.whenany?view=netframework-4.7.2)：只要有一个任务完成就返回调用任务。
- [`WaitAny()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.waitany?view=netframework-4.7.2)：阻塞调用任务，等待任何一个任务完成。
- `Delay()`：指定从这个方法返回的任务完成前要等待的毫秒数。
- `Yield()`：如果将释放CPU，从而允许其他任务运行，就可以调用`Task.Yield()`方法。该方法释放CPU，让其他任务运行。如果没有其他任务等待运行，调用`Task.Yield()`的任务就立即继续执行。否则，需要等到再次调度CPU，以调用任务。 



## 任务取消架构

.NET4.5之后包含一个取消框架，允许以标准方式取消长时间允许的任务。

取消框架基于协作行为，它不是强制的。长时间运行的任务会检查它是否被取消，并相应的返回控制权。

实际应用中，会对支持取消的方法传递一个`CancellationToken`参数，这个类定义了`IsCancellationRequested`属性，其中长时间运行的操作可以检查它是否应终止，一旦终止就会抛出一个异常。

#### `Parallel.For()`方法的取消

可以为`Parallel.For()`重载方法，传递一个`ParallelOptions`类型的参数，使用`ParallelOptions`类型，可以传递一个`CancellationToken`参数。`CancellationToken`通过创建`CancellationTokenSource`来生成。由于`CancellationTokenSource`实现了`ICancelableOperation`接口，因此可以用`CancellationToken`注册，并允许使用`Cancel()`方法取消操作。在下述示例中，没有直接调用`Cancel()`方法，而是使用`CancelAfter()`方法在指定的毫秒后标记取消。

```c#
public static void CancelParallelFor()
{
    var cts = new CancellationTokenSource();
    //注入取消时执行的操作
    cts.Token.Register(() => Console.WriteLine("----token cancelled"));
    //在500毫秒后取消任务
    cts.CancelAfter(500);

    try
    {
        ParallelLoopResult result = Parallel.For(0, 10, new ParallelOptions
        {
            CancellationToken = cts.Token
        }
        , x =>
        {
            Console.WriteLine($"loop {x} started");
            //等待400毫秒
            Task.Delay(400).Wait();
            Console.WriteLine($"loop {x} finished");
        });
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

在上述代码中，`Parallel`类验证`CancellationToken`的结果，并取消操作。一旦取消操作，`For()`方法就会抛出一个`OperationCanceledException`类型的异常。使用`CancellationToken`可以注册取消操作时的信息，为此，调用了`Register()`方法，并传递一个在取消操作时调用的委托。

执行的结果如下，该结果与CPU的核心数有关：

```
loop 0 started
loop 2 started
loop 4 started
loop 6 started
loop 8 started
loop 8 finished
loop 0 finished
loop 4 finished
loop 2 finished
loop 6 finished
----token cancelled
已取消该操作。
-----程序执行完毕-----
```

通过取消操作，所有其他的迭代操作都在启动之前就取消了，已经启动的迭代操作允许继续完成，因为取消操作总是以协助方式进行的，以避免在取消迭代操作的中间泄露资源。

#### 任务的的取消

同样的取消模式也可用于任务。首先，新建一个`CancellationTokenSource`。如果仅需要一个取消标记，就可以通过访问`Task.Factory.CancellationToken`以使用默认的取消标记。

```
public static void CancelTask()
{
    var cts = new CancellationTokenSource();
    cts.Token.Register(() => { Console.WriteLine("--- task cancelled"); });

    cts.CancelAfter(500);
    try
    {
        Task t1 = Task.Run(() =>
        {
            Console.WriteLine("in task");
            for (int i = 0; i < 20; i++)
            {
                Task.Delay(100).Wait();
                CancellationToken token = cts.Token;
                //是否请求了取消
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("得到取消请求");
                    //执行取消并直接抛出异常
                    token.ThrowIfCancellationRequested();
                    break;
                }
                Console.WriteLine("in loop");
            }
            Console.WriteLine("任务完成，没有取消");
        }, cts.Token); //把取消标记赋予TaskFactory

        t1.Wait();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine($"异常：{ex.GetType().Name},{ex.Message}");
        foreach (var innerException in ex.InnerExceptions)
        {
            Console.WriteLine($"异常详情：{ex.InnerException.GetType()}, {ex.InnerException.Message}");
        }
    }
}
```

上述示例中，一旦取消任务，会抛出`TaskCanceledException`异常，它是从方法调用`ThrowifCancellationRequested()`中启动的。调用者等待任务时，会捕获`AggregateException`异常，它包含内部异常`TaskCanceledException`。

如果在一个也被取消的任务中运行`Parallel.For()`方法，这就可以用于取消的层次结构，任务的最终状态是`Canceled`。

上述示例执行结果：

```
in task
in loop
in loop
in loop
in loop
--- task cancelled
得到取消请求
异常：AggregateException,发生一个或多个错误。
异常详情：System.Threading.Tasks.TaskCanceledException, 已取消一个任务。
-----程序执行完毕-----
```



## 数据流

使用Task Parallel Library Data Flow（TPL Data Flow，任务并行库数据流）可以对数据流进行处理，并行转换数据。需要使用NuGet引用`System.Threading.Tasks.Dataflow`才能进行操作。

#### 使用动作块（`ActionBlock`）

TPL Data Flow的核心是数据块，这些数据块作为提供数据的源或者接收数据的目标，或者同时作为源和目标。动作块是数据块实现数据传递的载体。

下面是一个简单的示例，使用`ActionBlock`将用户输入的信息输出到控制台：

```c#
public static void ActionBlockRun()
{
    //ActionBlock异步处理消息，把信息写入控制台
    var processInput = new ActionBlock<string>(s =>
    {
        Console.WriteLine("user input :" + s);
    });

    bool exit = false;
    while (!exit)
    {
        string input = Console.ReadLine();
        if (string.Compare(input, "exit", ignoreCase: true) == 0)
        {
            exit = true;
        }
        else
        {
            //把读入的所有字符串写入到ActionBlock中
            processInput.Post(input);
        }
    }
}
```

上述代码中，分配给`ActionBlock`的委托方法在执行时，`ActionBlock`会使用一个任务来并行执行。（可以检查任务和线程标识符来验证这一点）

> 每个`ActionBlock`都实现了`IDataflowBlock`接口，该接口包含了返回一个`Task`的属性`Completion`，以及`Complete()`和`Fault()`方法。调用`Complete()`方法后，块不再接收任何输入，也不再产生任何输出。调用`Fault()`方法则把块放入失败状态。
>
> 块既可以是源，也可以是目标，还可以同时是源和目标。
>
> 在上述示例汇总，`ActionBlock`是一个目标块，所以实现了`ITargetBlock`接口。`ITargetBlock`派生自`IDataflowBlock`，除了提供`IDataflowBlock`接口的成员外，还定义了`OfferMessage()`方法。`OfferMessage()`发送一条由块处理的消息。`Post`是比`OfferMessage`更方便的一个方法，它实现了`ITargetBlock`接口的扩展方法。
>
> `ISourceBlock`接口由作为数据源的块实现。除了`IDataflowBlock`接口的成员外，`ISourceBlock`还提供了链接到目标块以及处理消息的方法。

#### 源和目标数据块

`BufferBlock`可以同时作为数据源和数据目标，它实现了`ISourceBlock`和`ITargetBlock`。

使用`BufferBlock`来收发消息示例：

```c#
private static BufferBlock<string> s_buffer = new BufferBlock<string>();
//用于从控制台读取字符串
private static void Producer()
{
    bool exit = false;
    while (!exit)
    {
        string input = Console.ReadLine();
        if (string.Compare(input, "exit", ignoreCase: true) == 0)
        {
            exit = true;
        }
        else
        {
            //将字符串写入到BufferBlock中
            s_buffer.Post(input);
        }
    }
}

private static async Task ConsumerAsync()
{
    while (true)
    {
        //接收BufferBlock中的数据，ReceiveAsync是ISourceBlock的一个扩展方法
        string data = await s_buffer.ReceiveAsync();
        Console.WriteLine("user input:" + data);
    }
}
```

上述分别定义了使用`BufferBlock`进行数据写入和读取的方法，调用代码如下，通过两个独立的任务完成启动操作：

```c#
public static void BufferBlockRun()
{
    Task t1 = Task.Run(() => Producer());
    Task t2 = Task.Run(async () => { await ConsumerAsync(); });
    Task.WaitAll(t1, t2);
}
```

#### 连接块

可以使用`TransformBlock`连接多个块，创建一个管道。

为了说明具体的用法，我们先定义3个由块使用的方法，每个方法的说明见注释：

```c#
//返回一个路径下的所有文件名
private static IEnumerable<string> GetFileNames(string path)
{
    return System.IO.Directory.EnumerateFiles(path, "*.txt");
}

//读取文件中的每一行
private static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
{
    foreach (var fileName in fileNames)
    {
        using (FileStream stream = File.OpenRead(fileName))
        {
            var reader = new StreamReader(stream);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
//分割每一行的所有词组
private static IEnumerable<string> GetWords(IEnumerable<string> lines)
{
    foreach (var line in lines)
    {
        string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
        foreach (var word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                yield return word;
            }
        }
    }
}
```

为了创建管道，可以使用`TransformBlock`对象。`TransformBlock`是一个源和目标块，通过使用委托来转换源。

创建管道的代码如下，说明见代码注释：

```c#
private static ITargetBlock<string> SetupPipiline()
{
    //第一个TransformBlock被声明为将一个字符串转换为IEnumerable<string>，通过GetFileNames()完成转换
    var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(path =>
      {
          return GetFileNames(path);
      });
    var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames =>
    {
        return LoadLines(fileNames);
    });

    var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines2 =>
      {
          return GetWords(lines2);
      });
    //使用ActionBlock定义最后一个块，该块只是一个用于接收数据的目标块
    var dispaly = new ActionBlock<IEnumerable<string>>(col =>
    {
        foreach (var s in col)
        {
            Console.WriteLine(s);
        }
    });
    //将这些块彼此连接起来
    //fileNamesForPath的结果被传递给lines块
    fileNamesForPath.LinkTo(lines);
    //lines块链接到words块
    lines.LinkTo(words);
    //words块链接到display块
    words.LinkTo(dispaly);
    //返回用于启动管道的块
    return fileNamesForPath;
}

```

启动管道的代码如下，在调用`Post()`方法传递目录时，管道就会启动。

```c#
var target = SetupPipiline();
target.Post("../../");
```

这里可以发出多个启动管道的请求，传递多个目录，并行执行这些任务。



> TPL Data Flow库还提供了许多其他的功能，例如以不同方式处理数据的不同块。`BroadcastBlock`允许向多个目标传递输入源（例如数据写入一个文件并显示该文件），`JoinBlock`将多个源连接到一个目标，`BatchBlock`将输入作为数组进行批处理。使用`DataflowBlockOptions`选项可以配置块，例如一个任务中可以处理的最大项数，还可以向其传递取消标记来取消管道。使用链接技术，可以对消息进行筛选，只传递满足指定条件的消息。





------



### 参考资源

- 《C#高级编程（第10版）》
- [.NET API——Task.Wait()方法](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.wait?view=netframework-4.7.2)
- [.NET API——Parallel.For()方法](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.parallel.for?view=netframework-4.7.2)
- [.NET API——Parallel.ForEach()方法](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.parallel.foreach?view=netframework-4.7.2)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。  

最后一次更新时间 ：2018-07-30

------



