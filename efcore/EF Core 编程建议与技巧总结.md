# EF Core 编程建议与技巧总结



### CancellationToken

建议在 Controller 中，每个异步方法都采用带有 CancellationToken 参数的方法，这样在前台中断请求时，后端可以接收到前台中断的消息，从而取消后端的程序执行，以此来提高性能。



### 单独设置 EF Core 的日志级别

单独设置 EF Core 的日志级别：

"Microsoft.EntityFrameworkCore.Database.Command":"Debug"，一旦设置之后，可以在控制台中查看EF Core 执行的生成脚本。



### AddDbContext 和 AddDbContextPool

AddDbContext 和 AddDbContextPool：

参考EF Core官方文档说明，https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/context-pooling#limitations。并不是AddDbContextPool一定会起到很明显的优化作用。



### 遍历结果集时提前执行ToList()

foreach时，要先将要遍历的结果集提前执行ToList()，使其马上加载，然后再进行遍历。否则，它将在foreach内循环执行多次的数据库取值。

错误的写法：

```
foreach(var p in wy.source){}
```

正确的写法：

```
var list=wy.source.ToList();
foreach(var p in list){}
```



### 使用FirstOrDefaultAsync替代SingleOrDefaultAsync

获取单个实体时，无论是否通过主键获取，都推荐使用FirstOrDefaultAsync方法，而不是`SingleOrDefaultAsync`方法。

使用 `FirstOrDefaultAsync` 比使用 `SingleOrDefaultAsync` 更高效：

- 代码需要验证查询仅返回一个实体时除外。
- `SingleOrDefaultAsync` 会提取更多数据并执行不必要的工作。
- 如果有多个实体符合筛选部分，`SingleOrDefaultAsync` 将引发异常。
- 如果有多个实体符合筛选部分，`FirstOrDefaultAsync` 不引发异常。



### 使用AsNoTracking方法提升性能

使用AsNoTracking方法提升性能：

https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/crud?view=aspnetcore-2.2#add-related-data



### TryUpdateModelAsync

使用TryUpdateModelAsync尝试更新实体，可以避免过多发布。

https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/crud?view=aspnetcore-2.2#tryupdatemodelasync

实体状态：https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/crud?view=aspnetcore-2.2#entity-states



where语句区分大小写问题以及性能优化、

```c#
public async Task OnGetAsync(string sortOrder, string searchString)
{
    NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
    DateSort = sortOrder == "Date" ? "date_desc" : "Date";
    CurrentFilter = searchString;

    IQueryable<Student> studentIQ = from s in _context.Student
                                    select s;
    if (!String.IsNullOrEmpty(searchString))
    {
        studentIQ = studentIQ.Where(s => s.LastName.Contains(searchString)
                               || s.FirstMidName.Contains(searchString));
    }
}
```

上述代码调用 IQueryable 对象上的 Where 方法，且在服务器上处理该筛选器。 在某些情况下，应用可能会对内存中的集合调用 Where 方法作为扩展方法。 例如，假设 _context.Students 从 EF Core DbSet 更改为可返回 IEnumerable 集合的存储库方法。 结果通常是相同的，但在某些情况下可能不同。
例如，Contains 的 .NET Framework 实现会默认执行区分大小写的比较。 在 SQL Server 中，Contains 区分大小写由 SQL Server 实例的排序规则设置决定。 SQL Server 默认为不区分大小写。 可调用 ToUpper，进行不区分大小写的显式测试：
Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())

==如果上述代码改为使用 IEnumerable，则该代码会确保结果区分大小写。 如果在 IEnumerable 集合上调用 Contains，则使用 .NET Core 实现。 如果在 IQueryable 对象上调用 Contains，则使用数据库实现。 从存储库返回 IEnumerable 可能会大幅降低性能：==
==所有行均从 DB 服务器返回。==
==筛选应用于应用程序中所有返回的行。==
调用 ToUpper 不会对性能产生负面影响。 ToUpper 代码会在 TSQL SELECT 语句的 WHERE 子句中添加一个函数。 添加的函数会防止优化器使用索引。 如果安装的 SQL 区分大小写，则最好避免在不必要时调用 ToUpper。



当为一个实体定义集合的时候，尽量使用ICollection，而不是List。如果指定了 ICollection<T>，EF Core 会默认创建 HashSet<T> 集合。



### 允许事务中的Context异步提交事务

```c#

//允许事务中context异步提交数据
using TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled);
try
{
    // 添加主订单
    DateTime inputDate = DateTime.Now;
    string orderNo = Guid.NewGuid().ToString();
    SaleOrderMaster master = new SaleOrderMaster
    {
        CustomerNo = customerNo,
        DeliveryDate = input.DeliveryDate,
        EditUserNo = customerNo,
        InputDate = inputDate,
        Remark = input.Remark,
        InvoiceNo = input.invoice,
        SaleOrderNo = orderNo,
        StockNo = ""
    };
    await OrderMasterRepo.InsertAsync(master);
    // 添加流程
    await AddProgress(orderNo, inputDate);
    // 添加订单详情
    await AddOrderDetail(carts, customerNo, orderNo, inputDate);
    // 提交事务
    ts.Complete();
    // 删除Redis中的购物车数据
    foreach (var cart in carts)
    {
        RedisWorker.RemoveKey($"cart:{cart.CartGuid}:{customerNo}");
    }
    return true;
}
catch (System.Exception)
{
    ts.Dispose();
    throw;
}
```



