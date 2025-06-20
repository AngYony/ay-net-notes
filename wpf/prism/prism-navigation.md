# Prism - Navigation（导航）

### Navigation（导航）

用于视图的动态切换、回退、以及参数传递等功能。

导航功能相关的对象：

- INavigationAware：需要进行导航的View对应的ViewModel要实现该接口。
- IConfirmNavigationRequest：派生自INavigationAware的字接口，提供了触发导航前的确认导航服务。

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

