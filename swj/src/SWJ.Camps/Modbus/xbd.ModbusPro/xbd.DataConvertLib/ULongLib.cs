using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// ULong类型数据转换类
    /// </summary>
    [Description("ULong类型数据转换类")]
    public class ULongLib
    {
        /// <summary>
        /// 字节数组中截取转成64位无符号整型
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回一个ULong类型</returns>
        [Description("字节数组中截取转成64位无符号整型")]
        public static ulong GetULongFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt64(data, 0);
        }

        /// <summary>
        /// 将字节数组中截取转成64位无符号整型数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回Long数组</returns>
        [Description("将字节数组中截取转成64位无符号整型数组")]
        public static ulong[] GetULongArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("检查数组长度是否为空");

            if (value.Length % 8 != 0) throw new ArgumentNullException("检查数组长度是否为8的倍数");

            ulong[] values = new ulong[value.Length / 8];

            for (int i = 0; i < value.Length / 8; i++)
            {
                values[i] = GetULongFromByteArray(value, 8 * i, dataFormat);
            }
            return values;
        }

        /// <summary>
        /// 将字符串转转成64位无符号整型数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="spilt">分隔符</param>
        /// <returns>返回ULong数组</returns>
        [Description("将字符串转转成64位无符号整型数组")]
        public static ulong[] GetULongArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<ulong> result = new List<ulong>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToUInt64(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToUInt64(value.Trim()));
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }

        }
    }
}
