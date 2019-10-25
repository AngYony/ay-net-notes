# C# 特性、反射、元数据和动态编程

特性描述：

> 公共语言运行时使你能够添加类似于关键字的描述性声明（称为特性），以便批注编程元素（如类型、字段、方法和属性）。 编译运行时的代码时，它将被转换为 Microsoft 中间语言 (MSIL)，并和编译器生成的元数据一起放置在可移植可执行 (PE) 文件内。 特性使你能够将额外的描述性信息放到可使用运行时反射服务提取的元数据中。 当你声明派生自 [`System.Attribute`](https://docs.microsoft.com/zh-cn/dotnet/api/system.attribute)的特殊类的实例时，编译器会创建特性。 
>
> .NET Framework 出于多种原因且为解决许多问题而使用特性。 特性描述如何将数据序列化、指定用于强制安全性的特征并限制通过实时 (JIT) 编译器进行优化，从而使代码易于调试。 特性还可记录文件的名称或代码的作者，或控制窗体开发过程中控件和成员的可见性。 
>
> 【注：本段说明来自于微软官方文档：https://docs.microsoft.com/zh-cn/dotnet/standard/attributes/】

反射描述：

> 反射提供描述程序集、模块和类型的对象（`Type`类型）。 可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型，然后调用其方法或访问器字段和属性。 如果代码中使用了特性，可以利用反射来访问它们。



## 自定义特性

在编写自定义特性之前，先了解一下当编译器遇到代码中某个应用了自定义特性的元素时，是如何处理的。

例如：

```c#
[FieldName("SocialSecurityNumber")]
public string SocialSecurityNumber { get; set; }
```

当C#编译器发现这个属性应用了一个`FieldName`特性时，首先会把字符串`Attribute`追加到这个名称的后面，形成一个组合名称`FieldNameAttribute`，然后在`using`语句中提及的所有命名空间中搜索该组合名称对应的类（这个类直接或间接派生自`System.Attribute`）。注意：如果标记数据项的特性（如上述中的`FieldName`）本身以`Attribute`结尾，编译器就不会再把`Attribute`字符串加到组合名称中，而是保留原特性名。

#### 编写自定义特性

编写自定义特性需要注意以下几点：

- 特性类名称以`Attribute`结尾，并且该类直接或间接派生自`System.Attribute`。
- 特性类需要包含控制特性用法的信息，如：特性可以应用到哪些类型的程序元素上（类、结构、属性和方法等）；是否可以多次应用到同一个程序集上；是否由派生类和接口继承等。

```c#
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false, Inherited=false)]
class FieldNameAttribute : Attribute
{
    public string Comment { get; set; }
    private string _fileName;
    public FieldNameAttribute(string fileName)
    {
        _fileName = fileName;
    }
}
```

#### 指定AttributeUsage特性

特性类本身用一个特殊的特性——`System.AttributeUsage`特性来标记，它更像一个元特性，因为它只能应用在其他特性上，不能应用到类上，C#编译器为它提供了特殊的支持。

`AttributeUsage`主要用于标识自定义特性可以应用到哪些类型的程序元素上。这些信息由它的第一个参数给出，该参数是必选的，其类型是枚举类型`AttributeTargets`，在`AttributeTargets`枚举值中有两个特殊的值：`Assembly`和`Module`，使用了这两个值定义的特性，可以应用到整个程序集或模块中，而不是应用到代码中的一个元素上（其他值定义的特性使用时需要放在元素前面的方括号中），在这种情况下，这个特性可以放在源代码的任何地方，但需要使用关键字`Assembly`或`Module`作为前缀，例如：

```
[assembly:SomeAssemblyAttribute(Parameters)]
[module:SomeModuleAttribute(Parameters)]
```

在指定自定义特性的有效目标元素时，多个`AttributeTargets`的枚举值可以使用按位`OR`运算符“`|`”把这些值组合起来。例如上述中的`FieldName`特性可以同时应用到属性和字段上：

```c#
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
class FieldNameAttribute : Attribute
```

也可以使用`AttributeTargets.All`指定自定义特性可以应用到所有类型的程序元素上。

除了`AttributeTargets`之外，`AttributeUsage`特性还包含另外两个参数：`AllowMultiple`和`Inherited`。它们使用不同的语法形式来指定：`<ParameterName>=<ParameterValue>`。注意：必须使用这种形式，必须指定参数名称和对应的值，而不是像方法传参那样只给出参数的值，因为它不是方法，而是一种特殊规则。这种形式同样适用于自定义的特性应用于其他元素时需要指明的可选的参数和对应的值。

`AllowMultiple`参数表示一个特性是否可以多次应用到同一个项上，当指定为`false`，同一个项多次应用了该特性就会产生错误。

`Inherited`参数设置为`true`，就表示应用到类或接口上的特性也可以自动应用到所有派生的类或接口上。如果特性应用到方法或属性上，它就可以自动应用到该方法或属性等的重写版本上。

#### 指定特性参数

```c#
[FieldName("SocialSecurityNumber",Comment ="测试")]
public string SocialSecurityNumber { get; set; }
```

编译器会检查传递给特性的参数，并查找该特性中带这些参数的构造函数，在上述示例中，`FieldNameAttribute`类存在一个构造方法，并且只有一个字符串参数，因此在把FieldName特性应用到一个属性时，必须为它提供一个字符串作为参数，如上述代码所示。注意：在代码中，先不用关注`Comment ="测试"`，这是前面已经说过的特殊语法形式（`<ParameterName>=<ParameterValue>`）来指定其他可选的参数，具体见下述说明。

#### 指定特性的可选参数

在`AttributeUsage`特性中，可以使用另一种语法（`<ParameterName>=<ParameterValue>`），把可选参数添加到特性中。这种语法指定可选参数的名称和值，可选参数对应特性类中的公共属性或字段。例如：

```c#
[FieldName("SocialSecurityNumber",Comment ="测试")]
public string SocialSecurityNumber { get; set; }
```

代码中的Comment参数来自于FieldNameAttribute类中的

```c#
public string Comment { get; set; }
```

在本例中，编译器识别第二个参数的语法`<ParameterName>=<ParameterValue>`，并且不会把这个参数传递给`FieldNameAttribute`类的构造函数，而是查找一个有该名称的公共属性或字段（最好不要使用公共字段，所以一般情况下要使用属性），编译器可以用这个属性设置第二个参数的值。



## 反射

System.Type：通过这个类可以访问关于任何数据类型的信息。

System.Reflection.Assembly：通过这个类可以用于访问给定程序集的相关信息，或者把这个程序集加载到程序中。

#### System.Type类

Type类是一个抽象的基类，它的定义如下：

```c#
[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
[System.Runtime.InteropServices.ComVisible(true)]
public abstract class Type : System.Reflection.MemberInfo, System.Reflection.IReflect, System.Runtime.InteropServices._Type
```

获取指定任何给定类型的Type引用有一下3种常用方式

1. 使用C#的typeof运算符，例如：`Type t = typeof(double);`
2. 使用GetType()方法，例如：`double d = 10; Type t= d.GetType();`
3. 使用Type.GetType()方法，例如：`Type t = Type.GetType("System.Double");`

###### Type的属性

Type类的许多属性（有些是继承自其它基类）都可以获取包含与类相关的各种名称的字符串，如下表所示：

| 属性      | 返回值                               |
| :-------- | :----------------------------------- |
| Name      | 数据类型名                           |
| FullName  | 数据类型的完全限定名（包括命名空间） |
| Namespace | 在其中定义数据类型的命名空间名       |

有些属性还可以进一步获取Type对象的引用，这些引用表示相关的类：

| 属性                 | 返回对应的Type引用                                           |
| -------------------- | ------------------------------------------------------------ |
| BaseType             | 该Type的直接基本类型                                         |
| UnderlyingSystemType | 该Type在.NET运行库中映射的类型（某些.NET基类实际上映射到由IL识别的特定预定义类型） |

许多布尔属性表示这种类型是一个类，还是一个枚举等。

```c#
Console.WriteLine(t.IsAbstract);
Console.WriteLine(t.IsClass);
Console.WriteLine(t.IsArray);
Console.WriteLine(t.IsEnum);
Console.WriteLine(t.IsInterface);
Console.WriteLine(t.IsPointer);
//一种预定义的基元数据类型
Console.WriteLine(t.IsPrimitive);
Console.WriteLine(t.IsPublic);
Console.WriteLine(t.IsSealed);
Console.WriteLine(t.IsValueType);
```

###### Type的方法

System.Type的大多数方法都用于获取对应数据类型的成员信息：构造函数、属性、方法和事件等。这些方法的模式相关，既提供了获取单个对象类型的方法，也提供了同时获取多个对象类型组成的数组的方法。

下表给出了方法返回的对象类型，注意方法名称为复数形式的方法返回一个数组：

| 返回的对象类型  | 方法                                           |
| --------------- | ---------------------------------------------- |
| ConstructorInfo | GetConstructor()，GetConstructors()            |
| EventInfo       | GetEvent()，GetEvents()                        |
| FieldInfo       | GetField()，GetFields()                        |
| MemberInfo      | GetMember()，GetMembers()，GetDefaultMembers() |
| MethodInfo      | GetMethod()，GetMethods()                      |
| PropertyInfo    | GetProperty()，GetProperties()                 |

其中，GetMember()和GetMembers()方法返回数据类型的任何成员或所有成员的详细信息，不管这些成员是构造函数、属性、方法等。

------

#### System.Reflection.Assembly类

Assembly类在System.Reflection命名空间中定义，它允许访问给定程序集的元数据，它也包含可以加载和执行程序集（假定该程序集是可执行的）的方法。

```c#
[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
[System.Runtime.InteropServices.ComVisible(true)]
public abstract class Assembly : System.Reflection.ICustomAttributeProvider, System.Runtime.InteropServices._Assembly, System.Runtime.Serialization.ISerializable, System.Security.IEvidenceFactory
```

使用Assembly的静态方法Assembly.Load()或Assembly.LoadFrom()，可以将程序集加载到正在运行的进程中。其中，Load()方法的参数是程序集的名称，运行库会在各个位置上搜索该程序集，视图找到该程序集，这些位置包括本地目录和全局程序集缓存。而LoadFrom()方法的参数是程序集的完整路径名，它不会在其他位置搜索该程序集。

```c#
 Assembly assembly1 = Assembly.Load("WhatsNewAttributes");
 //必须是完整的路径
 Assembly assembly2 = Assembly.LoadFrom(
 @"D:\SunshineCsharp\TestAssembly\bin\Debug\TestAssembly.dll");
 string name = assembly1.FullName;
 string name2 = assembly2.FullName;

 Console.WriteLine(name);
 Console.WriteLine(name2);
```

###### 获取在程序集中定义的类型的详细信息

可以通过调用Assembly.GetTypes()方法来获得在相应程序集中定义的所有类型的详细信息。它返回一个包含所有类型的详细信息的System.Type引用数组。

```c#
 Type[] types = assembly1.GetTypes();
 foreach(Type definedType in types)
 {
     Console.WriteLine(definedType.Name);
 }
```

###### 获取自定义特性的详细信息

用于查找在程序集或类型中定义了什么自定义特性的方法取决于该特性相关的对象类型。如果要确定程序集从整体上关联了什么自定义特性，就需要调用Attribute类的一个静态方法GetCustomAttributes()，给它传递程序集的引用。

```c#
 //获取自定义特性的详细信息
 Attribute [] definedAttributes = Attribute.GetCustomAttributes(assembly1);
```

Attribute.GetCustomAttributes()存在另一个重载方法，可以获得与给定数据类型Type相关的自定义特性的详细信息，该重载方法需要传递一个Type引用，它描述了要获取的任何相关特性的类型。

```c#
Assembly theAssembly = Assembly.Load(new AssemblyName("VectorClass"));
Attribute supprotsAttribute = theAssembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
```



## 为反射使用动态语言扩展

可以使用反射，从编译时还不清楚的类型中动态创建实例。可以实现在不添加引用的情况下，动态加载程序集，并使用程序集中的成员。

首先创建一个独立的类库Calculator，在该程序集中包含一个Calculator类，代码如下：

```c#
namespace Calculator
{
    public class Calculator
    {
        public double Add(double x, double y) => x + y;

        public double Subtract(double x, double y) => x - y;
    }
}
```

然后将生成好的DLL文件复制到D盘下，使用Assembly动态加载该程序集，代码如下：

```c#
private static object GetCalculator()
{
    Assembly assembly = Assembly.LoadFile("D:/Calculator.dll");
    //创建实例
    return assembly.CreateInstance("Calculator.Calculator"); //命名空间和类名
}
```

接着使用反射，去执行Calculator中的两个方法。

#### 使用反射API调用成员

可以利用GetType()方法检索实例的Type对象，使用InvokeMember()或者GetMethod()的Invoke()方法进行调用。代码如下：

```c#
private static void ReflectionOld()
{
    double x = 3;
    double y = 4;
    object calc = GetCalculator();
    //方式一，不能用于.net core中
    object result = calc.GetType().InvokeMember("Add", 
        BindingFlags.InvokeMethod, null, calc, new object[] { x, y });
    System.Console.WriteLine(result);
    //方式二
    object result2 = calc.GetType().GetMethod("Add")
        .Invoke(calc, new object[] { x, y });
    System.Console.WriteLine(result2);
}
```

还可以使用dynamic关键字，使用动态类型调用成员，见下述说明。

#### 使用动态类型调用成员

```c#
private static void ReflectionNew()
{
    double x = 3;
    double y = 4;
    //方式三
    dynamic calc = GetCalculator();
    double result= calc.Add(x, y);
    System.Console.WriteLine(result);
}
```

> 与以强类型方式访问对象相比，使用dynamic类型有更多的开销。因此，这个关键字只用于某些特定的情形，如反射。



## dynamic类型

dynamic类型允许编写忽略编译期间的类型检查的代码。编译器假定，给dynamic类型的对象定义的任何操作都是有效的。如果该操作无效，则在代码运行之前不会检查该错误。

#### 与var关键字的不同

与var关键字不同，定义为dynamic的对象可以在运行期间改变其类型。注意在使用var关键字时，对象类型的确定会延迟。类型一旦确定，就不能改变。动态对象的类型可以改变，而且可以改变多次，这不同于把对象的类型强制转换为另一种类型。在强制转换对象的类型时，是用另一种兼容的类型创建一个新对象。例如，不能把int强制转换为Person对象，但是如果对象是动态对象，就可以把它从int变成Person类型。



## DLR（Dynamic Language Runtime，动态语言运行时）【略】

DLR是添加到CLR（公共语言运行时）的一系列服务，它允许添加动态语言，如Ruby和Python，并使C#局部和这些动态语言相同的某些动态功能。

DLR位于System.Dynamic命名空间和System.Runtime.ComplierServices命名空间中，为了与IronRuby和IronPython等脚本语言集成，需要安装DLR中的额外的类型，这个DLR是IronRuby和IronPython环境的一部分，它可以从http://ironpython.codeplex.com上下载。

关于DLR实际使用的并不多，更多的介绍请查阅其他资料。



------



#### 参考资源

- 《C#高级编程（第10版）》
- [Assembly类](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly?redirectedfrom=MSDN&view=netframework-4.7.2)
- [.NET指南——特性](https://docs.microsoft.com/zh-cn/dotnet/standard/attributes/)
- [.NET Framework 中的动态编程——反射](https://docs.microsoft.com/zh-cn/dotnet/framework/reflection-and-codedom/reflection)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

最后一次更新时间：2018-08-22

------

