using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Transformer
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value == null) { return null; }
            string str= Regex.Replace(value.ToString(),"([a-z])([A-Z])", "$1-$2").ToLower();
            return str;
        }
    }
}
