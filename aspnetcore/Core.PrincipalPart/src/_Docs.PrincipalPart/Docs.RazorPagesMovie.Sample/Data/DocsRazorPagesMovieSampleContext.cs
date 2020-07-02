using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Docs.RazorPagesMovie.Sample.Models
{
    public class DocsRazorPagesMovieSampleContext : DbContext
    {
        public DocsRazorPagesMovieSampleContext (DbContextOptions<DocsRazorPagesMovieSampleContext> options)
            : base(options)
        {
        }

        public DbSet<Docs.RazorPagesMovie.Sample.Models.Movie> Movie { get; set; }
    }
}
