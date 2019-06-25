using Microsoft.EntityFrameworkCore;

namespace Ay.EFCore.Frame.Sample.Models
{
    public class AyDbContext : DbContext
    {
        public AyDbContext(DbContextOptions<AyDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}