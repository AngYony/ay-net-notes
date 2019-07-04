using Docs.DependencyInjection.Sample.interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.DependencyInjection.Sample.Models
{
    public class MyDependency: IMyDependency
    {
        private readonly ILogger<MyDependency> logger;


        public MyDependency(ILogger<MyDependency> _logger)
        {
            logger = _logger;
        }


        public Task WriteMessage(string message)
        {
            logger.LogInformation(
            "MyDependency.WriteMessage called. Message: {MESSAGE}",
            message);

            return Task.FromResult(0);
        }



    }
}
