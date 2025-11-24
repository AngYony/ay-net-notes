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
    public interface ISectionDataRepository<TDbContext> : IDataRepositoryBase<Section,TDbContext> where TDbContext : DbContext
    {
        Task<List<Section>> GetSectionsByCategoryAsync(int categoryId);
    }
}
