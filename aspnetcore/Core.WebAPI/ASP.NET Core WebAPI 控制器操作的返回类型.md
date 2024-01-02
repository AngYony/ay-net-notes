# ASP.NET Core WebAPI 控制器操作的返回类型

在ASP.NET Core中，Web API控制器操作返回的类型有以下几种：

- 特定类型
- `IActionResult`
- `ActionResult<T>`(推荐)



## 特定类型

Web API控制器操作返回的是基元类型（如int/double等）或复杂数据类型（如string或其定义对象类型）中的一种（不会出现根据条件的不同会返回基元或复杂类型的混合情况），就可以直接将返回的类型限定为特定类型。

简单来说，如果特定类型能够满足不同的条件下都能返回统一的类型的结果时，就可以使用特定类型进行返回。

```c#
[HttpGet]
public IEnumerable<Product> Get()
{
    return _repository.GetProducts();
}
```



## `IActionResult`类型

如果根据条件的不同，会返回不同类型的结果，例如可能返回int类型，也可能返回对象类型，此时就需要返回`IActionResult`或`IActionResult<T>`类型。

当操作中可能有多个 ActionResult 返回类型时，适合使用 IActionResult 返回类型。 

ActionResult 类型表示多种 HTTP 状态代码，属于该类别的一些常见返回类型包括：BadRequestResult (400)、NotFoundResult (404) 和 OkObjectResult (200)。

### 同步方式的操作

```c#
[HttpGet("{id}")]
[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public IActionResult GetById(int id)
{
    if (!_repository.TryGetProduct(id, out var product))
    {
        return NotFound(); //404
    }

    return Ok(product); //200
}
```

上述代码中，由于操作中有多个返回类型和路径，因此必须自由使用 [ProducesResponseType] 特性。 此特性可针对 Swagger 等工具生成的 API 帮助页生成更多描述性响应详细信息。 [ProducesResponseType] 指示操作将返回的已知类型和 HTTP 状态代码。也就是说，如果返回的类型包含多个不同的HTTP状态码时，需要显式使用[ProducesResponseType]特性进行指示。

### 异步方式的操作

```c#
[HttpPost]
[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> CreateAsync([FromBody] Product product)
{
    if (product.Description.Contains("XYZ Widget"))
    {
        return BadRequest(); //400
    }

    await _repository.AddProductAsync(product); 
	//201,CreatedAtAction 方法生成 201 状态代码。 在此代码路径中，返回的对象是 Product
    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}
```

注意：ApiController 属性使模型验证错误自动触发 HTTP 400 响应。 因此，Web API的操作方法中不需要以下代码：

```c#
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
```



## `ActionResult<T>`类型

`ActionResult<T>`是ASP.NET Core 2.1 引入的面向 Web API 控制器操作的返回类型。它支持返回从 ActionResult 派生的类型或返回特定类型，因此如果WebAPI应用程序支持ActionResult<T>类型，那么优先使用该类型进行返回。

ActionResult<T>类型提供了以下优势：

- 可排除 [ProducesResponseType] 特性的 Type 属性。 例如，[ProducesResponseType(200, Type = typeof(Product))] 可简化为 [ProducesResponseType(200)]。 此操作的预期返回类型改为根据 ActionResult<T> 中的 T 进行推断。
- 隐式强制转换运算符支持将 T 和 ActionResult 均转换为 ActionResult<T>。 将 T 转换为 ObjectResult，也就是将 return new ObjectResult(T); 简化为 return T;。

注意：C# 不支持对接口使用隐式强制转换运算符。 因此，必须使用 ActionResult<T>，才能将接口转换为具体类型。例如，在下面的示例中，使用 IEnumerable 不起作用：

```c#
[HttpGet]
public ActionResult<IEnumerable<Product>> Get()
{
    return _repository.GetProducts();
}
```

上面代码的一种修复方法是返回 _repository.GetProducts().ToList();。

### 同步方式的操作

```c#
[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public ActionResult<Product> GetById(int id)
{
    if (!_repository.TryGetProduct(id, out var product))
    {
        //返回ActionResult类型
        return NotFound();
    }
	//返回特定类型
    return product;
}
```

**提示：**从 ASP.NET Core 2.1 开始，使用 [ApiController] 特性修饰控制器类时，将启用操作参数绑定源推理。 与路由模板中的名称相匹配的参数名称将通过请求路由数据自动绑定。 因此，不会使用 [FromRoute] 特性对上述操作中的 id 参数进行显示批注。

### 异步方式的操作

```c#
[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Product>> CreateAsync(Product product)
{
    if (product.Description.Contains("XYZ Widget"))
    {
        return BadRequest(); //400
        //如果控制器已应用 [ApiController] 属性，且模型验证失败，也会返回400
    }
    await _repository.AddProductAsync(product);
    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}
```

**提示：**从 ASP.NET Core 2.1 开始，使用 [ApiController] 特性修饰控制器类时，将启用操作参数绑定源推理。 复杂类型参数通过请求正文自动绑定。 因此，不会使用 [FromBody] 特性对前面操作中的 product 参数进行显示批注。



## 总结

判断一个API应该返回哪种类型，应该结合HTTP状态代码来决定，特定的场景对应特定的HTTP状态代码，应该在首先满足HTTP状态代码的特定场景下，返回最适合的类型。