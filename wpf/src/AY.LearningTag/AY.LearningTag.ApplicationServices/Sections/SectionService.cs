using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices.Sections
{
    public class SectionService : ISectionService
    {
        private readonly ISectionDataRepository sectionRepository;

        public SectionService(ISectionDataRepository sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        public async Task<List<Domain.Entities.Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            return await sectionRepository.GetSectionsByCategoryAsync(categoryId);
        }
    }
}
