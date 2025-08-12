using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace AY.Shared.Core
{
    /// <summary>
    /// 自定义行为Action，用于实现对附加属性的支持
    /// 是对ChangePropertyAction不支持附加属性的补充
    /// https://www.bilibili.com/video/BV1aM4m127HT?spm_id_from=333.788.videopod.sections&vd_source=e3d65fed6c5d2bee448a9a010e7d9a81
    ///用法：
    /// <b:Interation.Triggers>
    ///     <b:EventTrigger EventName="Click">
    ///         <local:ChangeAttachedPropertyAction PropertyName="ToolTip" ClassType="{x:Type ToolTipService}" Value="Hello World"/>
    ///     </ b:EventTrigger>
    /// </ b:Interation.Triggers>
    /// </summary>
    public class ChangeAttachedPropertyAction : TargetedTriggerAction<UIElement>
    {
        public Type? ClassType
        {
            get { return (Type)GetValue(ClassTypeProperty); }
            set { SetValue(ClassTypeProperty, value); }
        }

        public static readonly DependencyProperty ClassTypeProperty = DependencyProperty.Register(
            nameof(ClassType),
            typeof(Type),
            typeof(ChangeAttachedPropertyAction),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// 外部附加属性的名称
        /// </summary>
        public string? PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            nameof(PropertyName),
            typeof(string),
            typeof(ChangeAttachedPropertyAction),
            new PropertyMetadata("")
        );

        public object? Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(object),
            typeof(ChangeAttachedPropertyAction),
            new PropertyMetadata(null)
        );

        protected override void Invoke(object parameter)
        {
            ArgumentNullException.ThrowIfNull(ClassType, nameof(ClassType));
            if (string.IsNullOrWhiteSpace(PropertyName))
            {
                throw new ArgumentException("PropertyName cannot be null or empty", nameof(PropertyName));
            }
            //ArgumentNullException.ThrowIfNullOrEmpty(PropertyName, nameof(PropertyName));
            MethodInfo setter =
                ClassType.GetMethod($"Set{PropertyName}", BindingFlags.Static | BindingFlags.Public)
                ?? throw new ArgumentException($"Method Set{PropertyName} not found in {ClassType}");

            Type parameterType = setter.GetParameters()[1].ParameterType;
            if (parameterType.IsAssignableFrom(Value.GetType()))
            {
                setter.Invoke(null, new[] { Target, Value });
                return;
            }
            TypeConverter tc = TypeDescriptor.GetConverter(parameterType);
            if (Value != null && tc.CanConvertFrom(Value.GetType()))
            {
                setter.Invoke(null, new[] { Target, tc.ConvertFrom(Value) });
                return;
            }
            throw new ArgumentException($"Cannot convert {Value?.GetType()} to {parameterType}");
        }
    }
}