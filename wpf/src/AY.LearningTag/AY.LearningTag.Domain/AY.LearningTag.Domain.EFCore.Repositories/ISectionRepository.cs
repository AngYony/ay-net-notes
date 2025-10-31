using AY.LearningTag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.EFCore.Repositories
{
    public interface ISectionRepository<TDbContext> : IEFCoreRepository<Section, int, TDbContext> where TDbContext : DbContext
    {

    }
}
