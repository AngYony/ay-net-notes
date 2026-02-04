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
    public class SectionDataRepository<TDbContext> : EFCoreRepository<Section, TDbContext>,
        ISectionDataRepository where TDbContext : DbContext
    {
        public SectionDataRepository(IDbContextFactory<TDbContext> factory) : base(factory)
        {

        }

        public async Task<List<Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            return await Table.ToListAsync();
        }
    }
}
