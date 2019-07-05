using Docs.DependencyInjection.Sample.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.DependencyInjection.Sample.Models
{
    public class Operation: IOperationTransient,
    IOperationScoped,
    IOperationSingleton,
    IOperationSingletonInstance
    {

        public Operation():this(Guid.NewGuid())
        {
            
        }

        public Operation(Guid id){
            this.OperationId = id;
        }


        public Guid OperationId { get; private set; }
    }
}
