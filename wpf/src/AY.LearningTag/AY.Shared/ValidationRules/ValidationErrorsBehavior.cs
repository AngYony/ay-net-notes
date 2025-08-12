using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AY.Shared.ValidationRules
{
    public class ValidationErrorsBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ValidationErrorsProperty =
         DependencyProperty.Register(
             name: "ValidationErrors",
             propertyType: typeof(IList<ValidationError>), //属性使用的数据类型
             ownerType: typeof(ValidationErrorsBehavior),  //拥有该属性的类型
             typeMetadata: new PropertyMetadata(new List<ValidationError>()) //指定默认值
         );

        public IList<ValidationError> ValidationErrors
        {
            get { return (IList<ValidationError>)GetValue(ValidationErrorsProperty); }
            set { SetValue(ValidationErrorsProperty, value); }
        }

        public static readonly DependencyProperty HasValidationErrorProperty =
         DependencyProperty.Register(
             name: "HasValidationError",
             propertyType: typeof(bool), //属性使用的数据类型
             ownerType: typeof(ValidationErrorsBehavior),  //拥有该属性的类型
             typeMetadata: new PropertyMetadata(false) 
         );



        public bool HasValidationError
        {
            get { return (bool)GetValue(HasValidationErrorProperty); }
            set { SetValue(HasValidationErrorProperty, value); }
        }



        protected override void OnAttached()
        {
            base.OnAttached();
            //向指定的对象添加Error附加事件的事件处理程序
            //由于该行为用在了窗体本身，所以将会为窗体本身添加附加事件处理程序，通过在窗体中的控件上面进行事件冒泡，最终会触发该事件处理程序
            Validation.AddErrorHandler(this.AssociatedObject, OnValidationError);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Validation.RemoveErrorHandler(this.AssociatedObject, OnValidationError);
        }

        private void OnValidationError(object? sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                this.ValidationErrors.Add(e.Error);
            }
            else
            {
                this.ValidationErrors.Remove(e.Error);
            }

            this.HasValidationError = this.ValidationErrors.Count > 0;
        }
    }
}
