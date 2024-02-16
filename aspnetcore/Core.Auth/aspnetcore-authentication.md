# ASP.NET Core 认证

认证是一个确定请求访问者真实身份的过程。认证的目的在于确定请求者是否与其声称的这个身份相符。







## 认证相关的核心对象

- Claim：描述用户身份的声明
- IIdentity：用户的身份
- IPrincipal：接受认证的对象，可能对应一个用户，也可能对应一个应用、进程或服务，不管这个对象是哪种类型，都用IPrincipal接口表示。

用户通过`IPrincipal`对象表示，而用户采用的身份通过`IIdentity`接口表示。通过`IPrincipal`对象表示的用户可以拥有一个或者多个通过`IIdentity`对象表示的身份。



### Claim

Claim可以用来描述用户的信息（如地址、电话号码），也可以描述用户的权限（如拥有的角色或所在用户组）。

Claim只能通过构造函数创建，Claim的所有成员属性的值只能获取不能修改。

其中Type属性可以通过静态类型`ClaimTypes`中的常量形式获取；ValueType属性可以通过静态类型`ClaimValueTypes`中的常量形式获取。

### `IIdentity`的衍生类：`ClaimsIdentity`和`GenericIdentity`

` IIdentity`表示用户的身份，`ClaimsIdentity`实现了`IIdentity`接口，而`GenericIdentity`又继承自`ClaimsIdentity`。

三者定义如下：

```c#
public interface IIdentity
{
    //认证类型
    string? AuthenticationType { get; }
    //身份是否经过认证
    bool IsAuthenticated { get; }
    //用户名
    string? Name { get; }
}
public class ClaimsIdentity : IIdentity{...}
public class GenericIdentity : ClaimsIdentity{...}
```

#### ClaimsIdentity

ClaimsIdentity 表示采用声明来描述的身份。它是对一组Claim对象的封装，对外提供了操作这组Claim对象集合的一些方法。

ClaimsIdentity中表示身份是否经过认证的IsAuthenticated属性取决于ClaimsIdentity对象是否设置了认证类型。

ClaimsIdentity中的相关源码如下：

```c#
public virtual bool IsAuthenticated
{
    get { return !string.IsNullOrEmpty(_authenticationType); }
}
public virtual string? AuthenticationType
{
    get { return _authenticationType; }
}
```

换句话说，如果AuthenticationType属性不是Null或者空字符串，IsAuthenticated就返回true。

#### GenericIdentity

GenericIdentity是ClaimsIdentity的子类，表示一个“泛化”的身份。

注意：**GenericIdentity重写了IsAuthenticated属性**，源码如下：

```c#
public override string Name
{
    get
    {
        return m_name;
    }
}
public override bool IsAuthenticated
{
    get
    {
        return !m_name.Equals("");
    }
}
```

此时，IsAuthenticated属性的值取决于表示用户名的Name属性是否是一个空字符串，如果有一个具体的用户名，就返回True。

### IPrincipal的衍生类：ClaimsPrincipal和GenericPrincipal

IPrincipal表示接受认证的对象，通常指用户。

ClaimsPrincipal实现了IPrincipal接口，而GenericPrincipal类又继承自ClaimsPrincipal。

三者定义如下：

```c#
public interface IPrincipal
{
    //认证用户的身份
    IIdentity? Identity { get; }

    //用户是否被添加到指定的角色中
    bool IsInRole(string role);
}
public class ClaimsPrincipal : IPrincipal{...}
public class GenericPrincipal : ClaimsPrincipal{...}
```

#### ClaimsPrincipal

ClaimsPrincipal是对多个ClaimsIdentity对象的封装，因此一个用户可以有多个身份。对外提供了操作ClaimsIdentity集合的方法。

其他属性参见源码。

#### GenericPrincipal

GenericPrincipal是ClaimsPrincipal的子类，当创建一个GenericPrincipal对象时，可以直接指定作为身份的IIdentity对象和角色列表。

由于GenericPrincipal的构造函数的参数类型是IIdentity接口，因此创建GenericPrincipal对象时，可以指定一个任意类型的IIdentity对象。







 AuthenticationMiddleware：

ClaimsPrincipal

GenericIdentity

