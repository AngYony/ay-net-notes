using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace My.MvcApplicationModel.Study.Conventions
{
    public class ApplicationDescription : IApplicationModelConvention
    {
        private readonly string _desc;

        public ApplicationDescription(string description)
        {
            _desc = description;
        }

        public void Apply(ApplicationModel application)
        {
            application.Properties["Desc"] = _desc;
        }
    }
}