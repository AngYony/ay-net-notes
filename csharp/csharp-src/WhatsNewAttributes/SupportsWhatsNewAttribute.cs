using System;

namespace WhatsNewAttributes
{
    /// <summary>
    /// 表示不带任何参数的特性，用于把程序集标记为通过LastModifiedAttribute维护的文档。
    /// 这样，以后查看这个程序集的程序会知道，它读取程序集是我们使用自动文档过程生成的那个程序集
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsWhatsNewAttribute : Attribute
    {

    }
}
