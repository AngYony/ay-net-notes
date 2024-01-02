using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace My.ModelValidation.Study.Data
{
    public class StudentAttributeAdapter : AttributeAdapterBase<CusStudentAttribute>
    {

        private int _year;

        public StudentAttributeAdapter(CusStudentAttribute attribute, IStringLocalizer stringLocalizer)
        : base(attribute, stringLocalizer)
        {
            _year = attribute.XdYear;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new NotImplementedException();
            MergeAttribute(context.Attributes, "data-wy", "true");
            MergeAttribute(context.Attributes, "data-wy-student", GetErrorMessage(context));

            var year=Attribute.XdYear.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-wy-student-year", year);


        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return Attribute.GetErrorMessage();
        }
    }
}
