using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ay.ConfigurationOptions.Frame.Sample.CusConfigurationProvider
{
    public class AyConfigurationProvider: ConfigurationProvider
    {
        Action<AyInfo> AyAction{ get; }

        public AyConfigurationProvider(Action<AyInfo> _ayAction)
        {
            AyAction = _ayAction;
        }

        public override void Load()
        {
           var ay= new AyInfo { Key = "AAA", Value = "BBB" };
            //为匿名委托传入实参
            AyAction(ay);

            Data = new Dictionary<string, string>
            {
                { "quote1", "I aim to misbehave." },
                { "quote2", "I swallowed a bug." },
                { "quote3", "You can't stop the signal, Mal." }
            };
            Data[ay.Key] = ay.Value;

        }
    }
}
