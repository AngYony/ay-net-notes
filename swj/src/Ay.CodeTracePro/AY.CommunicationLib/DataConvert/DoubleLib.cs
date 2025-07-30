using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Double��������ת����
    /// </summary>
    [Description("Double��������ת����")]
    public class DoubleLib
    {
        /// <summary>
        /// ���ֽ�������ĳ8���ֽ�ת����Double����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼλ��</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>Double������ֵ</returns>
        [Description("���ֽ�������ĳ8���ֽ�ת����Double����")]
        public static double GetDoubleFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToDouble(data, 0);
        }

        /// <summary>
        /// ���ֽ�����ת����Double����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>Double����</returns>
        [Description("���ֽ�����ת����Double����")]
        public static double[] GetDoubleArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 8 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊ8�ı���");

            double[] values = new double[value.Length / 8];

            for (int i = 0; i < value.Length / 8; i++)
            {
                values[i] = GetDoubleFromByteArray(value, 8 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// ��Double�ַ���ת����˫���ȸ���������
        /// </summary>
        /// <param name="value">Double�ַ���</param>
        /// <param name="spilt">�ָ��</param>
        /// <returns>˫���ȸ���������</returns>
        [Description("��Double�ַ���ת����˫���ȸ���������")]
        public static double[] GetDoubleArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<double> result = new List<double>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToDouble(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToDouble(value.Trim()));
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }
    }
}
