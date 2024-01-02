using Microsoft.AspNetCore.Mvc;
using My.CustomModelBinding.Study.Binders;

namespace My.CustomModelBinding.Study.Data
{
    [ModelBinder(BinderType = typeof(StudentEntityBinder))]
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StuCode { get; set; }
    }
}