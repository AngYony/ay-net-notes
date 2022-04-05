# C# 委托

委托是类型安全的类，它定义了返回类型和参数的类型，委托类可以包含一个或多个方法的引用。可以使用lambda表达式实现参数是委托类型的方法。

## 委托

当需要把一个方法作为参数传递给另一个方法时，就需要使用委托。委托是一种特殊类型的对象，其特殊之处在于，我们以前定义的所有对象都包含数据，而委托包含的只是一个或多个方法的地址。

#### 声明委托类型

声明委托类型就是告诉编译器，这种类型的委托表示的是哪种类型的方法。语法如下：

```c#
delegate void delegateTypeName[<T>]([参数列表]);
```

声明委托类型时指定的参数，就是该委托类型引用的方法对应的参数。

```c#
 //声明一个委托类型
 private delegate void IntMethodInvoker(int x);
 //该委托表示的方法有两个long型参数，返回类型为double
 protected delegate double TwoLongsOp(double first, double second);
 //方法不带参数的委托，返回string
 public delegate string GetString();
 public delegate int Comparison<in T>(T left, T right);
```

（注：我们把上述定义的`Comparison<in T>`、`IntMethodInvoker`等统称为委托类型。）

在定义委托类型时，必须给出它要引用的方法的参数信息和返回类型等全部细节。声明委托类型的语法和声明方法的语法类似，但没有方法体，并且需要指定`delegate`关键字。

委托实现为派生自基类`System.MulticastDelegate`的类，`System.MulticastDelegate`有派生自基类`System.Delegate`。因此定义委托类型基本上是定义一个新类，所以可以在定义类的任何相同地方定义委托类型。（可以在类的内部定义委托类型，也可以在任何类的外部定义，还可以在命名空间中把委托定义为顶层对象）。

> 我们从“delegate”关键字开始，因为这是你在使用委托时会使用的主要方法。 编译器在你使用 `delegate` 关键字时生成的代码会映射到调用 [Delegate](https://docs.microsoft.com/zh-cn/dotnet/api/system.delegate) 和 [MulticastDelegate](https://docs.microsoft.com/zh-cn/dotnet/api/system.multicastdelegate) 类的成员的方法调用。 
>
> 可以在类中、直接在命名空间中、甚至是在全局命名空间中定义委托类型。 
>
> **建议不要直接在全局命名空间中声明委托类型（或其他类型）。** 

#### 使用委托

定义委托类型之后，可以创建该类型的实例。 为了便于说明委托是如何将方法进行传递的，针对上述的三个委托类型，分别定义三个方法：

```c#
static void ShowInt(int x)
{
    Console.WriteLine("这是一个数字："+x);
}
static double ShowSum(double first,double second)
{
    return first + second;
}
//最后一个委托，直接可以使用int.ToString()方法，所以此处不再定义
```

调用委托有两种形式，一种形式是实例化委托，并在委托的构造函数中传入要引用的方法名（注意仅仅是方法名，不需要带参数），另一种形式是使用**委托推断**，即不需要显式的实例化委托，而是直接指向要引用的方法名即可，编译器将会自动把委托实例解析为特定的类型。具体示例如下：

```c#
public static void Run()
{
    int a = 10;
    //调用委托形式一
    IntMethodInvoker showIntMethod = new IntMethodInvoker(ShowInt);
    showIntMethod(a);

    //调用委托形式二
    TwoLongsOp showSumMethod = ShowSum;
    double sum= showSumMethod.Invoke(1.23, 2.33);
    Console.WriteLine("两数之和："+sum);

    //由于int.Tostring()不是静态方法，所以需要指定实例a和方法名ToString
    GetString showString = a.ToString;
    string str=showString();
    Console.WriteLine("使用委托调用a.ToString()方法："+str);
}
```

在使用委托调用引用的方法时，委托实例名称后面的小括号需要传入要调用的方法的参数信息。实际上，给委托实例提供圆括号的调用和使用委托类的`Invoke()`方法完全相同。委托实例`showSumMethod`最终会被解析为委托类型的一个变量，所以C#编译器会用`showSumMethod.Invoke()`代替`showSumMethod()`。

委托实例可以引用任何类型的任何对象上的实例方法或静态方法，只要方法的签名匹配委托的签名即可。（所谓签名，指的是定义方法或委托时，指定的参数列表和返回类型）

#### 简单的委托示例

后面的内容将会基于此示例进行扩展，首先定义一个简单的数字操作类`MathOperations`，代码如下：

```c#
internal class MathOperations
{
    //显示数值的2倍结果
    public static double MultiplyByTwo(double value)
    {
        double result = value * 2;
        Console.WriteLine($"{value}*2={result}");
        return result;
    }
	//显示数值的乘方结果
    public static double Square(double value)
    {
        double result = value * value;
        Console.WriteLine($"{value}*{value}={result}");
        return result;
    }
}
```

然后定义一个引用上述方法的委托：

```c#
delegate double DoubleOp(double x);
```

如果要使用该委托的话，对应的代码为：

```c#
DoubleOp op = MathOperations.MultiplyByTwo;
op(double_num);// 假设double_num为一个double类型的变量
```

但是很多时候，我们并不是直接这样使用，而是将委托实例作为一个方法（假设该方法为A）的参数进行传入，并且将委托实例引用的方法的参数 作为另一个参数传递给该方法A。将上述代码进行封装转换：

```c#
static void ShowDouble(DoubleOp op, double double_num)
{
    double result = op(double_num);
    Console.WriteLine("值为："+result);
}
```

调用该方法：

```c#
ShowDouble(MathOperations.MultiplyByTwo, 3);
```

使用委托一个好的思路就是，先定义普通方法，然后针对该方法定义一个引用该方法的委托，然后写出对应的委托使用代码，接着再将使用的代码用一个新定义的方法进行封装转换，在新的方法参数中，需要指明委托实例和将要为委托实例引用的方法传入的参数（也就是上述示例中的op和double_num），接着就可以在其他地方调用该方法了。

完整的实例代码如下：

```c#
delegate double DoubleOp(double x);
static void ProcessAndDisplayNumber(DoubleOp action, double value)
{
    double result = action(value);
    Console.WriteLine($"Value is {value },result of operation is {result}");
}
public static void Run()
{
    DoubleOp[] operations = {
        MathOperations.MultiplyByTwo,
        MathOperations.Square
    };
    for (int i = 0; i < operations.Length; i++)
    {
        Console.WriteLine($"Using operations[{i}]:");
        ProcessAndDisplayNumber(operations[i], 2);
        ProcessAndDisplayNumber(operations[i], 3);
        ProcessAndDisplayNumber(operations[i], 4);
    }
}
```

#### `Action<T>`、`Func<T>`、`Predicate<T>`委托

泛型`Action<T>`委托表示引用一个`void`返回类型的方法。 该委托类最多可以为将要引用的方法传递`16`种不同的参数类型。

泛型`Func<T>`委托表示引用一个带有返回值类型的方法。该委托类最多可以为将要引用的方法传递`16`中不同的参数类型，其中最后一个参数代表的是将要引用的方法的返回值类型。

泛型`Predicate<T>` 用于需要确定参数是否满足委托条件的情况。 也可将其写作 `Func<T, bool>` 。例如：

```c#
Predicate<int> pre = b => b > 5;
```

此处只对`Action<T>`和`Func<T>`做详细说明。

有了这两个委托类，在定义委托时，就可以省略`delegate`关键字，采用新的形式声明委托。

```c#
Func<double,double> operations = MathOperations.MultiplyByTwo;
Func<double, double>[] operations2 ={
    MathOperations.MultiplyByTwo,
    MathOperations.Square
};
static void ProcessAndDisplayNumber(Func<double, double> action, double value)
{
    double result = action(value);
    Console.WriteLine($"Value is {value },result of operation is {result}");
}
```

下面使用一个示例对委托的用途进行说明，首先定义一个普通的方法，该方法是冒泡排序的另一种写法：

```c#
public static void Sort(int[] sortArray)
{
    bool swapped = true;
    do
    {
        swapped = false;
        for (int i = 0; i < sortArray.Length - 1; i++)
        {
            if (sortArray[i] > sortArray[i + 1])
            {
                int temp = sortArray[i];
                sortArray[i] = sortArray[i + 1];
                sortArray[i + 1] = temp;
                swapped = true;
            }
        }
    } while (swapped);
}
```

上述方法中，接收的参数局限于数值，为了扩展 使其支持对其他类型的排序，并且不仅仅是升序，对该方法进行泛型改写，并使用泛型委托。

```c#
internal class BubbleSorter
{
    public static void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
    {
        bool swapped = true;
        do
        {
            swapped = false;
            for (int i = 0; i < sortArray.Count - 1; i++)
            {
                if (comparison(sortArray[i + 1], sortArray[i]))
                {
                    T temp = sortArray[i];
                    sortArray[i] = sortArray[i + 1];
                    sortArray[i + 1] = temp;
                    swapped = true;
                }
            }
        } while (swapped);
    }
}
```

上述方法中的参数`comparison`是一个泛型委托，将要引用的方法带有两个参数，类型和`T`相同，值可以来自于`sortArray`，并返回`bool`类型值，因此实际调用该委托时，不用单独的为泛型类型传入参数，直接使用`sortArray`中的项即可。

为了更好的调用该方法，定义如下类：

```c#
internal class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; private set; }

    public override string ToString() => $"{Name},{Salary:C}";

    public Employee(string name, decimal salary)
    {
        this.Name = name;
        this.Salary = salary;
    }
	//为了匹配Func<T,T,bool>委托，定义如下方法
    public static bool CompareSalary(Employee e1, Employee e2) => e1.Salary < e2.Salary;
}
```

使用该类：

```c#
Employee[] employees = {
    new Employee("小明",8000),
    new Employee("小芳",9800),
    new Employee("小黑",4000),
    new Employee("小米",13000),
    new Employee("小马",12000)
};
//调用排序
BubbleSorter.Sort(employees, Employee.CompareSalary);
ForeachWrite(employees); //输出结果，该方法的定义如下：
public static void ForeachWrite<T>(T[] list)
{
    foreach (T item in list)
    {
        Console.WriteLine(item.ToString());
    }
}
```

#### 多播委托

一个委托包含多个方法的调用，这种委托称为多播委托。多播委托可以识别运算符“`+`”和“`+=`“（在委托中添加方法的调用）以及”`-`“和”`-=`“（在委托中删除方法的调用）。

> 多播委托实际上是一个派生自 `System.MulticastDelegate`的类，而`System.MulticastDelegate`又派生自基类`System.Delegate`。`System.MulticastDelegate`的其他成员允许把多个方法调用链接为一个列表。

```c#
internal class MathOperations_V2
{
    public static void MultiplyByTwo(double value)
    {
        double result = value * 2;
        Console.WriteLine($"{value}*2={result}");
    }

    public static void Square(double value)
    {
        double result = value * value;
        Console.WriteLine($"{value}*{value}={result}");
    }
}
```

针对上述方法定义一个带有泛型委托的方法：

```c#
private static void ProcessAndDisplayNumber(Action<double> action, double value)
{
    Console.WriteLine("调用ProcessAndDisplayNumber方法：value=" + value);
    action(value);
}
```

使用多播委托的形式进行调用：

```c#
Action<double> operations = MathOperations_V2.MultiplyByTwo;
operations += MathOperations_V2.Square;

ProcessAndDisplayNumber(operations, 3);
ProcessAndDisplayNumber(operations, 4);
ProcessAndDisplayNumber(operations, 5);
```

上述在调用方法时，会依次执行`MathOperations_V2.MultiplyByTwo`和`MathOperations_V2.Square`。

注意：在使用多播委托时，多播委托包含一个逐个调用的委托集合，一旦通过委托调用的其中一个方法抛出一个异常，整个迭代就会停止。

```c#
private static void One()
{
    Console.WriteLine("调用One()方法");
    throw new Exception("Error in one");
}
static void Two()
{
    Console.WriteLine("调用Two()方法");
}
public static void Run()
{
    Action d1 = One;
    d1 += Two;
    try
    {
        d1();
    }
    catch (Exception)
    {
        Console.WriteLine("调用d1出错了");
    }
}
```

上述使用了多播委托，一旦`One`出现了异常，Two并不能够继续执行。因为第一个方法抛出了一个异常，委托迭代就会停止，不再调用`Two()`方法。为了避免这个问题，应自己迭代方法列表。`Delegate`类定义`GetInvocationList()`方法，返回`Delegate`对象数组，可以迭代这个数组进行方法的执行：

```c#
public static void Run2()
{
    Action d1 = One;
    d1 += Two;
    Delegate[] delegates = d1.GetInvocationList();
    foreach (Action d in delegates)
    {
        try
        {
            d();
        }
        catch (Exception)
        {
            Console.WriteLine("调用出错了！！");
        }
    }
}
```

上述迭代，即使第一个方法出错，依然就执行第二个方法。

#### 匿名方法和Lambda表达式

匿名方法是用作委托的参数的一段代码。

```c#
string start = "厉害了，";
Func<string, string> print = delegate (string param)
{
    return start + param;
};
Console.WriteLine(print("我的国！"));
```

在该示例中，`Func<string,string>`委托接受一个字符串参数，返回一个字符串。`print`是这种委托类型的变量。不要把方法名赋予这个变量，而是使用一段简单的代码：前面是关键字`delegate`，后面是一个字符串参数。

匿名方法的优点是减少了要编写的代码，但代码的执行速度并没有加快。

使用匿名方法时，在匿名方法中不能使用跳转语句（`break`、`goto`或`continue`）调到该匿名方法的外部，也不能在匿名方法的外部使用跳转语句调到匿名方法的内部。并且不能访问在匿名方法外部使用的`ref`和`out`参数。

实际使用中，不建议使用上述的方式定义匿名方法，而是使用lambda表达式。

只要有委托参数类型的地方，就可以使用lambda表达式，将上述示例改为lambda表达式，代码如下：

```c#
//使用Lambda表达式进行匿名方法的定义
string start = "厉害了，";
Func<string, string> lambda = param => start + param;
Console.WriteLine(lambda("我的C#!!!"));
```

使用lambda表达式规则：

**参数**

只有一个参数时，可以省略小括号

```c#
Func<string, string> oneParam = s => $"将{s}转换为大写：" + s.ToUpper();
//调用
Console.WriteLine(oneParam("abc"));
```

没有参数或者有多个参数时必须使用小括号

```c#
//无参数
Action a = () => Console.WriteLine("无参数");
a();
//多个参数，在小括号中指定参数类型
Func<double, double, double> twoParamsWithTypes = (double x, double y) => x + y;
//调用
Console.WriteLine("2.3+1.3=" + twoParamsWithTypes(2.3, 1.3));
```

**多行代码**

如果lambda表达式只有一条语句，在方法块内就不需要花括号（`{}`）和`return`语句，因为编译器会添加一条隐式的`return`语句。如果lambda表达式有多条语句，必须显式的添加花括号或return语句。例如：

```c#
Func<string, string, string> joinString = (str1, str2) =>
  {
      str1 += str2;
      return str1.ToUpper();
  };
Console.WriteLine(joinString("abc", "def"));
```

补充：

```csharp
public void Run()
{
    // 方法内定义一个简单的匿名方法
    string Hi() => "这是什么";
    // 调用方法
    Console.WriteLine(Hi());

    //方法内定义一个复杂的匿名方法
    Func<string, string> fun = (string p) =>
    {
        return "传入的参数是：" + p;
    };

    Console.WriteLine(fun("参数A"));
}
```

**闭包**

在lambda表达式的内部使用表达式外部的变量，称为闭包。使用闭包需要注意的一点就是 ，如果在表达式中修改了闭包的值，可以在表达式的外部访问已修改的值 。



#### 委托和 MulticastDelegate 类

> `System.Delegate` 类及其单个直接子类 `System.MulticastDelegate` 可提供框架支持，以便创建委托、将方法注册为委托目标以及调用注册为委托目标的所有方法。
>
> 有趣的是，`System.Delegate` 和 `System.MulticastDelegate` 类本身不是委托类型。 它们为所有特定委托类型提供基础。 相同的语言设计过程要求不能声明派生自 `Delegate` 或 `MulticastDelegate` 的类。 C# 语言规则禁止这样做。
>
> 相反，C# 编译器会在你使用 C# 语言关键字声明委托类型时，创建派生自 `MulticastDelegate` 的类的实例。
>
> 要记住的首要且最重要的事实是，使用的每个委托都派生自 `MulticastDelegate`。 多播委托意味着通过委托进行调用时，可以调用多个方法目标。 





------



#### 参考资源

- 《C#高级编程（第10版）》
- [委托概述](https://docs.microsoft.com/zh-cn/dotnet/csharp/delegates-overview) 
- [System.Delegate 和 `delegate` 关键字](https://docs.microsoft.com/zh-cn/dotnet/csharp/delegate-class)
- [委托和事件](https://docs.microsoft.com/zh-cn/dotnet/csharp/delegates-events)
- [委托和 lambda](https://docs.microsoft.com/zh-cn/dotnet/standard/delegates-lambdas)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

本文最后一次更新时间：2022-03-27

------

