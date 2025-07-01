# Behavior（行为）

行为旨在封装一些UI功能，它是某些控件的共同特征的实现。相当于把通用的功能或操作进行包装，从而可以不必编写代码就能够将其应用到其他元素上。

Behavior抽象类的定义：

```csharp
namespace Microsoft.Xaml.Behaviors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media.Animation;

    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {
        protected Behavior(): base(typeof(T))
        {
        }
        protected new T AssociatedObject
        {
            get { return (T)base.AssociatedObject; }
        }
    }

    public abstract class Behavior :Animatable,IAttachedObject
    {
        protected Type AssociatedType { get; }
        protected DependencyObject AssociatedObject { get; }
 
        public void Attach(DependencyObject dependencyObject);
        public void Detach();
        protected override Freezable CreateInstanceCore();
        protected virtual void OnAttached();
        protected virtual void OnDetaching();
    }
}
```

AssociatedObject属性：通过该属性访问放置行为的元素，这个属性通常表示一个控件，类型是DependencyObject（依赖对象），也就是说，我们写的行为要给某个控件使用的前提是，这个控件是一个DependencyObject（依赖对象）。

OnAttached()：这是一个虚方法，将来在行为中被重写，表示附加一个行为时要执行的业务逻辑。

OnDetaching()：这是一个虚方法，将来在行为中被重写，表示分离一个行为时要执行的业务逻辑。



## 定义行为

创建行为通常派生自Behavior<T>，然后重写OnAttached()和OnDetaching()方法。

当调用OnAttached()方法时，可通过AssociatedObject属性访问放置行为的元素，并可关联事件处理程序。

当调用OnDetaching()方法时，移除事件处理程序。







## 行为相关的其他应用



### 行为结合附加事件实现页面绑定验证（重点）

自定义行为，ValidationErrorsBehavior.cs：

```csharp
public class ValidationErrorsBehavior : Behavior<FrameworkElement>
{
    public static readonly DependencyProperty ValidationErrorsProperty =
     DependencyProperty.Register(
         name: "ValidationErrors",
         propertyType: typeof(IList<ValidationError>), //属性使用的数据类型
         ownerType: typeof(ValidationErrorsBehavior),  //拥有该属性的类型
         typeMetadata: new PropertyMetadata(new List<ValidationError>()) //指定默认值
     );

    public IList<ValidationError> ValidationErrors
    {
        get { return (IList<ValidationError>)GetValue(ValidationErrorsProperty); }
        set { SetValue(ValidationErrorsProperty, value); }
    }

    public static readonly DependencyProperty HasValidationErrorProperty =
     DependencyProperty.Register(
         name: "HasValidationError",
         propertyType: typeof(bool), //属性使用的数据类型
         ownerType: typeof(ValidationErrorsBehavior),  //拥有该属性的类型
         typeMetadata: new PropertyMetadata(false) 
     );



    public bool HasValidationError
    {
        get { return (bool)GetValue(HasValidationErrorProperty); }
        set { SetValue(HasValidationErrorProperty, value); }
    }



    protected override void OnAttached()
    {
        base.OnAttached();
        //向指定的对象添加Error附加事件的事件处理程序
		//由于该行为用在了窗体本身，所以将会为窗体本身添加附加事件处理程序，通过在窗体中的控件上面进行事件冒泡，最终会触发该事件处理程序
        Validation.AddErrorHandler(this.AssociatedObject, OnValidationError);
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        Validation.RemoveErrorHandler(this.AssociatedObject, OnValidationError);
    }

    private void OnValidationError(object? sender, ValidationErrorEventArgs e)
    {
        if (e.Action == ValidationErrorEventAction.Added)
        {
            this.ValidationErrors.Add(e.Error);
        }
        else
        {
            this.ValidationErrors.Remove(e.Error);
        }

        this.HasValidationError = this.ValidationErrors.Count > 0;
    }
}
```

UserRegistWindow.xaml：

```xaml
<Window.Resources>
    <ResourceDictionary>
        <vc:SexConverter x:Key="SexConvert" />
    </ResourceDictionary>
</Window.Resources>
<!--  监听整个Window的验证操作  -->
<i:Interaction.Behaviors>
    <rules:ValidationErrorsBehavior HasValidationError="{Binding IsInvalid, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
</i:Interaction.Behaviors>
...

<TextBox
    Grid.Row="1"
    Grid.Column="1"
    Width="200"
    VerticalAlignment="Center"
    FontSize="16">
    <TextBox.Text>
        <Binding NotifyOnValidationError="True" Path="User.Mail">
            <Binding.ValidationRules>
                <rules:MailValidationRule />
            </Binding.ValidationRules>
        </Binding>
    </TextBox.Text>
</TextBox>
```

这里需要特别注意，要将Binding指定为NotifyOnValidationError="True"，表示在源更新过程中出现验证错误时应对绑定对象引发Error附加事件。

该示例完整代码见个人LearningTag项目。

### 使用Interaction.Triggers实现控件事件与命令的关联

```xaml
xmlns:i="http://schemas.microsoft.com/xaml/behaviors"
<ListBox
    x:Name="menuBar"
    ItemContainerStyle="{StaticResource myListBoxItemsStyle}"
    ItemsSource="{Binding MenuBars}">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="SelectionChanged">
            <i:InvokeCommandAction Command="{Binding NavigateCommand}" CommandParameter="{Binding ElementName=menuBar, Path=SelectedItem}" />
        </i:EventTrigger>
    </i:Interaction.Triggers>
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <materialDesign:PackIcon Margin="15,0" Kind="{Binding Icon}" />
                <TextBlock Margin="10,0" Text="{Binding Title}" />
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

