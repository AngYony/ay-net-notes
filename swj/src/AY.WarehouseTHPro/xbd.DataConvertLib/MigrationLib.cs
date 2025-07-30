using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// 数值线性转换类
    /// </summary>
    [Description("数值线性转换类")]
    public class MigrationLib
    {
        private static string ByteMax = byte.MaxValue.ToString();
        private static string ByteMin = byte.MinValue.ToString();

        private static string ShortMax = short.MaxValue.ToString();
        private static string ShortMin = ushort.MinValue.ToString();

        private static string UShortMax = ushort.MaxValue.ToString();
        private static string UShortMin = ushort.MinValue.ToString();

        private static string IntMax = int.MaxValue.ToString();
        private static string IntMin = int.MinValue.ToString();

        private static string UIntMax = uint.MaxValue.ToString();
        private static string UIntMin = uint.MinValue.ToString();

        private static string FloatMax = float.MaxValue.ToString();
        private static string FloatMin = float.MinValue.ToString();

        private static string LongMax = long.MaxValue.ToString();
        private static string LongMin = long.MinValue.ToString();

        private static string ULongMax = ulong.MaxValue.ToString();
        private static string ULongMin = ulong.MinValue.ToString();

        private static string DoubleMax = double.MaxValue.ToString();
        private static string DoubleMin = double.MinValue.ToString();

        private static string GetErrorMsg(DataType type)
        {
            string result = string.Empty;

            switch (type)
            {
                case DataType.Byte:
                    result = "设置范围：" + ByteMin + "-" + ByteMax;
                    break;
                case DataType.Short:
                    result = "设置范围：" + ShortMin + "-" + ShortMax;
                    break;
                case DataType.UShort:
                    result = "设置范围：" + UShortMin + "-" + UShortMax;
                    break;
                case DataType.Int:
                    result = "设置范围：" + IntMin + "-" + IntMax;
                    break;
                case DataType.UInt:
                    result = "设置范围：" + UIntMin + "-" + UIntMax;
                    break;
                case DataType.Long:
                    result = "设置范围：" + LongMin + "-" + LongMax;
                    break;
                case DataType.ULong:
                    result = "设置范围：" + ULongMin + "-" + ULongMax;
                    break;
                case DataType.Float:
                    result = "设置范围：" + FloatMin + "-" + FloatMax;
                    break;
                case DataType.Double:
                    result = "设置范围：" + DoubleMin + "-" + DoubleMax;
                    break;
                default:
                    result = "非有效值类型";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取线性转换结果
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="scale">线性系数</param>
        /// <param name="offset">线性偏移</param>
        /// <returns>带操作结果的转换结果</returns>
        [Description("获取线性转换结果")]
        public static OperateResult<object> GetMigrationValue(object value, float scale, float offset)
        {
            if (scale == 1.0 && offset == 0.0)
            {
                return OperateResult.CreateSuccessResult(value);
            }
            else
            {
                object val;
                try
                {
                    string type = value.GetType().Name;
                    switch (type.ToLower())
                    {
                        case "byte":
                        case "int16":
                        case "uint16":
                        case "int32":
                        case "uint32":
                        case "single":
                            val = Convert.ToSingle((Convert.ToSingle(value) * scale +offset).ToString("N4"));
                            break;
                        case "int64":
                        case "uint64":
                        case "double":
                            val = Convert.ToDouble((Convert.ToDouble(value) * scale + offset).ToString("N4"));
                            break;
                        default:
                            val = value;
                            break;
                    }
                    return OperateResult.CreateSuccessResult(val);
                }
                catch (Exception ex)
                {
                    return new OperateResult<object>("转换出错：" + ex.Message);
                }

            }
        }

        /// <summary>
        /// 线性转换后的设定值
        /// </summary>
        /// <param name="set">设定值</param>
        /// <param name="type">数据类型</param>
        /// <param name="scale">线性系数</param>
        /// <param name="offset">线性偏移</param>
        /// <returns>带操作结果的转换结果</returns>
        [Description("线性转换后的设定值")]
        public static OperateResult<string> SetMigrationValue(string set, DataType type, float scale, float offset)
        {
            OperateResult<string> result = new OperateResult<string>(false);
            if (scale == 1.0 && offset == 0.0)
            {
                try
                {
                    switch (type)
                    {
                        case DataType.Byte:
                            result.Content = Convert.ToByte(set).ToString();
                            break;
                        case DataType.Short:
                            result.Content = Convert.ToInt16(set).ToString();
                            break;
                        case DataType.UShort:
                            result.Content = Convert.ToUInt16(set).ToString();
                            break;
                        case DataType.Int:
                            result.Content = Convert.ToInt32(set).ToString();
                            break;
                        case DataType.UInt:
                            result.Content = Convert.ToUInt32(set).ToString();
                            break;
                        case DataType.Long:
                            result.Content = Convert.ToInt64(set).ToString();
                            break;
                        case DataType.ULong:
                            result.Content = Convert.ToUInt64(set).ToString();
                            break;
                        case DataType.Float:
                            result.Content = Convert.ToSingle(set).ToString();
                            break;
                        case DataType.Double:
                            result.Content = Convert.ToDouble(set).ToString();
                            break;
                        default:
                            result.Content = set;
                            break;
                    }
                    result.IsSuccess = true;
                    return result;
                }
                catch (Exception)
                {
                    result.IsSuccess = false;
                    result.Message = "转换出错，" + GetErrorMsg(type);
                    return result;
                }
            }
            else
            {
                try
                {
                    switch (type)
                    {
                        case DataType.Byte:
                            result.Content = Convert.ToByte((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.Short:
                            result.Content = Convert.ToInt16((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.UShort:
                            result.Content = Convert.ToUInt16((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.Int:
                            result.Content = Convert.ToInt32((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.UInt:
                            result.Content = Convert.ToUInt32((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.Long:
                            result.Content = Convert.ToInt64((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.ULong:
                            result.Content = Convert.ToUInt64((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.Float:
                            result.Content = Convert.ToSingle((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        case DataType.Double:
                            result.Content = Convert.ToDouble((Convert.ToSingle(set) - offset) / scale).ToString();
                            break;
                        default:
                            result.Content = set;
                            break;
                    }
                    result.IsSuccess = true;
                    return result;
                }
                catch (Exception)
                {
                    result.IsSuccess = false;
                    result.Message = "转换出错，" + GetErrorMsg(type);
                    return result;
                }
            }
        }

    }
}
