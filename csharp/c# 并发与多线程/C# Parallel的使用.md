# C# Parallel的使用



## Parallel.Invoke

将串行化执行的任务并行化。

```csharp
static void Email() {
    Console.WriteLine("开始发送邮件...");
    Thread.Sleep(2000);
    Console.WriteLine("发送邮件耗时2秒");
}
static void SMS() {
    Console.WriteLine("开始发送短信...");
    Thread.Sleep(1000);
    Console.WriteLine("发送短信耗时1秒");

}
static void WeChat() {
    Console.WriteLine("开始发送微信...");
    Thread.Sleep(3000);
    Console.WriteLine("发送微信耗时3秒");
}

static void Sample1() {
    Parallel.Invoke(Email, SMS, WeChat);
    // 三个方法执行完成之后，才会执行下述语句
    Console.WriteLine("执行完毕");
    Console.ReadLine();
}
```



## Parallel.For

适用于并行处理带索引的集合。

```csharp
static void Sample2() {
    Parallel.For(0, 100, (index) => {
        Console.WriteLine(index);
    });
}
```

上述代码中，会根据CPU的逻辑核心数分配对应数量的线程数，然后将0~100的数分成对应的份数，并发的去执行处理。例如：12核，会将0 到 100分成12份，让12个线程并发的处理。



## Parallel.ForEach

适用于并行处理不带索引的键值对。

```csharp
static void Sample3() {
    var dic = new Dictionary<int, int>() { [1] = 10, [2] = 20, [3] = 30 };
    Parallel.ForEach(dic, (item) => {
        Console.WriteLine(item.Key+"：" +item.Value);
    });
    Console.ReadLine();
}
```



## ParallelOptions与MaxDegreeOfParallelism

如果只是单纯的使用Parallel而不对CPU资源进行限制，将会出现CPU爆高的情况，此时需要指定MaxDegreeOfParallelism的值。

下述代码允许的并发任务的最大数据为2。

```csharp
static void Sample2() {
    Parallel.For(0, 100, new ParallelOptions(){MaxDegreeOfParallelism=2 },
    (index) => {
        Console.WriteLine(index);
    });
}
```



## 复杂示例

计算1~100的总和:

```csharp
static void Sample4() {
    // 计算1~100的总和
    var total = 0;
    Parallel.For(1, 100, () => 0,
    (num, state, sum) => {
        return num + sum;
    }, sum => {
        Interlocked.Add(ref total, sum);
    });
    Console.WriteLine($"total={total}");
}
```

