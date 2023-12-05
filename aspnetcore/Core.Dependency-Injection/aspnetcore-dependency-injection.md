# ASP.NET Core 依赖注入

[TOC]

## 基本概念

### 服务

服务（Service）：在ASP.NET Core应用启动，以及为后续请求处理提供服务的各种组件，称为服务。为了便于定制，这些组件一般会以接口的形式进行标准化。

### IOC（Inverse of Control，控制反转）

- IoC是设计框架所采用的一种基本思想，或设计原则，所谓的控制反转就是将应用对流程的控制转移到框架之中。
- IoC 一方面通过流程控制从应用程序向框架的反转，实现了针对流程自身的重用，另一方面通过内置的扩展机制使这个被重用的流程能够自由地被定制，这两个因素决定了框架自身的价值。
- IoC 主要体现了这样一种设计思想：通过将一组通用流程的控制权从应用转移到框架之中以实现对流程的复用，并按照好莱坞法则实现应用程序的代码与框架之间的交互。

### 依赖关系

- 如果类型A中具有一个类型B的字段或者属性，那么类型A就对类型B产生了依赖，即类型A依赖于类型B
- 如果类型B中又依赖于类型C，那么类型C就成了类型A的间接依赖，即类型A间接依赖于类型C。
- 对于依赖注入容器最终提供的类型A对象，它所直接或者间接依赖的对象B和C都会预先被初始化并自动注入该对象之中。

### 依赖注入（Dependency Injection，DI）

依赖注入是实现IOC（控制反转）的一种方式。依赖注入是一种“对象提供型”模式。采用依赖注入的框架利用一个独立的容器（Container）来提供服务实例，它又被称为“依赖注入容器”。服务消费者只需要告诉容器所需服务的类型（一般是一个服务接口或抽象服务类），容器就能根据预先注册的规则提供一个匹配的服务实例。

#### 依赖注入的三种方式

1. 构造器注入：将依赖对象以参数的形式注入构造函数中。
2. 属性注入：如果依赖直接体现为类的某个属性，并且该属性不是只读的，则可以让依赖注入容器直接对其赋值。一般来说，在定义这种类型时，需要对自动注入的属性进行标记，使之与其他普通属性进行区分。
3. 方法注入：体现依赖关系的字段或属性，通过调用方法为方法传参的形式对其进行赋值。

### 依赖注入容器

用于提供服务实例和存放服务注册的集合。



## ASP.NET Core 依赖注入

依赖注入主要包括三部分的内容，分别为服务容器、服务的注册、服务的消费。

### 服务容器：IServiceCollection 和 IServiceProvider

核心对象：IServiceCollection 和 IServiceProvider

IServiceCollection 接口表示的是一个集合，存放着所有添加的注册服务。

IServiceProvider 表示的是通过该集合创建的依赖注入容器，用来提供服务实例和管理服务实例的生命周期。

两者关系：包含服务注册信息的 IServiceCollection 集合最终被用来创建作为依赖注入容器的 IServiceProvider 对象。

服务注册和消费的简单示例：

```c#
var provider = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddTransient<IBar, Bar>()
    .AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>))
    .BuildServiceProvider();


//通过IServiceProvider对象获取注册的服务实例
Debug.Assert(provider.GetService<IFoo>() is Foo);
Debug.Assert(provider.GetService<IBar>() is Bar);
Debug.Assert(provider.GetService<IBaz>() is Baz);
//获取泛型服务实例
var foobar = (Foobar<IFoo, IBar>?)provider.GetService<IFoobar<IFoo, IBar>>();
Debug.Assert(foobar?.Foo is Foo);
Debug.Assert(foobar?.Bar is Bar);
```

所有通过注入的服务对象，都可以通过IServiceProvider对象进行获取。包括IServiceProvider对象本身：

```c#
var provider= new ServiceCollection().BuildServiceProvider();
provider.GetService<IServiceProvider>();
```

一旦在应用中利用注入的 IServiceProvider 来获取其他依赖的服务实例，就意味着使用了Service Locator 模式，这是一种反模式，尽量别这么使用。



### 服务的注册：ServiceDescriptor

可以通过源码查看 IServiceCollection 和 ServiceDescriptor 之间的关系，如下所示：

```c#
public interface IServiceCollection : IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable
{
}

public class ServiceCollection : IServiceCollection, IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable
{
	...
}
```

`IServiceCollection` 实现 `IList<ServiceDescriptor>`泛型集合。IServiceCollection对象本质上就是一个元素类型为ServiceDescriptor的列表。因此注册到`IServiceCollection`中的每个元素都是`ServiceDescriptor`类型。

服务注册的本质上就是将创建的ServiceDescriptor对象添加到指定的IServiceCollection集合中的过程。

ServiceDescriptor是对单个服务注册项的描述。

#### ServiceDescriptor对象的创建

ServiceDescriptor的三个构造函数，分别对应三种不同的服务注册形式。

形式一：

```c#
public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
```

如果指定的是服务的实现类型（对应ImplementationType属性），则服务实例将通过调用定义在该类型中的某个构造函数来创建。

形式二：

```c#
public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime)
```

如果指定的是一个`Func<IServiceProvider, object>`委托对象（对应ImplementationFactory属性），则该委托对象将作为提供服务实例的工厂。

形式三（不推荐）：

```c#
public ServiceDescriptor(Type serviceType, object instance)
    : this(serviceType, ServiceLifetime.Singleton)
```

如果直接指定为一个现成的对象（对应的属性为ImplementationInstance），则该对象就是最终提供的服务实例。

采用形式三以现成的服务实例创建的ServiceDescriptor对象默认采用Singleton生命周期模式。对于形式一和形式二创建的ServiceDescriptor对象，需要显式指定生命周期模式。

除了上述3个构造函数外，还可以直接利用ServiceDescriptor类型中静态方法来创建ServiceDescriptor对象。

形式四：

使用ServiceDescriptor提供的两个名为Describe的重载方法来创建对应的ServiceDescriptor对象。

```c#
public static ServiceDescriptor Describe(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime){...}

public static ServiceDescriptor Describe(Type serviceType,Type implementationType, ServiceLifetime lifetime){...}
```

Describe()方法的内部实现依然是调用形式一和形式二的方式来创建ServiceDescriptor对象的。

形式五：

使用ServiceDescriptor类型中定义的一系列针对具体生命周期模式的静态工厂方法来创建ServiceDescriptor对象。

```c#
public static ServiceDescriptor Singleton（...)
public static ServiceDescriptor Transient(...)
public static ServiceDescriptor Scoped(...)
```

这些方法的内部实现都是先调用Describe方法，通过Describe方法的内部实现再去调用形式一或形式二的构造函数来创建ServiceDescriptor对象的。

总结：所有往IServiceCollection中添加的ServiceDescriptor对象，本质上都是通过ServiceDescriptor的三个构造函数来创建ServiceDescriptor对象的，然后再添加到ServiceCollection集合中。

考虑到服务注册是一个高频调用的操作，所以框架为IServiceCollection接口定义了一系列扩展方法来简化服务注册。

**通过IServiceCollection扩展方法来创建ServiceDescriptor对象和注册服务的常用形式如下所述。**

形式六：

使用 IServiceCollection的一系列扩展方法` Add{Lifetime}() 和 TryAdd{Lifetime}() `将ServiceDescriptor对象添加到集合中。

TryAdd相关的扩展方法只会在指定类型的服务注册不存在的前提下才将提供的ServiceDescriptor对象添加到集合中，可以避免服务的重复注册。

清除现有服务注册，可以调用IServiceCollection的Clear、Remove等方法来删除现有的ServiceDescriptor对象。 

#### 服务实例在创建时的匹配原则

如果一个服务类型有多个构造函数，那么在注册该服务后，需要消费对应的服务实例时，按照如下原则来调用构造函数从而创建服务实例。

被选择的构造函数的所有参数都可以通过IServiceProvider对象来提供，并且其他所有候选构造函数的参数类型，都能在该构造函数中找到。如果这样的构造函数不存在，则会直接抛出一个InvalidOperationException类型的异常。



#### 生命周期

表示依赖注入容器的 IServiceProvider 对象之间的层次结构促成了服务实例的3种生命周期模式。

- Singleton
- Scoped
- Transient

##### Scoped

如下代码所示，Scoped 是指由 IServiceScope 接口表示的服务范围（对应CreateScope方法的返回值），该范围由 IServiceScopeFactory对象来创建。

```c#
public interface IServiceScope : IDisposable
{
    IServiceProvider ServiceProvider { get; }
}

 public interface IServiceScopeFactory
 {
     IServiceScope CreateScope();
 }
 public static class ServiceProviderServiceExtensions
 {
     public static IServiceScope CreateScope(this IServiceProvider provider)
     {
         return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
     }
 }
```

任何一个IServiceProvider对象都可以利用IServiceScopeFactory工厂来创建一个表示服务范围的IServiceScope对象，每个服务范围拥有自己的IServiceProvider对象，对应IServiceScope接口的ServiceProvider属性。

3种生命周期模式：

1. 由于 Singleton 服务实例保存在作为根容器的 IServiceProvider 对象上，所以能够在多个同根 IServiceProvider对象之间提供真正的单例保证。
2. Scoped 服务实例被保存在当前服务范围对应的 IServiceProvider对象上，所以只能在当前服务范围内保证提供的实例是单例的。
3. Transient 服务实例采用“即用即建，用后即弃”的策略。

从服务实例的提供方式来说：

- Singleton：由于创建的服务实例保存在作为根容器的IServiceProvider对象中，所以多个同根的IServiceProvider对象针对同一类型提供的服务实例都是同一个对象。
- Scoped：由于创建的服务实例由当前IServiceProvider对象保存，所以同一个IServiceProvider对象针对同一类型提供的服务实例均是同一个对象。
- Transient：针对每次服务提供的请求，IServiceProvider对象总是创建一个新的服务实例。

```c#
var root = new ServiceCollection()
            .AddTransient<IFoo, Foo>()
            .AddScoped<IBar>(_ => new Bar())
            .AddSingleton<IBaz, Baz>()
            .BuildServiceProvider();
var provider1 = root.CreateScope().ServiceProvider;
var provider2 = root.CreateScope().ServiceProvider;

GetServices<IFoo>(provider1);
GetServices<IBar>(provider1);
GetServices<IBaz>(provider1);
Console.WriteLine();
GetServices<IFoo>(provider2);
GetServices<IBar>(provider2);
GetServices<IBaz>(provider2);

static void GetServices<T>(IServiceProvider provider)
{
    provider.GetService<T>();
    provider.GetService<T>();
}
```

上述代码中，IBar服务的生命周期模式为Scoped，同一个IServiceProvider对象只会创建一个Bar对象，所以整个过程中会创建两个Bar对象。

##### 不同生命周期的服务实例的释放策略

IServiceProvider 负责管理服务实例的生命周期，如果某个服务实例的类型实现了 IDisposable 接口，就意味着当生命周期完结时需要调用 Dispose 方法执行一些资源释放操作，服务实例的释放同样由 IServiceProvider对象来负责。

不同生命周期的服务实例的释放策略如下：

- Transient 和 Scoped：所有实现了 IDisposable 接口的服务实例会被当前 IServiceProvider 对象保存，当 IServiceProvider 对象的 Dispose 方法被调用时，这些服务实例的 Dispose 方法会随之被调用。
- Singleton：服务实例保存在作为根容器的 IServiceProvider 对象上，只有当后者的 Dispose 方法被调用时，这些服务实例的 Dispose 方法才会随之被调用。

ASP.NET Core 应用具有一个表示根容器的 IServiceProvider 对象，由于它与应用具有一致的生命周期而被称为 ApplicationServices。对于处理的每一次请求，应用都会利用这个根容器来创建基于当前请求的服务范围，该服务范围所在的 IServiceProvider 对象被称为 RequestServices，处理请求所需的服务实例均由它来提供。请求处理完成之后，创建的服务范围被终结，RequestServices被释放，此时在当前请求范围内创建的Scoped服务实例和实现了 IDisposable接口的 Transient 服务实例被及时释放。

```c#
using (var root = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddScoped<IBar, Bar>()
    .AddSingleton<IBaz, Baz>()
    .BuildServiceProvider())
{
    using (var scope = root.CreateScope())
    {
        var provider = scope.ServiceProvider;
        provider.GetService<IFoo>();
        provider.GetService<IBar>();
        provider.GetService<IBaz>();
        Console.WriteLine("Child container is disposed.");
    }
    Console.WriteLine("Root container is disposed.");
}
```

不推荐的做法：

[不要通过用户代码将实例添加到容器中](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#disposal-of-services)，因为不会自动调用Dispose()方法，服务关闭时不会释放，会带来性能影响。错误示例如下：

```c#
services.AddSingleton<Service3>(new Service3());
services.AddSingleton(new Service3());
```

也就是说，尽量通过接口的形式注入服务，即在<T>中，为T指定接口类型，如果没有接口，T可以指定为具体类，但是不要为其再提供实例对象。

 

#### 服务注册的验证

如果希望 IServiceProvider 对象在提供服务时针对服务范围进行有效性检验，则只需要在调用 IServiceCollection 接口的 BuildServiceProvider 扩展方法时提供一个 True 值作为参数即可。

```csharp
var root = new ServiceCollection()
            .AddSingleton<IFoo, Foo>()
            .AddScoped<IBar, Bar>()
            .BuildServiceProvider(validateScopes: true);
var child = root.CreateScope().ServiceProvider;
```

服务注册的验证主要体现在 ServiceProviderOptions 配置上，当 ServiceProviderOptions 的 ValidateScopes 属性设为 true 是，将对服务范围进行检验。另一个 ValidateOnBuild 属性设为 true 时，IServiceProvider 对象被构建时会对每个 ServiceDescriptor 对象实施有效性验证。

```c#
BuildServiceProvider(false);
BuildServiceProvider(true);

static void BuildServiceProvider(bool validateOnBuild)
{
    try
    {
        var options = new ServiceProviderOptions
        {
            //用于范围检验
            ValidateScopes = true,
            //用于对象被构建时检验
            ValidateOnBuild = validateOnBuild,
        };
        new ServiceCollection()
            .AddSingleton<IFoobar, Foobar>()
            .BuildServiceProvider(options);
        Console.WriteLine($"Status: Success; ValidateOnBuild: {validateOnBuild}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```



### 服务的消费：IServiceProvider

当需要根据指定的服务类型提取对应的服务注册信息来提供对应的服务实例时，需要调用IServiceProvider对象的GetService方法。

IServiceProvider接口定义了唯一的方法GetService()：

```c#
public interface IServiceProvider
{
    object? GetService(Type serviceType);
}
```

其他获取服务对象的方法均是IServiceProvider的扩展方法，如`GetService<T>()`、GetServices() 和 GetRequiredService()。



#### `GetService`和`GetServices`

在注册服务时，可以为同一个类型添加多个服务注册，但`GetService`方法只返回一个服务实例，框架采用“后来居上”的策略，总是采用最近添加的服务注册来创建服务实例。如果需要返回该服务类型的所有的服务注册的服务实例，此时可以调用`GetServices`方法，该方法将返回包含所有该类型的服务实例的集合。



#### `GetService`和`GetRequiredService`区别

如果对应的服务注册不存在，GetService方法会返回null；GetRequiredService方法会抛出一个InvalidOperationException类型的异常。



### ActivatorUtilities 静态类

使用 IServiceProvider获取服务实例时，该服务实例必须是注册到容器中的，如果需要利用容器创建一个对应类型不曾注册的实例时，可以使用ActivatorUtilities静态类中提供的方法。

常用的静态方法有：

| 方法名                                                       | 描述                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [`CreateFactory(Type, Type[]`)](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.createfactory?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-createfactory(system-type-system-type()) | 创建一个委托，该委托将使用直接和/或从 [IServiceProvider](https://learn.microsoft.com/zh-cn/dotnet/api/system.iserviceprovider?view=dotnet-plat-ext-8.0) 提供的构造函数参数实例化类型。 |
| [`CreateFactory(Type[])`](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.createfactory?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-createfactory-1(system-type()) | 创建一个委托，该委托将使用直接提供的或从 [IServiceProvider](https://learn.microsoft.com/zh-cn/dotnet/api/system.iserviceprovider?view=dotnet-plat-ext-8.0)提供的构造函数参数实例化类型。 |
| [`CreateInstance(IServiceProvider, Type, Object[])`](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.createinstance?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-createinstance(system-iserviceprovider-system-type-system-object()) | 使用直接提供的或从 [IServiceProvider](https://learn.microsoft.com/zh-cn/dotnet/api/system.iserviceprovider?view=dotnet-plat-ext-8.0)提供的构造函数参数实例化类型。 |
| [`CreateInstance(IServiceProvider, Object[])`](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.createinstance?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-createinstance-1(system-iserviceprovider-system-object()) | 使用直接提供的或从 [IServiceProvider](https://learn.microsoft.com/zh-cn/dotnet/api/system.iserviceprovider?view=dotnet-plat-ext-8.0)提供的构造函数参数实例化类型。 |
| [`GetServiceOrCreateInstance(IServiceProvider, Type)`](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.getserviceorcreateinstance?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-getserviceorcreateinstance(system-iserviceprovider-system-type)) | 从服务提供程序中检索给定类型的实例。 如果找不到该实例，则直接实例化。 |
| [`GetServiceOrCreateInstance(IServiceProvider)`](https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities.getserviceorcreateinstance?view=dotnet-plat-ext-8.0#microsoft-extensions-dependencyinjection-activatorutilities-getserviceorcreateinstance-1(system-iserviceprovider)) | 从服务提供程序中检索给定类型的实例。 如果找不到该实例，则直接实例化。 |







----



【完结】

笔记来源：《ASP.NET  Core 6 框架揭秘》

更新时间：2023-12-05
