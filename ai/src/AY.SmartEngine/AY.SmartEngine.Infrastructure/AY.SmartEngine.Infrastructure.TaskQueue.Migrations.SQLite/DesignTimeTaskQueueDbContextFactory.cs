using AY.SmartEngine.Infrastructure.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.TaskQueue.Migrations.SQLite
{
    public class DesignTimeTaskQueueDbContextFactory : IDesignTimeDbContextFactory<TaskQueueDbContext>
    {
        public TaskQueueDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskQueueDbContext>();
            optionsBuilder.UseSqlite("Data Source=C:\\AYDB\\tq_design.db",
                b => b.MigrationsAssembly(typeof(DesignTimeTaskQueueDbContextFactory).Assembly.FullName));
            return new TaskQueueDbContext(optionsBuilder.Options);
        }
    }
}
