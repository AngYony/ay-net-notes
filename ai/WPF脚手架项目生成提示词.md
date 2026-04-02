你是一名高级 .NET 架构师，请根据以下要求生成完整的 WPF 项目脚手架代码：

### 技术栈要求

- .NET10 + WPF + CommunityToolkit.Mvvm
- EF Core 10 + SQLite
- 日志：Serilog

### 项目结构要求

请遵循DDD生成清晰的分层结构，包括但不限于：

- ProjectName.Domain（实体层）
- ProjectName.Application（业务逻辑层）
- ProjectName.Infrastructure（基础设施层）
- ProjectName.WPF（UI层）

每个项目需包含：

- 必要的依赖引用
- 命名空间规范
- 示例代码

### 功能要求

- 必须使用标准的MVVM模式
- 使用通用Host进行依赖注入管理，如日志、其他层的服务注入等
- 使用IDbContextFactory支持多个不同的Sqllite数据库，实现仓储模式。