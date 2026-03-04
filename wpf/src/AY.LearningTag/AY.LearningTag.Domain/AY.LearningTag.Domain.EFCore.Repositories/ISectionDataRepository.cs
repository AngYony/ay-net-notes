using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.EFCore.Repositories.Common;
using AY.LearningTag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.EFCore.Repositories
{
    public interface ISectionDataRepository : IEFCoreRepository<Section,int>
    {
        Task<List<Section>> GetSectionsByCategoryAsync(int categoryId);
    }
}
