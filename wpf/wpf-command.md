# 命令

事件与命令的区别？

命令可以约束代码和步骤逻辑。

WPF的命令是实现了ICommand接口的类。



## 命令系统的构造

WPF的命令系统由以下几个基本要素构成：

- Command（命令）：实现了ICommand接口的类，常用的如RoutedCommand类。
- Command Source（命令源）：命令的发送者，实现了ICommandSource接口的类。常见的控件元素都实现了这个接口，如Button、ListBoxItems等。
- Command Target（命令目标）：命令将作用在谁身上，命令目标必须是实现了IInputElement接口的类。
- Command Binding（命令关联）：负责把一些外围逻辑与命令关联起来，比如执行之前 对命令是否可以执行 进行判断、命令执行之后还有哪些后续工作等。

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
    this.stackPanel.CommandBindings.Add(cb);
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

### 第一步：创建命令类

有两种方式：

- 直接使用RoutedCommand类，如上述代码。
- 创建一个实现ICommand接口的类，或派生自RoutedCommand的类。



### 第二步：声明命令实例

创建命令类的实例，如上述代码中的：

```c#
private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));
```

一般情况下，程序中某种操作只需要一个命令实例与之对应即可，而不管该命令有多少命令源，这种单件模式（Singletone Pattern）可以减少代码的复杂度，比如内置的ApplicationCommands中就存在很多这种单件模式定义的命令。



### 第三步：指定命令的源

指定由谁来发送这个命令。同一个命令可以有多个源。

```c#
//把命令赋值给命令源（发送者）并指定快捷键
this.button1.Command = this.clearCmd;
// 使用命令可以避免自己写代码判断Button是否可用以及添加快捷键
this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
```

一旦把命令指派给命令源，那么命令源就会受命令的影响，当命令不能被执行的时候，作为命令源的控件将处在不可用状态。

==各种控件发送命令的方法不尽相同==，比如Button和MenuItem是在单击时发送命令，而ListBoxItem 单击时表示被选中所以双击时才发送命令。



### 第四步：指定命令的目标

为命令源的CommandTarget属性设置值，告诉命令源向哪个组件发送命令，无论这个组件是否拥有焦点它都会收到这个命令。

如果没有为命令源指定命令目标，则WPF系统认为当前拥有焦点的对象就是命令目标。

```c#
//指定命令目标
this.button1.CommandTarget = this.textBoxA;
```

### 第五步：设置命令关联

WPF 命令需要CommandBinding在执行前来帮助判断是不是可以执行；在执行后做一些后续操作。

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





## RoutedCommand  和 ICommand 

WPF的命令是实现了ICommand接口的类。

该接口的定义如下：

```c#
public interface ICommand
{
    event EventHandler? CanExecuteChanged;
	bool CanExecute(object? parameter);
    void Execute(object? parameter);
}
```

- Execute(...)：命令被执行。
- CanExecute(...)：在执行之前用来探知命令是否可被执行。
- CanExecuteChanged：当命令可执行状态发生改变时，可激发此事件来通知其他对象。

RoutedCommand类实现了ICommand接口，但在实现时，并未向Execute 和 CanExecute 方法中添加任何逻辑，也就是说，RoutedCommand是通用的、与具体业务逻辑无关的。



## WPF的内置命令库

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



## ICommandSource

命令源一定是实现了ICommandSource接口的类。

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





## 自定义 Command

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





参考资源：

- 《深入浅出WPF》



