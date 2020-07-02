using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using My.Razor.Study.Data;
using My.Razor.Study.Models;

namespace My.Razor.Study.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly My.Razor.Study.Data.MyRazorContext _context;

        public CreateModel(My.Razor.Study.Data.MyRazorContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StudentModel StudentModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Students.Add(StudentModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}