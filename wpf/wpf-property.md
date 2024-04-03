# 属性（Property）

CLR属性：使用get/set 对字段（类中的变量，Field）进行封装访问的叫属性（Property）。

WPF中的属性，指的不是CLR属性，而是依赖属性，通过DependencyProperty实现的，能够通过使用Binding从数据源获得值的属性。



## 依赖属性（Dependency Property）

传统的.NET开发中，一个对象所占用的内存空间在调用new操作符进行实例化的时候就已经决定了，而WPF允许对象在被创建的时候并不包含用于存储数据的空间（即字段所占用的空间），只保留在需要用到数据时能够获得默认值、借用其他对象数据或实时分配空间的能力。这种对象就被称为依赖对象（Dependency Object），而它这种实时获取数据的能力则依靠依赖属性（Dependency Property）来实现。

若要实现Binding的数据绑定，必须声明依赖对象的依赖属性。

依赖对象需要继承DependencyObject类，通过其SetValue和GetValue方法进行属性的写入和读取，而依赖属性需要通过调用DependencyProperty的方法来声明。

由于DependencyObject是WPF系统中相当底层的一个基类（FrameworkElement --> UIElement --> Visual --> DependencyObject），WPF的所有UI都继承自该类，因此WPF中的所有元素全是依赖对象，同时WPF的类库在设计时充分利用了依赖属性的优势，UI控件的绝大多数属性都已经依赖化，都是依赖属性。



### 自定义依赖对象和依赖属性

在DependencyObject的派生类中通过DependencyProperty.Register(...)方法创建依赖对象和依赖属性。

```c#
public class Student : DependencyObject
{
    //定义Name依赖属性
    public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Student));
    //即使未定义Name的CLR属性也不影响依赖属性的使用
}
```

依赖属性的声明注意点：

- DependencyProperty 一定使用在 DependencyObject 里，所以依赖对象必须派生自 DependencyObject。
- DependencyProperty的实例变量由public static readonly 三个修饰符修饰。
- DependencyProperty的实例不是通过new()得到，而是通过调用DependencyProperty的静态方法Register(...)来创建的。
- 依赖属性和CLR属性之间可以互不相干，即使不存在CLR属性，依然可以使用依赖属性。但为了向外界以CLR属性的形式暴露依赖属性，并且能够成为数据源的一个Path，所以会同时使用CLR属性。

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

#### DependencyProperty.Register(...)

该方法有多个重载方法，这里以参数最多的版本进行说明。

```c#
public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);
```

- name：用来指明以哪个CLR属性作为这个依赖属性的包装器，或者说此依赖属性支持的是哪个CLR属性。
- propertyType：用来指明该依赖属性存储的值是什么类型。
- ownerType：用来指明该依赖属性的宿主（依赖对象）是什么类型，或者说DependencyProperty.Register方法把这个依赖属性注册关联到哪个类型上。因为使用了public static修饰，因此ownerType指定的类型不一定非得是当前声明的依赖对象的类型，只是大多数情况下如此。
- typeMetadata：给依赖属性的DefaultMetadata属性赋值。

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



## 附加属性（Attached Properties）

附加属性的作用是将属性与数据类型（宿主或依赖对象）解耦，让数据类型的设计更加灵活。

附加属性的本质仍然是依赖属性，二者仅在注册和包装器上有一点区别。

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





参考资源：

- 《深入浅出WPF》