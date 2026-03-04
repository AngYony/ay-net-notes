# EF Core 实体状态流转图

## 一、EF Core 五种实体状态

```
Detached   → 未被 DbContext 跟踪
Unchanged  → 已跟踪，但未修改
Modified   → 已跟踪，已修改
Added      → 将要插入
Deleted    → 将要删除
```

## 二、完整状态流转图

```
               ┌──────────────┐
               │   Detached   │
               └──────┬───────┘
                      │
        ┌─────────────┼────────────────┐
        │             │                │
     Attach        Add / AddRange    Update
        │             │                │
        ▼             ▼                ▼
   Unchanged        Added           Modified
        │             │                │
        │             │                │
        ├──── 修改属性 ────────┐        │
        │                     │        │
        ▼                     ▼        ▼
     Modified              Modified  Modified
        │                     │        │
        └────────── SaveChanges ───────┘
                              │
                              ▼
                         Unchanged
```

## 三、每个方法的作用

### Add / AddRange

```
context.Add(entity);
```

状态变化：

```
Detached → Added
```

SaveChanges：

```
INSERT INTO ...
```

### Attach / AttachRange

```
context.Attach(entity);
```

状态变化：

```
Detached → Unchanged
```

✔ 不会插入
 ✔ 不会更新
 ✔ 只是开始跟踪

如果你改属性：

```
entity.Name = "Tom";
```

状态会变：

```
Unchanged → Modified
```

### Update / UpdateRange

```
context.Update(entity);
```

状态变化：

```
Detached → Modified
```

⚠ 所有字段都标记为 Modified
 SaveChanges 会：

```
UPDATE 表 SET 所有字段
```

### Remove / RemoveRange

```
context.Remove(entity);
```

状态变化：

```
Unchanged → Deleted
```

SaveChanges：

```
DELETE FROM ...
```

## 四、SaveChanges 之后发生什么？

所有：

```
Added / Modified / Deleted
```

在成功执行 SQL 后，都会变成：

```
Unchanged
```

除非你调用：

```
context.ChangeTracker.Clear();
```

## 五、实战常见场景解析

### 场景1：前端传回 ID + 修改字段

错误写法：

```
context.Update(entity);
```

问题：

- 所有字段都更新
- 容易覆盖 null
- 性能差

推荐：

```
context.Attach(entity);
context.Entry(entity).Property(x => x.Name).IsModified = true;
```

状态流转：

```
Detached → Unchanged → Modified
```

只更新指定字段。

### 场景2：批量更新已知主键数据

```
context.AttachRange(list);

foreach (var item in list)
{
    context.Entry(item).State = EntityState.Modified;
}
```

避免：

```
foreach (...) context.Update(...)
```

性能更可控。

## 六、状态变化核心规律

### 规律 1：EF 不知道数据库是否存在

EF 只看状态：

| 状态      | 行为   |
| --------- | ------ |
| Added     | 插入   |
| Modified  | 更新   |
| Deleted   | 删除   |
| Unchanged | 不做事 |

### 规律 2：Attach ≠ Update

Attach：

> 只是开始跟踪

Update：

> 强制全部更新

## 七、理解层级提升版（非常重要）

真正的核心不是：

> 用哪个方法

而是：

> 你想让实体处于什么状态？

当你理解：

```
context.Entry(entity).State = ?
```

你就完全掌控 EF。

------

## 八、终极一句话总结

```
Add      = 我要插入
Attach   = 它已存在
Update   = 我要更新（全部）
Remove   = 我要删除
```