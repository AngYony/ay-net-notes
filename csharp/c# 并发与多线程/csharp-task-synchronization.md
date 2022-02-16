# C#任务同步

如果需要共享数据，就必须使用同步技术，确保一次只有一个线程访问和改变共享状态。如果不注意同步，就会出现争用条件和死锁。



## 不同步导致的线程问题

如果两个或多个线程访问相同的对象，并且对共享状态的访问没有同步，就会出现争用条件。为了解决这类问题，可以使用`lock`语句，对共享对象进行锁定，除了进行锁定之外，还可以将共享对象设置为线程安全的对象。

注意：只有引用类型才能使用`lock`进行锁定。

锁定并不是越多越好，过多的锁定会造成死锁，在死锁中，至少有两个线程被挂起，并等待对象解除锁定。由于两个线程都在等待对方，就出现了死锁，线程将无限等待下去。



## lock语句和线程安全

C#为多个线程的同步提供了自己的关键字：`lock`语句。

使用一个简单的示例来说明`lock`的使用，首先定义两个简单的类来模拟线程计算，这两个类不包含任何的锁操作：

```c#
class SharedState
{
    public int State { get; set; }
}
class Job
{
    private SharedState _sharedState;
    public Job(SharedState sharedState)
    {
        this._sharedState = sharedState;
    }
//该方法不是最终解决方案，存在漏洞，请不要直接应用到实际代码中
    public void DoTheJob()
    {
        for (int i = 0; i < 50000; i++)
        {
            //每循环一次，值+1
            _sharedState.State += 1;
        }
    }
}
```

接着使用并行任务同时调用上述方法，这里使用循环创建了20个`Task`对象，代码如下：

```c#

public static void Run()
{
    int numTasks = 20;
    //在循环外声明一个SharedState实例，所有的Task都将接收该实例对象
    var state = new SharedState();
    //声明Task数组
    var tasks = new Task[numTasks];
    for(int i = 0; i < numTasks; i++)
    {
        //传入共用的SharedState实例
        tasks[i] = Task.Run(() => new Job(state).DoTheJob());
    }
    //等待所有任务的执行
    Task.WaitAll(tasks);
    Console.WriteLine("结果："+state.State);
}
```

上述代码没有使用`lock`语句，多个Task对于`_sharedState.State`的访问存在线程不安全的情况，这就导致每次执行上述方法时输出的结果各不相同并且还是错误的（正确值是50000*20=100 0000）。多次调用上述方法，输出的结果如下：

```
结果：402798
结果：403463
结果：467736
结果：759837
```

为了得到正确结果，必须在这个程序中添加同步功能，可以使用`lock`关键字实现，它表示要等待指定对象的锁定。当锁定了一个线程后，就可以运行`lock`语句块。在`lock`语句块结束时，对象的锁定被解除，另一个等待锁定的线程就可以获得该锁定块了。`lock`语句只能传递引用类型，因为值类型只是锁定了一个副本，并没有任何意义。

使用`lock`语句，如果要锁定静态成员，可以把锁放在`object`类型或静态成员上；如果要将类的实例成员设置为线程安全的（一次只能有一个线程访问相同实例的成员），可以在类中单独定义一个`object`类型的成员对象，在该类的其他成员只用将这个对象用于`lock`语句。

在`Job`类中，对`DoTheJob()`方法进行改写，使用`lock`语句进行锁定，方法如下：

```c#
 public void DoTheJob()
 {
     for (int i = 0; i < 50000; i++)
     {
         lock (_sharedState)
         {
             _sharedState.State += 1;
         }
     }
 }
```

接着执行之前的`Run()`方法，此时可以得到正确的值：

```
结果：1000000
-----程序执行完毕-----
```



## Interlocked类

对于常用的`i++`这种运算，在多线程中，它并不是线程安全的，它的操作包括从内存中获取一个值，给该值递增`1`，再将它存储回内存中。这些操作都可能被线程调度器打断。`Interlocked`类提供了以线程安全的方式递增、递减、交换和读取值的方法。

在使用`lock`语句对类似`i++`这种操作进行锁同步时，使用`Interlocked`类会快的多。但是，它只能用于简单的同步问题。

示例一，使用`lock`语句锁定对某个变量的访问，对该变量进行比较操作：

```c#
lock (obj)
{
    if (someState == null)
    {
        someState = newState;
    }
}
```

上述可以使用`Interlocked.CompareExchange()`方法进行改写，并且执行的更快：

```c#
Interlocked.CompareExchange(ref someState, newState, null);
```

示例二，如果是简单的对变量递增进行`lock`语句：

```c#
lock (obj)
{
    return ++_state;
}
```

可以使用执行更快的`Interlocked.Increment()`方法进行改写：

```c#
Interlocked.Increment(ref _state);
```



## Monitor类

`lock`语句由C#编译器解析为使用`Monitor`类。

```c#
lock(obj)
{    
}
```

上述`lock`语句被解析为调用`Monitor`类的`Enter()`方法，该方法会一直等待，直到线程锁定对象为止。一次只有一个线程能锁定对象。~~只要解除了锁定，线程就可以进入同步阶段~~【只要对象被锁定，线程就可以进入同步阶段】。`Monitor`类的`Exit()`方法解除了锁定。编译器把`Exit()`方法放在`try`块的`finally`处理程序中，所以如果抛出了异常，就会解除该锁定。

```
Monitor.Enter(obj);
try
{
	//同步执行代码块
}
finally
{
    Monitor.Exit(obj);
}
```

与C#的`lock`语句相比，`Monitor`类的主要优点是：可以添加一个等待被锁定的超时值。这样其他线程就不会无限期地等待被锁定。可以使用`Monitor.TryEnter()`方法，并为该方法传递一个超时值，指定等待被锁定的最长时间。

```
bool _lockTaken = false;
Monitor.TryEnter(_obj, 500, ref _lockTaken);
if (_lockTaken)
{
    try
    {

    }
    finally
    {
        Monitor.Exit(_obj);
    }
}
else
{
    //didn't get the lock,do something else
}
```

> 上述中，如果`obj`被锁定，`TryEnter()`方法就把布尔型的引用参数设置为`true`，并同步的访问由对象`obj`锁定的状态。如果另个一线程锁定`obj`的时间超过了500毫秒，`TryEnter()`方法就把变量`lockTaken`设置为`false`，线程不在等待，而是用于执行其他操作。也许在以后，该线程会尝试再次获得锁定。



## SpinLock结构

`SpinLock`结构的用法非常类似于`Monitor`类。使用`Enter()`或`TryEnter()`方法获得锁，使用`Exit()`方法释放锁定。与`Monitor`相比，如果基于对象的锁定对象（使用`Monitor`）的系统开销由于垃圾回收而过高，就可以使用`SpinLock`结构。如果有大量的锁定，且锁定的时间总是非常短，`SpinLock`结构就很有用。~~应避免使用多个`SpinLock`结构，也不要调用任何可能阻塞的内容。~~

`SpinLock`结构还提供了属性`IsHeld`和`IsHeldByCurrentThread`，指定它当前是否被锁定。

注意：由于`SpinLock`定义为结构，因此传递`SpinLock`实例时，是按照值类型传递的。



## WaitHandle抽象类

`WaitHandle`是一个抽象基类，用于等待一个信号的设置。可以等待不同的信号，因为`WaitHandle`是一个基类，可以从中派生一些其他类。

异步委托的`BeginInvoke()`方法返回一个实现了`IAsycResult`接口的对象。使用`IAsycResult`接口，可以用`AsycWaitHandle`属性访问`WaitHandle`基类。在调用`WaitHandle`的`WaitOne()`方式或者超时发生是，线程会等待接收一个与等待句柄相关的信号。调用`EndInvoke()`方法，线程最终会阻塞，知道得到结果为止。

示例如下：

```c#
static int TakesAWhile(int x,int ms)
{
    Task.Delay(ms).Wait();
    return 42;
}
delegate int TakesAWhileDelegate(int x, int ms);
public static void Run()
{
    TakesAWhileDelegate d1 = TakesAWhile;
    IAsyncResult ar= d1.BeginInvoke(1, 3000, null, null);
    while (true)
    {
        if (ar.AsyncWaitHandle.WaitOne(50))
        {
            Console.WriteLine("Can get the result now");
            break;
        }
    }
    int result = d1.EndInvoke(ar);
    Console.WriteLine("result:"+result);
}
```

调用上述方法，输出结果如下：

```
Can get the result now
result:42
-----程序执行完毕-----
```

> 使用`WaitHandle`基类可以等待一个信号的出现（`WaitOne()`方法）、等待必须发出信号的多个对象（`WaitAll()`方法），或者等待多个对象中的一个（`WaitAny()`方法）。`WaitAll()`和`WaitAny()`是`WaitHandle`类的静态方法，接收一个`WaitHandle`参数数组。
>
> `WaitHandle`基类有一个`SafeWaitHandle`属性，其中可以将一个本机句柄赋予一个操作系统资源，并等待该句柄。例如，可以指定一个`SafeFileHandle`等待文件I/O操作的完成。

因为`Mutex`、`EventWaitHandle`和`Semaphore`类派生自`WaitHandle`基类，所以可以在等待时使用它们。



## Mutex类

`Mutex`（mutual exclusion，互斥）是.NET Framework中提供跨多个进程同步访问的一类。它非常类似于`Monitor`类，因为它们都只有一个线程能拥有锁定。只有一个线程能获得互斥锁定，访问受互斥保护的同步代码区域。

在`Mutex`类的构造函数中，可以指定互斥是否最初应由主调线程拥有，定义互斥的名称，获得互斥是否已存在的信息。

```c#
bool createdNew;
var mutex=new Mutex(false,"ProCSharpMutex",out createdNew);
```

上述示例代码中，第3个参数定义为输出参数，接收一个表示互斥是否为新建的布尔值。如果返回值为`false`，就表示互斥已经定义。互斥可以在另一个进程中定义，因为操作系统能够识别有名称的互斥，它由不同的进程共享。如果没有给互斥指定名称，互斥就是为命名的，不在不同的进程之间共享。

由于系统能识别有名称的互斥，因此可以使用它禁止应用程序启动两次，常用于WPF/winform中：

```c#
bool mutexCreated;
var mutex=new Mutex(false,"SingleOnWinAppMutex",out mutexCreated);
if(!mutexCreated){
    MessageBox.Show("当前程序已经启动！");
    Application.Current.Shutdown();
}
```



## Semaphore类

`Semaphore`非常类似于`Mutex`，其区别是，`Semaphore`可以同时由多个线程使用，它是一种计数的互斥锁定。使用`Semaphore`，可以定义允许同时访问受锁定保护的资源的线程个数。如果需要限制可以访问可用资源的线程数，`Semaphore`就很有用。

.NET Core中提供了两个类`Semaphore`和`SemaphoreSlim`。`Semaphore`类可以使用系统范围内的资源，允许在不同进程之间同步。`SemaphoreSlim`类是对较短等待时间进行了优化的轻型版本。

```c#
static void TaskMain(SemaphoreSlim semaphore)
{
    bool isCompleted = false;
    while (!isCompleted)
    {
        //锁定信号量，定义最长等待时间为600毫秒
        if (semaphore.Wait(600))
        {
            try
            {
                Console.WriteLine($"Task {Task.CurrentId} locks the semaphore");
                Task.Delay(2000).Wait();
            }
            finally
            {
                Console.WriteLine($"Task {Task.CurrentId} releases the semaphore");
                semaphore.Release();
                isCompleted = true;
            } 
        }
        else{
            Console.WriteLine($"Timeout for task {Task.CurrentId}; wait again");
        }
    }
}

public static void Run()
{
    int taskCount = 6;
    int semaphoreCount = 3;
    //创建计数为3的信号量
    //该构造函数第一个参数表示最初释放的锁定量，第二个参数定义了锁定个数的计数
    var semaphore = new SemaphoreSlim(semaphoreCount, semaphoreCount);
    var tasks = new Task[taskCount];
    for(int i = 0; i < taskCount; i++)
    {
        tasks[i] = Task.Run(()=>TaskMain(semaphore));
    }

    Task.WaitAll(tasks);
    Console.WriteLine("All tasks finished");
}
```

> 上述代码中的`Run()`方法中，创建了6个任务和一个计数为3的信号量。在`SemaphoreSlim`类的构造方法中，第一个参数定义了最初释放的锁定数，第二个参数定义了锁定个数的计数。如果第一个参数的值小于第二个参数，它们的差就是已经分配线程的计数值。与互斥一样，可以给信号量指定名称，使之在不同的进程之间共享。实例中，定义信号量时没有指定名称，所以它只能在这个进程中使用。
>
> 上述代码中的`TaskMain()`方法中，任务利用`Wait()`方法锁定信号量。信号量的计数是3，所以有3个任务可以获得锁定。第4个任务必须等待，这里还定义了最长等待时间为600毫秒。如果在该等待时间过后未能获得锁定，任务就把一条消息写入控制台，在循环中继续等待。只要获得了锁定，任务就把一条消息写入控制台，等待一段时间，然后解除锁定。在解除锁定时，在任何情况下一定要解除资源的锁定，这一点很重要。这就是要在`finally`处理程序中调用`SemaphoreSlim.Release()`方法的原因。

上述代码执行后，输出结果如下：

```
Task 3 locks the semaphore
Task 2 locks the semaphore
Task 1 locks the semaphore
Timeout for task 4; wait again
Timeout for task 4; wait again
Timeout for task 5; wait again
Timeout for task 4; wait again
Task 1 releases the semaphore
Task 9 locks the semaphore
Task 3 releases the semaphore
Task 5 locks the semaphore
Task 2 releases the semaphore
Task 4 locks the semaphore
Task 4 releases the semaphore
Task 5 releases the semaphore
Task 9 releases the semaphore
All tasks finished
-----程序执行完毕-----
```



## Events类(略)

此处的`Events`并不是C#中的某个类名，而是一系列类的统称。主要使用到的类有`ManualResetEvent`、`AutoResetEvent`、`ManualResetEventSlim`和`CountdownEvent`类。与`Mutex`和`Semaphore`对象一样，`Events`对象也是一个系统范围内的资源同步方法。

注意：C#中的`event`关键字与`System.Threading`命名空间中的`event`类没有任何关系。`event`关键字基于委托，而上述`event`类是.net封装器，用于系统范围内的本机事件资源的同步。

可以使用`Events`通知其他任务：这里有一些数据，并完成了一些操作等。`Events`可以发信号，也可以不发信号。



## Barrier类（略）

对于同步，`Barrier`类非常适用于其中工作有多个任务分支且以后又需要合并工作的情况。`Barrier`类用于需要同步的参与者。激活一个任务时，就可以动态的添加其他参与者。

`Barrier`类型提供了一个更复杂的场景，其中可以同时运行多个任务，直到达到一个同步点为止。一旦所有任务达到这一点，他们旧客户以继续同时满足于下一个同步点。



## ReaderWriterLockSlim类（略）

为了使锁定机制允许锁定多个读取器（而不是一个写入器）访问某个资源，可以使用`ReaderWriterLockSlim`类。这个类提供了一个锁定功能，如果没有写入器锁定资源，就允许多个读取器访问资源，但只能有一个写入器锁定该资源。



## Timer类（略）

使用计时器，可以重复调用方法。



## 任务同步补充说明

上述内容带略的都是很少使用到的，但是不代表一定不会用到。建议实际应用中通过官方文档去了解具体的用法。

在使用多个线程时，尽量避免共享状态，如果实在不可避免要用到同步，尽量使同步要求最低化，因为同步会阻塞线程。







------



### 参考资源

- 《C#高级编程（第10版）》
- [.NET API——Interlocked](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.interlocked?view=netframework-4.7.2)
- [.NET API——Monitor](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.monitor?view=netframework-4.7.2)
- [C#编程指南——C#线程同步](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/threading/thread-synchronization)
- [.NET API——SpinLock](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.spinlock?view=netframework-4.7.2)
- [.NET API——WaitHandle](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.waithandle?view=netframework-4.7.2)
- [.NET API——Mutex](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.mutex?view=netframework-4.7.2)
- [.NET API——SemaphoreSlim](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.semaphoreslim.-ctor?view=netframework-4.7.2)
- [.NET API——Barrier](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.barrier?view=netframework-4.7.2)
- [.NET API——ReaderWriterLockSlim](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.readerwriterlockslim?view=netframework-4.7.2)
- [.NET API——Timer](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.timer?view=netframework-4.7.2)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。  

最后一次更新时间 ：2018-08-01

------



