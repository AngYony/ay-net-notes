using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Int��������ת���࣬���ڽ��ֽ�����ת��Ϊ32λ���ͻ���������
    /// </summary>
    [Description("Int��������ת����")]
    public class IntLib
    {
        /// <summary>
        /// �ֽ������н�ȡת��32λ����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����int����</returns>
        [Description("�ֽ������н�ȡת��32λ����")]
        public static int GetIntFromByteArray(byte[] value, int start = 0, EndianType dataFormat = EndianType.ABCD)
        {
            byte[] data = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// ���ֽ������н�ȡת��32λ��������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����int����</returns>
        [Description("���ֽ������н�ȡת��32λ��������")]
        public static int[] GetIntArrayFromByteArray(byte[] value, EndianType dataFormat = EndianType.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 4 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊ4�ı���");

            int[] values = new int[value.Length / 4];

            for (int i = 0; i < value.Length / 4; i++)
            {
                values[i] = GetIntFromByteArray(value, 4 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// ���ַ���תת��32λ��������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="spilt">�ָ���</param>
        /// <returns>����int����</returns>
        [Description("���ַ���תת��32λ��������")]
        public static int[] GetIntArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<int> result = new List<int>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToInt32(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToInt32(value.Trim()));
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }

        /// <summary>
        /// ͨ����������ȡ����
        /// </summary>
        /// <param name="boolLength">��������</param>
        /// <returns>����</returns>
        [Description("ͨ����������ȡ����")]
        public static int GetByteLengthFromBoolLength(int boolLength)
        {
            return boolLength % 8 == 0 ? boolLength / 8 : boolLength / 8 + 1;
        }
    }
}