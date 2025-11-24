using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices.Sections
{
    public class SectionService<TDbContext> : ISectionService<TDbContext> where TDbContext : DbContext
    {
        private readonly ISectionDataRepository<TDbContext> sectionRepository;

        public SectionService(ISectionDataRepository<TDbContext> sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        public async Task<List<Domain.Entities.Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            return await sectionRepository.GetSectionsByCategoryAsync(categoryId);
        }
    }
}
