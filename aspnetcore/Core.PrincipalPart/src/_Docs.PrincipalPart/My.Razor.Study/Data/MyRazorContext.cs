using Microsoft.EntityFrameworkCore;
using My.Razor.Study.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.Razor.Study.Data
{
    public class MyRazorContext : DbContext
    {
        public MyRazorContext(DbContextOptions<MyRazorContext> options):base(options)
        {

        }

        public DbSet<StudentModel> Students { get; set; }
    }
}
