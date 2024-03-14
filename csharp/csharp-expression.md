# C# 表达式树（Expression）

表达式树以树形数据结构表示代码。

如果一个方法的参数是`Expression<T>`类型，当把lambda表达式赋予这个参数时，C#编译器就会从lambda表达式中创建一个表达式树，并存储在程序集中，这样，就可以在运行期间分析表达式树，并进行优化，以便于查询数据源。表达式树从派生自抽象基类`Expression`的类中构建。`Expression`类与`Expression<T>`不同。继承自`Expression`类的表达式有`BinaryExpression`、`ConstantExpression`、`InvocationExpression`、`LambdaExpression`、`NewExpression`、`NewArrayExpression`、`TernaryExpression`以及`UnaryExpression`等。编译器会从Lambda表达式中创建表达式树。

## 创建表达式树

创建表达式树有两种方式：

- 通过 API 组装法
- 通过 lambda 表达式

### API 组装法

使用API创建表达树指的就是：先编写好每个结点，最后使用代码将所有结点组合起来，生成表达式树。

使用 **ParameterExpression** 类型 来修饰参数，使用 **Expression.Parameter(Type type,string name)** 实例化参数。

```c#
static void Main(string[] args)
{
    ParameterExpression a = Expression.Parameter(typeof(int), "i");
    ParameterExpression b = Expression.Parameter(typeof(int), "j");
    //生成结点r1
    Expression r1 = Expression.Multiply(a, b);      //乘法运行

    ParameterExpression c = Expression.Parameter(typeof(int), "x");
    ParameterExpression d = Expression.Parameter(typeof(int), "y");
    //生成结点r2
    Expression r2 = Expression.Multiply(c, d);      //乘法运行

    //将结点r1和r2组合起来，生成终结点
    Expression result = Expression.Add(r1, r2);     //相加
    
    //生成表达式树变量
    Expression<Func<int, int, int, int, int>> func = Expression.Lambda<Func<int, int, int, int, int>>(result, a, b, c, d);
    //将表达式树生成一个 委托，执行表达式树
    var com = func.Compile();
    Console.WriteLine("表达式" + func);
    Console.WriteLine(com(12, 12, 13, 13));
    Console.ReadKey();
}
```

用于生成表达式树结点的，是 Expression 类型。Expression 参数分类：https://blog.csdn.net/zhuqinfeng/article/details/70168337



### 通过 lambda 表达式创建表达式树

```c#
Expression<Func<int, int, int, int, int>> func = (i, j, x, y) => (i * j) + (x * y);
```

注意：如果使用 lambda 生成表达式树， lambda 只能使用**单行语句，不能使用 if、for等语句。**



## 变量、常量与赋值

`ParameterExpression` 用来创建变量、变量参数表达式。

ConstantExpression 用来表示常量。

常用方法说明：

- `Expression.Variable()`表示创建一个变量；

  ```c#
  ParameterExpression varA = Expression.Variable(typeof(int), "x");
  ```

- `Expression.Parameter()`表示创建一个传入参数；

  ```c#
  ParameterExpression varB = Expression.Parameter(typeof(int), "y");
  ```

- `Expression.Constan()` 定义一个常量。

  ```c#
  ConstantExpression constant1 = Expression.Constant(100, typeof(int));
  ```

- `Expression.Assign()` 用于给表达式树变量赋值。`BinaryExpression Assign(Expression left, Expression right);`将右边表达式的值，赋予左边表达式。

  ```c#
  ParameterExpression a = Expression.Variable(typeof(int), "x");
  ConstantExpression constant = Expression.Constant(100, typeof(int));
  BinaryExpression assign = Expression.Assign(a, constant);
  ```



## 方法重载处理

`Console` 的常用重载方法有

```c#
public static void WriteLine(object value);

public static void WriteLine(float value);

public static void WriteLine(string value);
```

在使用表达式树时，注意要调用的重载方法，不能被正常代码的隐式转换误导。

```c#
int a = 100;
Console.WriteLine(a);

ParameterExpression aa = Expression.Parameter(typeof(int), "a");
BinaryExpression aaa = Expression.Assign(aa, Expression.Constant(100, typeof(int)));
MethodCallExpression method = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), aa);

// 如果没有学到过执行表达式树，现在可以先忽略这里
var call = Expression.Block(new ParameterExpression[] { aa }, aaa, method);
Expression<Action> lambda = Expression.Lambda<Action>(call);
lambda.Compile()();
```

前面输出 变量 a ，系统会进行隐式的类型转换。但是使用表达式树调用方法，要对应类型才行，以便找到正确的重载方法。上面的表达式树调用 `Console.WriteLine()` 会报如下错误:

```c#
System.ArgumentException:“Expression of type 'System.Int32' cannot be used for parameter of type 'System.String' of method 'Void WriteLine(System.String)'
Arg_ParamName_Name”
```



## 运算符

对于一元运算符和二元运算符的 `Expression` 的子类型如下：

```c#
UnaryExpression; //一元运算表达式
BinaryExpression; //二元运算表达式
```

### + 与 Add()

正常代码

```c#
int a;
int b;
a = 100;
b = 200;
var ab = a + b;
Console.WriteLine(ab);
```

使用表达式树构建

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// ab = a + b
BinaryExpression ab = Expression.Add(a, b);

// 打印 a + b 的值
MethodCallExpression method = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), ab);

Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(method, a, b);
lambda.Compile()(100, 200);

Console.ReadKey();
```

如果想复杂一些，使用 `块` 来执行：

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// 别忘记了赋值
BinaryExpression aa = Expression.Assign(a, Expression.Constant(100, typeof(int)));
BinaryExpression bb = Expression.Assign(b, Expression.Constant(200, typeof(int)));

// ab = a + b
BinaryExpression ab = Expression.Add(a, b);

// 打印 a + b 的值
MethodCallExpression method = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), ab);

// 以块的形式执行代码，相当于{ }
// 不需要纠结这里，后面会有详细说明，重点是上面
var call = Expression.Block(new ParameterExpression[] { a, b }, aa, bb, method);
Expression<Action> lambda = Expression.Lambda<Action>(call);
lambda.Compile()();
```

上面两个示例，是使用表达式树计算结果，然后还是使用表达式树打印结果。

前者依赖外界传入参数值，赋予 a、b，后者则全部使用表达式树赋值和运算。

那么，如何通过表达式树执行运算，获取执行结果呢？

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// ab = a + b
BinaryExpression ab = Expression.Add(a, b);

Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(ab, a, b);
int result = lambda.Compile()(100, 200);

Console.WriteLine(result);
Console.ReadKey();
```

这些区别在于如何编写 `Expression.Lambda()`。

另外，使用 `AddChecked()` 可以检查操作溢出。

### - 与 Subtract()

与加法一致，此处不再赘述，`SubtractChecked()` 可以检查溢出。

`a - b` ，结果是 100 。

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// ab = a - b
BinaryExpression ab = Expression.Subtract(a, b);

Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(ab, a, b);
int result = lambda.Compile()(200, 100);

Console.WriteLine(result);
```

### 乘除、取模

乘法

```c#
// ab = a * b
BinaryExpression ab = Expression.Multiply(a, b);
// ab = 20000
```

除法

```c#
// ab = a / b
BinaryExpression ab = Expression.Divide(a, b);
// ab = 2
```

取模(%)

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// ab = a % b
BinaryExpression ab = Expression.Modulo(a, b);

Expression<Func<int, int, int>> lambda = Expression.Lambda<Func<int, int, int>>(ab, a, b);
int result = lambda.Compile()(200, 150);
// ab = 50
Console.WriteLine(result);
Console.ReadKey();
```

### 自增自减

自增自减有两种模型，一种是 `x++ 或 x--`，另一种是 `++x 或 --x`。

他们都是属于 UnaryExpression 类型。

| 算术运算符 | 表达式树                         | 说明 |
| ---------- | -------------------------------- | ---- |
| x++        | Expression.PostIncrementAssign() | 后置 |
| x--        | Expression.PostDecrementAssign() | 后置 |
| ++x        | Expression.PreIncrementAssign()  | 前置 |
| --x        | Expression.PreDecrementAssign()  | 前置 |

巧记：Post 后置， Pre 前置；Increment 是加，Decrement是减；Assign与赋值有关(后面会说到)；

**`x++` 与 `x--` 的使用**

```c#
int a = 10;
int b = 10;
a++;
b--;
Console.WriteLine(a);
Console.WriteLine(b);
// int a,b;
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// a = 10,b = 10;
BinaryExpression setA = Expression.Assign(a, Expression.Constant(10));
BinaryExpression setB = Expression.Assign(b, Expression.Constant(10));

// a++
UnaryExpression aa = Expression.PostIncrementAssign(a);

// b--
UnaryExpression bb = Expression.PostDecrementAssign(b);

//Console.WriteLine(a);
//Console.WriteLine(b);
MethodCallExpression callA = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
MethodCallExpression callB = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

BlockExpression block = Expression.Block(
    new ParameterExpression[] { a, b },
    setA,
    setB,
    aa,
    bb,
    callA,
    callB
    );

Expression<Action> lambda = Expression.Lambda<Action>(block);
lambda.Compile()();

Console.ReadKey();
```

如果想把参数从外面传入，设置 a，b

```c#
// int a,b;
ParameterExpression a = Expression.Variable(typeof(int), "a");
ParameterExpression b = Expression.Variable(typeof(int), "b");

// a++
UnaryExpression aa = Expression.PostIncrementAssign(a);

// b--
UnaryExpression bb = Expression.PostDecrementAssign(b);

//Console.WriteLine(a);
//Console.WriteLine(b);
MethodCallExpression callA = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
MethodCallExpression callB = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

BlockExpression block = Expression.Block(
    aa,
    bb,
    callA,
    callB
    );

Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(block, a, b);
lambda.Compile()(10, 10);
Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`2[System.Int32,System.Int32]>(
    System.Int32 $a,
    System.Int32 $b) {
    .Block() {
        $a++;
        $b--;
        .Call System.Console.WriteLine($a);
        .Call System.Console.WriteLine($b)
    }
}
```

为了理解一下 `Expression.Block()`，可以在这里学习一下(后面会说到 `Block()`)。

```c#
// int a,b;
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");
ParameterExpression c = Expression.Variable(typeof(int), "c");

BinaryExpression SetA = Expression.Assign(a, c);
BinaryExpression SetB = Expression.Assign(b, c);
// a++
UnaryExpression aa = Expression.PostIncrementAssign(a);

// b--
UnaryExpression bb = Expression.PostDecrementAssign(b);

//Console.WriteLine(a);
//Console.WriteLine(b);
MethodCallExpression callA = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
MethodCallExpression callB = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

BlockExpression block = Expression.Block(
    new ParameterExpression[] { a, b },
    SetA,
    SetB,
    aa,
    bb,
    callA,
    callB
    );

Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(block, c);
lambda.Compile()(10);

Console.ReadKey();
```

为什么这里要多加一个 c 呢？我们来看看生成的表达式树

```c#
.Lambda #Lambda1<System.Action`1[System.Int32]>(System.Int32 $c) {
    .Block(
        System.Int32 $a,
        System.Int32 $b) {
        $a = $c;
        $b = $c;
        $a++;
        $b--;
        .Call System.Console.WriteLine($a);
        .Call System.Console.WriteLine($b)
    }
}
```

观察一下下面代码生成的表达式树

```c#
// int a,b;
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// a++
UnaryExpression aa = Expression.PostIncrementAssign(a);

// b--
UnaryExpression bb = Expression.PostDecrementAssign(b);

//Console.WriteLine(a);
//Console.WriteLine(b);
MethodCallExpression callA = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a);
MethodCallExpression callB = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), b);

BlockExpression block = Expression.Block(
    new ParameterExpression[] { a, b },
    aa,
    bb,
    callA,
    callB
    );

Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(block, a, b);
lambda.Compile()(10, 10);
Console.ReadKey();
```

```c#
.Lambda #Lambda1<System.Action`2[System.Int32,System.Int32]>(
    System.Int32 $a,
    System.Int32 $b) {
    .Block(
        System.Int32 $a,
        System.Int32 $b) {
        $a++;
        $b--;
        .Call System.Console.WriteLine($a);
        .Call System.Console.WriteLine($b)
    }
}
```

关于前置的自增自减，按照上面示例编写即可，但是需要注意的是， ++x 和 --x ，是“先运算后增/自减”。

### ==、!=、>、<、>=、<=

C# 中的关系运算符如下

| 运算符 | 描述                                                         |
| :----- | :----------------------------------------------------------- |
| ==     | 检查两个操作数的值是否相等，如果相等则条件为真。             |
| !=     | 检查两个操作数的值是否相等，如果不相等则条件为真。           |
| >      | 检查左操作数的值是否大于右操作数的值，如果是则条件为真。     |
| <      | 检查左操作数的值是否小于右操作数的值，如果是则条件为真。     |
| >=     | 检查左操作数的值是否大于或等于右操作数的值，如果是则条件为真。 |
| <=     | 检查左操作数的值是否小于或等于右操作数的值，如果是则条件为真。 |

`==` 表示相等比较，如果是值类型和 string 类型，则比较值是否相同；如果是引用类型，则比较引用的地址是否相等。

其它的关系运算符则是仅比较值类型的大小。

实例代码

```c#
int a = 21;
int b = 10;
Console.Write("a == b：");
Console.WriteLine(a == b);

Console.Write("a < b ：");
Console.WriteLine(a < b);


Console.Write("a > b ：");
Console.WriteLine(a > b);

// 改变 a 和 b 的值 
a = 5;
b = 20;

Console.Write("a <= b：");
Console.WriteLine(a <= b);


Console.Write("a >= b：");
Console.WriteLine(b >= a);

Console.ReadKey();
```

使用表达式树实现

```c#
// int a,b;
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");

// a = 21,b = 10;
BinaryExpression setA = Expression.Assign(a, Expression.Constant(21));
BinaryExpression setB = Expression.Assign(b, Expression.Constant(20));

// Console.Write("a == b：");
// Console.WriteLine(a == b);
MethodCallExpression call1 = Expression.Call(null,
    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
    Expression.Constant("a == b："));
MethodCallExpression call11 = Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.Equal(a, b));

// Console.Write("a < b ：");
// Console.WriteLine(a < b);
MethodCallExpression call2 = Expression.Call(null,
    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
    Expression.Constant("a < b ："));
MethodCallExpression call22 = Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.LessThan(a, b));

// Console.Write("a > b ：");
// Console.WriteLine(a > b);
MethodCallExpression call3 = Expression.Call(null,
    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
    Expression.Constant("a > b ："));
MethodCallExpression call33 = Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.GreaterThan(a, b));


// 改变 a 和 b 的值 
// a = 5;
// b = 20;
BinaryExpression setAa = Expression.Assign(a, Expression.Constant(5));
BinaryExpression setBb = Expression.Assign(b, Expression.Constant(20));

// Console.Write("a <= b：");
// Console.WriteLine(a <= b);
MethodCallExpression call4 = Expression.Call(null,
    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
    Expression.Constant("a <= b："));
MethodCallExpression call44 = Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.LessThanOrEqual(a, b));

// Console.Write("a >= b：");
// Console.WriteLine(b >= a);
MethodCallExpression call5 = Expression.Call(null,
    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
    Expression.Constant("a >= b："));
MethodCallExpression call55 = Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.GreaterThanOrEqual(a, b));

BlockExpression block = Expression.Block(new ParameterExpression[] { a, b },
    setA,
    setB,
    call1,
    call11,
    call2,
    call22,
    call3,
    call33,
    setAa,
    setBb,
    call4,
    call44,
    call5,
    call55
    );

Expression<Action> lambda = Expression.Lambda<Action>(block);
lambda.Compile()();
Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action>() {
    .Block(
        System.Int32 $a,
        System.Int32 $b) {
        $a = 21;
        $b = 20;
        .Call System.Console.Write("a == b：");
        .Call System.Console.WriteLine($a == $b);
        .Call System.Console.Write("a < b ：");
        .Call System.Console.WriteLine($a < $b);
        .Call System.Console.Write("a > b ：");
        .Call System.Console.WriteLine($a > $b);
        $a = 5;
        $b = 20;
        .Call System.Console.Write("a <= b：");
        .Call System.Console.WriteLine($a <= $b);
        .Call System.Console.Write("a >= b：");
        .Call System.Console.WriteLine($a >= $b)
    }
}
```

### 逻辑运算符：&&、||、!

| 运算符 | 描述                                                         |      |                                                              |
| :----- | :----------------------------------------------------------- | ---- | ------------------------------------------------------------ |
| &&     | 称为逻辑与运算符。如果两个操作数都非零，则条件为真。         |      |                                                              |
| \      | \                                                            |      | 称为逻辑或运算符。如果两个操作数中有任意一个非零，则条件为真。 |
| !      | 称为逻辑非运算符。用来逆转操作数的逻辑状态。如果条件为真则逻辑非运算符将使其为假。 |      |                                                              |

逻辑运算符的运行，结果是 true 或 false。

| 逻辑运算符 | 表达式树             |      |                     |
| ---------- | -------------------- | ---- | ------------------- |
| &&         | Expression.AndAlso() |      |                     |
| \          | \                    |      | Expression.OrElse() |
| ！         | Expression.Not()     |      |                     |

```c#
int a = 10;
int b = 11;

Console.Write("[a == b && a > b]：");
Console.WriteLine(a == b && a > b);

Console.Write("[a > b || a == b]：");
Console.WriteLine(a > b || a == b);

Console.Write("[!(a == b)]：");
Console.WriteLine(!(a == b));
Console.ReadKey();
```

使用表达式树编写

```c#
//int a = 10;
//int b = 11;
ParameterExpression a = Expression.Parameter(typeof(int), "a");
ParameterExpression b = Expression.Parameter(typeof(int), "b");
BinaryExpression setA = Expression.Assign(a, Expression.Constant(10));
BinaryExpression setB = Expression.Assign(b, Expression.Constant(11));

//Console.Write("[a == b && a > b]：");
//Console.WriteLine(a == b && a > b);
MethodCallExpression call1 = Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("[a == b && a > b]："));

MethodCallExpression call2 = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
     Expression.AndAlso(Expression.Equal(a, b), Expression.GreaterThan(a, b))
    );

//Console.Write("[a > b || a == b]：");
//Console.WriteLine(a > b || a == b);
MethodCallExpression call3 = Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("[a > b || a == b]："));
MethodCallExpression call4 = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.OrElse(Expression.Equal(a, b), Expression.GreaterThan(a, b))
    );

//Console.Write("[!(a == b)]：");
//Console.WriteLine(!(a == b));
MethodCallExpression call5 = Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("[!(a == b)]："));
MethodCallExpression call6 = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(bool) }),
    Expression.Not(Expression.Equal(a, b))
    );
BlockExpression block = Expression.Block(
    new ParameterExpression[] { a, b },
    setA,
    setB,
    call1,
    call2,
    call3,
    call4,
    call5,
    call6
    );

Expression<Action> lambda = Expression.Lambda<Action>(block);
lambda.Compile()();
Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action>() {
    .Block(
        System.Int32 $a,
        System.Int32 $b) {
        $a = 10;
        $b = 11;
        .Call System.Console.Write("[a == b && a > b]：");
        .Call System.Console.WriteLine($a == $b && $a > $b);
        .Call System.Console.Write("[a > b || a == b]：");
        .Call System.Console.WriteLine($a == $b || $a > $b);
        .Call System.Console.Write("[!(a == b)]：");
        .Call System.Console.WriteLine(!($a == $b))
    }
}
```

### 位运算符：&、|、^、~、<<、>>

| 运算符 | 描述                                                         | 实例                                                         |      |                              |
| :----- | :----------------------------------------------------------- | :----------------------------------------------------------- | ---- | ---------------------------- |
| &      | 如果同时存在于两个操作数中，二进制 AND 运算符复制一位到结果中。 | (A & B) 将得到 12，即为 0000 1100                            |      |                              |
| \      |                                                              | 如果存在于任一操作数中，二进制 OR 运算符复制一位到结果中。   | (A \ | B) 将得到 61，即为 0011 1101 |
| ^      | 如果存在于其中一个操作数中但不同时存在于两个操作数中，二进制异或运算符复制一位到结果中。 | (A ^ B) 将得到 49，即为 0011 0001                            |      |                              |
| ~      | 按位取反运算符是一元运算符，具有"翻转"位效果，即0变成1，1变成0，包括符号位。 | (~A ) 将得到 -61，即为 1100 0011，一个有符号二进制数的补码形式。 |      |                              |
| <<     | 二进制左移运算符。左操作数的值向左移动右操作数指定的位数。   | A << 2 将得到 240，即为 1111 0000                            |      |                              |
| >>     | 二进制右移运算符。左操作数的值向右移动右操作数指定的位数。   | A >> 2 将得到 15，即为 0000 1111                             |      |                              |

限于篇幅，就写示例了。

| 位运算符 | 表达式树                                                 |                                                  |
| -------- | -------------------------------------------------------- | ------------------------------------------------ |
| &        | Expression.Add(Expression left, Expression right)        |                                                  |
| \        |                                                          | Expression.Or(Expression left, Expression right) |
| ^        | Expression.ExclusiveOr(Expression expression)            |                                                  |
| ~        | Expression.OnesComplement( Expression expression)        |                                                  |
| <<       | Expression.LeftShift(Expression left, Expression right)  |                                                  |
| >>       | Expression.RightShift(Expression left, Expression right) |                                                  |

### 赋值运算符

| 运算符 | 描述                                                         | 实例                            |      |                    |      |
| :----- | :----------------------------------------------------------- | :------------------------------ | ---- | ------------------ | ---- |
| =      | 简单的赋值运算符，把右边操作数的值赋给左边操作数             | C = A + B 将把 A + B 的值赋给 C |      |                    |      |
| +=     | 加且赋值运算符，把右边操作数加上左边操作数的结果赋值给左边操作数 | C += A 相当于 C = C + A         |      |                    |      |
| -=     | 减且赋值运算符，把左边操作数减去右边操作数的结果赋值给左边操作数 | C -= A 相当于 C = C - A         |      |                    |      |
| *=     | 乘且赋值运算符，把右边操作数乘以左边操作数的结果赋值给左边操作数 | C *= A 相当于 C = C* A          |      |                    |      |
| /=     | 除且赋值运算符，把左边操作数除以右边操作数的结果赋值给左边操作数 | C /= A 相当于 C = C / A         |      |                    |      |
| %=     | 求模且赋值运算符，求两个操作数的模赋值给左边操作数           | C %= A 相当于 C = C % A         |      |                    |      |
| <<=    | 左移且赋值运算符                                             | C <<= 2 等同于 C = C << 2       |      |                    |      |
| >>=    | 右移且赋值运算符                                             | C >>= 2 等同于 C = C >> 2       |      |                    |      |
| &=     | 按位与且赋值运算符                                           | C &= 2 等同于 C = C & 2         |      |                    |      |
| ^=     | 按位异或且赋值运算符                                         | C ^= 2 等同于 C = C ^ 2         |      |                    |      |
| \      | =                                                            | 按位或且赋值运算符              | C \  | = 2 等同于 C = C \ | 2    |

限于篇幅,请自行领略... ...

| 运算符 | 表达式树                     |                     |
| :----- | :--------------------------- | ------------------- |
| =      | Expression.Assign            |                     |
| +=     | Expression.AddAssign         |                     |
| -=     | Expression.SubtractAssign    |                     |
| *=     | Expression.MultiplyAssign    |                     |
| /=     | Expression.DivideAssign      |                     |
| %=     | Expression.ModuloAssign      |                     |
| <<=    | Expression.LeftShiftAssign   |                     |
| >>=    | Expression.RightShiftAssign  |                     |
| &=     | Expression.AndAssign         |                     |
| ^=     | Expression.ExclusiveOrAssign |                     |
| \      | =                            | Expression.OrAssign |

`^=` ，注意有两种意思一种是位运算符的`异或(ExclusiveOrAssign)`，一种是算术运算符的`幂运算(PowerAssign)`。

### 其他运算符

| 运算符   | 描述                                   | 实例                                                         |
| :------- | :------------------------------------- | :----------------------------------------------------------- |
| sizeof() | 返回数据类型的大小。                   | sizeof(int)，将返回 4.                                       |
| typeof() | 返回 class 的类型。                    | typeof(StreamReader);                                        |
| &        | 返回变量的地址。                       | &a; 将得到变量的实际地址。                                   |
| *        | 变量的指针。                           | *a; 将指向一个变量。                                         |
| ? :      | 条件表达式                             | 如果条件为真 ? 则为 X : 否则为 Y                             |
| is       | 判断对象是否为某一类型。               | If( Ford is Car) // 检查 Ford 是否是 Car 类的一个对象。      |
| as       | 强制转换，即使转换失败也不会抛出异常。 | Object obj = new StringReader("Hello"); StringReader r = obj as StringReader; |

表达式树里面暂未找到这些运算符的如何编写。

## 判断语句

C# 提供了以下类型的判断语句：

| 语句           | 描述                                                         |
| :------------- | :----------------------------------------------------------- |
| if             | 一个 **if 语句** 由一个布尔表达式后跟一个或多个语句组成。    |
| if...else      | 一个 **if 语句** 后可跟一个可选的 **else 语句**，else 语句在布尔表达式为假时执行。 |
| 嵌套 if 语句   | 您可以在一个 **if** 或 **else if** 语句内使用另一个 **if** 或 **else if** 语句。 |
| switch 语句    | 一个 **switch** 语句允许测试一个变量等于多个值时的情况。     |
| 嵌套 switch 语 | 您可以在一个 **switch** 语句内使用另一个 **switch** 语句。   |

当然还有 `??`、`?:` 等判断，下面将详细实践。

### if

If 语句，使用 `IfThen(Expression test, Expression ifTrue);` 来表达

`Expression test`表示用于判断的表达式，`Expression ifTrue`表示结果为 true 时执行的表达式树。

示例

```c#
int a = 10;
int b = 10;

if (a == b)
{
    Console.WriteLine("a == b 为 true，语句被执行");
}

Console.ReadKey();
```

使用表达式树实现如下

```c#
ParameterExpression a = Expression.Variable(typeof(int), "a");
ParameterExpression b = Expression.Variable(typeof(int), "b");
MethodCallExpression call = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a == b 为 true，表达式树被执行"));

ConditionalExpression _if = Expression.IfThen(Expression.Equal(a, b),call);

Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(_if,a,b);
lambda.Compile()(10,10);

Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`2[System.Int32,System.Int32]>(
    System.Int32 $a,
    System.Int32 $b) {
    .If ($a == $b) {
        .Call System.Console.WriteLine("a == b 为 true，表达式树被执行")
    } .Else {
        .Default(System.Void)
    }
}
```

### if...else

if...else 使用以下表达式树表示

```c#
 ConditionalExpression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse);
```

示例代码如下

```c#
int a = 10;
int b = 11;

if (a == b)
{
    Console.WriteLine("a == b 为 true，此语句被执行");
}
else
{
    Console.WriteLine("a == b 为 false，此语句被执行");
}
Console.ReadKey();
```

用表达式树实现如下

```c#
ParameterExpression a = Expression.Variable(typeof(int), "a");
ParameterExpression b = Expression.Variable(typeof(int), "b");
MethodCallExpression call1 = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a == b 为 true，此表达式树被执行"));

MethodCallExpression call2 = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a == b 为 false，此表达式树被执行"));

ConditionalExpression _if = Expression.IfThenElse(Expression.Equal(a, b), call1,call2);

Expression<Action<int, int>> lambda = Expression.Lambda<Action<int, int>>(_if, a, b);
lambda.Compile()(10, 11);

Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`2[System.Int32,System.Int32]>(
    System.Int32 $a,
    System.Int32 $b) {
    .If ($a == $b) {
        .Call System.Console.WriteLine("a == b 为 true，此表达式树被执行")
    } .Else {
        .Call System.Console.WriteLine("a == b 为 false，此表达式树被执行")
    }
}
```

### switch

示例代码如下

```c#
int a = 2;
switch (a)
{
    case 1:Console.WriteLine("a == 1");break;
    case 2:Console.WriteLine("a == 2");break;
    default:Console.WriteLine("a != 1 && a = 2");
}

Console.ReadKey();
```

每个 case 使用 SwitchCase 类型表示，使用 Expression.SwitchCase 生成 SwitchCase 类型。

Expression.Switch 用来构建一个 switch 表达式树，

Expression.Switch 的重载比较多，常用的是这种形式

```c#
SwitchExpression Switch(Expression switchValue, Expression defaultBody, params SwitchCase[] cases);
```

switchValue 表示传入参数；

defaultBody 表示 default 执行的表达式；

cases 表示多条 case 。

上面代码对应使用表达式树编写如下

```c#
ParameterExpression a = Expression.Parameter(typeof(int), "a");
MethodCallExpression _default = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a != 1 && a = 2"));

SwitchCase case1 = Expression.SwitchCase(
    Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a == 1")),
    new ConstantExpression[] { Expression.Constant(1) }
    );

SwitchCase case2 = Expression.SwitchCase(
    Expression.Call(null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("a == 2")),
    new ConstantExpression[] { Expression.Constant(2) }
    );

SwitchExpression _switch = Expression.Switch(a, _default, new SwitchCase[] { case1, case2 });
Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(_switch, a);
lambda.Compile()(1);

Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`1[System.Int32]>(System.Int32 $a) {
    .Switch ($a) {
    .Case (1):
            .Call System.Console.WriteLine("a == 1")
    .Case (2):
            .Call System.Console.WriteLine("a == 2")
    .Default:
            .Call System.Console.WriteLine("a != 1 && a = 2")
    }
}
```

很奇怪，没有 break，但是表达式树是正常的，并且运行没问题；

### ?? 和 ?:

?? 表示空合并运算符，例如 `a ?? b`，如果 a 不为 null，即返回 a，否则返回 b；

常用定义如下

```c#
BinaryExpression Coalesce(Expression left, Expression right)
```

这里就不再赘述。

?: 是三元运算符，例如 a > b ? a : b 。

常用定义如下

```c#
ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
```

可以参考上面的 if...else 表达式树，这里不再赘述。

## 循环

C# 提供了以下几种循环类型。

| 循环类型         | 描述                                                         |
| :--------------- | :----------------------------------------------------------- |
| while 循环       | 当给定条件为真时，重复语句或语句组。它会在执行循环主体之前测试条件。 |
| for/foreach 循环 | 多次执行一个语句序列，简化管理循环变量的代码。               |
| do...while 循环  | 除了它是在循环主体结尾测试条件外，其他与 while 语句类似。    |
| 嵌套循环         | 您可以在 while、for 或 do..while 循环内使用一个或多个循环。  |

当然，还有以下用于控制循环的语句

| 控制语句      | 描述                                                         |
| :------------ | :----------------------------------------------------------- |
| break 语句    | 终止 **loop** 或 **switch** 语句，程序流将继续执行紧接着 loop 或 switch 的下一条语句。 |
| continue 语句 | 引起循环跳过主体的剩余部分，立即重新开始测试条件。           |

### LabelTarget

LabelTarget 是用于创建循环标记的。

无论是 for 还是 while ，平时编写循环时，都需要有跳出循环的判断，有时需要某个参数自增自减并且作为判断依据。

C# 表达式树里面是没有专门表示 for /while 的，里面只有一个 Loop。看一下Loop 生成的表达式树

```c#
.Lambda #Lambda1<System.Func`1[System.Int32]>() {
    .Block(System.Int32 $x) {
        $x = 0;
        .Loop  {
            .If ($x < 10) {
                $x++
            } .Else {
                .Break #Label1 { $x }
            }
        }
        .LabelTarget #Label1:
    }
}
```

要实现循环控制，有 break，contauine 两种 Expression：

```c#
public static GotoExpression Break(LabelTarget target, Type type);

public static GotoExpression Break(LabelTarget target, Expression value);

public static GotoExpression Break(LabelTarget target);

public static GotoExpression Break(LabelTarget target, Expression value, Type type);
public static GotoExpression Continue(LabelTarget target, Type type);

public static GotoExpression Continue(LabelTarget target);
```

所以，要实现循环控制，必须要使用 LabelTarget，不然就无限循环了。

要理解 LabelTarget ，最好的方法是动手做。

### for / while 循环

Expression.Loop 用于创建循环，包括 for 和 while，定义如下

```c#
public static LoopExpression Loop(Expression body, LabelTarget @break, LabelTarget @continue);

System.Linq.Expressions.LoopExpression.
public static LoopExpression Loop(Expression body);

public static LoopExpression Loop(Expression body, LabelTarget @break);
```

表达式树里面的循环，只有 Loop，无 for / while 的区别。

那么，我们来一步步理解 Loop 循环和 LabelTarget；

### 无限循环

```c#
while (true)
{
    Console.WriteLine("无限循环");
}
```

那么，对应的 Loop 重载是这种

```c#
public static LoopExpression Loop(Expression body)
```

使用表达式树编写

```c#
BlockExpression _block = Expression.Block(
    new ParameterExpression[] { },
    Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),Expression.Constant("无限循环") )
);

LoopExpression _loop = Expression.Loop(_block);

Expression<Action> lambda = Expression.Lambda<Action>(_loop);
lambda.Compile()();
```

### 最简单的循环

如果我想用表达式树做到如下最简单的循环，怎么写？

```c#
while (true)
{
    Console.WriteLine("我被执行一次就结束循环了");
    break;
}
```

表达式树编写

```c#
LabelTarget _break = Expression.Label();

BlockExpression _block = Expression.Block(
   new ParameterExpression[] { },
   Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("我被执行一次就结束循环了")), Expression.Break(_break));
LoopExpression _loop = Expression.Loop(_block, _break);

Expression<Action> lambda = Expression.Lambda<Action>(_loop);
lambda.Compile()();

Console.ReadKey();
```

生成的表达式树

```c#
.Lambda #Lambda1<System.Action>() {
    .Loop  {
        .Block() {
            .Call System.Console.WriteLine("我被执行一次就结束循环了");
            .Break #Label1 { }
        }
    }
    .LabelTarget #Label1:
}
```

首先要明确，`Expression.Label()` 里面可以为空，它是一种标记，不参与传递参数，不参与运算。有参无参，前后保持一致即可。

但是上面的循环只有一次，你可以将上面的标签改成这样试试 `LabelTarget _break = Expression.Label(typeof(int));`，原因后面找。

还有， Expression.Label() 变量需要一致，否则无法跳出。

试试一下代码

```c#
BlockExpression _block = Expression.Block(
   new ParameterExpression[] { },
   Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("我被执行一次就结束循环了")), Expression.Break(Expression.Label()));
LoopExpression _loop = Expression.Loop(_block, Expression.Label());

Expression<Action> lambda = Expression.Lambda<Action>(_loop);
lambda.Compile()();

Console.ReadKey();
```

里面用到了 Expression.Block()，Block() 是块，即{}。

如果 Block() 是在最外层，那么相当于是函数；如果是内嵌，相当于{}；

但不是真的这样。。。表达式树里面不是完全按照 C# 的语法来还原操作的。

对于 Block() 的使用，多加实践即可。

### 多次循环

写一个循环十次的循环语句

```c#
for (int i = 0; i < 10; i++)
{
    if (i < 10)
    {
        Console.WriteLine(i);
    }
    else
        break;
}
```

或者使用 while 表示

```c#
int i = 0;
while (true)
{
    if (i < 10)
    {
        Console.WriteLine(i);
    }
    else
        break;
    i++;
}
```

使用表达式树编写

```c#
LabelTarget _break = Expression.Label(typeof(int));
ParameterExpression a = Expression.Variable(typeof(int), "a");

BlockExpression _block = Expression.Block(new ParameterExpression[] { },
    Expression.IfThenElse
    (
        Expression.LessThan(a, Expression.Constant(10)),
        Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a),
        Expression.Break(_break, a)
    ),
    Expression.PostIncrementAssign(a)   // a++
    );


LoopExpression _loop = Expression.Loop(_block, _break);

Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(_loop, a);
lambda.Compile()(0);
Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`1[System.Int32]>(System.Int32 $a) {
    .Loop  {
        .Block() {
            .If ($a < 10) {
                .Call System.Console.WriteLine($a)
            } .Else {
                .Break #Label1 { $a }
            };
            $a++
        }
    }
    .LabelTarget #Label1:
}
```

试试将 `Expression.Break(_break, a)` 改成 `Expression.Break(_break)`。看看报什么错。。。

解决方法是，上面的标记也改成 `LabelTarget _break = Expression.Label();`。

就跟你写代码写注释一样，里面的东西是为了让别人看代码是容易理解。

有些同学纠结于 `Expression.Label(有参或无参);`，`Expression.Break(_break, a)` 与 `Expression.Break(_break)`，只要看看最终生成的表达式树就清楚了。

### break 和 continue 一起

C# 循环代码如下

```c#
int i = 0;
while (true)
{
    if (i < 10)
    {
        if (i % 2 == 0)
        {
            Console.Write("i是偶数:");
            Console.WriteLine(i);
            i++;
            continue;
        }
        Console.WriteLine("其他任务 --");
        Console.WriteLine("其他任务 --");
    }
    else break;
    i++;
}
```

使用 C# 表达式树编写(笔者将步骤详细拆分了，所以代码比较长)

```c#
ParameterExpression a = Expression.Variable(typeof(int), "a");

LabelTarget _break = Expression.Label();
LabelTarget _continue = Expression.Label();

//        if (i % 2 == 0)
//        {
//            Console.Write("i是偶数:");
//            Console.WriteLine(i);
//            i++;
//            continue;
//        }
ConditionalExpression _if = Expression.IfThen(
    Expression.Equal(Expression.Modulo(a, Expression.Constant(2)), Expression.Constant(0)),
    Expression.Block(
        new ParameterExpression[] { },
        Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("i是偶数:")),
        Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a),
        Expression.PostIncrementAssign(a),
        Expression.Continue(_continue)
        )
    );

//        if (i % 2 == 0)
//        {
//            Console.Write("i是偶数:");
//            Console.WriteLine(i);
//            i++;
//            continue;
//        }
//        Console.WriteLine("其他任务 --");
//        Console.WriteLine("其他任务 --");
BlockExpression block1 = Expression.Block(
    new ParameterExpression[] { },
    _if,
    Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("其他任务 --")),
    Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("其他任务 --"))
    );

//    if (i < 10)
//    {
//        if (i % 2 == 0)
//        {
//            Console.Write("i是偶数:");
//            Console.WriteLine(i);
//            i++;
//            continue;
//        }
//        Console.WriteLine("其他任务 --");
//        Console.WriteLine("其他任务 --");
//    }
//    else break;
ConditionalExpression if_else = Expression.IfThenElse(
   Expression.LessThan(a, Expression.Constant(10)),
    block1,
    Expression.Break(_break)
    );


//    if (i < 10)
//    {
//        if (i % 2 == 0)
//        {
//            Console.Write("i是偶数:");
//            Console.WriteLine(i);
//            i++;
//            continue;
//        }
//        Console.WriteLine("其他任务 --");
//        Console.WriteLine("其他任务 --");
//    }
//    else break;
//    i++ ;

BlockExpression block2 = Expression.Block(
    new ParameterExpression[] { },
    if_else,
    Expression.PostIncrementAssign(a)
    );
// while(true)
LoopExpression loop = Expression.Loop(block2, _break, _continue);

Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(loop, a);
lambda.Compile()(0);
Console.ReadKey();
```

生成的表达式树如下

```c#
.Lambda #Lambda1<System.Action`1[System.Int32]>(System.Int32 $a) {
    .Loop .LabelTarget #Label1: {
        .Block() {
            .If ($a < 10) {
                .Block() {
                    .If (
                        $a % 2 == 0
                    ) {
                        .Block() {
                            .Call System.Console.Write("i是偶数:");
                            .Call System.Console.WriteLine($a);
                            $a++;
                            .Continue #Label1 { }
                        }
                    } .Else {
                        .Default(System.Void)
                    };
                    .Call System.Console.WriteLine("其他任务 --");
                    .Call System.Console.WriteLine("其他任务 --")
                }
            } .Else {
                .Break #Label2 { }
            };
            $a++
        }
    }
    .LabelTarget #Label2:
}
```

为了便于理解，上面的代码拆分了很多步。

来个简化版本

```c#
ParameterExpression a = Expression.Variable(typeof(int), "a");

LabelTarget _break = Expression.Label();
LabelTarget _continue = Expression.Label();

LoopExpression loop = Expression.Loop(
    Expression.Block(
        new ParameterExpression[] { },
        Expression.IfThenElse(
            Expression.LessThan(a, Expression.Constant(10)),
            Expression.Block(
                new ParameterExpression[] { },
                Expression.IfThen(
                    Expression.Equal(Expression.Modulo(a, Expression.Constant(2)), Expression.Constant(0)),
                    Expression.Block(
                        new ParameterExpression[] { },
                        Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("i是偶数:")),
                        Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), a),
                        Expression.PostIncrementAssign(a),
                        Expression.Continue(_continue)
                        )
                    ),
                Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("其他任务 --")),
                Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("其他任务 --"))
                ),
            Expression.Break(_break)
            ),
        Expression.PostIncrementAssign(a)
        ),
    _break,
    _continue
    );

Expression<Action<int>> lambda = Expression.Lambda<Action<int>>(loop, a);
lambda.Compile()(0);
Console.ReadKey();
```

需要注意的是，`Expression.Break` `Expression.Continue` 有所区别。

当标签实例化都是 `Expression.Label()` 时，

```c#
Expression.Break(label);
Expression.Continu(label);
```

区别在于 continu 只能用 Expression.Label()。

Break 可以这样

```c#
LabelTarget label = Expression.Label ( typeof ( int ) );
ParameterExpression a = Expression.Variable(typeof(int), "a");

Expression.Break ( label , a )
```



## 值类型、引用类型、泛型、集合、调用函数

### 一，定义变量

C# 表达式树中，定义一个变量，使用 `ParameterExpression`。

创建变量结点的方法有两种，

```
Expression.Parameter()
Expression.Variable()
// 另外，定义一个常量可以使用 Expression.Constant()。
```

两种方式都是生成 `ParameterExpression` 类型 `Parameter()` 和 `Variable()` 都具有两个重载。他们创建一个 ParameterExpression节点，该节点可用于标识表达式树中的参数或变量。

对于使用定义：

`Expression.Variable` 用于在块内声明局部变量。

`Expression.Parameter`用于声明输入值的参数。

先看第一种

```c#
public static ParameterExpression Parameter(Type type)
{
    return Parameter(type, name: null);
}

        public static ParameterExpression Variable(Type type)
{
    return Variable(type, name: null);
}
```

从代码来看，没有区别。

再看看具有两个参数的重载

```c#
public static ParameterExpression Parameter(Type type, string name)
{
    Validate(type, allowByRef: true);
    bool byref = type.IsByRef;
    if (byref)
    {
        type = type.GetElementType();
    }

    return ParameterExpression.Make(type, name, byref);
}
public static ParameterExpression Variable(Type type, string name)
{
    Validate(type, allowByRef: false);
    return ParameterExpression.Make(type, name, isByRef: false);
}
```

如你所见，两者只有一个 allowByRef 出现了区别，Paramter 允许 Ref， Variable 不允许。

笔者在官方文档和其他作者文章上，都没有找到具体区别是啥，去 stackoverflow 搜索和查看源代码后，确定他们的区别在于 Variable 不能使用 ref 类型。

从字面意思来看，声明一个变量，应该用`Expression.Variable`， 函数的传入参数应该使用`Expression.Parameter`。

无论值类型还是引用类型，都是这样子定义。

### 二，访问变量/类型的属性字段和方法

访问变量或类型的属性，使用

```c#
Expression.Property()
```

访问变量/类型的属性或字段，使用

```c#
Expression.PropertyOrField()
```

访问变量或类型的方法，使用

```c#
Expression.Call()
```

访问属性字段和方法

```c#
Expression.MakeMemberAccess
```

他们都返回一个 MemberExpression类型。

使用上，根据实例化/不实例化，有个小区别，上面说了变量或类型。

意思是，已经定义的值类型或实例化的引用类型，是变量；

类型，就是指引用类型，不需要实例化的静态类型或者静态属性字段/方法。

上面的解释不太严谨，下面示例会慢慢解释。

#### 1. 访问属性

使用 `Expression.Property()` 或 `Expression.PropertyOrField()`调用属性。

##### 调用静态类型属性

Console 是一个静态类型，Console.Title 可以获取编译器程序的实际位置。

```c#
Console.WriteLine(Console.Title);
```

使用表达式树表达如下

```c#
MemberExpression member = Expression.Property(null, typeof(Console).GetProperty("Title"));
Expression<Func<string>> lambda = Expression.Lambda<Func<string>>(member);

string result = lambda.Compile()();
Console.WriteLine(result);

Console.ReadKey();
```

因为调用的是静态类型的属性，所以第一个参数为空。

第二个参数是一个 PropertyInfo 类型。

##### 调用实例属性/字段

C#代码如下

```c#
List<int> a = new List<int>() { 1, 2, 3 };
int result = a.Count;
Console.WriteLine(result);
Console.ReadKey();
```

在表达式树，调用实例的属性

```c#
ParameterExpression a = Expression.Parameter(typeof(List<int>), "a");
MemberExpression member = Expression.Property(a, "Count");

Expression<Func<List<int>, int>> lambda = Expression.Lambda<Func<List<int>, int>>(member, a);
int result = lambda.Compile()(new List<int> { 1, 2, 3 });
Console.WriteLine(result);

Console.ReadKey();
```

除了 Expression.Property() ，其他的方式请自行测试，这里不再赘述。

#### 2. 调用函数

使用 `Expression.Call()` 可以调用一个静态类型的函数或者实例的函数。

##### 调用静态类型的函数

以 Console 为例，调用 WriteLine() 方法

```c#
Console.WriteLine("调用WriteLine方法");

MethodCallExpression method = Expression.Call(
    null,
    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
    Expression.Constant("调用WriteLine方法"));

Expression<Action> lambda = Expression.Lambda<Action>(method);
lambda.Compile()();
Console.ReadKey();
```

Expression.Call() 的重载方法比较多，常用的重载方法是

```c#
public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
```

因为要调用静态类型的函数，所以第一个 instance 为空(instance英文意思是实例)。

第二个 method 是要调用的重载方法。

最后一个 arguments 是传入的参数。

##### 调用实例的函数

写一个类

```c#
public class Test
{
    public void Print(string info)
    {
        Console.WriteLine(info);
    }
}
```

调用实例的 Printf() 方法

```c#
Test test = new Test();
test.Print("打印出来");
Console.ReadKey();
```

表达式表达如下

```c#
ParameterExpression a = Expression.Variable(typeof(Test), "test");

MethodCallExpression method = Expression.Call(
    a,
    typeof(Test).GetMethod("Print", new Type[] { typeof(string) }),
    Expression.Constant("打印出来")
    );

Expression<Action<Test>> lambda = Expression.Lambda<Action<Test>>(method,a);
lambda.Compile()(new Test());
Console.ReadKey();
```

注意的是，`Expression.Variable(typeof(Test), "test");` 仅定义了一个变量，还没有初始化/赋值。对于引用类型来说，需要实例化。

上面的方式，是通过外界实例化传入里面的，后面会说如何在表达式内实例化。

### 三，实例化引用类型

引用类型的实例化，使用 new ，然后选择调用合适的构造函数、设置属性的值。

那么，根据上面的步骤，我们分开讨论。

#### new

使用 `Expression.New()`来调用一个类型的构造函数。

他有五个重载，有两种常用重载：

```c#
 public static NewExpression New(ConstructorInfo constructor);
 public static NewExpression New(Type type);
```

依然使用上面的 Test 类型

```c#
NewExpression newA = Expression.New(typeof(Test));
```

默认没有参数的构造函数，或者只有一个构造函数，像上面这样调用。

如果像指定一个构造函数，可以

```c#
NewExpression newA = Expression.New(typeof(Test).GetConstructor(xxxxxx));
```

这里就不详细说了。

#### 给属性赋值

实例化一个构造函数的同时，可以给属性赋值。

```c#
public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings);

public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings);
```

两种重载是一样的。

我们将 Test 类改成

```c#
public class Test
{
    public int sample { get; set; }
    public void Print(string info)
    {
        Console.WriteLine(info);
    }
}
```

然后

```c#
var binding = Expression.Bind(
    typeof(Test).GetMember("sample")[0],
    Expression.Constant(10)
);
```

#### 创建引用类型

```c#
Expression.MemberInit()
```

表示调用构造函数并初始化新对象的一个或多个成员。

如果实例化一个类，可以使用

```c#
NewExpression newA = Expression.New(typeof(Test));
MemberInitExpression test = Expression.MemberInit(newA,
    new List<MemberBinding>() { }
    );
```

如果要在实例化时给成员赋值

```c#
NewExpression newA = Expression.New(typeof(Test));

// 给 Test 类型的一个成员赋值
var binding = Expression.Bind(
    typeof(Test).GetMember("sample")[0],Expression.Constant(10));

MemberInitExpression test = Expression.MemberInit(newA,
    new List<MemberBinding>() { binding}
    );
```

#### 示例

实例化一个类型，调用构造函数、给成员赋值，示例代码如下

```c#
// 调用构造函数
NewExpression newA = Expression.New(typeof(Test));

// 给 Test 类型的一个成员赋值
var binding = Expression.Bind(
    typeof(Test).GetMember("sample")[0], Expression.Constant(10));

// 实例化一个类型
MemberInitExpression test = Expression.MemberInit(newA,
    new List<MemberBinding>() { binding }
    );

// 调用方法
MethodCallExpression method1 = Expression.Call(
    test,
    typeof(Test).GetMethod("Print", new Type[] { typeof(string) }),
    Expression.Constant("打印出来")
    );

// 调用属性
MemberExpression method2 = Expression.Property(test, "sample");

Expression<Action> lambda1 = Expression.Lambda<Action>(method1);
lambda1.Compile()();

Expression<Func<int>> lambda2 = Expression.Lambda<Func<int>>(method2);
int sample = lambda2.Compile()();
Console.WriteLine(sample);

Console.ReadKey();
```

### 四，实例化泛型类型于调用

将 Test 类，改成这样

```c#
public class Test<T>
{
    public void Print<T>(T info)
    {
        Console.WriteLine(info);
    }
}
```

Test 类已经是一个泛型类，表达式实例化示例

```c#
static void Main(string[] args)
{
    RunExpression<string>();
    Console.ReadKey();
}
public static void RunExpression<T>()
{
    // 调用构造函数
    NewExpression newA = Expression.New(typeof(Test<T>));

    // 实例化一个类型
    MemberInitExpression test = Expression.MemberInit(newA,
        new List<MemberBinding>() { }
        );

    // 调用方法
    MethodCallExpression method = Expression.Call(
        test,
        typeof(Test<T>).GetMethod("Print").MakeGenericMethod(new Type[] { typeof(T) }),
        Expression.Constant("打印出来")
        );

    Expression<Action> lambda1 = Expression.Lambda<Action>(method);
    lambda1.Compile()();

    Console.ReadKey();
}
```

### 五，定义集合变量、初始化、添加元素

集合类型使用 `ListInitExpression`表示。

创建集合类型，需要使用到

ElementInit 表示 IEnumerable集合的单个元素的初始值设定项。

ListInit 初始化一个集合。

C# 中，集合都实现了 IEnumerable，集合都具有 Add 扥方法或属性。

使用 C# 初始化一个集合并且添加元素，可以这样

```c#
List<string> list = new List<string>()
{
    "a",
    "b"
};
list.Add("666");
```

而在表达式树里面，是通过 ElementInit 调用 Add 方法初始化/添加元素的。

示例

```c#
MethodInfo listAdd = typeof(List<string>).GetMethod("Add");

/*
 * new List<string>()
 * {
 *     "a",
 *     "b"
 * };
 */
ElementInit add1 = Expression.ElementInit(
    listAdd,
    Expression.Constant("a"),
    Expression.Constant("b")
    );
// Add("666")
ElementInit add2 = Expression.ElementInit(listAdd, Expression.Constant("666"));
```

示例

```c#
MethodInfo listAdd = typeof(List<string>).GetMethod("Add");

ElementInit add1 = Expression.ElementInit(listAdd, Expression.Constant("a"));
ElementInit add2 = Expression.ElementInit(listAdd, Expression.Constant("b"));
ElementInit add3 = Expression.ElementInit(listAdd, Expression.Constant("666"));

NewExpression list = Expression.New(typeof(List<string>));

// 初始化值
ListInitExpression setList = Expression.ListInit(
    list,
    add1,
    add2,
    add3
    );
// 没啥执行的，就这样看看输出的信息
Console.WriteLine(setList.ToString());

MemberExpression member = Expression.Property(setList, "Count");

Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(member);
int result = lambda.Compile()();
Console.WriteLine(result);

Console.ReadKey();
```





本文参考资源：

- [1. 表达式树基础 · C# 表达式树教程大全 ](https://ex.whuanle.cn/1.base.html)
- [Expression 核心操作符、表达式、操作方法_expression 溢位检查-CSDN博客](https://blog.csdn.net/zhuqinfeng/article/details/70168337)
- [Linq To SQL 自定义表达式树](https://www.albahari.com/expressions)









