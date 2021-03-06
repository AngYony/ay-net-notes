﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    /// <summary>
    /// 登记、注册信息
    /// </summary>
    public class Enrollment
    {
        public int EnrollmentID { get; set; }


        //如果属性命名为 <navigation property name><primary key property name>，EF Core 会将其视为外键。 
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText ="No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
