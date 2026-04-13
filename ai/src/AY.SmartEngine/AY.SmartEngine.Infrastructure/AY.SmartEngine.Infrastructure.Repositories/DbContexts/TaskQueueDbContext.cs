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

        public DbSet<JobHistoryEntity> JobHistories { get; set;  }

        public TaskQueueDbContext(DbContextOptions<TaskQueueDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobEntity>(entity =>
            {
                entity.HasIndex(e => new { e.QueueName, e.JobStatus })
                    .HasDatabaseName("idx_jobs_queue_status");

                entity.HasIndex(e => e.ParentJobId)
                    .HasDatabaseName("idx_jobs_parent");

                entity.HasIndex(e => e.ScheduledAt)
                    .HasDatabaseName("idx_jobs_scheduled");

                entity.HasOne<JobEntity>()  //当前实体引用自己
                    .WithMany()             //无导航集合
                    .HasForeignKey(j => j.ParentJobId)
                    .OnDelete(DeleteBehavior.Restrict); //删除父级前必须先删除子级

                //entity.HasOne(x => x.ParentJob)
                //    .WithMany(x => x.ChildJobs)
                //    .HasForeignKey(x => x.ParentJobId)
                //    .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<JobQueueEntity>(entity =>
            {
                entity.HasIndex(e => e.QueueName)
                      .IsUnique()
                      .HasDatabaseName("idx_jobqueues_name");
            });

            modelBuilder.Entity<JobHistoryEntity>(entity => {
                entity.HasIndex(e => e.JobId)
                    .HasDatabaseName("idx_Jobhistory_jobid");
            });
        }
    }
}
