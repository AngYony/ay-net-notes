using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// 常规数据类型
    /// </summary>
    [Description("常规数据类型")]
    public enum DataType
    {
        /// <summary>
        /// 布尔类型，长度：1/8字节，[True、False]
        /// </summary>
        [Description("布尔类型")]
        Bool,

        /// <summary>
        /// 无符号字节类型，长度：1字节，[0~255]
        /// </summary>
        [Description("无符号字节类型")]
        Byte,


        /// <summary>
        /// 有符号16位短整型，长度：2字节，[-32768~32767]
        /// </summary>
        [Description("有符号16位短整型")]
        Short,
        /// <summary>
        /// 无符号16位短整型，长度：2字节，[0~65535]
        /// </summary>
        [Description("无符号16位短整型")]
        UShort,


        /// <summary>
        /// 有符号32位短整型，长度：4字节，[-2147483648~2147483647]
        /// </summary>
        [Description("有符号32位短整型")]
        Int,
        /// <summary>
        /// 无符号32位短整型，长度：4字节，[0-4294967295]
        /// </summary>
        [Description("无符号32位短整型")]
        UInt,
        /// <summary>
        /// 32位单精度浮点数，长度：4字节，[-3.4E38~3.4E38]
        /// </summary>
        [Description("32位单精度浮点数")]
        Float,

        /// <summary>
        /// 有符号64位长整型，长度：8字节，[-2E63~2E63-1]
        /// </summary>
        [Description("有符号64位长整型")]
        Long,
        /// <summary>
        /// 无符号64位长整型，长度：8字节，[0~2E64-1]
        /// </summary>
        [Description("无符号64位长整型")]
        ULong,
        /// <summary>
        /// 64位双精度浮点数，长度：8字节，[-1.79E308~1.79E308]
        /// </summary>
        [Description("64位双精度浮点数")]
        Double,

        /// <summary>
        /// 字符串类型
        /// </summary>
        [Description("字符串类型")]
        String,

        /// <summary>
        /// 字节数组
        /// </summary>
        [Description("字节数组")]
        ByteArray,

        /// <summary>
        /// 16进制字符串
        /// </summary>
        [Description("16进制字符串")]
        HexString
    }


    /// <summary>
    /// 字节类型的数据的存储顺序
    /// </summary>
    [Description("字节存储顺序")]
    public enum EndianType
    {
        /// <summary>
        /// 按照顺序排序，大端
        /// </summary>
        [Description("按照顺序排序，大端")]
        ABCD = 0,
        /// <summary>
        /// 按照单字反转，大端反转
        /// </summary>
        [Description("按照单字反转，大端反转")] 
        BADC = 1,
        /// <summary>
        /// 按照双字反转，小端反转
        /// </summary>
        [Description("按照双字反转，小端反转")] 
        CDAB = 2,
        /// <summary>
        /// 按照倒序排序，小端
        /// </summary>
        [Description("按照倒序排序，小端")] 
        DCBA = 3,
    }

    /// <summary>
    /// 复杂数据类型
    /// </summary>
    [Description("复杂数据类型")]
    public enum ComplexDataType
    {
        /// <summary>
        /// 布尔
        /// </summary>
        [Description("布尔")]
        Bool,
        /// <summary>
        /// 无符号字节
        /// </summary>
        [Description("无符号字节")]
        Byte,
        /// <summary>
        /// 有符号字节
        /// </summary>
        [Description("有符号字节")]
        SByte,
        /// <summary>
        /// 有符号16位整型
        /// </summary>
        [Description("有符号16位整型")]
        Short,
        /// <summary>
        /// 无符号16位整型
        /// </summary>
        [Description("无符号16位整型")]
        UShort,
        /// <summary>
        /// 有符号32位整型
        /// </summary>
        [Description("有符号32位整型")]
        Int,
        /// <summary>
        /// 无符号32位整型
        /// </summary>
        [Description("无符号32位整型")]
        UInt,
        /// <summary>
        /// 32位浮点型
        /// </summary>
        [Description("32位浮点型")]
        Float,
        /// <summary>
        /// 64位浮点型
        /// </summary>
        [Description("64位浮点型")]
        Double,
        /// <summary>
        /// 有符号64位整型
        /// </summary>
        [Description("有符号64位整型")]
        Long,
        /// <summary>
        /// 无符号64位整型
        /// </summary>
        [Description("无符号64位整型")]
        ULong,
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串")]
        String,
        /// <summary>
        /// 超文本字符串
        /// </summary>
        [Description("超文本字符串")]
        WString,
        /// <summary>
        /// 结构体
        /// </summary>
        [Description("结构体")]
        Struct,
        /// <summary>
        /// 布尔数组
        /// </summary>
        [Description("布尔数组")]
        BoolArray,
        /// <summary>
        /// 无符号字节数组
        /// </summary>
        [Description("无符号字节数组")]
        ByteArray,
        /// <summary>
        /// 有符号字节数组
        /// </summary>
        [Description("有符号字节数组")]
        SByteArray,
        /// <summary>
        /// 有符号16位整型数组
        /// </summary>
        [Description("有符号16位整型数组")]
        ShortArray,
        /// <summary>
        /// 无符号16位整型数组
        /// </summary>
        [Description("无符号16位整型数组")]
        UShortArray,
        /// <summary>
        /// 有符号32位整型数组
        /// </summary>
        [Description("有符号32位整型数组")]
        IntArray,
        /// <summary>
        /// 无符号32位整型数组
        /// </summary>
        [Description("无符号32位整型数组")]
        UIntArray,
        /// <summary>
        /// 32位浮点数数组
        /// </summary>
        [Description("32位浮点数数组")]
        FloatArray,
        /// <summary>
        /// 64位浮点数数组
        /// </summary>
        [Description("64位浮点数数组")]
        DoubleArray,
        /// <summary>
        /// 64位有符号整型数组
        /// </summary>
        [Description("64位有符号整型数组")]
        LongArray,
        /// <summary>
        /// 64位无符号整型数组
        /// </summary>
        [Description("64位无符号整型数组")]
        ULongArray,
        /// <summary>
        /// 字符串数组
        /// </summary>
        [Description("字符串数组")]
        StringArray,
        /// <summary>
        /// 超文本字符串数组
        /// </summary>
        [Description("超文本字符串数组")]
        WStringArray,
    }

}
