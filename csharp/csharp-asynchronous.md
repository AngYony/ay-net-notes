# C#异步编程

关于异步的概述，这里引用MSDN的一段文字：

> 异步编程是一项关键技术，使得能够简单处理多个核心上的阻塞 I/O 和并发操作。  如果需要 I/O 绑定（例如从网络请求数据或访问数据库），则需要利用异步编程。 还可以使用 CPU 绑定代码（例如执行成本高昂的计算），对编写异步代码而言，这是一个不错的方案。 
>
> 异步代码具有以下特点：
>
> - 等待 I/O 请求返回的同时，可通过生成处理更多请求的线程，处理更多的服务器请求。
> - 等待 I/O 请求的同时生成 UI 交互线程，并通过将长时间运行的工作转换到其他 CPU 核心，让 UI 的响应速度更快。
> - 许多较新的 .NET APIs 都是异步的。

使用异步编程，方法的调用是后台运行，并且不会阻塞调用线程。

## 异步编程的基础

> 异步编程的核心是 `Task` 和 `Task<T>` 对象，这两个对象对异步操作建模。 它们受关键字 `async` 和 `await` 的支持。 在大多数情况下模型十分简单：
>
> 对于 I/O 绑定代码，当你 `await` 一个操作，它将返回 `async` 方法中的一个 `Task` 或 `Task<T>`。
> 对于 CPU 绑定代码，当你`await` 一个操作，它将在后台线程通过 `Task.Run` 方法启动。
> `await` 关键字有这奇妙的作用。 它控制执行 `await` 的方法的调用方，且它最终允许 UI 具有响应性或服务具有灵活性。

`async`和`await`关键字只是编译器功能，实质是编译器会用`Task`类创建代码。

#### 创建任务

我们先定义一个简单的方法，在该方法中，等待3秒之后返回一个字符串：

```c#
public static string Greeting(string name)
{
    //等待3秒
    Task.Delay(3000).Wait(); //Wait方法用来等待之前的任务完成
    return "Hello," + name;
}
```

上述方法中使用了[`Wait()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task.wait?view=netframework-4.7.2)方法，该方法是一个同步方法，它使调用线程等到当前任务完成。如果当前任务尚未开始执行，则`Wait()`方法尝试从调度程序中删除该任务，并在当前线程上内联执行该任务。如果无法执行此操作，或者当前任务已经开始执行，则会阻止调用线程，直到任务完成。 

我们使用一个简单的代码来测试一下运行效果，如下：

```c#
Stopwatch sw = new Stopwatch();
Console.WriteLine("-----开始程序-----");
//开始计时
sw.Start();
//调用方法
Console.WriteLine( AsyncDemo.Greeting("world"));
Console.WriteLine("总执行时间：" + sw.Elapsed.Seconds + "秒");
sw.Stop();
Console.WriteLine("-----结束程序-----");
Console.Read();
```

控制台在打印出“-----开始程序-----”后，将会花费3秒时间调用`Greeting()`方法，上述执行结果如下：

```
-----开始程序-----
Hello,world
总执行时间：3秒
-----结束程序-----
```

接着我们定义一个将此方法异步化的另一个方法`GreetingAsync()`，基于任务的异步模式，指定在异步方法名后加上`Async`后缀，并返回一个任务。如下：

```c#
private static Task<string> GreetingAsync(string name)
{
    return Task.Run<string>(() => { return Greeting(name); });
}
```

上述`Task<string>`定义了一个返回字符串的任务，该方法返回的是一个任务，该任务返回的是一个字符串。

#### 调用异步方法

可以使用`await`关键字来调用返回任务的异步方法。使用`await`关键字需要使用`async`修饰符声明的方法。

```c#
public async static void CallerWithAsync()
{
    string result = await GreetingAsync("异步调用方法");
    Console.WriteLine(result);
}
```

在`GreetingAsync()`方法完成前，`CallerWithAsync()`内的其他代码不会继续执行。但是调用`CallerWithAsync()`方法的线程并没有阻塞，可以被重用。调用`CallerWithAsync()`方法如下：

```c#
Stopwatch sw = new Stopwatch();
Console.WriteLine("-----开始程序-----");
//开始计时
sw.Start();
AsyncDemo.CallerWithAsync();
Console.WriteLine("总执行时间：" + sw.Elapsed.Seconds + "秒");
sw.Stop();
Console.WriteLine("-----结束程序-----");
Console.Read();
```

在上述代码中，直接调用了`CallerWithAsync()`方法，由于外部并不会被阻塞，所以直接会执行`AsyncDemo.CallerWithAsync()`之后的代码，得到的结果如下：

```
-----开始程序-----
总执行时间：0秒
-----结束程序-----
Hello,异步调用方法
```

#### 延续任务（`await`关键字的实质）

`Task`类的`ContinueWith()`方法定义了任务完成后就调用的代码。指派给`ContinueWith()`方法的委托接收将已完成的任务作为参数传入，使用`Result`属性可以访问任务返回的结果。

将上述使用`await`关键字调用的方法`CallerWithAsync()`，使用Task类的`ContinueWith()`进行实现：

```c#
public static void CallerWithContinuationTask()
{
    Task<string> t1 = GreetingAsync("异步调用方法");
    t1.ContinueWith(t =>
    {
        string result = t.Result;
        Console.WriteLine(result);
    });
}
```

C#编译器会把`await`关键字后的所有代码放进`ContinueWith()`方法的代码块中来转换`await`关键字。

因此该方法的执行效果和`CallerWithAsync()`方法的执行效果一样，它们输出结果也相同。

#### 同步上下文

在上述的方法`CallerWithAsync()`中（或`CallerWithContinuationTask()`方法中），不同的执行阶段使用了不同的线程，一个线程用于调用`CallerWithAsync()`方法，我们把这个线程称作为外部调用线程或前台线程、主线程，另一个线程执行`await`关键字后面的代码，或者继续执行`ContinueWith()`方法内的代码块，我们把这个线程称为方法内部执行线程或后台线程。在使用异步时，必须保证在所有应该完成的后台任务之前，至少有一个前台线程仍然在运行。上述实例中的`Console.Read()`就是用来保证主线程一直在运行。

有时候，为了执行某些动作，有些应用程序会绑定到指定的线程上。例如，在winform或WPF应用程序中，只有UI线程才能访问UI元素，这将会是一个问题。在未出现`async`和`await`之前，需要借助委托来解决这类问题，代码相对比较繁琐。

> 而使用`async`和`await`关键字，当`await`完成之后，不需要进行任何特别处理，就能访问UI线程。默认情况下，生成的代码就会把线程转换到拥有同步上下文的线程中。WPF设置了`DispatcherSynchronizationContex`t属性，winfrom设置了`WindowsFormsSynchronizationContext`属性。如果调用异步方法的线程分配了同步上下文，`await`完成之后将继续执行。默认情况下，使用了同步上下文。如果不使用相同的同步上下文，则必须调用Task方法`ConfigureAwait(continueOnCapturedContext:false)`。例如， 一个WPF应用程序，其`await`后面的代码没有用到任何的UI元素。在这种情况下，避免切换到同步上下文会执行的更快。

#### 使用多个异步方法

在一个异步方法里，可以调用一个或多个异步方法。如何编写代码，取决于一个异步方法的结果是否依赖于另一个异步方法。

**按顺序调用多个异步方法**

如果某个异步方法，需要在之前的其他的异步方法执行完后才被调用，就需要使用`await`关键字。它可以实现一个异步方法依赖另一个异步方法的结果的情况。

```c#
public async static void MultipleAsyncMehtods()
{
    string s1 =await GreetingAsync("Mul1");
    string s2 =await GreetingAsync("Mul2");
    Console.WriteLine("Mul:" + s1 + " " + s2);
}
```

**使用组合器**

如果异步方法不依赖于其他异步方法，比如无返回值的异步方法或返回值互不联系，可以在异步方法被调用时，不使用`await`关键字，而是把每个异步方法的返回结果赋值给`Task`变量，这样运行的就会更快一些。

使用组合器，可以同时并行运行多个异步方法。一个组合器可以接受多个同一类型的参数，并返回同一类型的值。多个同一类型的参数被组合成一个参数来传递。Task组合器接受多个`Task`对象作为参数，并返回一个`Task`。

下述示例中使用`Task.WhenAll`组合器方法，它可以等待，直到两个任务都完成：

```c#
public async static void MultipleAsyncMethodsWithCombinators1()
{
    Task<string> t1 = GreetingAsync("mulA");
    Task<string> t2 = GreetingAsync("mulB");
    await Task.WhenAll(t1, t2);
    Console.WriteLine("结果：" + t1.Result + "  " + t2.Result);
}
```

`Task`类型的`WhenAll`方法定义了多个重载版本，如果所有的任务返回相同的类型，可以使用该类型的数组接收`await`返回的结果。如下：

```c#
private async static void MultipleAsyncMethodsWithCombinators2()
{
    Task<string> t1 = GreetingAsync("mulA");
    Task<string> t2 = GreetingAsync("mulB");
    string[] result = await Task.WhenAll(t1, t2);
    Console.WriteLine("结果:" + result[0] + "  " + result[1]);
}
```

除了`WhenAll`组合器外，`Task`类还定义了`WhenAny`组合器。

- `WhenAll`：从`WhenAll`方法返回的`Task`，是在所有传入方法的任务都完成了才会返回`Task`。
- `WhenAny`：从`WhenAny`返回的`Task`，是在其中一个传入方法的任务完成了就会返回`Task`。

#### 转换异步模式

并非.NET Framework的所有类都引入了新的异步方法，有些类只提供了BeginXXX方法和EndXXX方法的异步模式，没有提供基于任务的异步模式。我们可以把这些旧的异步模式转换为新的基于任务的异步模式。

为了模拟出BeginXXX和EndXXX这种形式的方法，对之前的同步方法`Greeting()`进行扩展：

```c#
public static string Greeting(string name)
{
    //等待3秒
    Task.Delay(3000).Wait(); //Wait方法用来等待之前的任务完成
    return "Hello," + name;
}
//定义一个委托
private static  Func<string, string> greetingInvoker = Greeting;

//模拟异步模式
private static IAsyncResult BeginGreeting(
    string name,AsyncCallback callback, object state)
{
    return greetingInvoker.BeginInvoke(name, callback, state);
}
//该方法返回来自于Greeting的结果
private static string EndGreeting(IAsyncResult ar)
{
    return greetingInvoker.EndInvoke(ar);
}
//使用新的基于任务的异步模式进行调用
public static async void ConvertingAsyncPattern()
{
    string s = await Task<string>.Factory.FromAsync<string>(
        BeginGreeting, EndGreeting, "测试", null);
    Console.WriteLine(s);
}
```

> 上述实例中，使用`TaskFactory`类的`FromAsync()`方法，把使用旧的异步模式的方法转换为基于任务的异步模式的方法（TAP）。其中，`Task`类型的第一个泛型参数`Task<string>`定义了调用方法的**返回值类型**。`FromAsync()`方法的泛型参数定义了方法的**输入类型**。`FromAsync()`方法的前两个参数是委托类型，传入`BeginGreeting`和`EndGreeting`方法的声明。紧跟这两个参数后面的是输入参数和对象状态参数。因对象状态没有用到，所以分配`null`值。因为`FromAsync()`方法返回`Task`类型，可以使用`await`修饰。



## 错误处理

为了说明多个异步方法出现错误时的异常处理情况，我们先定义一个简单的方法，如下：

```c#
//注：该方法不是最终解决方案，终极方法见下述说明
public static async void ThrowAfter(int ms, string message)
{
    await Task.Delay(ms);
    throw new Exception(message);
}
```

上述方法将在指定的时间间隔抛出一个异常。如果直接在try/catch块中调用该异步方法，并且没有等待，就会捕获不到异常，代码如下：

```c#
//注：该方法不是最终解决方案，终极方法见下述说明
public static void DontHandle()
{
    try
    {
        ThrowAfter(200, "first");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

上述的代码并不能捕获到任何异常，这是因为`DontHandle()`方法在`ThrowAfter()`抛出异常之前，就已经执行完毕。正确的做法是使用`await`关键字等待`ThrowAfter()`方法执行完才能捕获。由于`ThrowAfter()`是一个void方法，返回`void`的异步方法不能使用`await`关键字进行等待，就无法捕获异常，因此异常方法最好返回一个`Task`类型。对上述方法进行重构：

```c#
//注：终极方法
public async static Task ThrowAfter(int ms, string message)
{
    await Task.Delay(ms);
    throw new Exception(message);
}
//注：终极方法
public async static void DontHandle()
{
    try
    {
        await ThrowAfter(200, "first");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

重构后的方法可以正常的捕获抛出的异常信息。

#### 多个异步方法的异常处理

上述示例针对单一的异步方法比较容易捕获，如果是多个异步方法，使用上述这种做法并不能够捕获全部的异常。

例如：

```c#
//注：该方法不是最终解决方案，终极方法见下述说明
public static async void StartTwoTask()
{
    try
    {
        await ThrowAfter(2000, "first");
        await ThrowAfter(1000, "second");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

上述代码中，在第一个`ThrowAfter()`方法抛出异常后，try/catch代码块就会捕获到异常，直接跳过第二个`ThrowAfter()`方法的执行，因此本示例只能捕获第一个方法抛出的异常，并不能够捕获第二次抛出的异常。

如果采用`Task.WhenAll()`方法并行的调用这两个`ThrowAfter()`方法，代码如下：

```c#
//注：该方法不是最终解决方案，终极方法见下述说明
public async static void StartTwoTaskParallel()
{
    try
    {
        Task t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await Task.WhenAll(t1, t2);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

使用`Task.WhenAll`，不管任务是否抛出异常，都会等到两个任务完成。但是，如果只是单纯的使用`Task.WhenAll`，实际上并不能捕获所有的异常，上述代码只能捕获第一次调用抛出的异常，并不能捕获第二次抛出的异常。为了捕获所有的异常，需要结合使用`AggregateException`类型。

#### 使用`AggregateException`信息捕获所有异常

并行调用异步方法捕获异常的终极解决方案如下：

```c#
//注：终极方案代码
public static async void ShowAggregatedException()
{
    Task taskResult = null;
    try
    {
        Task t1 = ThrowAfter(2000, "first");
        Task t2 = ThrowAfter(1000, "second");
        await (taskResult = Task.WhenAll(t1, t2));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        foreach(var ex1 in taskResult.Exception.InnerExceptions)
        {
            Console.WriteLine(ex1.Message);
        }
    }
}
```

通过外部任务的`Exception`属性，`Exception`属性是`AggregateException`类型的，这个类型定义了`InnerExceptions`属性，它包含了等待中的所有异常的列表，通过遍历列表，可以捕获每一次任务的异常信息。



## 取消任务

取消任务常常应用于长时间运行的后台任务。对于取消任务，.NET提供了一种标准的机制。这种机制可用于基于任务的异步模式。

> 取消框架基于协助行为，不是强制性的。一个运行时间很长的任务需要检查自己是否被取消，在这种情况下，它的工作就是清理所有已打开的资源，并结束相关工作。

取消基于[`CancellationTokenSource`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.cancellationtokensource?view=netframework-4.7.2_)类，该类用于发送取消请求。请求发送给引用[`CancellationToken`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.cancellationtoken?view=netframework-4.7.2)结构类的任务，其中`CancellationToken`结构与`CancellationTokenSource`类相关联。

`CancellationTokenSource`类还支持在指定时间后才取消任务。[`CancelAfter`](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.cancellationtokensource.cancelafter?view=netframework-4.7.2#System_Threading_CancellationTokenSource_CancelAfter_System_TimeSpan_)方法传入一个时间值，单位是毫秒，在该时间过后，就取消任务。

可以将`CancellationToken`传入异步方法，框架中的某些异步方法提供可以传入`CancellationToken`的重载版本，来支持取消任务。一旦取消，就会清理资源，之后抛出异常。

注：取消任务之后，都会抛出异常，可以通过调试的方式，在catch块中进行捕获对应的异常信息。





## 个人总结

- `await`关键字用来修饰的是返回`Task[<T>]`的方法，而不是一个返回普通类型的方法，并不是方法名带有`async`就一定要使用await关键字修饰，需要根据该方法返回的类型进行确定。
- 在方法的内部使用了`await`关键字的方法，必须使用`async`关键字进行修饰。
- 使用了`async`关键字修饰的方法，在被主线程调用时，该主线程并不会受方法内部的`await`关键字的影响，不会被阻塞，依然会运行。而方法内部使用`await`修饰的代码，在未完成前，其他代码不会被执行。
- 如果方法使用了async关键字修饰，无论该方法返回的是void还是Task或者Task<T>，该方法被调用时，如果主调线程没有使用await关键字，那么该方法都是以异步的方式被执行的。也就是异步调用跟方法的返回值无关，而与async关键字修饰有关。一般来说，如果返回的是Task，都会使用async修饰，如果不想在方法调用时，被VS提示建议使用await关键字，可以将方法的返回值由Task改为void。





------



#### 参考资源

- 《C#高级编程（第10版）》
- [C#概念——异步编程](https://docs.microsoft.com/zh-cn/dotnet/csharp/async)
- [.NET并行处理、并发和异步——异步概述](https://docs.microsoft.com/zh-cn/dotnet/standard/async)
- [C#指南——C#使用Async和await的异步编程](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

本文最后一次更新时间：2018-07-23

------































