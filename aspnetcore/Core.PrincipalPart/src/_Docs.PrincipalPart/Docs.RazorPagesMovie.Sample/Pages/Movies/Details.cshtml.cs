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
    public class DetailsModel : PageModel
    {
        private readonly Docs.RazorPagesMovie.Sample.Models.DocsRazorPagesMovieSampleContext _context;

        public DetailsModel(Docs.RazorPagesMovie.Sample.Models.DocsRazorPagesMovieSampleContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
