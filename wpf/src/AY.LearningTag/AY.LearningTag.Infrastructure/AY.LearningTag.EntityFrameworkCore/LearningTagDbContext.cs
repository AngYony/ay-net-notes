using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.EntityFrameworkCore
{
    public class LearningTagDbContext : DbContext
    {
        public LearningTagDbContext(DbContextOptions options) : base(options)
        {
        }

        protected LearningTagDbContext()
        {
        }
    }
}
