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
    /// 字节数组类型数据转换类
    /// </summary>
    [Description("字节数组类型数据转换类")]
    public class ByteArrayLib
    {
        /// <summary>
        /// 根据起始地址和长度自定义截取字节数组
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="start">开始字节</param>
        /// <param name="length">截取长度</param>
        /// <returns>字节数组</returns>
        [Description("根据起始地址和长度自定义截取字节数组")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start, int length)
        {
            if (start < 0) throw new ArgumentException("开始索引不能为负数");

            if (length <= 0) throw new ArgumentException("长度必须为正数");

            if (data.Length < (start + length)) throw new ArgumentException("字节数组长度不够或开始索引太大");

            byte[] result = new byte[length];

            Array.Copy(data, start, result, 0, length);

            return result;
        }


        /// <summary>
        /// 根据起始地址自定义截取字节数组
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="start">开始字节</param>
        /// <returns>字节数组</returns>
        [Description("根据起始地址自定义截取字节数组")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start)
        {
            return GetByteArrayFromByteArray(data, start, data.Length - start);
        }



        /// <summary>
        /// 从字节数组中截取2个字节,并按指定字节序返回
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">字节顺序，默认为ABCD</param>
        /// <returns>字节数组</returns> 
        [Description("从字节数组中截取2个字节,并按指定字节序返回")]
        public static byte[] Get2BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] res = GetByteArrayFromByteArray(value, start, 2);

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    return res.Reverse().ToArray();
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    return res;
            }
            return res;
        }


        /// <summary>
        /// 从字节数组中截取4个字节,并按指定字节序返回
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">字节顺序，默认为ABCD</param>
        /// <returns>字节数组</returns>
        [Description("从字节数组中截取4个字节,并按指定字节序返回")]
        public static byte[] Get4BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = GetByteArrayFromByteArray(value, start, 4);

            byte[] res = new byte[4];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[3];
                    res[1] = resTemp[2];
                    res[2] = resTemp[1];
                    res[3] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[2];
                    res[1] = resTemp[3];
                    res[2] = resTemp[0];
                    res[3] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;

        }

        /// <summary>
        /// 从字节数组中截取8个字节,并按指定字节序返回
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="start">开始索引</param>
        /// <param name="dataFormat">字节顺序，默认为ABCD</param>
        /// <returns>字节数组</returns>
        [Description("从字节数组中截取8个字节,并按指定字节序返回")]
        public static byte[] Get8BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] res = new byte[8];

            byte[] resTemp = GetByteArrayFromByteArray(value, start, 8);

            if (resTemp == null) return null;

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[7];
                    res[1] = resTemp[6];
                    res[2] = resTemp[5];
                    res[3] = resTemp[4];
                    res[4] = resTemp[3];
                    res[5] = resTemp[2];
                    res[6] = resTemp[1];
                    res[7] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    res[4] = resTemp[5];
                    res[5] = resTemp[4];
                    res[6] = resTemp[7];
                    res[7] = resTemp[6];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[6];
                    res[1] = resTemp[7];
                    res[2] = resTemp[4];
                    res[3] = resTemp[5];
                    res[4] = resTemp[2];
                    res[5] = resTemp[3];
                    res[6] = resTemp[0];
                    res[7] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 比较两个字节数组是否完全相同
        /// </summary>
        /// <param name="value1">字节数组1</param>
        /// <param name="value2">字节数组2</param>
        /// <returns>是否相同</returns>
        [Description("比较两个字节数组是否完全相同")]
        public static bool GetByteArrayEquals(byte[] value1, byte[] value2)
        {
            if (value1 == null || value2 == null) return false;
            if (value1.Length != value2.Length) return false;
            for (int i = 0; i < value1.Length; i++)
            {
                if (value1[i] != value2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 将单个字节转换成字节数组
        /// </summary>
        /// <param name="value">单个字节</param>
        /// <returns>字节数组</returns>
        [Description("将单个字节转换成字节数组")]
        public static byte[] GetByteArrayFromByte(byte value)
        {
            return new byte[] { value };
        }

        /// <summary>
        /// 将Short类型数值转换成字节数组
        /// </summary>
        /// <param name="value">Short类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Short类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromShort(short value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[2];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    break;
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将UShort类型数值转换成字节数组
        /// </summary>
        /// <param name="value">UShort类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将UShort类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromUShort(ushort value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[2];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    break;
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将Int类型数值转换成字节数组
        /// </summary>
        /// <param name="value">Int类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Int类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromInt(int value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[4];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[3];
                    res[1] = resTemp[2];
                    res[2] = resTemp[1];
                    res[3] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[2];
                    res[1] = resTemp[3];
                    res[2] = resTemp[0];
                    res[3] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }
        /// <summary>
        /// 将UInt类型数值转换成字节数组
        /// </summary>
        /// <param name="value">UInt类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将UInt类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromUInt(uint value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[4];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[3];
                    res[1] = resTemp[2];
                    res[2] = resTemp[1];
                    res[3] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[2];
                    res[1] = resTemp[3];
                    res[2] = resTemp[0];
                    res[3] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将Float数值转换成字节数组
        /// </summary>
        /// <param name="value">Float类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Float数值转换成字节数组")]
        public static byte[] GetByteArrayFromFloat(float value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[4];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[3];
                    res[1] = resTemp[2];
                    res[2] = resTemp[1];
                    res[3] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[2];
                    res[1] = resTemp[3];
                    res[2] = resTemp[0];
                    res[3] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }
        /// <summary>
        /// 将Double类型数值转换成字节数组
        /// </summary>
        /// <param name="value">Double类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Double类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromDouble(double value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[8];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[7];
                    res[1] = resTemp[6];
                    res[2] = resTemp[5];
                    res[3] = resTemp[4];
                    res[4] = resTemp[3];
                    res[5] = resTemp[2];
                    res[6] = resTemp[1];
                    res[7] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    res[4] = resTemp[5];
                    res[5] = resTemp[4];
                    res[6] = resTemp[7];
                    res[7] = resTemp[6];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[6];
                    res[1] = resTemp[7];
                    res[2] = resTemp[4];
                    res[3] = resTemp[5];
                    res[4] = resTemp[2];
                    res[5] = resTemp[3];
                    res[6] = resTemp[0];
                    res[7] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将Long类型数值转换成字节数组
        /// </summary>
        /// <param name="value">Long类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Long类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromLong(long value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[8];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[7];
                    res[1] = resTemp[6];
                    res[2] = resTemp[5];
                    res[3] = resTemp[4];
                    res[4] = resTemp[3];
                    res[5] = resTemp[2];
                    res[6] = resTemp[1];
                    res[7] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    res[4] = resTemp[5];
                    res[5] = resTemp[4];
                    res[6] = resTemp[7];
                    res[7] = resTemp[6];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[6];
                    res[1] = resTemp[7];
                    res[2] = resTemp[4];
                    res[3] = resTemp[5];
                    res[4] = resTemp[2];
                    res[5] = resTemp[3];
                    res[6] = resTemp[0];
                    res[7] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将ULong类型数值转换成字节数组
        /// </summary>
        /// <param name="value">ULong类型数值</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将ULong类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromULong(ulong value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] resTemp = BitConverter.GetBytes(value);

            byte[] res = new byte[8];

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    res[0] = resTemp[7];
                    res[1] = resTemp[6];
                    res[2] = resTemp[5];
                    res[3] = resTemp[4];
                    res[4] = resTemp[3];
                    res[5] = resTemp[2];
                    res[6] = resTemp[1];
                    res[7] = resTemp[0];
                    break;
                case DataFormat.CDAB:
                    res[0] = resTemp[1];
                    res[1] = resTemp[0];
                    res[2] = resTemp[3];
                    res[3] = resTemp[2];
                    res[4] = resTemp[5];
                    res[5] = resTemp[4];
                    res[6] = resTemp[7];
                    res[7] = resTemp[6];
                    break;
                case DataFormat.BADC:
                    res[0] = resTemp[6];
                    res[1] = resTemp[7];
                    res[2] = resTemp[4];
                    res[3] = resTemp[5];
                    res[4] = resTemp[2];
                    res[5] = resTemp[3];
                    res[6] = resTemp[0];
                    res[7] = resTemp[1];
                    break;
                case DataFormat.DCBA:
                    res = resTemp;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 将Short数组转换成字节数组
        /// </summary>
        /// <param name="value">Short数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Short数组转换成字节数组")]
        public static byte[] GetByteArrayFromShortArray(short[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromShort(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将UShort数组转换成字节数组
        /// </summary>
        /// <param name="value">UShort数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将UShort数组转换成字节数组")]
        public static byte[] GetByteArrayFromUShortArray(ushort[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromUShort(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将Int类型数组转换成字节数组
        /// </summary>
        /// <param name="value">Int类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Int类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromIntArray(int[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromInt(item, dataFormat));
            }
            return array.array;
        }
        /// <summary>
        /// 将UInt类型数组转换成字节数组
        /// </summary>
        /// <param name="value">UInt类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将UInt类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromUIntArray(uint[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromUInt(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将Float类型数组转成字节数组
        /// </summary>
        /// <param name="value">Float类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Float类型数组转成字节数组")]
        public static byte[] GetByteArrayFromFloatArray(float[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromFloat(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将Double类型数组转成字节数组
        /// </summary>
        /// <param name="value">Double类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Double类型数组转成字节数组")]
        public static byte[] GetByteArrayFromDoubleArray(double[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromDouble(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将Long类型数组转换成字节数组
        /// </summary>
        /// <param name="value">Long类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将Long类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromLongArray(long[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromDouble(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将ULong类型数组转换成字节数组
        /// </summary>
        /// <param name="value">ULong类型数组</param>
        /// <param name="dataFormat">字节顺序</param>
        /// <returns>字节数组</returns>
        [Description("将ULong类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromULongArray(ulong[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray array = new ByteArray();

            foreach (var item in value)
            {
                array.Add(GetByteArrayFromULong(item, dataFormat));
            }
            return array.array;
        }

        /// <summary>
        /// 将指定编码格式的字符串转换成字节数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>字节数组</returns>
        [Description("将指定编码格式的字符串转换成字节数组")]
        public static byte[] GetByteArrayFromString(string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// 将16进制字符串按照空格分隔成字节数组
        /// </summary>
        /// <param name="value">16进制字符串</param>
        /// <param name="spilt">分隔符</param>
        /// <returns>字节数组</returns>
        [Description("将16进制字符串按照空格分隔成字节数组")]
        public static byte[] GetByteArrayFromHexString(string value, string spilt = " ")
        {
            value = value.Trim();//去除空格

            List<byte> result = new List<byte>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToByte(item.Trim(), 16));
                    }
                }
                else
                {
                    result.Add(Convert.ToByte(value.Trim(), 16));
                }
                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败："+ex.Message);
            }
        }

        /// <summary>
        /// 将16进制字符串不用分隔符转换成字节数组（每2个字符为1个字节）
        /// </summary>
        /// <param name="value">16进制字符串</param>
        /// <returns>字节数组</returns>
        [Description("将16进制字符串不用分隔符转换成字节数组（每2个字符为1个字节）")]
        public static byte[] GetByteArrayFromHexStringWithoutSpilt(string value)
        {
            if (value.Length % 2 != 0) throw new ArgumentNullException("检查字符串长度是否为偶数"); 
            
            List<byte> result = new List<byte>();
            try
            {
                for (int i = 0; i < value.Length; i += 2)
                {
                    string temp = value.Substring(i, 2);

                    result.Add(Convert.ToByte(temp, 16));
                }
                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 将byte数据转换成一个Asii格式字节数组
        /// </summary>
        /// <param name="value">byte数据</param>
        /// <returns>字节数组</returns>
        [Description("将byte数据转换成一个Asii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(byte value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X2"));
        }

        /// <summary>
        /// 将short数据转换成一个Ascii格式字节数组
        /// </summary>
        /// <param name="value">short数据</param>
        /// <returns>字节数组</returns>
        [Description("将short数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(short value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// 将ushort数据转换成一个Ascii格式字节数组
        /// </summary>
        /// <param name="value">ushort数据</param>
        /// <returns>字节数组</returns>
        [Description("将ushort数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(ushort value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// 将string数据转换成一个Ascii格式字节数组
        /// </summary>
        /// <param name="value">string数据</param>
        /// <returns>字节数组</returns>
        [Description("将string数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// 将布尔数组转换成字节数组
        /// </summary>
        /// <param name="data">布尔数组</param>
        /// <returns>字节数组</returns>
        [Description("将布尔数组转换成字节数组")]
        public static byte[] GetByteArrayFromBoolArray(bool[] data)
        {

            if (data == null || data.Length == 0)  throw new ArgumentNullException("检查数组长度是否正确"); ;

            byte[] result = new byte[data.Length % 8 != 0 ? data.Length / 8 + 1 : data.Length / 8];

            //遍历每个字节
            for (int i = 0; i < result.Length; i++)
            {
                int total = data.Length < 8 * (i + 1) ? data.Length - 8 * i : 8;

                //遍历当前字节的每个位赋值
                for (int j = 0; j < total; j++)
                {
                    result[i] = ByteLib.SetbitValue(result[i], j, data[8 * i + j]);
                }
            }
            return result;
        }

        /// <summary>
        /// 将西门子字符串转换成字节数组
        /// </summary>
        /// <param name="value">西门子字符串</param>
        /// <returns>字节数组</returns>
        [Description("将西门子字符串转换成字节数组")]
        public static byte[] GetByteArrayFromSiemensString(string value)
        {
            byte[] data = GetByteArrayFromString(value, Encoding.GetEncoding("GBK"));
            byte[] result = new byte[data.Length + 2];
            result[0] = (byte)(data.Length + 2);
            result[1] = (byte)data.Length;
            Array.Copy(data, 0, result, 2, data.Length);
            return result;
        }

        /// <summary>
        /// 将欧姆龙CIP字符串转换成字节数组
        /// </summary>
        /// <param name="data">西门子字符串</param>
        /// <returns>字节数组</returns>
        [Description("将欧姆龙CIP字符串转换成字节数组")]
        public static byte[] GetByteArrayFromOmronCIPString(string data)
        {
            byte[] b = GetByteArrayFromString(data, Encoding.ASCII);

            byte[] res = GetEvenByteArray(b);

            byte[] array = new byte[res.Length + 2];
            array[0] = BitConverter.GetBytes(array.Length - 2)[0];
            array[1] = BitConverter.GetBytes(array.Length - 2)[1];
            Array.Copy(res, 0, array, 2, res.Length);
            return array;
        }

        /// <summary>
        /// 扩展为偶数长度字节数组
        /// </summary>
        /// <param name="data">原始字节数据</param>
        /// <returns>返回字节数组</returns>
        [Description("扩展为偶数长度字节数组")]
        public static byte[] GetEvenByteArray(byte[] data)
        {
            if (data == null) return new byte[0];

            if (data.Length % 2 !=0)
                return GetFixedLengthByteArray(data, data.Length + 1);
            else
                return data;
        }

        /// <summary>
        /// 扩展或压缩字节数组到指定数量
        /// </summary>
        /// <param name="data">原始字节数据</param>
        /// <param name="length">指定长度</param>
        /// <returns>返回字节数组</returns>
        [Description("扩展或压缩字节数组到指定数量")]
        public static byte[] GetFixedLengthByteArray(byte[] data, int length)
        {
            if (data == null) return new byte[length];

            if (data.Length == length) return data;

            byte[] buffer = new byte[length];

            Array.Copy(data, buffer, Math.Min(data.Length, buffer.Length));

            return buffer;
        }


        /// <summary>
        /// 将字节数组转换成Ascii字节数组
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="segment">分隔符</param>
        /// <returns>ASCII字节数组</returns>
        [Description("将字节数组转换成Ascii字节数组")]
        public static byte[] GetAsciiBytesFromByteArray(byte[] value, string segment = "")
        {
            return Encoding.ASCII.GetBytes(StringLib.GetHexStringFromByteArray(value, segment));
        }


        /// <summary>
        /// 将Ascii字节数组转换成字节数组
        /// </summary>
        /// <param name="value">ASCII字节数组</param>
        /// <returns>字节数组</returns>
        [Description("将Ascii字节数组转换成字节数组")]
        public static byte[] GetBytesArrayFromAsciiByteArray(byte[] value)
        {
            return GetByteArrayFromHexStringWithoutSpilt(Encoding.ASCII.GetString(value));
        }



        /// <summary>
        /// 将2个字节数组进行合并
        /// </summary>
        /// <param name="bytes1">字节数组1</param>
        /// <param name="bytes2">字节数组2</param>
        /// <returns>返回字节数组</returns>
        [Description("将2个字节数组进行合并")]
        public static byte[] GetByteArrayFromTwoByteArray(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null && bytes2 == null) return null;
            if (bytes1 == null) return bytes2;
            if (bytes2 == null) return bytes1;

            byte[] buffer = new byte[bytes1.Length + bytes2.Length];
            bytes1.CopyTo(buffer, 0);
            bytes2.CopyTo(buffer, bytes1.Length);
            return buffer;
        }

        /// <summary>
        /// 将3个字节数组进行合并
        /// </summary>
        /// <param name="bytes1">字节数组1</param>
        /// <param name="bytes2">字节数组2</param>
        /// <param name="bytes3">字节数组3</param>
        /// <returns>返回字节数组</returns>
        [Description("将3个字节数组进行合并")]
        public static byte[] GetByteArrayFromThreeByteArray(byte[] bytes1, byte[] bytes2, byte[] bytes3)
        {
            return GetByteArrayFromTwoByteArray(GetByteArrayFromTwoByteArray(bytes1, bytes2), bytes3);
        }

        /// <summary>
        /// 将字节数组中的某个数据修改
        /// </summary>
        /// <param name="sourceArray">字节数组</param>
        /// <param name="value">数据，确定好类型</param>
        /// <param name="start">开始索引</param>
        /// <param name="offset">偏移，布尔及字符串才起作用</param>
        /// <returns>返回字节数组</returns>
        [Description("将字节数组中的某个数据修改")]
        public static byte[] SetByteArray(byte[] sourceArray, object value, int start, int offset)
        {
            string name = value.GetType().Name;
            byte[] b = null;
            switch (name.ToLower())
            {
                case "boolean":
                    Array.Copy(GetByteArrayFromByte(ByteLib.SetbitValue(sourceArray[start], offset, Convert.ToBoolean(value))), 0, sourceArray, start, 1);
                    break;
                case "byte":
                    Array.Copy(GetByteArrayFromByte(Convert.ToByte(value)), 0, sourceArray, start, 1);
                    break;
                case "int16":
                    Array.Copy(GetByteArrayFromShort(Convert.ToInt16(value)), 0, sourceArray, start, 2);
                    break;
                case "uint16":
                    Array.Copy(GetByteArrayFromUShort(Convert.ToUInt16(value)), 0, sourceArray, start, 2);
                    break;
                case "int32":
                    Array.Copy(GetByteArrayFromInt(Convert.ToInt32(value)), 0, sourceArray, start, 4);
                    break;
                case "uint32":
                    Array.Copy(GetByteArrayFromUInt(Convert.ToUInt32(value)), 0, sourceArray, start, 4);
                    break;
                case "single":
                    Array.Copy(GetByteArrayFromFloat(Convert.ToSingle(value)), 0, sourceArray, start, 4);
                    break;
                case "double":
                    Array.Copy(GetByteArrayFromDouble(Convert.ToDouble(value)), 0, sourceArray, start, 8);
                    break;
                case "int64":
                    Array.Copy(GetByteArrayFromLong(Convert.ToInt64(value)), 0, sourceArray, start, 8);
                    break;
                case "uint64":
                    Array.Copy(GetByteArrayFromULong(Convert.ToUInt64(value)), 0, sourceArray, start, 8);
                    break;
                case "byte[]":
                    b = GetByteArrayFromHexString(value.ToString());
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "int16[]":
                    b = GetByteArrayFromShortArray(ShortLib.GetShortArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "uint16[]":
                    b = GetByteArrayFromUShortArray(UShortLib.GetUShortArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "int32[]":
                    b = GetByteArrayFromIntArray(IntLib.GetIntArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "uint32[]":
                    b = GetByteArrayFromUIntArray(UIntLib.GetUIntArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "single[]":
                    b = GetByteArrayFromFloatArray(FloatLib.GetFloatArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "double[]":
                    b = GetByteArrayFromDoubleArray(DoubleLib.GetDoubleArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "int64[]":
                    b = GetByteArrayFromLongArray(LongLib.GetLongArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                case "uint64[]":
                    b = GetByteArrayFromULongArray(ULongLib.GetULongArrayFromString(value.ToString()));
                    Array.Copy(b, 0, sourceArray, start, b.Length);
                    break;
                default:
                    break;
            }

            return sourceArray;

        }
    }
}
