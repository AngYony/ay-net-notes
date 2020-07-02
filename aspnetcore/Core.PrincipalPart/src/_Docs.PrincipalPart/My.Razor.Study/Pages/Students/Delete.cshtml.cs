using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My.Razor.Study.Data;
using My.Razor.Study.Models;

namespace My.Razor.Study.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly My.Razor.Study.Data.MyRazorContext _context;

        public DeleteModel(My.Razor.Study.Data.MyRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentModel StudentModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentModel = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

            if (StudentModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentModel = await _context.Students.FindAsync(id);

            if (StudentModel != null)
            {
                _context.Students.Remove(StudentModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
