# EF Core 技巧总结



获取单个实体时，无论是否通过主键获取，都推荐使用FirstOrDefaultAsync方法，而不是`SingleOrDefaultAsync`方法。

使用 `FirstOrDefaultAsync` 比使用 `SingleOrDefaultAsync` 更高效：

- 代码需要验证查询仅返回一个实体时除外。
- `SingleOrDefaultAsync` 会提取更多数据并执行不必要的工作。
- 如果有多个实体符合筛选部分，`SingleOrDefaultAsync` 将引发异常。
- 如果有多个实体符合筛选部分，`FirstOrDefaultAsync` 不引发异常。





使用AsNoTracking方法提升性能

