using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// Float类型数据转换类
    /// </summary>
    [Description("Float类型数据转换类")]
    public class FloatLib
    {
        /// <summary>
        /// 将字节数组中某4个字节转换成Float类型
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回一个浮点数</returns>
        [Description("将字节数组中某4个字节转换成Float类型")]
        public static float GetFloatFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] b = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// 将字节数组转换成Float数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns>返回浮点数组</returns>
        [Description("将字节数组转换成Float数组")]
        public static float[] GetFloatArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("检查数组长度是否为空");

            if (value.Length % 4 != 0) throw new ArgumentNullException("检查数组长度是否为4的倍数");

            float[] values = new float[value.Length / 4];

            for (int i = 0; i < value.Length / 4; i++)
            {
                values[i] = GetFloatFromByteArray(value, 4 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// 将Float字符串转换成单精度浮点型数组
        /// </summary>
        /// <param name="value">Float字符串</param>
        /// <param name="spilt">分隔符</param>
        /// <returns>单精度浮点型数组</returns>
        [Description("将Float字符串转换成单精度浮点型数组")]
        public static float[] GetFloatArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<float> result = new List<float>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in str)
                    {
                        result.Add(Convert.ToSingle(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToSingle(value.Trim()));
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
