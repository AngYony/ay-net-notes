using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using My.ModelValidation.Study.ViewModels;

namespace My.ModelValidation.Study.Data
{
    public class CusStudentAttribute: ValidationAttribute//,IClientModelValidator
    {
        private int _year;
        public CusStudentAttribute(int year){
            _year = year;
        }

        public int XdYear => _year;

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    if (context == null)
        //    {
        //        throw new ArgumentNullException(nameof(context));
        //    }

        //    MergeAttribute(context.Attributes, "data-smallz", "true");
        //    MergeAttribute(context.Attributes, "data-smallz-student", GetErrorMessage());

        //    var year = _year.ToString(CultureInfo.InvariantCulture);
        //    MergeAttribute(context.Attributes, "data-smallz-student-year", year);
        //}


        //private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        //{
        //    if (attributes.ContainsKey(key))
        //    {
        //        return false;
        //    }

        //    attributes.Add(key, value);
        //    return true;
        //}



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //validationContext.ObjectInstance表示要验证的对象
            var student = validationContext.ObjectInstance as StudentViewModel;

            var repleaseYear=( (DateTime)value).Year;

            if(repleaseYear<_year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        internal string GetErrorMessage()
        {
            return "出生年份必须大于" + _year;
        }
    }
}
