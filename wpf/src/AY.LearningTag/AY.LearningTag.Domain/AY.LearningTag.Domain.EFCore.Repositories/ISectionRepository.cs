using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.EFCore.Repositories
{
    public interface IBaseRepository<TEntity, TDbContext> : IEFCoreRepository<TEntity, int, TDbContext> where TDbContext : DbContext
        where TEntity : class, IEntity<int>
    {
    }

    public interface ISectionRepository<TDbContext> : IBaseRepository<Section, TDbContext> where TDbContext : DbContext
    {

    }
}
