# ASP.NET Core Web Server 概述

ASP.NET Core应用基于进程内HTTP Server来运行，Server实现侦听HTTP请求，并将它们作为由HttpContext组成的请求功能的集合呈现给应用。

ASP.NET Core随附以下Server组件：

- Kestrel Server：默认跨平台HTTP Server
- IIS HTTP Server：用于ASP.NET Core 模块的IIS进程内Server实现。
- HTTP.sys Server：是仅用于Windows的HTTP Server，它基于HTTP.sys 核心驱动程序和 HTTP Server API（HTTP Server API使应用程序无需使用Microsoft Internet Information Server（IIS）即可通过HTTP进行通信）。

为了获得最佳性能，通常建议使用Kestrel，本文的内容也是基于Kestrel的。



## Kestrel

Kestrel 是 ASP.NET Core 项目模板中包括的默认 Web Server。

Kestrel 的使用方式如下：

- 本身作为边缘服务器，处理直接来自网络（包括 Internet）的请求。![kestrel-to-internet2](assets/kestrel-to-internet2.png)
- 与反向代理服务器（如 [Internet Information Services (IIS)](https://www.iis.net/)、[Nginx](http://nginx.org/) 或 [Apache](https://httpd.apache.org/)）结合使用。 反向代理服务器接收来自 Internet 的 HTTP 请求，并将这些请求转发到 Kestrel。![kestrel-to-internet](assets/kestrel-to-internet.png)

使用或不使用反向代理服务器进行配置对 ASP.NET Core 2.0 或更高版本的应用来说都是有效且受支持的托管配置。