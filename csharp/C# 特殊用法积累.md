# C# 特殊用法积累



## ReferenceEquals

确定指定的 Object 实例是否是相同的实例。

```csharp
Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceProvider>(), rootScope));
```



## ??=

该运算符仅在左侧操作数的求值结果为 null 时，才将其右侧操作数的值赋值给左操作数。 如果左操作数的计算结果为非 null，则 ??= 运算符不会计算其右操作数。

```c#
List<int> numbers = null;
(numbers ??= new List<int>()).Add(5);
```

