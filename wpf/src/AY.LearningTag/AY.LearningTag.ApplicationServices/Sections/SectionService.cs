using AY.LearningTag.Domain.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices.Sections
{
    public class SectionService<TDbContext> where TDbContext : DbContext
    {
        private readonly ISectionRepository<TDbContext> sectionRepository;

        public SectionService(ISectionRepository<TDbContext> sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        public async Task<List<Domain.Entities.Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            return await sectionRepository.GetSectionsByCategoryAsync(categoryId);
        }
    }
}
