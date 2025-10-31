using AY.LearningTag.Domain.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
{
    public class SectionRepository : EFCoreRepository<AY.LearningTag.Domain.Entities.Section, int, LearningTagDbContext>,
        ISectionRepository<LearningTagDbContext>
    {
        public SectionRepository(IDbContextFactory<LearningTagDbContext> factory) : base(factory)
        {

        }
    }
}
