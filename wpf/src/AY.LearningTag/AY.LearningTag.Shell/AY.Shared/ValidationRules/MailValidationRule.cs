using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AY.Shared.ValidationRules
{
    /// <summary>
    /// 邮件地址验证规则
    /// </summary>
    public class MailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult(false, "邮件地址不能为空");
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "邮件地址格式不正确");
            }
        }
    }
}
