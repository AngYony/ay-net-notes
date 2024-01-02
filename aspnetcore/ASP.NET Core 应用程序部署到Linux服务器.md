# ASP.NET Core 应用程序部署到Linux服务器

本文将介绍如何将ASP.NET Core应用程序部署到Linux服务器上。



## 环境

Linux系统：CentOS 7

服务器：Nginx



## 如何在CentOS 7上安装Nginx

Nginx是一款高性能的Web服务器软件。它是一个比Apache HTTP Server更灵活，更轻量级的程序。在安装Nginx之前，用户必须具有root权限。

#### 第一步：添加Nginx存储库

要添加CentOS 7 EPEL存储库，请打开终端并使用以下命令：

```shell
sudo yum install epel-release
```

#### 第二步：安装Nginx

安装完Nginx存储库后，接着使用以下yum命令安装Nginx ：

```shell
sudo yum install nginx
```

在对提示回答“yes”后，Nginx将完成安装。

#### 第三步：启动Nginx

 安装完Nginx之后，它不能自己启动，要运行Nginx，使用以下命令：

```shell
sudo systemctl start nginx
```

如果开启了防火墙，使用以下命令以允许HTTP和HTTPS流量：

```shell
sudo firewall-cmd --permanent --zone=public --add-service=http 
sudo firewall-cmd --permanent --zone=public --add-service=https
sudo firewall-cmd --reload
```

完成了以上三个步骤之后，就可以使用IP地址进行访问，如果希望在系统启动时就启用Nginx。可以输入以下命令：

```shell
sudo systemctl enable nginx
```

关于CentOS安装Nginx的其他方面的详细说明，请参阅[如何在CentOS 7上安装Nginx](https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-centos-7)。



## 在CentOS上安装.NET Core SDK

此处不再累述，具体请参阅官方文档：[在Linux CentOS / Oracle x64上安装.NET Core SDK](https://dotnet.microsoft.com/download/linux-package-manager/centos/sdk-current)，只需要按照文档上的说明进行安装即可。

