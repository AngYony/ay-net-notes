using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using My.CustomModelBinding.Study.Data;

namespace My.CustomModelBinding.Study.Binders
{
    public class StudentEntityBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //判断当前实例表示的模型类型是否为Student
            if (context.Metadata.ModelType == typeof(Student))
            {
                return new BinderTypeModelBinder(typeof(StudentEntityBinder));
            }

            return null;
        }
    }
}