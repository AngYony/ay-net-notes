using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// Bit类型数据转换类
    /// </summary>
    [Description("Bit类型数据转换类")]
    public class BitLib
    {
        /// <summary>
        /// 返回某个字节的指定位
        /// </summary>
        /// <param name="value">字节</param>
        /// <param name="offset">偏移位</param>
        /// <remarks>偏移位0-7有效，否则结果不正确</remarks>
        /// <returns>布尔结果</returns>
        [Description("返回某个字节的指定位")]
        public static bool GetBitFromByte(byte value, int offset)
        {
            return (value & (1 << offset)) != 0;

            //return (data & (byte)Math.Pow(2, offset)) != 0;
        }

        /// <summary>
        /// 获取字节数组(长度为2)中的指定位
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="offset">偏移位</param>
        /// <param name="isLittleEndian">大小端</param>
        /// <remarks>偏移位0-15有效，否则结果不正确</remarks>
        /// <returns>布尔结果</returns>
        [Description("获取字节数组(长度为2)中的指定位")]
        public static bool GetBitFrom2Bytes(byte[] value, int offset, bool isLittleEndian = true)
        {
            if (value.Length < 2) throw new ArgumentException("数组长度小于2");

            if (isLittleEndian)
            {
                return GetBitFrom2Bytes(value[1], value[0], offset);
            }
            else
            {
                return GetBitFrom2Bytes(value[0], value[1], offset);
            }
        }

        /// <summary>
        /// 获取高低字节的指定位
        /// </summary>
        /// <param name="high">高位字节</param>
        /// <param name="low">低位字节</param>
        /// <param name="offset">偏移位</param>
        /// <remarks>偏移位0-15有效，否则结果不正确</remarks>
        /// <returns>布尔结果</returns>
        [Description("获取高低字节的指定位")]
        public static bool GetBitFrom2Bytes(byte high, byte low, int offset)
        {
            if (offset >= 0 && offset <= 7)
            {
                return GetBitFromByte(low, offset);
            }
            else
            {
                return GetBitFromByte(high, offset - 8);
            }
        }

        /// <summary>
        /// 返回字节数组中某个字节的指定位
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">字节索引</param>
        /// <param name="offset">偏移位</param>
        /// <remarks>偏移位0-7有效，否则结果不正确</remarks> 
        /// <returns>布尔结果</returns>
        [Description("返回字节数组中某个字节的指定位")]
        public static bool GetBitFromByteArray(byte[] value, int start, int offset)
        {
            if (start > value.Length - 1) throw new ArgumentException("数组长度不够或开始索引太大");

            return GetBitFromByte(value[start], offset);
        }

        /// <summary>
        /// 返回字节数组中某2个字节的指定位
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">字节索引</param>
        /// <param name="offset">偏移位</param>
        /// <param name="isLittleEndian">大小端</param>
        /// <remarks>偏移位0-15有效，否则结果不正确</remarks> 
        /// <returns>布尔结果</returns>
        [Description("返回字节数组中某2个字节的指定位")]
        public static bool GetBitFrom2BytesArray(byte[] value, int start, int offset, bool isLittleEndian = true)
        {
            if (start > value.Length - 2) throw new ArgumentException("数组长度不够或开始索引太大");

            byte[] array = new byte[] { value[start], value[start + 1] };

            return GetBitFrom2Bytes(array, offset, isLittleEndian);
        }

        /// <summary>
        /// 根据一个Short返回指定位
        /// </summary>
        /// <param name="value">short数值</param>
        /// <param name="offset">偏移位</param>
        /// <param name="isLittleEndian">大小端</param>
        /// <remarks>偏移位0-15有效，否则结果不正确</remarks>
        /// <returns>布尔结果</returns>
        [Description("根据一个Short返回指定位")]
        public static bool GetBitFromShort(short value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        /// <summary>
        /// 根据一个UShort返回指定位
        /// </summary>
        /// <param name="value">short数值</param>
        /// <param name="offset">偏移位</param>
        /// <param name="isLittleEndian">大小端</param>
        /// <remarks>偏移位0-15有效，否则结果不正确</remarks>
        /// <returns>布尔结果</returns>
        [Description("根据一个UShort返回指定位")]
        public static bool GetBitFromUShort(ushort value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        /// <summary>
        /// 将字节数组转换成布尔数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="length">布尔数组长度</param>
        /// <returns>布尔数组</returns>
        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value,  int length)
        {
            return GetBitArrayFromByteArray(value,0,length);
        }


        /// <summary>
        /// 将字节数组转换成布尔数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">布尔数组长度</param>
        /// <returns>布尔数组</returns>
        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value, int start, int length)
        {
            if (length <= 0) throw new ArgumentException("长度必须为正数");

            if (start < 0) throw new ArgumentException("开始索引必须为非负数");

            if (start + length > value.Length * 8) throw new ArgumentException("数组长度不够或长度太大");

            var bitArr = new BitArray(value);

            var bools = new bool[length];

            for (var i = 0; i < length; i++)
            {
                bools[i] = bitArr[i + start];
            }
            return bools;
        }

        /// <summary>
        /// 将字节数组转换成布尔数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <returns>布尔数组</returns>
        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value)
        {
            return GetBitArrayFromByteArray(value, value.Length * 8);
        }

        /// <summary>
        /// 将一个字节转换成布尔数组
        /// </summary>
        /// <param name="value">字节</param>
        /// <returns>布尔数组</returns>
        [Description("将一个字节转换成布尔数组")]
        public static bool[] GetBitArrayFromByte(byte value)
        {
            return GetBitArrayFromByteArray(new byte[] { value });
        }

        /// <summary>
        /// 根据位开始和长度截取布尔数组
        /// </summary>
        /// <param name="value">布尔数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns>返回布尔数组</returns>
        [Description("根据位开始和长度截取布尔数组")]
        public static bool[] GetBitArrayFromBitArray(bool[] value, int start, int length)
        {
            if (start < 0) throw new ArgumentException("开始索引不能为负数");

            if (length <= 0) throw new ArgumentException("长度必须为正数");

            if (value.Length < (start + length)) throw new ArgumentException("数组长度不够或开始索引太大");

            bool[] result = new bool[length];

            Array.Copy(value, start, result, 0, length);

            return result;
        }

        /// <summary>
        /// 从布尔数组中截取某个布尔
        /// </summary>
        /// <param name="value">布尔数组</param>
        /// <param name="start">开始索引</param>
        /// <returns>返回布尔</returns>
        [Description("从布尔数组中截取某个布尔")]
        public static bool GetBitFromBitArray(bool[] value, int start)
        {
            if (start > value.Length - 1) throw new ArgumentException("布尔数组长度不够或开始索引太大");

            return value[start];
        }


        /// <summary>
        /// 将字符串按照指定的分隔符转换成布尔数组
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <param name="spilt">分割符</param>
        /// <returns>返回布尔数组</returns>
        [Description("将字符串按照指定的分隔符转换成布尔数组")]
        public static bool[] GetBitArrayFromBitArrayString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<bool> result = new List<bool>();

            if (value.Contains(spilt))
            {
                string[] strings = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in strings)
                {
                    result.Add(IsBoolean(item));
                }
            }
            else
            {
                result.Add(IsBoolean(value));
            }

            return result.ToArray();
        }

        /// <summary>
        /// 判断是否为布尔
        /// </summary>
        /// <param name="value">布尔字符串</param>
        /// <returns>布尔结果</returns>
        [Description("判断是否为布尔")]
        public static bool IsBoolean(string value)
        {
            return value == "1" || value.ToLower() == "true";
        }
    }
}
