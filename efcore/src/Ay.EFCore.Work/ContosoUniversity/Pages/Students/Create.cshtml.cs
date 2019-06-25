using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public CreateModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        //[BindProperty]
        //public Student Student { get; set; }


        [BindProperty]
        public StudentVM StudentVM{ get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyStudent = new Student();


            if (await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",   // 是用于查找值的前缀。 该自变量不区分大小写
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
                {
                    _context.Student.Add(emptyStudent);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }

            return null;
        }



        public async Task<IActionResult> OnPostAsync2()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Student());
            //SetValues 使用属性名称匹配。 视图模型类型（StudentVM）不需要与模型类型（Student）相关，它只需要具有匹配的属性。
            entry.CurrentValues.SetValues(StudentVM);
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}