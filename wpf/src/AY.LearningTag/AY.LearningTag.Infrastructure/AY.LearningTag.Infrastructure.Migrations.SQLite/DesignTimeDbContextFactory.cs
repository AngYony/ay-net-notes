using AY.LearningTag.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 迁移项目在执行的时候，和App无关
 */
namespace AY.LearningTag.Infrastructure.Migrations.SQLite
{
    /// <summary>
    /// IDesignTimeDbContextFactory 只在开发时用于迁移生成，不在运行时使用，
    /// 因此设计时工厂（IDesignTimeDbContextFactory）是一个“孤立运行环境”，它必须自己知道数据库连接字符串，而不能依赖运行时 DI。
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LearningTagDbContext>
    {
        /// <summary>
        /// EF Core 设计时的配置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public LearningTagDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LearningTagDbContext>();
            optionsBuilder.UseSqlite("Data Source=C:\\DesignAY.db",
                b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.FullName));
            return new LearningTagDbContext(optionsBuilder.Options);
        }
    }
}
