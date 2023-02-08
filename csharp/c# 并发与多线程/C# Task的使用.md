# C# Task的使用

Task 的使用有以下三种方式：

- new Task() 
- Task.Factory.StartNew()
- Task.Run()



## 实例化Task对象

### 不传递参数

```csharp
// 1.使用构成函数实例化一个task
var task = new Task(() =>
{
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
});
task.Start();
Console.WriteLine($"task.id={task.Id}，task.status={task.Status}");

task.Wait();
Console.WriteLine($"task.id={task.Id}，task.status={task.Status}");

```

### 传递参数

```csharp
var task2 = new Task((obj) =>
{
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}，传入的值为：{obj}");
},"张三");
// 同步运行，只有任务运行完成之后，才会输出后续内容
task2.RunSynchronously(); // 等待任务的完成，类似于wait操作

Console.WriteLine($"task.id={task2.Id}，task.status={task2.Status}");
```



## Task.Factory.StartNew()

### 无参数

```csharp
var task3 = Task.Factory.StartNew(()=> {
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
});
task3.Wait();
Console.WriteLine($"task.id={task3.Id}，task.status={task3.Status}");
```

### 传递参数

```csharp
var task4 = Task.Factory.StartNew((obj) => {
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}，传入的值为：{obj}");
},"张三");
Console.WriteLine($"task.id={task4.Id}，task.status={task4.Status}");
```



## Task.Run()

Task.Run适用于不需要传递参数的场景，功能最简单。

```csharp
var task5 = Task.Run(()=> {
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
});
```



## 任务编排

### 任务A、任务B、任务C依次顺序执行

```csharp
public static void SayA() {
    Console.WriteLine("A开始执行");
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
    Thread.Sleep(2000);
    Console.WriteLine("A执行完毕");
}

public static void SayB() {
    Console.WriteLine("B开始执行");
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
    Thread.Sleep(1000);
    Console.WriteLine("B执行完毕");
}

public static void SayC() {
    Console.WriteLine("C开始执行");
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
    Thread.Sleep(3000);
    Console.WriteLine("C执行完毕");
}


// 串行化Task
public static void ShunXu() {
    Console.WriteLine("串行执行任务");
    var task = Task.Factory.StartNew(() => {
        SayA();
    }).ContinueWith(t => {
        SayB();
    }).ContinueWith(t => {
        SayC();
    });

    task.Wait();
    Console.WriteLine("全部执行完成!");
    Console.Read();
}
```



### 任务A和任务B并行执行完成后，执行任务C

核心在于调用WhenAll()和ContinueWith()方法。

```csharp
// 并行执行A和B，执行完成之后再执行C
public static void BingXing() {
    var tasks = new Task[2];
    tasks[0] = Task.Factory.StartNew(() => {
        SayA();
    });
    tasks[1] = Task.Factory.StartNew(() => {
        SayB();
    });
    Task.WhenAll(tasks).ContinueWith(t => {
        SayC();
    }).Wait();
    Console.WriteLine("全部执行完成!");
    Console.Read();
}
```



### 任务A里面包含子任务A1和A2，父任务A执行完成之后，再执行任务B

核心在于子任务指定TaskCreationOptions.AttachedToParent选项。

```csharp
public static void SayChildA() {
    Console.WriteLine("ChildA开始执行");
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
    Thread.Sleep(2000);
    Console.WriteLine("ChildA执行完毕");
}
public static void SayChildB() {
    Console.WriteLine("ChildB开始执行");
    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
    Thread.Sleep(3000);
    Console.WriteLine("ChildB执行完毕");
}

//一个父Task包含2个子Task，子任务不执行完，父级任务是不能结束的
public static void FuZiTask() {
    var parentTask = Task.Factory.StartNew(() => {
        // 子task1
        var child1Task = Task.Factory.StartNew(() => {
            SayChildA();
        }, TaskCreationOptions.AttachedToParent);

        var child2Task = Task.Factory.StartNew(() => {
            SayChildB();
        }, TaskCreationOptions.AttachedToParent);
    });
	// 父级任务执行完成之后，再执行任务B
    parentTask.ContinueWith(t => {
        SayB();
    }).Wait();

    Console.WriteLine("全部执行完成!");
    Console.Read();
}
```





## Task.Wait / WaitAll / WaitAny / WhenAll

Task.WhenAll 不会阻塞调用线程，而Wait、WaitAll、WaitAny 会阻塞调用线程。



## CancellationTokenSource

### 简单的取消操作

重点在于执行了Cancel()调用后，要等待要取消的任务执行完成，否则后续操作将会受影响。

```csharp
static void Run(CancellationToken token) {
    int i = 1;
    while (!token.IsCancellationRequested) {
        Console.WriteLine("正在执行..."+(++i));
        Thread.Sleep(1000);
    }
}

static void Sample1() {
    CancellationTokenSource cts = new CancellationTokenSource();
	// 启动一个任务
   var task=  Task.Factory.StartNew(() => {
        Run(cts.Token);
    });

    // 当在控制台输入ctrl+c是触发
    Console.CancelKeyPress += (sender, e) => {
        cts.Cancel();
        task.Wait(); //此处Wait调用必不可少，在其他任务（这里是事件）中进行了取消操作，必须在该任务中等待取消的任务完成。
    };
    // 此处wait()的调用也必不可少
    task.Wait();
    Console.WriteLine("执行完成！");
    Console.Read();
}
```

### 任务A执行完再执行任务B，当执行任务A时，取消任务B的执行

```csharp
static void Sample2() {

    CancellationTokenSource source = new CancellationTokenSource();
    // 启动任务A
    var task = Task.Factory.StartNew(() => {
        for (int i = 0; i < 5; i++) {
            Console.WriteLine($"启动了任务A，当前线程：{Environment.CurrentManagedThreadId}，{DateTime.Now}，执行时间需要5秒");
            Thread.Sleep(1000);
        }
    })
    // 任务A执行完，继续执行任务B
    .ContinueWith(t => {
        Console.WriteLine($"任务B，当前线程：{Environment.CurrentManagedThreadId},我是延续任务");
    }, source.Token);

    Thread.Sleep(3000);
    // 暂停三秒后取消任务B的执行，任务A仍然会执行完成
    source.Cancel();
    // 或者直接使用CancelAfter方法
    //source.CancelAfter(3000); // 在3秒后执行取消操作
    
    Console.WriteLine("主线程要取消你了...");
    Console.ReadLine();
}
```

### 延迟取消与注册取消通知

#### 延迟取消

延迟取消调用的是source.Cancel()方法。上诉代码中，在执行任务B时，会检测source.Token，当发现已取消时，将不再执行。

```
Thread.Sleep(3000);
source.Cancel();
```

可以直接用下述代码替代：

```csharp
source.CancelAfter(3000); // 在3秒后执行取消操作
```

详细代码见下文。

#### 注册取消通知

核心在于调用source.Token.Register()方法。

综合示例：

```csharp
static void Sample2() {

    CancellationTokenSource source = new CancellationTokenSource();
    // 启动任务A
    var task = Task.Factory.StartNew(() => {
        for (int i = 0; i < 5; i++) {
            Console.WriteLine($"启动了任务A，当前线程：{Environment.CurrentManagedThreadId}，{DateTime.Now}，执行时间需要5秒");
            Thread.Sleep(1000);
        }
        
    })
    // 任务A执行完，继续执行任务B
    .ContinueWith(t => {
        Console.WriteLine($"任务B，当前线程：{Environment.CurrentManagedThreadId},我是延续任务");
    }, source.Token);

    //Thread.Sleep(3000);
    //// 暂停三秒后取消任务B的执行，任务A仍然会执行完成
    //source.Cancel();

    source.CancelAfter(3000); // 在3秒后执行取消操作
    //取消回调
    source.Token.Register(() => {
        Console.WriteLine("取消啦，我是回调函数！");
    });

    Console.WriteLine("主线程要取消你了...");
    Console.ReadLine();
}
```



## 自定义任务调度机制（TaskScheduler）

默认情况下，创建多个Task，他们的ThreadId可能是不同的，可以自定义TaskScheduler，实现同一个线程执行不同Task。

```csharp
internal class Program {

    private static void Main(string[] args) {
        var scheduler = new CustomTaskScheduler();
        for (int i = 0; i < 100; i++) {
            var task = Task.Factory.StartNew(() => {
                Console.WriteLine($"线程Id：{Environment.CurrentManagedThreadId}");
            }, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }
        Console.ReadLine();
    }
}
// 自定义一个TaskScheduler，重写相关方法
public class CustomTaskScheduler : TaskScheduler {

    //定义一个Thread执行所有的Task
    private Thread th = null;

    private BlockingCollection<Task> collection = new BlockingCollection<Task>();

    public CustomTaskScheduler() {
        th = new Thread(() => {
            foreach (var task in collection.GetConsumingEnumerable()) {
                TryExecuteTask(task);
            }
        });
        th.Start();
    }

    protected override IEnumerable<Task> GetScheduledTasks() {
        return collection.ToArray();
    }

    protected override void QueueTask(Task task) {
        collection.Add(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
        throw new NotImplementedException();
    }
}
```



## 异步编程模型（APM）

之前的实现方式：

```
BeginXXX
EndXXX
```

使用Task替代实现：

1. Task.Factory.FromAsync
2. TaskCompletionSource

例如，获取文件的大小，原始实现方式：

```csharp
// 原生获取文件大小
private static void Sample1() {
    FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);
    byte[] bytes = new byte[fileStream.Length];
    fileStream.BeginRead(bytes, 0, bytes.Length, (IAsyncResult ir) => {
        var length = (ir.AsyncState as FileStream).EndRead(ir);
        Console.WriteLine($"当前的Length={length}");
    }, fileStream);

    Console.Read();
}
```

使用Task.Factory.FromAsync实现相同功能：

```csharp
private static void Sample2() {
    FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);

    byte[] bytes = new byte[fileStream.Length];

    var task = Task.Factory.FromAsync(fileStream.BeginRead, fileStream.EndRead, bytes, 0, bytes.Length, fileStream);
    Console.WriteLine($"当前的Length={task.Result}"); //阻塞直到获取了结果

    Console.Read();
}
```

使用TaskCompletionSource实现相同功能：

```csharp
private static Task<int> GetFileLengthAsync() {
    TaskCompletionSource<int> source = new TaskCompletionSource<int>();
    FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);

    byte[] bytes = new byte[fileStream.Length];

    fileStream.BeginRead(bytes, 0, bytes.Length, (IAsyncResult ir) => {
        var length = (ir.AsyncState as FileStream).EndRead(ir);
        // 将结果写入到source中
        source.SetResult(length);
    }, fileStream);
    // return返回的时候，上述方法还没有被执行，因此方法名标记为了异步
    return source.Task;
}
```

大多数的XXXAsync方法的内部实现，使用的都是TaskCompletionSource。



## 基于事件的编程模型（EAP）

也可以将事件进行包装，使用Task实现。

例如，使用WebClient获取一个HTML页面的大小，基于事件编程模型的原始实现如下：

```csharp
static void Sample3() {
    WebClient client = new WebClient();
    client.DownloadStringCompleted += (sender, e) => {
        var length = e.Result.Length;
        Console.WriteLine("当前的Length=" + length);
    };

    client.DownloadStringAsync(new Uri("http://cnblogs.com"));
    Console.Read();
}
```

使用TaskCompletionSource实现相同的功能：

```csharp
static Task<int> Sample4() {
    TaskCompletionSource<int> source = new TaskCompletionSource<int>();
    WebClient client = new WebClient();
    client.DownloadStringCompleted += (sender, e) => {
        if (e.Error != null) {
            source.SetException(e.Error);
        }
        else{
            var length = e.Result.Length;
            source.SetResult(length);
        }
    };

    client.DownloadStringAsync(new Uri("http://cnblogs.com"));
    return source.Task;
}

  private static void Main(string[] args) {
           var length= Sample4().Result;
            Console.WriteLine($"当前的Length={length}");  
        }
```

目前.NET中基于事件，已经提供了基于Task的异步方法。



## async

async方法内部运行的原理。



## Result

慎用Result，尤其是在带有上下文的编程模型中，使用Result可能会造成死锁。

带有上下文编程模型的有WPF、Winform等。

Result本质上是同步等待，线程会休眠，违背（异步编程）的理念。

Result + 异步，会导致代码异常复杂，产生bug不易发现。





## 常用场景

### 获取Task的返回值

```csharp
Console.WriteLine("开始");
var task6 = Task.Factory.StartNew(()=> {
    Thread.Sleep(1000 * 3);
    return "张三";
});
Console.WriteLine($"task.id={task6.Id}，task.status={task6.Status}");
// 不建议使用Result，有可能造成死锁
Console.WriteLine(task6.Result); // 相当于wait
Console.WriteLine($"task.id={task6.Id}，task.status={task6.Status}");
Console.WriteLine("主线程执行");

Console.ReadLine();
```

