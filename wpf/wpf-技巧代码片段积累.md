# WPF技巧代码片段积累



### 修改WPF控件默认系统样式

需要借助模板来实现：

```XAML
<ListBox BorderThickness="0">
    <ListBox.ItemContainerStyle>
        <Style TargetType="ListBoxItem">
            <!--让ListBoxItem的内容都填充每一项的空间-->
            <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
            <Setter Property="Height" Value="30"/>
            <Setter Property="Template">
                <Setter.Value>
                    <!--移除ListBox系统默认的鼠标悬浮样式-->
                    <ControlTemplate TargetType="{x:Type ListBoxItem}">
                        <ContentPresenter/>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </ListBox.ItemContainerStyle>
    
    <RadioButton>
        <DockPanel LastChildFill="False">
            <TextBlock Text="&#xe635;" Style="{StaticResource iconStyle}" />
            <TextBlock Text="sdfsd"/>
            <TextBlock Text="2" DockPanel.Dock="Right"/>
        </DockPanel>
    </RadioButton>
</ListBox>
```

### 鼠标移动到控件上和选择该控件的样式触发器实现

```xaml
<Style TargetType="RadioButton">
    <Setter Property="VerticalContentAlignment" Value="Center"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type RadioButton}">
                <!--此处一定要将Grid设置为背景透明，否则只有移动到字体上面才会改变背景颜色-->
                <Grid Background="Transparent">
                    <!--border默认会填充整个Grid，因此可以作为背景元素来使用-->
                    <Border x:Name="border_back" />
                    <Border x:Name="border_thick" />
                    <ContentPresenter />
                </Grid>

                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter Property="Background" Value="#e0daff" TargetName="border_back"/>
                    </Trigger>
                    <Trigger Property="IsChecked" Value="True">
                        <Setter Property="Foreground" Value="Blue"/>
                        <Setter Property="FontWeight" Value="Bold"/>
                        <!--设置纯颜色背景与透明度-->
                        <Setter Property="Background" Value="Red" TargetName="border_back"/>
                        <Setter Property="Opacity" Value="0.1" TargetName="border_back"/>
                        <Setter Property="BorderThickness" Value="5 0 0 0" TargetName="border_thick"/>
                        <Setter Property="BorderBrush" Value="Red" TargetName="border_thick"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 数据绑定时使用`ObservableCollection<T>`代替`List<T>`



### 使用行为实现控件事件与命令的关联

```xaml
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

