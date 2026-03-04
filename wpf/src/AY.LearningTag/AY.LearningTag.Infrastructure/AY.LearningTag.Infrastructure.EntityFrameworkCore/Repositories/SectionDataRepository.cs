using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Domain.Entities;
using AY.LearningTag.Infrastructure.EntityFrameworkCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore.Repositories
{
    public class SectionDataRepository : EFCoreRepository<Section, int, LearningTagDbContext>, ISectionDataRepository
    {
        public SectionDataRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
        {

        }
        public async Task<List<Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            var data = await Table.ToListAsync();
            return data;
        }
    }
}
