# ItemsControl 派生类应用技巧

ItemsControl 表示可用于呈现一组项的控件，因此大多数需要呈现多个条目的控件都派生自该类型。

ItemsControl 相关的派生关系：

ItemsControl => Control => FrameworkElement => UIElement

其派生的后代常用的有：

- [System.Windows.Controls.TreeView](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.treeview?view=windowsdesktop-9.0)
- [System.Windows.Controls.Primitives.MenuBase](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.primitives.menubase?view=windowsdesktop-9.0)
  - [System.Windows.Controls.ContextMenu](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.contextmenu?view=windowsdesktop-9.0)
  - [System.Windows.Controls.Menu](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.menu?view=windowsdesktop-9.0)
- [System.Windows.Controls.Primitives.Selector](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.primitives.selector?view=windowsdesktop-9.0)
  - [System.Windows.Controls.ComboBox](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.combobox?view=windowsdesktop-9.0)
  - [==System.Windows.Controls.ListBox==](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.listbox?view=windowsdesktop-9.0)
    - [System.Windows.Controls.ListView](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.listview?view=windowsdesktop-9.0)
  - [System.Windows.Controls.TabControl](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.tabcontrol?view=windowsdesktop-9.0)
  - [System.Windows.Controls.Primitives.MultiSelector](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.primitives.multiselector?view=windowsdesktop-9.0)
    - [==System.Windows.Controls.DataGrid==](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-9.0)
  - [System.Windows.Controls.Ribbon.Ribbon](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.ribbon.ribbon?view=windowsdesktop-9.0)

由此可以看到：

- 当需要进行选中的操作时，优先选择使用Selector派生的子类，如ListBox。
- 当不需要进行选中操作，可以直接使用ItemsControl，只用于展示数据即可。



## ItemsControl

ItemsControl 通用样式定义模板：

```xaml
<ItemsControl Margin="10" ItemsSource="{Binding Source={StaticResource myTodoList}}">
  <ItemsControl.Template>
    <ControlTemplate TargetType="ItemsControl">
      <Border BorderBrush="Aqua" BorderThickness="1" CornerRadius="15">
        <ItemsPresenter/>
      </Border>
    </ControlTemplate>
  </ItemsControl.Template>
  
  <ItemsControl.ItemsPanel>
    <ItemsPanelTemplate>
      <WrapPanel />
    </ItemsPanelTemplate>
  </ItemsControl.ItemsPanel>
  
  <ItemsControl.ItemTemplate>
    <DataTemplate>
      <DataTemplate.Resources>
        <Style TargetType="TextBlock">
          <Setter Property="FontSize" Value="18"/>
          <Setter Property="HorizontalAlignment" Value="Center"/>
        </Style>
      </DataTemplate.Resources>
      <Grid>
        <Ellipse Fill="Silver"/>
        <StackPanel>
          <TextBlock Margin="3,3,3,0"
                     Text="{Binding Path=Priority}"/>
          <TextBlock Margin="3,0,3,7"
                     Text="{Binding Path=TaskName}"/>
        </StackPanel>
      </Grid>
    </DataTemplate>
  </ItemsControl.ItemTemplate>
  
  <ItemsControl.ItemContainerStyle>
    <Style>
      <Setter Property="Control.Width" Value="100"/>
      <Setter Property="Control.Margin" Value="5"/>
      <Style.Triggers>
        <Trigger Property="Control.IsMouseOver" Value="True">
          <Setter Property="Control.ToolTip"
                  Value="{Binding RelativeSource={x:Static RelativeSource.Self},
                          Path=Content.Description}"/>
        </Trigger>
      </Style.Triggers>
    </Style>
  </ItemsControl.ItemContainerStyle>
</ItemsControl>
```



ItemsControl.Template 和 ItemsControl.ItemTemplate



是不是***Template都是指的是数据模板？







----

References:

- [ItemsControl 类 (System.Windows.Controls) | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-9.0)
- 

Last updated：2025-10-10