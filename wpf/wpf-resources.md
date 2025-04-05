# 样式与资源

在WPF中，可以对特定类型的可视化对象的属性进行集中设置，样式由Style类表示。使用样式来描述对象的属性，可以实现重复利用。样式通常声明为资源，以方便在不同的地方进行引用。

资源分为两类：

- 对象资源，也称为WPF资源，指的是通过WPF界面元素的Resources属性设置的资源。
- 程序集资源，也称为二进制资源，指的是编译器把外部文件编译进程序主体中的传统意义上的程序资源。



## XAML样式

在资源列表中声明样式对象时，如果显式指定x:key（资源的键，资源集合本质上是一个字典集合，通过键来访问），则XAML文档要使用该样式的元素必须显式引用；如果不设置x:key值，则在资源集合有效范围内的所有可视化对象都会自动套用资源中的样式。

```xaml
<Application.Resources>
    <Style TargetType="{x:Type TextBlock}">
        <Setter Property="FontFamily" Value="楷体"/>
        <Setter Property="FontSize" Value="20"/>
        <Setter Property="Foreground" Value="Purple"/>
    </Style>
</Application.Resources>
```

### 样式中的触发器

Style类公开了一个Triggers集合，允许向其中添加触发器。触发器以TriggerBase抽象类为基础，即只要从TriggerBase类派生的类型都可以添加到Triggers集合中。

```xaml
<Window x:Class="S12_9.StyleTrigger"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:S12_9"
        mc:Ignorable="d"
        Title="样式中的触发器" Height="450" Width="800">

    <Window.Resources>
        <!--静态触发器-->
        <Style TargetType="{x:Type Rectangle}">
            <Setter Property="Width" Value="150" />
            <Setter Property="Height" Value="100"/>
            <Setter Property="Fill" Value="Yellow"/>
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Fill" Value="Red"/>
                </Trigger>
            </Style.Triggers>
        </Style>
        <!--动画触发器-->
        <Style TargetType="{x:Type Ellipse}">
            <Setter Property="Width" Value="120"/>
            <Setter Property="Height" Value="120"/>
            <Setter Property="Fill" Value="LightBlue"/>
            <Style.Triggers>
                <Trigger Property="UIElement.IsMouseOver" Value="True">
                    <Trigger.EnterActions>
                        <BeginStoryboard Name="start">
                            <Storyboard RepeatBehavior="Forever">
                                <ColorAnimation Storyboard.TargetProperty="(Shape.Fill).(SolidColorBrush.Color)" From="Orange" To="Blue" Duration="0:0:2"/>
                            </Storyboard>
                        </BeginStoryboard>
                    </Trigger.EnterActions>
                    <Trigger.ExitActions>
                        <StopStoryboard BeginStoryboardName="start"/>
                    </Trigger.ExitActions>
                </Trigger>
            </Style.Triggers>
        </Style>

    </Window.Resources>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition/>
            <RowDefinition/>
        </Grid.RowDefinitions>
        <StackPanel Grid.Row="0" Margin="30" Orientation="Horizontal">
            <Rectangle/>
            <Rectangle/>
            <Rectangle/>
        </StackPanel>

        <StackPanel Grid.Row="1" Margin="30" Orientation="Horizontal">
            <Ellipse/>
            <Ellipse/>
            <Ellipse/>
        </StackPanel>
    </Grid>
</Window>
```





## WPF资源或对象资源

每个WPF的界面元素都具有一个名为Resources的属性，该属性继承自FrameworkElement类，其类型为ResourceDictionary。

ResourceDictionary以“键-值”对的形式存储资源，并且键和值都是object类型，因此可以存储任意类型的对象。

XAML编译器能够根据标签的Attribute自动识别资源类型，如果类型不对就会抛出异常。

例如：

```xaml
<Window x:Class="ResourceSample.WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourceSample.WpfApp"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Window.Resources>
        <ResourceDictionary>
            <sys:String x:Key="str">
                这是一段文字
            </sys:String>
            <sys:Double x:Key="dbl">3.1415926</sys:Double>
        </ResourceDictionary>
    </Window.Resources>
    <StackPanel>
        <TextBlock Text="{StaticResource ResourceKey=str}" />
        <!--下述语句编译将会抛出异常-->
        <!--<TextBlock Text="{StaticResource ResourceKey=dbl}" />-->
    </StackPanel>
</Window>
```

资源匹配顺序：

1. 先查找控件自己的Resources属性
2. 如果控件本身没有Resources，就沿着逻辑树向上一级一级的查找
3. 如果连最顶层容器都没有这个资源，程序就会去查找Application.Resources
4. 如果还没有找到，就抛出异常。

在C#代码中使用资源：

```c#
this.mytxt.Text = (string)this.Resources["str"];
```

也可以将资源存放到独立的文件中，通过ResourceDictionary的Source属性指定该资源定义的文件路径即可。

```xaml
<Window.Resources>
    <ResourceDictionary Source="Dictionary1.xaml"/>
</Window.Resources>
```

### 资源的有效范围

| 类型             | 说明                                                         |
| ---------------- | ------------------------------------------------------------ |
| Application      | 表示应用程序级别的资源。在Application.Resources中声明的资源，其有效范围覆盖整个应用程序。只要位于当前应用程序中的代码都能访问。 |
| Style            | 资源的有效范围仅限于当前样式，在样式之外不可访问。           |
| FrameworkElement | 当前元素及其子元素都可以访问资源，而当前元素的父级元素或其他元素不能访问。由于WPF中许多类型都从FrameworkElement类派生，因此Resources属性也会被子类继承，如Control、TextBox等类型。 |



### StaticResource 和 DynamicResource

- StaticResource：静态资源指的是在程序载入内存时对资源的一次性使用，之后就不再去访问这个资源了。用于只在程序初始化的时候使用一次、之后不会再改变的资源。
- DynamicResource：动态资源指的是在程序运行过程中仍然会去访问资源。用于程序运行过程中还有可能发生改变的资源。

XAML示例代码：

```c#
<Window.Resources>
    <ResourceDictionary>
        <TextBlock x:Key="res1" Text="床前明月光"/>
        <TextBlock x:Key="res2" Text="疑是地上霜"/>
    </ResourceDictionary>
</Window.Resources>
   
<StackPanel>
    <Button Margin="5" Content="{StaticResource ResourceKey=res1}"/>
    <Button Margin="5" Content="{DynamicResource res2}"/>
    <Button Margin="5" Content="更改资源" Click="Button_Click"/>
</StackPanel>
```

后台代码：

```c#
private void Button_Click(object sender, RoutedEventArgs e)
{
    this.Resources["res1"] = new TextBlock { Text = "什么都不是" };
    this.Resources["res2"] = new TextBlock { Text = "什么都不是" };
}
```





## 程序集资源或二进制资源

指的是Resources.resx资源文件和其他通过编译器编译进程序集的文件，如图片、音视频文件等。

### Resources.resx

二进制资源通常指的是Resources.resx文件。

可以通过右击项目=>添加新建项=>资源文件，来创建资源文件，文件名可以随意指定，最终作为资源引用的类。

注意：为了让XAML编译器能够访问这个类，一定要把Resources.resx的访问级别由Internal改为Public。

使用的时候，需要在XAML中引入该类所在的命名空间。

```xaml
<Window ...
	xmlns:res="clr-namespace:ResourceSample.WpfApp.Resources">
	<StackPanel>
		<TextBlock Text="{x:Static res:Resource.String1}"/>
    </StackPanel>
</Window>
```

### 程序集资源

如果要添加的资源不是字符串，而是图片、音频、视频、XML文件，可以先将这些文件添加到项目中，然后右击文件=>属性=>生成操作选择“Resource”即可。注意不要选择“嵌入的资源（Embedded Resource）“。

设置好后，就可以直接使用路径的方式引用文件了。

```xaml
<Image x:Name="img" Source="images/user.png" Stretch="None" />
```

与之等价的C#代码：

```c#
//使用相对路径
var uri = new Uri(@"images/user.png",UriKind.Relative);
this.img.Source=new BitmapImage(uri);
```

与之等价的使用绝对路径：

```c#
var uri = new Uri(@"pack://application:,,,/images/user.png",UriKind.Absolute);
this.img.Source=new BitmapImage(uri);
```

对应的XAML文件代码：

```xaml
<Image x:Name="img" Source="pack://application:,,,/images/user.png" Stretch="None" />
```

注意：

- 使用正斜线（/）表示路径

- 如果要使用相对路径，UriKind必须为Relative，而且代表根目录的/可以省略。

- 如果要使用绝对路径，UriKind必须为Absolute，并且代表根目录的/不能省略。

  



----

References:

- 《深入浅出WPF》
- 《C#码农笔记-WPF应用程序》
- [XAML 资源概述 - WPF .NET | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/systems/xaml-resources-overview?view=netdesktop-9.0)
- [如何为控件创建样式 - WPF .NET | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/how-to-create-apply-style?view=netdesktop-9.0)

Last updated：2025-04-04