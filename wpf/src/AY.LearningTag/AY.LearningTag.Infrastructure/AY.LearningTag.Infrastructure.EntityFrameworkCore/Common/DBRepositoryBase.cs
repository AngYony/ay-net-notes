//using AY.LearningTag.Domain.Abstractions.Entities;
//using AY.LearningTag.Domain.EFCore.Repositories;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
//{
//    /// <summary>
//    /// 不同数据库上下文的通用仓储基类
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    /// <typeparam name="TDbContext"></typeparam>
//    public class DBRepositoryBase<TEntity, TDbContext> : EFCoreRepository<TEntity, int, TDbContext>,
//        IDataRepositoryBase<TEntity, TDbContext> where TEntity : class, IEntity<int> where TDbContext : DbContext
//    {
//        public DBRepositoryBase(IDbContextFactory<TDbContext> factory) : base(factory)
//        {
//        }
//    }
//}
