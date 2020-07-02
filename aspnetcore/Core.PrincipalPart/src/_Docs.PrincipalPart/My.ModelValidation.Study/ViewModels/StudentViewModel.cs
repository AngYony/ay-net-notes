using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using My.ModelValidation.Study.Data;

namespace My.ModelValidation.Study.ViewModels
{
    public class StudentViewModel : IValidatableObject
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "学生姓名不能为空")]
        [Remote(action: "VerifyData", controller: "Home", AdditionalFields = nameof(Age) + "," + nameof(Birthday))] //该特性必须借助前端js才能实现
        public string StudentName { get; set; }

        [DataType(DataType.Date)]
        [CusStudent(year: 2000)]
        public DateTime Birthday { get; set; }

        //[Required]
        //[Range(18,36)]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age > 100)
            {
                yield return new ValidationResult("Age不能大于100", new[] { nameof(Age) });
            }
        }
    }
}