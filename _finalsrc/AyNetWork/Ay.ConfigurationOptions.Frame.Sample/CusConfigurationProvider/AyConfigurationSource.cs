using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ay.ConfigurationOptions.Frame.Sample.CusConfigurationProvider
{
    public class AyConfigurationSource : IConfigurationSource
    {
        private readonly Action<AyInfo> AyAction;
        public AyConfigurationSource(Action<AyInfo> _ayAction)
        {
            AyAction = _ayAction;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            //此处需要返回IConfigurationProvider
            return new AyConfigurationProvider(AyAction);
        }
    }
}
