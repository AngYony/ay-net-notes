using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.Middleware.Sample.Data;
using Docs.Middleware.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Docs.Middleware.Sample.Pages
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Student> Students { get; private set; }
        public async Task OnGet()
        {
            Students = await _db.Students.ToListAsync();
        }
    }
}