# IIS 相关的踩坑记录

### IIS 注册 .net 4.5 相关的问题

从网上搜索来的注册命令，执行后提示如下错误信息：

```
C:\Windows\Microsoft.NET\Framework\v4.0.30319>aspnet_regiis -i
Microsoft (R) ASP.NET RegIIS 版本 4.0.30319.33440
用于在本地计算机上安装和卸载 ASP.NET 的管理实用工具。
版权所有(C) Microsoft Corporation。保留所有权利。
开始安装 ASP.NET (4.0.30319.33440)。
此操作系统版本不支持此选项。管理员应使用“打开或关闭 Windows 功能”对话框、“服
务器管理器”管理工具或 dism.exe 命令行工具安装/卸载包含 IIS8 的 ASP.NET 4.5。有
关更多详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=216771。
ASP.NET (4.0.30319.33440)安装完毕。
```

解决办法：

```
dism /online /enable-feature /featurename:IIS-ISAPIFilter
dism /online /enable-feature /featurename:IIS-ISAPIExtensions
dism /online /enable-feature /featurename:IIS-NetFxExtensibility45
dism /online /enable-feature /featurename:IIS-ASPNET45
```

参考链接：https://blog.csdn.net/sweety820/article/details/79538973

