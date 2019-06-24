# EF Core默认约定

默认情况下，EF Core 将名为 ID 或 classnameID 的属性视为主键。 在 classnameID 中，classname 为类名称。 



如果属性的类型是 ICollection<T> 时，EF Core 会默认创建 HashSet<T> 集合。



如果属性命名为 <navigation property name><primary key property name>，EF Core 会将其视为外键。例如Student 实体的主键为 ID，假如在如下实体中存在属性Student（指向Student实体）和属性StudentID，如下：

```c#
public class Enrollment
{
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
}
```

上述代码中，StudentID和CourseID 属性将被视为外键，其对应的导航属性为 分别为Student和Course。其中CourseID本身就是Course 实体的主键 CourseID。

简单来说，就是其他实体类名+ID就会被认为是外键。



DbContext相关约定：

```c#
public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Course> Course { get; set; }
    }
```

上述代码为每个实体集创建 DbSet<TEntity> 属性。 在 EF Core 术语中：

- 实体集通常对应一个数据库表。
- 实体对应表中的行。



