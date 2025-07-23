using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// UInt类型数据转换类
    /// </summary>
    [Description("UInt类型数据转换类")]
    public class UIntLib
    {
        /// <summary>
        /// 字节数组中截取转成32位无符号整型
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回UInt类型</returns>
        [Description("字节数组中截取转成32位无符号整型")]
        public static uint GetUIntFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// 将字节数组中截取转成32位无符号整型数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回int数组</returns>
        [Description("将字节数组中截取转成32位无符号整型数组")]
        public static uint[] GetUIntArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("检查数组长度是否为空");

            if (value.Length % 4 != 0) throw new ArgumentNullException("检查数组长度是否为4的倍数");

            uint[] values = new uint[value.Length / 4];

            for (int i = 0; i < value.Length / 4; i++)
            {
                values[i] = GetUIntFromByteArray(value, 4 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// 将字符串转转成32位无符号整型数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="spilt">分隔符</param>
        /// <returns>返回UInt数组</returns>
        [Description("将字符串转转成32位无符号整型数组")]
        public static uint[] GetUIntArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<uint> result = new List<uint>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToUInt32(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToUInt32(value.Trim()));
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 通过布尔长度取整数
        /// </summary>
        /// <param name="boolLength">布尔长度</param>
        /// <returns>整数</returns>
        [Description("通过布尔长度取整数")]
        public static uint GetByteLengthFromBoolLength(int boolLength)
        {
            return boolLength % 8 == 0 ? (uint)(boolLength / 8) : (uint)(boolLength / 8 + 1);
        }
    }
}
