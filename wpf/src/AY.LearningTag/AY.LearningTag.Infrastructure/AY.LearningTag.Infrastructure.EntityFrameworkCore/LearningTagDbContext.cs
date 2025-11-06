using AY.LearningTag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
{
    public class LearningTagDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public LearningTagDbContext()
        {
        }
        public LearningTagDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
