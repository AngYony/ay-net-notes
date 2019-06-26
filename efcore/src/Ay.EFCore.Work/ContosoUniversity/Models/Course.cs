using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course
    {

        //通过 DatabaseGenerated 特性指定主键，而无需靠数据库生成。
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }
        
        [StringLength(50,MinimumLength =3)]
        public string Title { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        [Range(0,5)]
        public int Credits { get; set; }

        public int DepartmentID{ get; set; }

        public Department Department { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
