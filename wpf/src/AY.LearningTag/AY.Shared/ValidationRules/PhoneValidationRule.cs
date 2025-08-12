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
    /// 手机号码验证
    /// </summary>
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var phone = value as string;
            if (string.IsNullOrWhiteSpace(phone))
            {
                return new ValidationResult(false, "手机号码不能为空");
            }
            // 简单的手机号码格式验证
            if (System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1[3-9]\d{9}$"))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "手机号码格式不正确");
            }
        }
    }
}
