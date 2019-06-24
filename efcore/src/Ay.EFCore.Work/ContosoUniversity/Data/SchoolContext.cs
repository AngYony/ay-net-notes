using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoUniversity.Models.Student> Student { get; set; }

        /*
         可以省略 DbSet<Enrollment> 和 DbSet<Course>。 
         EF Core 隐式包含了它们，因为 Student 实体引用 Enrollment 实体，而 Enrollment 实体引用 Course 实体。
         */
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Course> Course{ get; set; }
    }
}
