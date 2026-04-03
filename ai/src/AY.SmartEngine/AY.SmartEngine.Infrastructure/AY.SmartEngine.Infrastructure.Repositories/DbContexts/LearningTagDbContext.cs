using AY.SmartEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories.DbContexts
{
    public class LearningTagDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public LearningTagDbContext()
        {
        }
        public LearningTagDbContext(DbContextOptions<LearningTagDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasKey(u => u.Id);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
