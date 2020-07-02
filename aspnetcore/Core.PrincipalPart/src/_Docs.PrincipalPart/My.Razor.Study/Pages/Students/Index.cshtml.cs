using System;
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
    public class IndexModel : PageModel
    {
        private readonly My.Razor.Study.Data.MyRazorContext _context;

        public IndexModel(My.Razor.Study.Data.MyRazorContext context)
        {
            _context = context;
        }

        public IList<StudentModel> StudentModel { get; set; }

        public async Task OnGetAsync()
        {
            StudentModel = await _context.Students.ToListAsync();

            string [] gs = { "G1", "G2", "G3" };
            GroupNames = new SelectList(gs);


            //获取Address的值
            string _address = Address;
            string _qv = QueryValue;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string _address = Address;

            StudentModel = await _context.Students.ToListAsync();
            string gn = GroupNameValue;
            return Page();
           


        }

        public async Task<IActionResult> OnPostDeleteAsync(string wy)
        {
            return Page();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            return Page();
        }

        [BindProperty]
        public string Address { get; set; }


        public SelectList GroupNames { get; set; }
        [BindProperty]
        public string GroupNameValue { get; set; }


        [BindProperty(SupportsGet = true)]
        public string QueryValue { get; set; }




    }
}
