using System;
using System.Collections.Generic;
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
        public int CourseID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
