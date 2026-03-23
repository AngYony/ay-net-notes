using System;
using System.Collections.Generic;
using System.Text;

namespace AY.Samples.Common.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ChatProvider
    {
        AzureOpenAI,
        DeepSeek,
        Qwen,
        Anthropic,
        General       //兼容了OpenAPI的形式

    }
}
