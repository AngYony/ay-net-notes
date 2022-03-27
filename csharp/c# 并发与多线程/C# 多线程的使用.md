# C# 多线程

## Thread

使用Thread创建线程。

示例一，使用不带参数的构造函数启动一个新线程。

```csharp
// 无参数Thread
var th = new Thread(() =>
{
    Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
});
th.Start();
```



示例二，通过使用带参数的构造函数为线程传递参数。

```csharp
// 带参数的Thread
var pth = new Thread(obj=> {
    Thread.Sleep(1000 * 5);
    Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
    Console.WriteLine($"接收到的参数值：{obj}");
});

// 传入参数到线程内
pth.Start("wy");
```



### Thread其他用法

#### Join()

当一个主线程启动了一个新的子线程时，默认情况下，主线程执行完，会自动终止子线程的执行，此时可以通过子线程调用Join()方法，来阻止主线程的执行，直到子线程执行完毕或终止，才会继续执行主线程。

```csharp
// 带参数的Thread
var pth = new Thread(obj=> {
    Thread.Sleep(1000 * 5);
    Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
    Console.WriteLine($"接收到的参数值：{obj}");
});

// 传入参数到线程内
pth.Start("wy");

// Join的使用
// 多个线程支持存在交互和等待

//实现先输出其他线程再输出主线程
// 阻止调用线程（这里是主线程），直到子线程终止。
pth.Join(1000*3); // 等待3秒，过后依旧执行主线程

Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
Console.ReadLine();
```



#### IsBackground

IsBackground为true时，表示后台线程，当主线程退出时，该线程也会被终止。
IsBackground为false时，表示前台线程，此时如果该前台线程不退出，主线程也无法退出。只有所有的前台线程结束后，主线程才能退出。

```csharp
// 带参数的Thread
var pth = new Thread(obj=> {
    Thread.Sleep(1000 * 5);
    Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
    Console.WriteLine($"接收到的参数值：{obj}");
});

// 传入参数到线程内
pth.Start("wy");

pth.IsBackground = false; //设置为前台线程
// Join的使用
// 多个线程支持存在交互和等待

//实现先输出其他线程再输出主线程
// 阻止调用线程（这里是主线程），直到子线程终止。
pth.Join(1000*3); // 等待3秒，过后依旧执行主线程

Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
```



## ThreadPool

不建议直接使用Thread，有时候线程太多，造成上下文切换太过频繁，导致CPU爆高。
太多的线程会造成GC负担过大，托管堆很多“死thread”。

线程池的目的是让Thread得到更佳的使用，提高利用率，减少不必要的创建和销毁。

线程池中的线程类型有：

- WorkThread，CPU密集型
- IOThread，IO密集型

### 不传递参数

```csharp
// 不传递参数
ThreadPool.QueueUserWorkItem(_=> {
    Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}");
});
```

### 传递参数

```csharp
//传递参数
ThreadPool.QueueUserWorkItem(obj => {
    Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}，值为：{obj}");
},"张三");
```

### 泛型参数

```csharp
ThreadPool.QueueUserWorkItem<Student>(stu => {
    Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}，值为：{stu.Name}");
}, new Student { Name = "张三" }, true);

Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
Console.WriteLine("当前线程数量："+ ThreadPool.ThreadCount);
Console.ReadLine();

class Student{
    public string Name{ get; set; }
}
```



