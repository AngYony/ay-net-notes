# UserControl 和 ContentControl

一句话概述两者区别：

**UserControl 是“一个具体的可复用 UI 组件”。**

**ContentControl 是“一个用来承载任意内容的容器”。**表示：我这里有一个 Content 属性，你给我什么，我就负责把它显示出来。

在WinUI 3 中，两者派生的子类有：

UserControl：Page

ContentControl：Frame

`Frame` 的核心职责：显示当前导航到的 Page。Frame 在 ContentControl 的基础上增加了：Navigation、History、Back、Forward、Navigate、PageStack。Frame = ContentControl + Navigation 能力。

Page 在 UserControl 的基础上增加了页面导航相关能力。Page   = UserControl   + Navigation/Page semantics。

派生关系：

```
DependencyObject
    │
    └── UIElement
         │
         └── FrameworkElement
              │
              └── Control
                   │
                   ├── ContentControl
                   │    │
                   │    └── Frame
                   │
                   └── UserControl
                        │
                        └── Page
```





