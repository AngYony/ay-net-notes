# MVVM

MVVM = Model - View - ViewModel

ViewModel 与 View 的沟通：

- 传递数据：依靠数据属性（DataBinding）
- 传递操作：依靠命令属性（ICommand）



## MVVM的实现

核心在于如何将ViewModel展示到View上，即实现ViewModel的数据绑定和命令操作。XAML后台代码应该是空的或最少的代码，不再包含大量的控件事件，而是使用命令代替传统的事件处理。

在基于MVVM架构的基础上，只有通过实现INotifyPropertyChanged接口的ViewModel才能够用于Data Binding。





命令





