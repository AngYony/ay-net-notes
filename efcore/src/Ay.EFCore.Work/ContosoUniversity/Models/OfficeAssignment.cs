using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorID{ get; set; }

        [StringLength(50)]
        [Display(Name ="Office Location")]
        public string Location{ get; set; }

        [Required]
        public Instructor Instructor{ get; set; }
    }
}
