﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My.Razor.Study.Data;
using My.Razor.Study.Models;

namespace My.Razor.Study.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly My.Razor.Study.Data.MyRazorContext _context;

        public EditModel(My.Razor.Study.Data.MyRazorContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StudentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentModelExists(StudentModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentModelExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
