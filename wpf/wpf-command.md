# 命令

事件与命令的区别？

事件的作用是发布、传播一些消息，消息送达接收者，事件的使命也就完成了，至于如何响应事件送来的消息事件并不做规定，每个接收者可以使用自己的行为来响应事件。也就是说，事件不具有约束力。而命令与事件的区别就在于命名是具有约束力的。命令可以约束代码和步骤逻辑。

场景应用举例：打印操作有3种方式（窗体菜单、快捷键、右击），如果按照传统的事件来实现，需要进行3个事件方法的定义，而有了命令，只需要将单个命令挂接到这三个不同事件源即可。

命令概念中最重要的一点：多触发源操作的支持。

WPF的命令用来表示一个应用程序在某些条件下可以执行的操作。

在一定程度上，直接使用命令可以用来替代事件，更符合MVVM设计模式。WPF中的命令基本上是松散类型的事件，传统事件是要双击控件在后台编写事件处理程序，而命令意味着没有在按钮后面编写任何代码。

命令的真正实现将发生在ViewModel上，因为命令最终作用在视图上面。



## 命令系统的组成模型

WPF的命令系统由以下几个基本要素构成：

- Command（命令）：实现了ICommand接口的类，常用的如RoutedCommand类。RoutedUICommand继承自RoutedCommand，多了一个Text属性。
- Command Source（命令源）：触发命令的界面元素，命令的发送者，实现了ICommandSource接口的类。常见的控件元素都实现了这个接口，如Button、ListBoxItems等。
- Command Target（命令目标）：命令将作用在谁身上，命令目标必须是实现了IInputElement接口的类。命令目标可以指定命令的运行实例。
- Command Binding（命令关联）：负责把一些外围逻辑与命令关联起来，比如执行之前 对命令是否可以执行 进行判断、命令执行之后还有哪些后续工作等。每个命令绑定都将命令与应用程序的逻辑以及界面状态相关联。通过多次使用命令绑定，软件开发人员可以将同一个命令与多个界面元素相关联，以使该命令可以在多处触发。



### 命令本身：RoutedCommand  和 ICommand 

该接口的定义如下：

```c#
public interface ICommand
{
    event EventHandler? CanExecuteChanged;
	bool CanExecute(object? parameter);
    void Execute(object? parameter);
}
```

WPF的命令是实现了ICommand接口的类。

- Execute(...)：命令被执行。
- CanExecute(...)：在执行之前用来探知命令是否可被执行。
- CanExecuteChanged：当命令可执行状态发生改变时，可激发此事件来通知其他对象。

RoutedCommand类实现了ICommand接口，但在实现时，并未向Execute 和 CanExecute 方法中添加任何逻辑，也就是说，RoutedCommand是通用的、与具体业务逻辑无关的。

Execute()和CanExecute()方法会引发遍历元素树查找具有 CommandBinding 的对象的事件。附加到 CommandBinding 的事件处理程序包含命令逻辑。



#### WPF的内置命令库

在上述示例中，声明命令使用了如下代码：

```C#
 //声明并定义命令
 private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));
```

命令本身具有“一处声明，处处使用”的特点，比如Save命令，在程序的任何地方它都表示要求命令目标保存数据。因此，WPF类库中包含了一些便捷的命令库：

- ApplicationCommands
- ComponentCommands
- NavigationCommands
- MediaCommands
- EditingCommands

这些都是静态类，命令通过这些类的静态只读属性以单件模式暴露出来。

```c#
public static class ApplicationCommands
{
...
public static RoutedUICommand CancelPrint { get; }
public static RoutedUICommand SelectAll { get; }
public static RoutedUICommand SaveAs { get; }
public static RoutedUICommand Save { get; }
public static RoutedUICommand Replace { get; }
...
}
```



### 命令源：ICommandSource

ICommandSource接口的定义如下：

```csharp
public interface ICommandSource
{
    ICommand Command { get; }
    object CommandParameter { get; }
    IInputElement CommandTarget { get; }
}
```

命令源一定是实现了ICommandSource接口的类。WPF的大多数控件都实现了该接口，因此大多数控件都可以直接作为命令源。

通常会在命令源中进行发送命令的操作（本质是调用ICommand的Execute方法），见下文自定义命令部分。

各种控件发送命令的方法不尽相同，比如Button和MenuItem是在单击时发送命令，而ListBoxItem 单击时表示被选中所以双击时才发送命令。

**Command**

把命令赋值给命令源就是为ICommandSource的Command设置值。

```csharp
this.button1.Command = this.clearCmd;
```

一旦把命令指派给命令源，那么命令源就会受命令的影响，==当命令不能被执行的时候，作为命令源的控件将处在不可用状态==（即命令的CanExecute方法如果返回的为false，发送该命令的控件将处于不可用状态）。

另外同一个命令可以有多个源。

**CommandParameter**

ICommandSource的CommandParameter属性用来携带命令参数，通过该属性可以区分同一个命令不同处理逻辑。

XAML代码：

```xaml
<StackPanel>
    <!--命令和命令参数-->
    <TextBlock Text="Name:" VerticalAlignment="Center" HorizontalAlignment="Left" />
    <TextBox x:Name="nameTextBox" Margin="60,0,0,0"  />
    <Button Content="New Teacher" Command="New" CommandParameter="Teacher"  />
    <Button Content="New Student" Command="New" CommandParameter="Student" />
    <ListBox x:Name="listBoxNewItems" />
</StackPanel>

<!--为窗体添加CommandBinding-->
<Window.CommandBindings>
    <CommandBinding Command="New" 
    CanExecute="CommandBinding_CanExecute" Executed="CommandBinding_Executed"/>
</Window.CommandBindings>
```

C#代码：

```c#
private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
{
    e.CanExecute = !string.IsNullOrEmpty(this.nameTextBox.Text);
}

private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
{
    string name = this.nameTextBox.Text;
    if (e.Parameter.ToString() == "Teacher")
    {
        this.listBoxNewItems.Items.Add("Teacher:" + name);
    }
    if (e.Parameter.ToString() == "Student")
    {
        this.listBoxNewItems.Items.Add("Student:" + name);
    }
}
```



### 命令目标（CommandTarget）：IInputElement

ICommandSource的CommandTarget属性，其类型为IInputElement。

为命令源的CommandTarget属性设置值，告诉命令源向哪个组件发送命令，无论这个组件是否拥有焦点它都会收到这个命令。

如果没有为命令源指定命令目标，则WPF系统认为当前拥有焦点的对象就是命令目标。



### 命令关联：CommandBinding

CommandBinding用于命令在执行前来帮助判断是不是可以执行、在执行后做其他的事情。

CommandBinding定义如下：

```csharp
public class CommandBinding
{
    public CommandBinding(); 
    public CommandBinding(ICommand command); 
    public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed); 
    public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute);
    public ICommand Command { get; set; }
    public event CanExecuteRoutedEventHandler CanExecute;
    public event ExecutedRoutedEventHandler Executed;
    public event CanExecuteRoutedEventHandler PreviewCanExecute;
    public event ExecutedRoutedEventHandler PreviewExecuted;
}
```

它包含了4个事件：CanExecute、Executed、PreviewCanExecute、PreviewExecuted，需要特别注意这里的CanExecute、Executed事件和ICommand中的CanExecute、Execute方法的不同。

**命令目标（CommandTarget）与命令关联（CommandBinding）之间的微妙关系**

无论命令目标是由程序员指定，还是由WPF系统根据焦点所在地判断出来的，一旦某一个UI组件被命令源当成了命令目标，命令源就会不停地向命令目标”投石问路“，命令目标就会不停地发送出可路由的PreviewCanExecute和CanExecute附加事件，事件会沿着UI元素树向上传递并被命令关联所捕捉，命令关联捕捉到这些事件后会把命令能不能发送实时报告给命令。类似的，如果命令被发送出来并到达命令目标，命令目标就会发送PreviewExecuted和Executed两个附加事件，这两个事件也会沿着UI元素树向上传递并被命令关联所捕捉，命令关联会完成一些后续的任务。



### 命令各要素之间的关系

下面通过一个示例来说明这些要素之间的联系。

需求：定义一个命令，使用Button来发送这个命令，当命令送达TextBox时TextBox会被清空，如果TextBoxBox中没有文字则命令不可被发送。

XAML代码：

```xaml
<StackPanel x:Name="stackPanel">
    <Button x:Name="button1" Content="Send Command" Margin="5"/>
    <TextBox x:Name="textBoxA" Margin="5,0" Height="100"/>
</StackPanel>
```

C#代码：

```c#
//声明并定义命令
private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));

public MainWindow()
{
    InitializeComponent();

    //把命令赋值给命令源（发送者）并指定快捷键
    this.button1.Command = this.clearCmd;
    this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

    //指定命令目标
    this.button1.CommandTarget = this.textBoxA;

    //创建命令关联
    CommandBinding cb = new CommandBinding();
    cb.Command = this.clearCmd; //只关注与clearCmd相关的事件
    cb.CanExecute += new CanExecuteRoutedEventHandler(Cb_CanExecute);
    cb.Executed += new ExecutedRoutedEventHandler(Cb_Executed);

    //把命令关联安置在外围控件上
    this.stackPanel.CommandBindings.Add(cb);//按钮被点击之后，Executed事件从按钮冒泡到StackPanel上
}

//当探测命令是否可以执行时，此方法被调用
private void Cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
{
    e.CanExecute = !string.IsNullOrEmpty(this.textBoxA.Text);
    //避免继续向上传而降低程序性能
    e.Handled = true;
}

//当命令送达目标后，此方法被调用
private void Cb_Executed(object sender, ExecutedRoutedEventArgs e)
{
    this.textBoxA.Clear();
    e.Handled = true;
}
```

#### 第一步：创建命令类

有两种方式：

- 推荐直接使用RoutedCommand类，如上述代码。
- 创建一个实现ICommand接口的类，或派生自RoutedCommand的类。



#### 第二步：声明命令实例

创建命令类的实例，如上述代码中的：

```c#
private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));
```

一般情况下，程序中某种操作只需要一个命令实例与之对应即可，而不管该命令有多少命令源，这种单件模式（Singletone Pattern）可以减少代码的复杂度，比如内置的ApplicationCommands中就存在很多这种单件模式定义的命令。



#### 第三步：指定命令的源

指定由谁来发送这个命令。同一个命令可以有多个源。上文提到过命令源是实现了ICommandSource接口的类，大多数控件都实现了该接口。

```c#
//把命令赋值给命令源（发送者）并指定快捷键
this.button1.Command = this.clearCmd;
// 使用命令可以避免自己写代码判断Button是否可用以及添加快捷键
this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
```

一旦把命令指派给命令源，那么命令源就会受命令的影响，==当命令不能被执行的时候，作为命令源的控件将处在不可用状态==（即命令的CanExecute方法如果返回的为false，发送该命令的控件将处于不可用状态）。

各种控件发送命令的方法不尽相同，比如Button和MenuItem是在单击时发送命令，而ListBoxItem 单击时表示被选中所以双击时才发送命令。



#### 第四步：指定命令的目标

为命令源的CommandTarget属性设置值，告诉命令源向哪个组件发送命令，无论这个组件是否拥有焦点它都会收到这个命令。

如果没有为命令源指定命令目标，则WPF系统认为当前拥有焦点的对象就是命令目标。

```c#
//指定命令目标
this.button1.CommandTarget = this.textBoxA;
```

#### 第五步：设置命令关联

WPF 命令需要CommandBinding在执行前来帮助判断是不是可以执行；在执行后做一些后续操作。简单点来说就是指定命令触发时对应的事件处理程序。

```c#
//创建命令关联
CommandBinding cb = new CommandBinding();
cb.Command = this.clearCmd; //只关注与clearCmd相关的事件
cb.CanExecute += new CanExecuteRoutedEventHandler(Cb_CanExecute);
cb.Executed += new ExecutedRoutedEventHandler(Cb_Executed);

//把命令关联安置在外围控件上，不然无法捕捉到CanExecute 和 Exectued 等路由事件。
this.stackPanel.CommandBindings.Add(cb);

//当探测命令是否可以执行时，此路由事件被调用
private void Cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
{
    e.CanExecute = !string.IsNullOrEmpty(this.textBoxA.Text);
    //避免继续向上传而降低程序性能
    e.Handled = true;
}

//当命令送达目标后，此路由事件被调用
private void Cb_Executed(object sender, ExecutedRoutedEventArgs e)
{
    this.textBoxA.Clear();
    e.Handled = true;
}
```

因为CanExecute事件的激发频率比较高，为了避免降低性能，在处理完后建议把e.Handled设为true。



### 命令各要素之间的关系总结

- CommandManager中的[CanExecuteEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager.canexecuteevent?view=netframework-4.7.2#system-windows-input-commandmanager-canexecuteevent)、[ExecutedEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager.executedevent?view=netframework-4.7.2#system-windows-input-commandmanager-executedevent)、[PreviewCanExecuteEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager.previewcanexecuteevent?view=netframework-4.7.2#system-windows-input-commandmanager-previewcanexecuteevent)、[PreviewExecutedEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager.previewexecutedevent?view=netframework-4.7.2#system-windows-input-commandmanager-previewexecutedevent)均是**路由事件**。

- CommandBinding中的[CanExecute](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding.canexecute?view=netframework-4.7.2#system-windows-input-commandbinding-canexecute)、[Executed](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding.executed?view=netframework-4.7.2#system-windows-input-commandbinding-executed)、[PreviewCanExecute](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding.previewcanexecute?view=netframework-4.7.2#system-windows-input-commandbinding-previewcanexecute)、[PreviewExecuted](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding.previewexecuted?view=netframework-4.7.2#system-windows-input-commandbinding-previewexecuted)均为**普通事件**。

- CommandManager用于注册具有指定类型的CommandBinding和InputBinding，简单来说CommandManager只和CommandBinding或InputBinding打交道。

-  CommandBinding将命令（RoutedCommand）与 PreviewExecuted/Executed 和 PreviewCanExecute/CanExecute 事件相关联。具体来说，调用 RoutedCommand 的 Execute 或 CanExecute 方法时，将在命令目标上引发 PreviewExecuted/Executed 或 PreviewCanExecute/CanExecute 事件。如果命令目标具有命令的 CommandBinding，则调用相应的处理程序。如果命令目标没有命令的 CommandBinding，则事件将通过元素树路由，直到找到具有 CommandBinding 的元素。（**CommandBinding是WPF专门为RoutedCommand提供的**）

- CommandBinding 通常和RoutedCommand及其派生类一起使用，不建议直接使用派生自ICommand的非RoutedCommand类型。

- 所有的WPF元素都实现了ICommandSource接口，因此都可以通过该接口的Command、CommandParameter、CommandTarget去指定作为命令源控件的命令、命令参数和命令目标控件。

- >[深入浅出WPF的命令系统 - 叶落劲秋 - 博客园](https://www.cnblogs.com/tianlang358/p/17077102.html) 中的总结：
  >
  >1. 命令系统包含ICommand，ICommandSource，命令目标及CommandBinding 四个基本要素，但是ICommandSource中的CommandTarget属性只在命令是RoutedCommand时才有用，否则在命令执行时会被直接忽略；
  >2. RoutedCommand顾名思义，其本质还是路由事件，但它只负责发起路由事件，并不执行命令逻辑，命令逻辑是由与具体命令关联的CommandBinding来执行的；
  >3. 由于RoutedCommand是基于路由事件的，因此其发起路由事件、构建路由路径、沿路由路径执行命令处理程序等这一复杂的流程势必会对执行效率产生不好的影响，所以如果不需要命令进行路由，可以构建简单的自定义命令。
  >4. 自定义命令时，如果希望通过命令系统来改变命令源的可执行状态，需要在实现时通过CanExecuteChanged事件对CommandManager的RequerySuggested事件进行封装





## 命令示例

示例一：不使用事件仅使用命令在后台代码保持洁净的情况下进行ViewModel事件处理：

```csharp
public class WyCommand : ICommand
{
    Action<object> executeMethod;
    Func<object, bool> canExecuteMethod;

    public WyCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
    {
        this.executeMethod = executeMethod;
        this.canExecuteMethod = canExecuteMethod;
    }

    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return canExecuteMethod(parameter);
    }

    public void Execute(object? parameter)
    {
        executeMethod?.Invoke(parameter);
    }
}
public class ViewModel
{
    public ICommand MyCommand { get; set; }

    public ViewModel()
    {
        MyCommand = new WyCommand(Execute, CanExecute);
    }


    private bool CanExecute(object? parameter)
    {
        return true;
        //return false;
    }

    private void Execute(object? parameter)
    {
        MessageBox.Show("这是一个命令"); //当点击按钮时，该方法被触发
    }
}
```

将ViewModel与XAML进行绑定即可，XAML后台不需要任何C#代码：

```xaml
<Window.Resources>
    <local:ViewModel x:Key="vm">
    </local:ViewModel>
</Window.Resources>
<Grid>
    <Button x:Name="btn1" Command="{Binding Source={StaticResource vm}, Path=MyCommand}" Content="Button" Height="40" Width="120" />
</Grid>
```

示例二，使用内置的命令代替事件：

```xaml
<Window.CommandBindings>
    <CommandBinding Command="Open" CanExecute="CommandBinding_CanExecute" Executed="CommandBinding_Executed"/>
</Window.CommandBindings>
<Grid>
    <Menu VerticalAlignment="Top" Height="25">
        <MenuItem Header="文件" Height="25">
            <!--使用内置的预先存在的命令-->
            <MenuItem Command="Open"/>
        </MenuItem>
    </Menu>
</Grid>
```

xaml后台代码：

```csharp
public partial class Sample2 : Window
{
    public Sample2()
    {
        InitializeComponent();
    }

    private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Sample1 sample1 = new Sample1();
        sample1.Show();
    }
}
```

示例三，基于内置的命令库进行扩展使用

```xaml
<Window.CommandBindings>
    <CommandBinding Command="Open" CanExecute="CommandBinding_CanExecute" Executed="CommandBinding_Executed"/>
    <CommandBinding Command="local:AyCommands.Hello" CanExecute="CommandBinding_CanExecute" Executed="CommandBinding_Executed"/>
</Window.CommandBindings>
<Grid>
    <Menu VerticalAlignment="Top" Height="25">
        <MenuItem Header="文件" Height="25">
            <!--使用内置的预先存在的命令-->
             <MenuItem Header="打开" Command="Open" CommandParameter="P1"/>
            <!--使用自己声明的命令-->
			 <MenuItem Command="local:AyCommands.Hello" CommandParameter="P2"/>
        </MenuItem>
    </Menu>
</Grid>
```

声明RoutedUICommand类型的命令，并使用：

```csharp
public partial class Sample2 : Window
{
    public Sample2()
    {
        InitializeComponent();
    }

    private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        //获取命令参数
        var obj =  e.Parameter as string;
        MessageBox.Show(obj);
        Sample1 sample1 = new Sample1();
        sample1.Show();
    }
}

public static class AyCommands
{
    public static RoutedUICommand Hello { get; set; }
    static AyCommands()
    {
        //设置命令快捷键
        InputGestureCollection inputs = new InputGestureCollection();
        inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control));
        Hello = new RoutedUICommand(text: "Say Hi", "Hello", typeof(AyCommands),inputs);
    }
}
```



上述的三个示例中，虽然代码量各不相同，但核心都是应用了事件的4个核心要素（命令本身、命令源、命令目标、命令关联），当自定义命令时，需要显式的设置这四个要素，而使用WPF内置的命令时，这些都被隐式的设置好了。





## 自定义 Command（不常用，了解即可）

注：该示例使用了直接派生自ICommand的非RoutedCommand的命令，因此无法与CommandBinding结合使用。见[CommandBinding 与非 RoutedCommand 的 ICommand 一起使用受到限制](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding?view=netframework-4.7.2#remarks)。

>CommandBinding 与非 RoutedCommand 的 ICommand 一起使用受到限制。这是因为 CommandBinding 将命令绑定到 ExecutedRoutedEventHandler 和 CanExecuteRoutedEventHandler，它们侦听在调用 RoutedCommand 的 Execute 和 CanExecute 方法时引发的 Executed 和 CanExecute 路由事件。

定义命令目标的业务约束接口：

```c#
public interface IView
{
    bool IsChanged { get; set; }
    void Clear();
}
```

实现ICommand接口：

```c#
//自定义命令
public class ClearCommand : ICommand
{
    //当命令可执行状态发生改变时，应当被激发
    public event EventHandler? CanExecuteChanged;

    //用于判断命令是否可以执行（暂不实现）
    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    //命令执行，带有与业务相关的Clear逻辑
    public void Execute(object? parameter)
    {
        IView view = parameter as IView;
        if (view != null)
        {
            view.Clear();
        }
    }
}
```

自定义命令源：

```c#
//自定义命令源
public class MyCommandSource : UserControl, ICommandSource
{
    // 继承自接口的三个属性
    public ICommand Command { get; set; }
    public object CommandParameter { get; set; }
    public IInputElement CommandTarget { get; set; }

    //在组件被单击时连带执行命令
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        //在命令目标上执行命令，或称让命令作用于命令目标
        if (this.CommandTarget != null)
        {
            this.Command.Execute(this.CommandTarget);
        }
        //if(this.Command.CanExecute(CommandParameter))
        //{
        //    Command.Execute(CommandParameter);
        //}
    }
}
```

命令不会自己被发出，所以一定要为命令的执行选择一个合适的时机。上述代码在控件被鼠标左键单击时执行命令。

定义命令目标，这里使用UserControl：

```xaml
<UserControl x:Class="CommandSample.WpfApp.MiniView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:CommandSample.WpfApp"
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800">
    <StackPanel>
        <TextBox x:Name="textBox1" Margin="5"/>
        <TextBox x:Name="textBox2" Margin="5"/>
        <TextBox x:Name="textBox3" Margin="5"/>
        <TextBox x:Name="textBox4" Margin="5"/>
    </StackPanel>
</UserControl>
```

后台代码：

```c#
 public partial class MiniView : UserControl, IView
 {
     public MiniView()
     {
         InitializeComponent();
     }

     public bool IsChanged { get; set; }

     public void Clear()
     {
         this.textBox1.Clear();
         this.textBox2.Clear();
         this.textBox3.Clear();
         this.textBox4.Clear();
     }
 }
```

最后把自定义命令、命令源和命令目标集成起来：

```xaml
<Window x:Class="CommandSample.WpfApp.CusCommand"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CommandSample.WpfApp"
        mc:Ignorable="d"
        Title="CusCommand" Height="450" Width="800">
    <StackPanel>
        <local:MyCommandSource x:Name="ctrlClear" Margin="10">
            <TextBlock Text="清除" FontSize="16" TextAlignment="Center" Width="80" Background="LightGreen"/>
        </local:MyCommandSource>
        <local:MiniView x:Name="miniView"/>
    </StackPanel>
</Window>
```

后台代码：

```c#
public partial class CusCommand : Window
{
    public CusCommand()
    {
        InitializeComponent();

        //声明命令并使用命令源和目标与之关联
        ClearCommand clearCmd =new ClearCommand();
        this.ctrlClear.Command =clearCmd;
        this.ctrlClear.CommandTarget = this.miniView;
    }
}
```

注意：上述只是为了演示如何使用才将命令实例定义在方法内部，正规的做法应该像内置的命令那样，把命令声明在静态全局的地方供所有对象使用。





----



References：

- 《深入浅出WPF》
- [命令概述 - WPF | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/commanding-overview)
- [深入浅出WPF的命令系统 - 叶落劲秋 - 博客园](https://www.cnblogs.com/tianlang358/p/17077102.html)

Last updated：2025-05-20

