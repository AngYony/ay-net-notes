using Microsoft.EntityFrameworkCore;

namespace My.ViewComponents.Study.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Student>().HasData(new Student
                {
                    Id = i,
                    StudentAddress = "Address" + i,
                    StudentName = "Stu" + i
                });
            }
        }
    }
}