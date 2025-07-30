using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Long��������ת����
    /// </summary>
    [Description("Long��������ת����")]
    public class LongLib
    {
        /// <summary>
        /// �ֽ������н�ȡת��64λ����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����һ��Long����</returns>
        [Description("�ֽ������н�ȡת��64λ����")]
        public static long GetLongFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// ���ֽ������н�ȡת��64λ��������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����Long����</returns>
        [Description("���ֽ������н�ȡת��64λ��������")]
        public static long[] GetLongArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 8 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊ8�ı���");

            long[] values = new long[value.Length / 8];

            for (int i = 0; i < value.Length / 8; i++)
            {
                values[i] = GetLongFromByteArray(value, 8 * i, dataFormat);
            }
            return values;
        }

        /// <summary>
        /// ���ַ���תת��64λ��������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="spilt">�ָ���</param>
        /// <returns>����Long����</returns>
        [Description("���ַ���תת��64λ��������")]
        public static long[] GetLongArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<long> result = new List<long>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToInt64(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToInt64(value.Trim()));
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
