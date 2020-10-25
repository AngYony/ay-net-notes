using LighterApi.Data.Project;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data
{
    public class LighterDbContext : DbContext
    {
        public LighterDbContext(DbContextOptions<LighterDbContext> options) : base(options)
        {
            //dotnet ef migrations add Init
        }
        public DbSet<Project.Project> Projects { get; set; }
        public DbSet<Project.Member> Members { get; set; }
        public DbSet<Project.Assistant> Assistants { get; set; }

        public DbSet<Project.ProjectGroup> ProjectGroups { get; set; }
        public DbSet<Project.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Project.Project>()
                .Property(p => p.Id).ValueGeneratedOnAdd();

            //一对多
            modelBuilder.Entity<Project.ProjectGroup>()
                .HasOne<Project.Project>(g => g.Project)
                .WithMany(p => p.Groups);

            # region EF Core 3 多对多
            //多对多，由两个与中间表关联的一对多组成
            //A表与中间表C表一对多
            modelBuilder.Entity<Project.SubjectProject>()
            .HasOne<Project.Project>(s => s.Project)
            .WithMany(p => p.SubjectProjects)
            .HasForeignKey(s => s.ProjectId);
            //B表与中间表C表一对多
            modelBuilder.Entity<Project.SubjectProject>()
           .HasOne<Project.Subject>(s => s.Subject)
           .WithMany(p => p.SubjectProjects)
           .HasForeignKey(s => s.SubjectId);
            #endregion

            #region EF Core 5 多对多
            //modelBuilder.Entity<Project.Project>()
            //.HasMany(p => p.Subject)
            //.WithMany(p => p.Project)
            //.UsingEntity(j => j.ToTable("SubjectProject"));

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
