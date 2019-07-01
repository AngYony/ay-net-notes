using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ay.ConfigurationOptions.Frame.Sample.CusConfigurationProvider
{
    public static class AyConfigurationExtensions
    {
        public static IConfigurationBuilder AddAyConfiguration(
        this IConfigurationBuilder builder,
        Action<AyInfo> AyAction)
        {
            return builder.Add(new AyConfigurationSource(AyAction));
        }
    }
}
