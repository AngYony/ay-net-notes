using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices.Sections
{
    public interface ISectionService : ITransientServiceBase
    {
        Task<List<Domain.Entities.Section>> GetSectionsByCategoryAsync(int categoryId);
    }
}
