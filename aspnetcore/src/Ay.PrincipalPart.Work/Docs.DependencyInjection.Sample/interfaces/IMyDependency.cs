using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.DependencyInjection.Sample.interfaces
{
    public interface IMyDependency
    {
        Task WriteMessage(string message);
    }
}
