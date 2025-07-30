using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// Byte类型数据转换类
    /// </summary>
    [Description("Byte类型数据转换类")]
    public class ByteLib
    {
        /// <summary>
        /// 将字节中的某个位赋值
        /// </summary>
        /// <param name="value">原始字节</param>
        /// <param name="offset">位</param>
        /// <param name="bitValue">写入数值</param>
        /// <returns>返回字节</returns>
        [Description("将字节中的某个位赋值")]
        public static byte SetbitValue(byte value, int offset, bool bitValue)
        {
            return bitValue ? (byte)(value | (byte)Math.Pow(2, offset)) : (byte)(value & ~(byte)Math.Pow(2, offset));
        }

        /// <summary>
        /// 从字节数组中截取某个字节
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <returns>返回字节</returns>
        [Description("从字节数组中截取某个字节")]
        public static byte GetByteFromByteArray(byte[] value, int start)
        {
            if (start > value.Length - 1) throw new ArgumentException("字节数组长度不够或开始索引太大");

            return value[start];
        }

        /// <summary>
        /// 将布尔数组转换成字节数组
        /// </summary>
        /// <param name="value">布尔数组</param>
        /// <returns>字节数组</returns>
        [Description("将布尔数组转换成字节数组")]
        public static byte GetByteFromBoolArray(bool[] value)
        {
            if (value.Length != 8) throw new ArgumentNullException("检查数组长度是否为8");

            byte result = 0;

            //遍历当前字节的每个位赋值
            for (int i = 0; i < 8; i++)
            {
                result = SetbitValue(result, i, value[i]);
            }
            return result;
        }
    }
}
