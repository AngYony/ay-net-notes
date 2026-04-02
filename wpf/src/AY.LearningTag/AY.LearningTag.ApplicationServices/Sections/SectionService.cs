using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices.Sections
{
    public class SectionService : ISectionService
    {
        private readonly ISectionDataRepository _sectionDataRepository;

        public SectionService(ISectionDataRepository sectionDataRepository)
        {
            this._sectionDataRepository = sectionDataRepository;
        }

        public async Task<List<Domain.Entities.Section>> GetSectionsByCategoryAsync(int categoryId)
        {
            //using var _sectionDataRepository = serviceProvider.GetRequiredService<ISectionDataRepository>();
            return await _sectionDataRepository.GetSectionsByCategoryAsync(categoryId);
        }
    }
}
