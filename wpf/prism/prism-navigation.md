# Prism - Navigation（导航）

当用户与丰富的客户端应用程序交互时，其用户界面（UI）将不断更新，以反映用户正在处理的当前任务和数据。随着时间的推移，随着用户与应用程序内的交互并完成各种任务，UI可能会发生相当大的变化。应用程序协调这些UI更改的过程（界面切换）通常称为导航，这一过程由INavigationAware做支撑。

应用场景：

- View之间传值
- 需要在导航过程做操作（例如即使释放资源）。

用于视图的动态切换、回退、以及参数传递等功能。



## INavigationAware

INavigationAware：需要进行导航的View对应的ViewModel要实现该接口。

Register -> RequestNavigate -> OnNavigatedTo -> IsNavigationTarget -> ResoleView -> OnNavigatedFrom -> NavigateComplete

OnNavigatedFrom：导航离开当前页面前，此处可以传递过来的参数以及是否允许导航等动作的控制。

IsNavigationTarget：是否创建新实例。为true的时候表示不创建新实例，页面还是之前的，如果为false，则创建新的页面。

OnNavigationTo：导航到当前页面前，此处可以传递过来的参数以及是否允许导航等动作的控制。



## IConfirmNavigationRequest

IConfirmNavigationRequest：派生自INavigationAware的字接口，提供了触发导航前的确认导航服务。

当需要在导航操作期间与用户进行交互，以便用户可以确认或取消它。例如，在许多应用程序中，用户可能会尝试在输入或编辑数据时进行导航。在这些情况下，您可能需要询问用户是否希望保存或丢弃在继续从页面中导航之前已输入的数据，或者用户是否希望完全取消导航操作。

这些特性有IConfirmNavigationRequest做支撑，它融入了AOP的思想。

应用场景：

- 权限管理
- 检测用户行为（页面停留多久，哪个模块访问次数最多等），日志记录等。

RequestNavigate -> ConfirmNavigationRequest -> OnNavigatedFrom -> ContinueNavigationProcess



## IRegionNavigationJournal

导航日志其实就是对导航系统的一个管理功能，理论上来说，我们应该知道我们上一步导航的位置、以及下一步导航的位置，包括我们导航的历史记录。以便于我们使用导航对应用程序可以灵活的控制。类似于我们熟知的双向链表结构。

导航日志由IRegionNavigationJournal提供支持。

IRegionNavigationJournal接口有如下功能：

- GoBack()：返回上一页；
- CanGoBack：是否可以返回上一页；
- GoForward()：返回后一页
- CanGoForward：是否可以返回后一页；





## 综合示例

ViewB.xaml：

```xaml
<UserControl
    x:Class="PrismBlankApp.Views.ViewB"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:PrismBlankApp.Views"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:prism="http://prismlibrary.com/"
    d:DesignHeight="450"
    d:DesignWidth="800"
    prism:ViewModelLocator.AutoWireViewModel="True"
    mc:Ignorable="d">
    <Grid Background="Green">
        <TextBlock
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            FontSize="30"
            Text="{Binding Title}" />

    </Grid>
</UserControl>
```

ViewBViewModel.cs：

```csharp
public class ViewBViewModel : BindableBase, INavigationAware
{
    private string title;

    public string Title
    {
        get { return title; }
        set { SetProperty(ref title, value); }
    }


    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    /// <summary>
    /// 导航离开当前页时触发
    /// </summary>
    /// <param name="navigationContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    /// <summary>
    /// 导航完成前，接收用户传递的参数一级是否允许导航等控制
    /// </summary>
    /// <param name="navigationContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
    	//获取参数
        this.Title = navigationContext.Parameters.GetValue<string>("Value");
    }
}
```

ViewC.xaml：

```xaml
<UserControl
    x:Class="PrismBlankApp.Views.ViewC"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:PrismBlankApp.Views"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:prism="http://prismlibrary.com/"
    d:DesignHeight="450"
    d:DesignWidth="800"
    prism:ViewModelLocator.AutoWireViewModel="True"
    mc:Ignorable="d">
    <Grid Background="Red">
        <TextBlock
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            FontSize="30"
            Text="{Binding Title}" />

    </Grid>
</UserControl>
```

ViewCViewModel.cs，此处实现IConfirmNavigationRequest接口，添加了导航前的确认服务：

```csharp
public class ViewCViewModel : BindableBase, IConfirmNavigationRequest
{
    private string title;

    public string Title
    {
        get { return title; }
        set { SetProperty(ref title, value); }
    }

    public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
    {
        bool result = MessageBox.Show("确认要离开当前页吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        continuationCallback(result);
       
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    /// <summary>
    /// 导航离开当前页时触发
    /// </summary>
    /// <param name="navigationContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    /// <summary>
    /// 导航完成前，接收用户传递的参数一级是否允许导航等控制
    /// </summary>
    /// <param name="navigationContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        this.Title = navigationContext.Parameters.GetValue<string>("wy");
    }
}
```

在MainWindow.xaml中应用上述两个界面：

```xaml
<DockPanel>
    <StackPanel
        HorizontalAlignment="Center"
        DockPanel.Dock="Top"
        Orientation="Horizontal">
        <Button
            Width="100"
            Height="40"
            Margin="5"
            Command="{Binding OpenViewBCommand}"
            Content="ViewB" />
        <Button
            Width="100"
            Height="40"
            Margin="5"
            Command="{Binding OpenViewCCommand}"
            Content="ViewC" />

        <Button
            Width="100"
            Height="40"
            Margin="5"
            Command="{Binding GoForwordCommand}"
            Content="向前" />

        <Button
            Width="100"
            Height="40"
            Margin="5"
            Command="{Binding GoBackCommand}"
            Content="返回" />
    </StackPanel>
    <ContentControl prism:RegionManager.RegionName="ViewBRegion" />
</DockPanel>
```

MainWindowViewModel.cs：

```csharp
public class MainWindowViewModel : BindableBase
{
    private string _title = "Prism Application";

    //导航记录
    private IRegionNavigationJournal journal;

    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }

    private readonly IRegionManager regionManager;
    public DelegateCommand OpenViewBCommand { get; }
    public DelegateCommand OpenViewCCommand { get; }
    public DelegateCommand GoBackCommand { get; }
    public DelegateCommand GoForwordCommand { get; }

    public MainWindowViewModel(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        OpenViewBCommand = new DelegateCommand(OpenViewB);
        OpenViewCCommand = new DelegateCommand(OpenViewC);
        GoBackCommand = new DelegateCommand(() =>
        {
            journal.GoBack();
        });

        GoForwordCommand = new DelegateCommand(() =>
        {
            journal.GoForward();
        });
    }

    private void OpenViewB()
    {
        //方式一传递参数：
        NavigationParameters param = new NavigationParameters();
        param.Add("Value", "Hello");
        regionManager.RequestNavigate("ViewBRegion", "pageA", arg =>
        {
            journal = arg.Context.NavigationService.Journal;
        }, param);
    }

    private void OpenViewC()
    {
        //方式二：另一种传参形式，类似于url
        regionManager.RequestNavigate("ViewBRegion", "ViewC?wy=small");
    }
}
```

在App.xaml.cs中注册导航：

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    //注册导航
    containerRegistry.RegisterForNavigation<ViewB>("pageA"); //设置导航别名为pageA
    containerRegistry.RegisterForNavigation<ViewC>();
}
```

