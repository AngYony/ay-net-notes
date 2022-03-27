# C#中的各种Request总结

[TOC]

## WebRequest（不推荐使用）

命名空间：[System.Net](https://docs.microsoft.com/zh-cn/dotnet/api/system.net?view=netframework-4.7.2)

说明：该类是一个抽象类，一般不会使用。

注意，在官方文档中，明确说明：

> 建议你不要将 `WebRequest` 或其派生类用于新的开发。 请改用 [System.Net.Http.HttpClient](https://docs.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient?view=netframework-4.7.2) 类。



## HttpWebRequest（不推荐使用）

命名空间：[System.Net](https://docs.microsoft.com/zh-cn/dotnet/api/system.net?view=netframework-4.7.2)

说明：该类是WebRequest类的实现，在新的开发中，不建议使用。

注意，在官方文档中，明确说明：

> 建议不要将 `HttpWebRequest` 用于新的开发。 请改用 [System.Net.Http.HttpClient](https://docs.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient?view=netframework-4.7.2) 类。



## HttpRequest

命名空间：[System.Web](https://docs.microsoft.com/zh-cn/dotnet/api/system.web?view=netframework-4.8)

说明：该类主要用于ASP.NET程序中的服务端，用于获取客户端在Web请求期间发送的HTTP值。如在WebForm中的Request属性。



## WebClient（不建议使用）

命名空间：[System.Net](https://docs.microsoft.com/zh-cn/dotnet/api/system.net?view=netframework-4.8)

说明：提供用于将数据发送到由 URI 标识的资源及从这样的资源接收数据的常用方法。在新的开发中，不建议使用。

注意，在官方文档中，明确说明：

> 建议不要将 `WebClient` 类用于新的开发。 请改用 [System.Net.Http.HttpClient](https://docs.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient?view=netframework-4.8) 类。



## HttpClient（推荐使用）

命名空间：[System.Net.Http](https://docs.microsoft.com/zh-cn/dotnet/api/system.net.http?view=netframework-4.8)

说明：提供用于发送 HTTP 请求并从 URI 标识的资源接收 HTTP 响应的基类。