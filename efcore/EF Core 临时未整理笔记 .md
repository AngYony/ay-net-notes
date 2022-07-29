# EF Core 相关笔记

[TOC]





## EF Core 中的关系

### 一对一

### 一对多

一是主体实体（主表），多是依赖实体（从表）。

### 多对多

A表和B表时多对多的关系，在此基础上，存在中间表C表，将A表和B表进行串联，C表中含有两个外键，分别指向A表和B表。同时包含两个引用属性，指向A实体和B实体。



## EF Core 实体状态

Entity State：

- Added 添加
- Unchanged 没有变化 
- Modified 已修改
- Deleted 已删除 
- Detached 未跟踪 

![Entity Status](assets/MTY4ODg1MDY3NzE5MzgxNw_121073_SBj2-j-x2tyVtQen_1604237599.png)



## EF Core 中的并发处理

- 乐观处理：系统认为数据的更新在大多数情况下是不会产生冲突的，只在数据库更新操作提交的时候才对数据作冲突检测。
- 悲观处理：根据命名即对数据进行操作更新时，对操作持悲观保守的态度，认为产生数据冲突的可能性很大，需要先对请求的数据加锁再进行相关的操作。



## 重点

如果要在DbContext中获取HttpContext中的数据，必须要在DbContext的构造函数中获取，而不是在其他方法中获取。
例如代码中的tenantId，如果在其他方法中获取，会因为缓存策略，而无法命中，导致获取不到新的值。



HttpPut：一般用于整个对象的更新。

HttpPatch：一般用于对象的某个属性的更新。







