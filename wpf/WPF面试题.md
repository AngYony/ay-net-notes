# WPF面试题



**WPF中的资源字典（Resource Dictionary）是什么？如何使用？**

每个元素都有Resources属性，该属性派生自FrameworkElement类，它的类型是ResourceDictionary（资源字典）。

可以在应用级别或窗口级别定义和共享资源，用于集中管理样式、模板、颜色等资源。

最大的用途就是在多个项目之间共享资源，可以添加资源字段文件，为了提高性能，务必要将生成操作设置为Page，这样可保证将资源字典编译为BAML。

使用方式：

- 可以直接在XAML文件中，使用<ResourceDictionary>标签来定义，也可以通过元素的Resources扩展标签来定义资源。
- 资源字典可以被合并，允许在多个文件中分隔不同的资源部分。如果有单独的资源字典文件，使用ResourceDictionary.MergedDictionaries标签引入。
- 通过 StaticResource 或 DynamicResource 关键字来引用资源。
- 可以通过C#代码调用元素的FindResource方法查找定义的资源。



**WPF** **中的动态资源和静态资源有什么区别?**

静态资源（StaticResource）：静态资源只从资源集合中获取对象一次，并且总是在创建窗口时加载。因此资源在应用程序启动或首次使用时解析，解析后值不再改变。使用时，性能较好，但资源的动态更新不支持，

动态资源（DynamicResource）：动态资源在每次需要对象时都会重新从资源集合中查找对象。动态资源在运行时动态解析，每次访问时都会查找最新的值。适用于需要在运行时动态更新的资源(如主题切换)，但性能稍差。

当资源具有依赖属性的设置时，需要使用动态资源。



**数据模板和控件模板区别？**

数据模板(DataTemplate):用于定义数据的显示方式，决定如何在UI中展示数据对象，定义数据展示的外观。举个例子:你有一堆学生信息，数据模板就是决定每个学生的名字和成绩怎么排列、用什么颜色显示。

控件模板(ControlTemplate):用于定义控件的外观和结构，决定控件如何呈现，定义控件的外观。举个例子:你想把按钮从默认的灰色方块变成一个红色的圆形按钮控件模板就是干这个的。

用途总结

数据模板:适用于需要自定义数据展示的场景，如列表项、内容控件等。

控件模板:适用于需要自定义扩外观的场景，如按钮、文本框等。



**WPF中的触发器（Triggers）是什么？有哪几种类型？**

触发器是 WPF 用于响应属性值变化或事件的机制，允许在满足条件时自动应用样式或执行动作。

类型：

属性触发器（Property Trigger）：基于某个依赖属性的值变化时触发。

事件触发器（Event Trigger）：基于事件（如 Click 或 MouseEnter）的触发。

数据触发器（Data Trigger）：基于数据绑定的值变化来触发，常用于 MVVM 模式中。

多重触发器（MultiTrigger）：当多个属性满足特定条件时触发。

多重数据触发器（MultiDataTrigger）：与 DataTrigger 类似，但基于多个数据绑定的值来触发。





**WPF数据绑定**

数据绑定的核心思想是:当数据发生变化时，UI自动更新;当用户修改 UI时，数据也自动更新。

数据绑定的本质是调用的BindingOperations.SetBinding方法，该方法需要传入Binding对象，Binding对象具有如下基本概念：

1.绑定源(Source):数据的来源可以是对象、集合、数据库等

2.绑定目标(Target):Ul控件

3.绑定路径(Path):指定绑定源中的哪个属性或字段需要绑定。

4.绑定模式(Binding Mode):决定数据流动的方向，比如单向绑定，双向绑定等。

WPF 提供了几种绑定模式（BindingMode枚举值）:

- OneWay：只根据源更新目标
- OneWayToSource：只根据目标更新源
- TwoWay：源和目标一起更新
- OnTime：仅在初始化绑定的时候更新
- Default：根据控件属性自动选择模式。

当使用了TwoWay或OneWayToSource需要根据目标更新源时，由UpdateSourceTrigger控制行为：

- 根据目标属性变化更新源（PropertyChanged）
- 根据目标属性变化且失去焦点时更新源（LostFocus)
- 只在调用updateSource()方法后才更新源（Explicit）
- Default：根据模板属性的元数据确定更新行为。

RelativeSource属性根据相对于目标对象的关系指向源对象。RelativeSourceMode枚举值：

- Self
- **FindAncestor**
- PreviousData
- TemplateParent

数据绑定的关键点

1.DataContext:绑定的数据源通常通过 DataContext 设置。控件会继承父容器的 DataContext

2.INotifyPropertyChanged:如果希望数据源变化时 UI 自动更新，数据源需要实现INotifyPropertyChanged 接口

ObservableCollection:如果绑定到集合，并且希望集合变化时 U1自动更新，使用0bservableCollection<T>接口





**WPF 中的依赖属性（Dependency Property）有哪些优点？**

依赖项属性所在的类需要继承自DependencyObject，通过DependencyProperty.Register方法来注册依赖项属性，该方法提供的PropertyMetadata类型的参数，允许设置属性元数据。PropertyMetadata类型提供了回调机制。支持 PropertyChangedCallback 和 CoerceValueCallback，允许对属性的更改进行定制逻辑处理。

同时DependencyProperty.Register重载方法允许对属性值进行验证，指定ValidateValueCallback回调函数即可。

**依赖属性**是 WPF 的一种特殊属性，用于支持复杂的属性系统（如数据绑定、动画、样式），它是通过 DependencyObject 类实现的。

**依赖属性优点：**

数据绑定支持：依赖属性支持双向数据绑定，能够自动更新 UI。

性能优化：依赖属性的值仅在发生更改时存储，因此在大量属性存在时，性能更高。

属性继承：依赖属性支持继承，子元素可以继承父元素的依赖属性值。

默认值机制：依赖属性支持定义默认值，且可以在运行时通过样式或模板动态更改默认值。

回调机制：支持 PropertyChangedCallback 和 CoerceValueCallback，允许对属性的更改进行定制逻辑处理。

动画支持：依赖属性能够直接参与 WPF 动画系统，动态改变属性值。

 

**附加属性（Attached Property）** 是一种特殊的依赖属性，它允许一个控件为另一个控件定义属性。附加属性的主要用途是扩展现有控件的功能，而无需修改控件的原始定义。附加属性通常用于布局控件或行为扩展。

 



**WPF 中的路由事件是什么？有哪几种路由策略？**

EventManager.RegisterRoutedEvent方法需要指定路由策略和事件处理程序的委托（RoutedEventHandler）。

路由事件是 WPF 的事件处理机制，允许事件从源控件沿着 UI 树向上或向下传播，增强了事件处理的灵活性。

路由策略：

冒泡（Bubble）：事件从子控件向父控件传播。

隧道（Tunnel）：事件从根节点向子控件传播。

直接（Direct）：事件直接在控件上触发，不进行传播。

 通过RaiseEvent()方法来引发路由事件。





**WPF中的命令**

WPF命令使得命令源(即命令发送者，也称调用程序)和命令目标（即命令执行者，也称处理程序）分离。

命令本身：RoutedUICommand:RoutedCommand: ICommand

命令源：ICommandSource，WPF的大多数控件都实现了该接口。通过该对象指定命令、命令参数、命令目标元素。

命令绑定：CommandBinding。





**什么是行为（Behavior）？**

行为是一种附加到控件上的功能扩展，它可以在不修改控件本身的情况下，为控件添加新的交互逻辑或功能。行为通常用于 MVVM 模式中，帮助将 UI 逻辑与业务逻辑分离。

行为有两个主要用途：

- 封装UI功能，即将某些控件的共同特征进行封装。
- 使用行为将控件元素的事件与命令进行关联响应。



创建行为，继承自Behavior泛型抽象类，Behavior中的AssociatedObject属性表示放置行为的元素。在重写的OnAttached()和OnDetaching()方法中，通过该属性可关联各种控件事件处理程序。

通过在XAML中使用Interaction.Behaviors来使用行为。

当关联命令时，需要使用Interaction.Triggers，结合行为中的EventTrigger（InvokeCommandAction）实现某个控件事件关联命令。



使用行为传值的思路？

我们可以通过以下步骤实现页面之间的传值：

定义一个行为：用于在控件上附加逻辑，监听某些事件（如按钮点击）。

在行为中触发数据传递：当事件发生时，将数据传递给目标页面。

在目标页面中接收数据：通过绑定或事件接收传递的数据。



**WPF多线程**

WPF的 UI控件只能在 UI线程中更新，因此在异步操作完成后，需要切换回 UI 线程。

**Dispatcher**：**Dispatcher** 是 UI 线程的任务调度器，负责管理 UI 线程上的任务队列。每个WPF可视化对象都有该属性（WPF可视化对象都派生自DispatcherObject类），用于返回管理该对象的调度程序。

UI 线程是 Dispatcher 的宿主，Dispatcher 依赖于 UI 线程。

后台线程不能直接更新 UI，必须通过 UI 线程的 Dispatcher 来更新 UI。

步骤:

1. 使用 Task.Run 或 async/await 进行异步操作。
2. 在操作完成后，使用Dispatcher.Invoke 或Dispatcher.Beginlnvoke 来更新 UI控件。

Invoke 和 BeginInvoke 区别：

Dispatcher.Invoke：以同步方式执行，调用线程会被阻塞，直到目标委托在Dispatcher线程中完成执行。适用于需要立即获取结果或确保操作顺序严格的场景。
Dispatcher.BeginInvoke：以异步方式执行，调用线程不会被阻塞，提交任务后可继续执行其他代码。适用于非阻塞式操作或性能要求较高的场景。

例如，在处理用户交互时，如果需要等待某个操作完成后再进行下一步，可以选择`Dispatcher.Invoke`。

1. `Dispatcher.Invoke`：通过将任务加入Dispatcher队列并等待其执行完成来实现同步操作。
2. `Dispatcher.BeginInvoke`：同样将任务加入Dispatcher队列，但不等待任务完成，直接返回一个`DispatcherOperation`对象。

若任务需等待完成且依赖其结果，选择`Invoke`；若希望提高响应速度并允许任务在后台运行，则选择`BeginInvoke`。



**如何在 WPF 中创建复杂的自定义控件（如复合控件）？**



复合控件 是由多个现有控件组成的控件。创建自定义控件时，可以使用 UserControl 或完全从头继承 Control 来定义控件的行为和外观。

UserControl 是一个将多个控件组合在一起的简单方法，适合需要快速封装多个控件的场景。

继承 Control 则需要实现更多的模板和行为控制，适用于更复杂的控件。

 

**自定义控件（Custom Control）**：

**自定义控件**在 WPF 中是一种非常强大的功能，可以创建符合自己需求的控件。通过继承 Control 类并实现自定义的 ControlTemplate 和依赖属性，你可以控制控件的外观和行为。

创建自定义控件的流程：

1、创建继承自 Control 类的控件。

2、使用 ControlTemplate 定义控件的外观。

3、在 XAML 文件中引用和使用自定义控件。

 



**WPF动画**

1. 简单线性动画，名称以Animation后缀结尾，如DoubleAnimation，可以设置变化起点、终点、幅度和变化时间。
2. 关键帧动画，名称以AnimationUsingKeyFrames后缀结尾，如DoubleAnimationUsingKeyFrames。可以创建和添加关键帧（KeyFrames）。
3. 沿路径动画，名称以AnimationUsingPath后缀结尾，如DoubleAnimationUsingPath。

这些动画都派生自引发动画效果的对应的类型的*AnimationBase基类。

由这些动画对象组合到一起，就可以实现StoryBoard（故事板），通过storyboard.Children属性来添加动画对象来实现，并使用使用 BeginStoryboard 触发动画。

storyboard派生自TimelineGroup抽象类，*AnimationBase派生自AnimationTimeline抽象类。







**如何在 WPF 中使用 CompositionTarget.Rendering 进行高帧率动画？**

WPF 的 CompositionTarget.Rendering 事件可以用来创建高帧率的自定义动画。该事件在每一帧被调用，通常用于实现复杂的图形动画或逐帧动画。 

使用步骤： 

1. 订阅 CompositionTarget.Rendering 事件。 
2. 在事件处理程序中更新 UI 元素的位置或其他属性，以创建平滑的动画效果。

 

**WPF** **的动画系统是如何工作的？如何实现时间线动画（Timeline Animation）？**

WPF 的动画系统基于 Storyboard 和 Animation 类，它们允许 UI 元素的属性值随时间平滑变化。

时间线动画 是 WPF 动画系统的核心机制，基于时间线（Timeline）控制动画的时序。

步骤：

1、定义 Storyboard 容器，将动画附加到目标属性。

2、使用不同类型的动画类（如 DoubleAnimation、ColorAnimation）来操作 UI 属性值。

  

 

 

**在MVVM架构中，ViewModel所负责的工作有哪些?**

在 MVVM（Model-View-ViewModel）架构中，ViewModel 是**连接** View（视图） 和 Model（模型） 的核心组件。它的**主要职责**是将数据从 Model 层**暴露**给 View 层，并处理 View 层的交互逻辑。ViewModel 的设计**目标是让 View 和 Model 解耦**，使代码更易于维护和测试。

**ViewModel** **的主要职责包括**：

1. 数据暴露：将 Model 层的数据暴露给 View 层。
2. 数据验证：验证用户输入的数据。
3. 命令处理：处理用户交互逻辑。
4. 状态管理：管理 View 层的状态（如加载状态、错误状态等）。
5. 数据转换：将 Model 层的数据转换为 View 层需要的格式。
6. 事件聚合：处理多个 View 或 Model 之间的事件通信。

 



**WPF****中 有2个页面 这2个页面如何传值？**

1、构造函数传值：适合在页面导航时传递数据。

2、属性传值：适合在页面已经存在时传递数据。

3、静态类或单例模式：适合在多个页面之间共享数据。

4、事件或回调：适合将数据从页面 B 传回页面 A。

5、NavigationService：适合在使用 NavigationService 导航时传递数据。

6、依赖注入：适合在使用了 DI 框架的项目中传递数据。

**MVVM****中两个页面可以靠行为传值？**



 

**接口和抽象类的区别？**

(1)接口没有构造函数，不能有字段变量，不能定义每个方法的访问权限，方法必须是抽象的，没有函数体。

(2)抽象类可以有构造函数，可以有普通的字段变量，可以定义每个方法的访问权限，方法可以是非抽象的，可以有函数体。