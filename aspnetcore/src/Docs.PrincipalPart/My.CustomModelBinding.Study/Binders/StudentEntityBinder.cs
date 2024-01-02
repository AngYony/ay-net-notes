using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using My.CustomModelBinding.Study.Data;

namespace My.CustomModelBinding.Study.Binders
{
    public class StudentEntityBinder : IModelBinder
    {
        private readonly AppDbContext _db;

        public StudentEntityBinder(AppDbContext db)
        {
            _db = db;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            //获取模型名称
            var modelName = bindingContext.ModelName;
            //按照模型名称获取参数的值
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out int id))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Id必须是int类型");
                return Task.CompletedTask;
            }

            var model = _db.Students.Find(id);

            //设置最终模型绑定的结果
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}