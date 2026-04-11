using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.TaskQueue.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories.DbContexts
{
    public class TaskQueueDbContext : DbContext
    {
        public DbSet<JobEntity> Jobs { get; set; }
        public DbSet<JobQueueEntity> JobQueues { get; set; }

        public TaskQueueDbContext(DbContextOptions<TaskQueueDbContext> options) : base(options)
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
