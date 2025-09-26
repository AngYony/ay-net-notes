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
    /// <summary>
    /// 自定义行为，用于处理验证错误
    /// </summary>
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


        /// <summary>
        /// 当行为附加到对象时调用，类似于OnLoad方法，可以对附加的对象进行初始化和事件绑定等等操作
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            //向指定的对象添加Error附加事件的事件处理程序
            //由于该行为用在了窗体本身，所以将会为窗体本身添加附加事件处理程序，通过在窗体中的控件上面进行事件冒泡，最终会触发该事件处理程序
            Validation.AddErrorHandler(this.AssociatedObject, OnValidationError);
        }

        /// <summary>
        /// 当行为从对象中分离时调用，类似于OnUnload方法，可以对附加的对象进行清理
        /// </summary>
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
