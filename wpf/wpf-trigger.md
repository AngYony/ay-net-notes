# 触发器

WPF中可以通过如下几个类的Triggers属性来设置触发器：

- Style.Triggers
- ControlTemplate.Triggers
- DataTemplate.Triggers

Triggers属性的定义都相同：

```csharp
public System.Windows.TriggerCollection Triggers { get; }
```

System.Windows.TriggerCollection表示触发器对象的集合，每个触发器对象都是派生自抽象类TriggerBase的具体类的实例。

```csharp
public sealed class TriggerCollection : System.Collections.ObjectModel.Collection<System.Windows.TriggerBase>
```

而派生自抽象类的TriggerBase的子类有：

- System.Windows.Trigger：属性触发器，允许基于属性值应用更改。
- System.Windows.MultiTrigger：根据多个属性的状态应用更改。
- System.Windows.EventTrigger：事件触发器，允许在事件发生时应用更改。
- System.Windows.DataTrigger：数据触发器，在绑定数据满足指定条件时应用属性值或执行操作。
- System.Windows.MultiDataTrigger：多重数据触发器。



属性触发器、事件触发器、数据触发器之间的区别：

- 属性触发器（Trigger）当不再满足触发条件时，触发器更改的属性会自动重置为其以前的值。
- EventTrigger 对象在发生指定的路由事件时启动一组 Actions，与  Trigger不同， EventTrigger 没有终止状态的概念，因此一旦引发事件的条件不再成立，操作将不会撤消。



[TriggerBase 类 (System.Windows) | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.triggerbase?view=netframework-4.7.2)