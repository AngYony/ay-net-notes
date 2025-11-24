using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class DataRepositoryBase<TEntity, TDbContext> : EFCoreRepository<TEntity, int, TDbContext>,
        IDataRepositoryBase<TEntity, TDbContext> where TEntity : class, IEntity<int> where TDbContext : DbContext
    {
        public DataRepositoryBase(IDbContextFactory<TDbContext> factory) : base(factory)
        {
        }
    }
}
