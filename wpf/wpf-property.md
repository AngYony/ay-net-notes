# 属性（Property）

CLR属性：使用get/set 对字段（类中的变量，Field）进行封装访问的叫属性（Property）。

WPF中的属性，指的不是CLR属性，而是依赖项属性（dependency  property），也称为依赖属性，是标准.NET属性的全新实现。在WPF中，如动画、数据绑定以及样式都需要嵌入依赖项属性，且WPF元素提供的大多数属性都是依赖项属性。

当需要为控件支持数据绑定、动画或其他WPF功能时，需要手工创建依赖项属性。

传统的.NET开发中，一个对象所占用的内存空间在调用new操作符进行实例化的时候就已经决定了，而WPF允许对象在被创建的时候并不包含用于存储数据的空间（即字段所占用的空间），只保留在需要用到数据时能够获得默认值、借用其他对象数据或实时分配空间的能力。这种对象就被称为依赖对象（Dependency Object），而它这种实时获取数据的能力则依靠依赖属性（Dependency Property）来实现。



## 定义依赖项属性（DependencyProperty）

注意：只能为依赖对象（继承自DependencyObject的类）添加依赖项属性。WPF中的控件大部分都间接继承自了DependencyObject类。

> 由于DependencyObject是WPF系统中相当底层的一个基类（FrameworkElement --> UIElement --> Visual --> DependencyObject），WPF的所有UI都继承自该类，因此WPF中的所有元素全是依赖对象，同时WPF的类库在设计时充分利用了依赖属性的优势，UI控件的绝大多数属性都已经依赖化，都是依赖属性。

定义依赖项属性的本质是在派生自DependencyObject的类中声明一个类型为DependencyObject的静态变量。

定义依赖项属性的关键点：

- 需要在派生自DependencyObject的类中进行定义。
- 声明该变量的类型必须是DependencyProperty，即表示依赖属性。
- 使用static readonly进行修饰，按照约定该变量名称以“Property”结尾。之所以定义为静态字段，是为了在多个类之间共享该字段的信息。而使用readonly是为了防止其在使用的过程中被更改。

```csharp
public class WyElement : FrameworkElement
{
    public static readonly DependencyProperty JayProperty;
}
```



## 注册依赖项属性

注册依赖项属性就是为上述中定义的DependencyProperty类型的字段进行赋值操作。

由于DependencyProperty没有公有的构造函数，所以为DependencyProperty类型对象赋值只能使用DependencyProperty.Register()静态方法来创建对象。

由于DependencyProperty成员都是只读的（只有getter），所以一旦创建了DependencyProperty对象后就不能更改该对象，这也解释了为什么在定义DependencyProperty变量时，最好使用关键字readonly。

DependencyProperty.Register()方法共有三种重载形式：

```c#
public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
```

- name：用来指明以哪个CLR属性作为这个依赖属性的包装器，或者说此依赖属性支持的是哪个CLR属性。
- propertyType：用来指明该依赖属性存储的值是什么类型。
- ownerType：用来指明该依赖属性的宿主（依赖对象）是什么类型，或者说DependencyProperty.Register方法把这个依赖属性注册关联到哪个类型上。因为使用了public static修饰，因此ownerType指定的类型不一定非得是当前声明的依赖对象的类型，只是大多数情况下如此。
- typeMetadata：给依赖属性的DefaultMetadata属性赋值。
- validateValueCallback：一个用于验证属性的回调函数。

```c#
public class MyVisualB : FrameworkElement
{
    public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register(
       name: "FillBrushB",
       propertyType: typeof(Brush),
       ownerType: typeof(MyVisualB),
       typeMetadata: new FrameworkPropertyMetadata(
            //设置依赖项属性默认值
            Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender
                //当依赖项属性的值被修改后会调用
                //new PropertyChangedCallback(FillBrushPropertyChanged)
                ));
}
```

### FrameworkPropertyMetadata

FrameworkPropertyMetadata对象指示希望通过依赖项属性使用什么服务（如支持数据绑定、动画以及日志）。也就是说依赖项对象的其他附加的功能都是通过FrameworkPropertyMetadata对象来实现的。

FrameworkPropertyMetadata类的大多数属性都是简单的boolean类型。

### 总结

依赖属性的声明注意点：

- DependencyProperty 一定使用在 DependencyObject 里，所以依赖对象必须派生自 DependencyObject。
- DependencyProperty的实例变量由public static readonly 三个修饰符修饰。
- DependencyProperty的实例不是通过new()得到，而是通过调用DependencyProperty的静态方法Register(...)来创建的。



## 添加属性包装器

声明了DependencyProperty类型的变量之后，再使用传统的.NET属性封装该变量。封装的过程是使用在DependencyObject基类中定义的GetValue()和SetValue()方法。

依赖属性和CLR属性之间可以互不相干，即使不存在CLR属性，依然可以使用依赖属性。但为了向外界以CLR属性的形式暴露依赖属性，并且能够成为数据源的一个Path，所以会同时使用CLR属性。

```c#
public Brush FillBrush
{
    get { return (Brush)GetValue(FillBrushProperty); }
    set { SetValue(FillBrushProperty, value); }
}
```

> 当创建属性封装器时，应当只包含对SetValue()和GetValue()方法的调用，不应当添加任何验证属性值的额外代码或引发事件的代码等。这是因为WPF中的其他功能可能会忽略属性封装器，并直接调用SetValue()和GetValue()方法（一个例子是，在运行时解析编译过的XAML文件）。

属性封装器不是验证数据或引发事件的正确位置，而是使用依赖项属性回调函数。应当在声明DependencyProperty对象时，通过DependencyProperty.ValidateValueCallback回调函数进行验证操作，而事件的触发应当通过FrameworkPropertyMetadata.PropertyChangedCallback回调函数中进行。



## WPF 依赖项属性工作原理

每个依赖项属性都支持的两个关键行为：更改通知和动态值识别。

### 更改通知

当属性值发生变化时，依赖项属性不会自动引发事件以通知属性值发生了变化。相反，它们会触发受保护的名为OnPropertyChangedCallback()的方法。该方法通过两个WPF服务（数据绑定和触发器）传递信息，并调用PropertyChangedCallback回调函数（如果已经定义了该函数）。

换句话说，当属性变化时，如果希望进行响应，有两种方式：

- 数据绑定：使用属性值创建绑定
- 触发器：编写能够自动改变其他属性或开始动画的触发器

### 动态值识别

本质上，依赖项属性依赖于多个属性提供者，每个提供者都有各自的优先级。当从属性检索值时，WPF属性系统会通过一系列步骤获取最终值。

按照以下优先级从低到高的属性排列的因素来决定基本值：

1. 默认值：由FrameworkPropertyMetadata对象设置的值，优先级最低。
2. 继承而来的值：假设设置了FrameworkPropertyMetadata.Inherits标志，并为包含层次中的某个元素提供了值。
3. 来自主题样式的值
4. 来自项目样式的值
5. 本地值：使用代码或XAML直接为对象设置的值，优先级最高。

WPF按照上面优先级确定依赖项属性的基本值。这样做的优点是它占用的资源较少，如果没有显式的为属性设置本地值，WPF将从样式、其他元素或默认值中检索值。这时，就不需要内存来保存值。但基本值未必就是最后从属性中检索到的值，还需要考虑其他几个可能改变属性值的提供者。

WPF决定属性值的四步骤过程：

1. 确定基本值。
2. 如果属性是使用表达式设置的，就对表达式进行求值。WPF支持两类表达式：数据绑定和资源。
3. 如果属性是动画的目标，就应用动画。
4. 运行CoerceValueCallback回调函数来修正属性值。



## 共享的依赖项属性

同一个依赖项属性在不同的类中被共用。

例如，TextBlock.FontFamily属性和Control.FontFamily属性都指向TextElement类中定义的TextElement.FontFamilyPropery依赖项属性。当使用样式自动设置TextBlock.FontFamily属性时，样式也会影响Control.FontFamily属性，因为这两个类使用同一个依赖项属性。

通过调用DependencyProperty.AddOwner()方法实现依赖项属性的重用（共享）：

```csharp
static TextBlock()
{
    ...
    FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(TextBlock));
    FontStyleProperty = TextElement.FontStyleProperty.AddOwner(typeof(TextBlock));
    FontWeightProperty = TextElement.FontWeightProperty.AddOwner(typeof(TextBlock));
    ...
}
```

AddOwner()的重载形式：

```csharp
public DependencyProperty AddOwner(Type ownerType)
public DependencyProperty AddOwner(Type ownerType, PropertyMetadata typeMetadata)
```

AddOwner()方法的参数刚好和注册依赖项属性的所需参数一样。



## 依赖属性（Dependency Property）综合应用

### 自定义依赖对象和依赖属性

```csharp
public class Student : DependencyObject
{
    public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Student));
}
```

XAML文件代码：

```xaml
<StackPanel>
    <TextBox x:Name="txtbox1" BorderBrush="Black" Margin="5"/>
    <TextBox x:Name="txtbox2" BorderBrush="Black" Margin="5"/>
    <Button Content="OK" Margin="5" Click="Button_Click"/>
</StackPanel>
```

C# 代码：

```c#
private void Button_Click(object sender, RoutedEventArgs e)
{
    Student stu = new();
    stu.SetValue(Student.NameProperty, this.txtbox1.Text);
    txtbox2.Text= (string)stu.GetValue(Student.NameProperty);
}
```

上述代码中，分别调用了DependencyObject的SetValue(..)和GetValue(..)方法进行了依赖属性值的设置和读取。

通过上述代码可以看到，即使Student中没有定义Name的CLR属性，依然不影响依赖属性的使用。

#### 为依赖属性添加CLR属性外包装

快捷键：propdp+两次Tab

依赖属性依靠DependencyObject的SetValue和GetValue两个方法进行对外界的暴露，大多数情况下会为依赖属性添加一个CLR属性外包装。

```c#
 public class Student : DependencyObject
 {
     public string Name
     {
         get { return (string)GetValue(NameProperty); }
         set { SetValue(NameProperty, value); }
     }
     public static readonly DependencyProperty NameProperty =
             DependencyProperty.Register("Name", typeof(string), typeof(Student));
 }
```

这样在为依赖属性设置值时，可以直接通过属性赋值的方式进行即可。

改进后的代码：

```c#
private void Button_Click(object sender, RoutedEventArgs e)
{
    Student stu = new();
    stu.Name = this.txtbox1.Text;
    this.txtbox2.Text = stu.Name;
}
```

如果不关心底层的实现，下游程序员在使用依赖属性时与使用单纯的CLR属性感觉别无二致。

同时，为依赖对象的依赖属性添加了CLR属性包装，有了这个包装，就相当于为依赖对象准备了用于暴露数据的Binding Path。也就是说，现在的依赖对象已经具备了扮演数据源和数据目标双重角色的能力。

对Student进行扩展，添加SetBinding方法实现类似FrameworkElement类的SetBinding方法：

```c#
public class Student : DependencyObject
{
    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }
    public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Student));

    //SetBinding 包装
    public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
    {
        return BindingOperations.SetBinding(this, dp, binding);
    }
}
```

尽管Student类没有实现INotifyPropertyChanged接口，当属性的值发生改变时与之关联的Binding对象依然可以得到通知，依赖属性默认带有这样的功能，天生就是合格的数据源。

```c#
public partial class MainWindow : Window
{
    Student stu = new();
    public MainWindow()
    {
        InitializeComponent();
        stu.SetBinding(Student.NameProperty, new Binding("Text") { Source=txtbox1});
        //注意：这个stu必须是类成员，不能是函数中的局部变量，否则绑定将失效。
        txtbox2.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = stu });
    }
}
```

运行程序，当txtbox1中输入字符串时，txtbox2就会同步显示。

### 依赖属性值存取揭秘

#### DependencyProperty.Register(...)

- 依赖对象的依赖属性是static修饰的，因此依赖属性不保存在依赖对象中。
- DependencyProperty中定义了一个Hashtable类型的PropertyFromName变量，用来存放所有注册的DependencyProperty实例。
- DependencyProperty.Register(...)的实现是内部调用了RegisterCommon(...)方法，RegisterCommon的内部，通过实例化FromNameKey对象，将CLR属性名字符串和其宿主类型（依赖对象的类型）进行hash code重写，从而实现每对“CLR属性-依赖对象类型”所决定的DependencyProperty实例就是唯一的。此时如果尝试使用同一个CLR属性名称和同一个宿主类型（依赖对象类型）进行注册，程序会抛出异常。
- RegisterCommon(...)方法的内部实例化DependencyProperty对象，并将其放到Hashtable中。

一句话概括DependencyProperty对象的创建与注册：

==使用CLR属性名和宿主类型生成hash code，创建一个DependencyProperty实例，把hash code和该实例作为key-value对存入全局的、名为PropertyFromName的Hashtable中。这样，WPF属性系统通过CLR属性名和宿主类型就可以从这个全局的Hashtable中检索出对应的DependencyProperty实例。==

注意：这里的全局Hashtable使用的key 由CLR属性名哈希值和宿主类型哈希值经过运算得到的，并不是DependencyProperty实例的哈希值。

#### DependencyProperty的GetHashCode()

每个DependencyProperty实例都具有一个名为GlobalIndex的int类型属性，它的值是经过一些算法处理得到的，可以确保每个DependencyProperty实例的GlobalIndex是唯一的。

```c#
public override int GetHashCode()
{
    return GlobalIndex;
}
```

通过源码可以看到，GlobalIndex属性值也就是DependencyProperty实例的哈希值。因此通过GlobalIndex可以直接检索到某个DependencyProperty实例。



#### DependencyObject的GetValue(...)

DependencyObject的GetValue(...)的核心是内部调用了GetValueEntry(...)方法。该方法返回的是EffectiveValueEntry实例，EffectiveValueEntry的所有构造函数中都包含一个DependencyProperty类型的参数，换句话说，每个EffectiveValueEntry都关联着一个DependencyProperty。EffectiveValueEntry类具有一个名为PropertyIndex的属性，这个属性的值实际上就是与之关联的DependencyProperty的GlobalIndex属性值。

在DependencyObject中，还定义如下成员变量：

```c#
 private EffectiveValueEntry[] _effectiveValues;
```

这个数组依据每个成员的PropertyIndex属性值（来源于GlobalIndex）进行排序。

每个DependencyObject实例都自带一个EffectiveValueEntry类型数组，当某个依赖属性的值要被读取时，算法就会从这个数组中去检索值，如果数组中没有包含这个值，算法会返回依赖属性的默认值（默认值由依赖属性的DefaultMetadata来提供）。

依赖属性的值除了通过EffectiveValueEntry数组和默认值提供外，还可以通过其他途径获得，可能来自于元素的Style或Theme，也可能由上层元素继承下来，还可能是在某个动画过程的控制下不断变化而来。



## 附加的依赖项属性（附加属性，Attached Properties）

附加属性也是一种依赖项属性，不同之处在于附加属性被应用到的类并非定义附加属性的那个类。

附加属性的作用是将属性与数据类型（宿主或依赖对象）解耦，让数据类型的设计更加灵活。

附加属性的本质仍然是依赖属性，二者仅在注册和包装器上有一点区别。

定义附加属性，需要使用RegisterAttached()方法。

创建附加属性快捷键：propa+ 两次Tab

```c#
public class School : DependencyObject
{
    public static int GetGrade(DependencyObject obj)
    {
        return (int)obj.GetValue(GradeProperty);
    }

    public static void SetGrade(DependencyObject obj, int value)
    {
        obj.SetValue(GradeProperty, value);
    }

    // Using a DependencyProperty as the backing store for Grade.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GradeProperty =
        DependencyProperty.RegisterAttached("Grade", typeof(int), typeof(School), new UIPropertyMetadata(0));
}
```

附加属性与依赖属性在创建时的相同点与区别：

- 都使用 public static readonly 三个关键字修饰。
- 附加属性使用DependencyProperty.RegisterAttached(...)来声明，但参数和DependencyProperty.Register(...)方法无异。
- 依赖属性使用CLR属性对GetValue和SetValue两个方法进行包装，而附加属性则通过两个方法（非属性形式）分别进行包装。

使用附加属性：

```c#
class Human: DependencyObject
{
}
private void Button_Click(object sender, RoutedEventArgs e)
{
    Human hu = new Human();
    School.SetGrade(hu, 6);
    int grade = School.GetGrade(hu);
    MessageBox.Show(grade.ToString());
}
```

由于上述SetGrade(..)的内部调用的依然是DependencyObject实例的SetValue(..)方法，而DependencyObject实例传入的为Human实例，==因此该附加属性最终还是作用在Human上，只是定义寄宿在了School类中而已==。

### 附加属性的应用

```xaml
<Grid ShowGridLines="True">
    <Grid.ColumnDefinitions>
        <ColumnDefinition/>
        <ColumnDefinition/>
        <ColumnDefinition/>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition/>
        <RowDefinition/>
        <RowDefinition/>
    </Grid.RowDefinitions>
    <Button Content="OK" Grid.Column="1" Grid.Row="1"/>
</Grid>
```

与之等效的C#代码：

```c#
Grid g = new Grid() { ShowGridLines = true };

g.ColumnDefinitions.Add(new ColumnDefinition());
g.ColumnDefinitions.Add(new ColumnDefinition()); 
g.ColumnDefinitions.Add(new ColumnDefinition());

g.RowDefinitions.Add(new RowDefinition());
g.RowDefinitions.Add(new RowDefinition());
g.RowDefinitions.Add(new RowDefinition());

Button button = new Button() { Content = "OK" };
//关键代码
Grid.SetColumn(button, 1);
Grid.SetRow(button, 1);

g.Children.Add(button);
this.Content = g;
```

附加属性的本质就是依赖属性，附加属性也可以使用Binding依赖在其他对象的数据上。

```xaml
<Canvas>
    <Slider x:Name="sliderX" Canvas.Top="10" Canvas.Left="10" Width="260" Minimum="50" Maximum="200"/>
    <Slider x:Name="sliderY" Canvas.Top="40" Canvas.Left="10" Width="260" Minimum="50" Maximum="200"/>
    <Rectangle x:Name="rect" Fill="Blue" Width="30" Height="30"
    Canvas.Left="{Binding ElementName=sliderX,Path=Value}"
    Canvas.Top="{Binding ElementName=sliderY,Path=Value}"/>
</Canvas>
```

与之等效的c#代码：

```c#
this.rect.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = sliderX });
this.rect.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = sliderY });
```



## 属性的验证

依赖项属性的验证，不能直接在属性包装器中进行，而是在定义依赖项属性时，调用DependencyProperty.Register()方法通过传入的参数来实现。

WPF提供两种方法来阻止非法值：

- ValidateValueCallback（验证回调）：该回调函数可接受或拒绝新值。通常，该回调函数用于捕获违反属性月约束的明细错误。

  ```csharp
  public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
  ```

- CoerceValueCallback（强制回调）：该回调函数可将新值修改为更能被接受的值。该回调函数通常用于处理，为相同对象设置的依赖项属性值相互冲突的问题。这些值本身可能是合法的，但当同时应用时它们是不相容的。

  ```csharp
  public FrameworkPropertyMetadata(object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
  ```

设置依赖项属性时的作用过程：

1. 首先，CoerceValueCallback方法有机会修改提供的值（通常，使提供的值和其他属性相容），或者返回DependencyProperty.UnsetValue，这将完全拒绝修改。
2. 接下来激活ValidateValueCallback方法。该方法返回true亿接受一个值作为合法值，或者返回false拒绝值。与CoerceValueCallback方法不同，ValidateValueCallback方法不能访问设置属性的实际对象，这意味着不能检查其他属性值。
3. 最后，如果前两个阶段都获得成功，就会出发PropertyChangedCallback方法，此时，如果希望为其他类提供通知，可以引发更改事件。







----

References:

- 《深入浅出WPF》
- 《C#码农笔记-WPF应用程序》
- 《WPF编程宝典》

Last updated：2025-04-04