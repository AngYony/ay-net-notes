using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// 字符串类型数据转换类
    /// </summary>
    [Description("字符串类型数据转换类")]
    public class StringLib
    {

        /// <summary>
        /// 将字节数组转换成字符串
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="count">数量</param>
        /// <returns>转换结果</returns>
        [Description("将字节数组转换成字符串")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value, int start, int count)
        {
            return BitConverter.ToString(value, start, count);
        }

        /// <summary>
        /// 将字节数组转换成字符串
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <returns>转换结果</returns>
        [Description("将字节数组转换成字符串")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value)
        {
            return BitConverter.ToString(value, 0, value.Length);
        }

        /// <summary>
        /// 将字节数组转换成带编码格式字符串
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="count">数量</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>转换结果</returns>
        [Description("将字节数组转换成带编码格式字符串")]
        public static string GetStringFromByteArrayByEncoding(byte[] value, int start, int count, Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, start, count));
        }

        /// <summary>
        /// 将字节数组转换成带编码格式字符串
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>转换结果</returns>
        [Description("将字节数组转换成带编码格式字符串")]
        public static string GetStringFromByteArrayByEncoding(byte[] value,  Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, 0, value.Length));
        }

        /// <summary>
        /// 根据起始地址和长度将字节数组转换成带16进制字符串
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="count">数量</param>
        /// <param name="segment">连接符</param>
        /// <returns>转换结果</returns>
        [Description("根据起始地址和长度将字节数组转换成带16进制字符串")]
        public static string GetHexStringFromByteArray(byte[] value, int start, int count, string segment = " ")
        {
            byte[] b = ByteArrayLib.GetByteArrayFromByteArray(value, start, count);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in b)
            {
                if (segment.Length == 0)
                {
                    stringBuilder.Append(string.Format("{0:X2}", item));
                }
                else
                {
                    stringBuilder.Append(string.Format("{0:X2}{1}", item, segment));
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && (stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment))
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将整个字节数组转换成带16进制字符串
        /// </summary>
        /// <param name="source">字节数组</param>
        /// <param name="segment">连接符</param>
        /// <returns>转换结果</returns>
        [Description("将整个字节数组转换成带16进制字符串")]
        public static string GetHexStringFromByteArray(byte[] source, string segment = " ")
        {
            return GetHexStringFromByteArray(source, 0, source.Length, segment);
        }

        /// <summary>
        /// 将字节数组转换成西门子字符串
        /// </summary>
        /// <param name="source">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">长度</param>
        /// <param name="emptyStr">如果为空，显示什么内容</param>
        /// <returns>转换结果</returns>
        [Description("将字节数组转换成西门子字符串")]
        public static string GetSiemensStringFromByteArray(byte[] data, int start)
        {
            int valid = data[start+1];
            if (valid > 0)
            {
                return Encoding.GetEncoding("GBK").GetString(ByteArrayLib.GetByteArrayFromByteArray(data, start+2, valid));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据起始地址和长度将各种类型数组转换成字符串
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">长度</param>
        /// <param name="segment">连接符</param>
        /// <returns>返回结果</returns>
        [Description("根据起始地址和长度将各种类型数组转换成字符串")]
        public static string GetStringFromValueArray<T>(T[] value, int start, int length, string segment = " ")
        {

            if (start < 0) throw new ArgumentException("开始索引不能为负数");

            if (length <= 0) throw new ArgumentException("长度必须为正数");

            if (value.Length < (start + length)) throw new ArgumentException("字节数组长度不够或开始索引太大");

            T[] result = new T[length];

            Array.Copy(value, start, result, 0, length);

            return GetStringFromValueArray(result, segment);

        }

        /// <summary>
        /// 各种类型数组转换成字符串
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">数组</param>
        /// <param name="segment">连接符</param>
        /// <returns>返回结果</returns>
        [Description("各种类型数组转换成字符串")]
        public static string GetStringFromValueArray<T>(T[] value, string segment = " ")
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (value.Length > 0)
            {
                foreach (T item in value)
                {
                    if (segment.Length == 0)
                    {
                        stringBuilder.Append(item.ToString());
                    }
                    else
                    {
                        stringBuilder.Append(item.ToString() + segment.ToString());
                    }
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && (stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment))
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }

    }
}
