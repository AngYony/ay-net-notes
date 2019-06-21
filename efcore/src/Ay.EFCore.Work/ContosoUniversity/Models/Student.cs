using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        //ID 属性成为此类对应的数据库 (DB) 表的主键列。 默认情况下，EF Core 将名为 ID 或 classnameID 的属性视为主键。 
        public int ID{ get; set; }

        public string LastName{ get; set; }
        
        public string FirstMidName { get; set; }
        
        public DateTime EnrollmentDate { get; set; }

        // 使用 ICollection<T> 时，EF Core 会默认创建 HashSet<T> 集合
        public ICollection<Enrollment> Enrollments{ get; set; }
    }
}
