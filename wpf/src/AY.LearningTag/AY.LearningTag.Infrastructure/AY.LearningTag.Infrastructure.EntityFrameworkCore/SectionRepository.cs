using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
{
    //public class BaseRepository<TEntity> : EFCoreRepository<TEntity, int, LearningTagDbContext>,
    //    IBaseRepository<TEntity, LearningTagDbContext>
    //    where TEntity : class, IEntity<int>
    //{
    //    public BaseRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
    //    {
    //    }
    //}


    public class SectionRepository : EFCoreRepository<Section, int, LearningTagDbContext>, ISectionRepository<LearningTagDbContext>
    {
        public SectionRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
        {

        }

        public async Task<List<Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            return await Table.ToListAsync();
        }
    }
}
