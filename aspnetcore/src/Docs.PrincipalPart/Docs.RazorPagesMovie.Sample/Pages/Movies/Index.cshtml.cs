using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Docs.RazorPagesMovie.Sample.Models;

namespace Docs.RazorPagesMovie.Sample.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Docs.RazorPagesMovie.Sample.Models.DocsRazorPagesMovieSampleContext _context;

        public IndexModel(Docs.RazorPagesMovie.Sample.Models.DocsRazorPagesMovieSampleContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
