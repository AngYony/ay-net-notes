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
    public class BaseRepository<TEntity> : EFCoreRepository<TEntity, int, LearningTagDbContext>
        where TEntity : class, IEntity<int>
    {
        public BaseRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
        {
        }
    }


    public class SectionRepository : BaseRepository<Domain.Entities.Section>
    {
        public SectionRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
        {
        }
    }
}
