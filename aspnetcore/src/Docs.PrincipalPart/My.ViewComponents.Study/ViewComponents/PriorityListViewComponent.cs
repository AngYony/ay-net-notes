using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.ViewComponents.Study.Models;

namespace My.ViewComponents.Study.ViewComponents
{
    [ViewComponent(Name = "MyStudent")]
    public class StudentViewComponent : ViewComponent
    {
        private readonly StudentDbContext dbContext;

        public StudentViewComponent(StudentDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int StudentNo, string StudentAddress)
        {
            var item = await GetItemsAsync(StudentNo, StudentAddress);
            return View(item);
        }

        private Task<List<Student>> GetItemsAsync(int StudentNo, string StudentAddress)
        {
            return dbContext.Students.Where(x => x.Id >= StudentNo).ToListAsync();
        }
    }
}