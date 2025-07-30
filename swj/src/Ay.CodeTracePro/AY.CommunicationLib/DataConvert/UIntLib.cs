
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// UInt��������ת����
    /// </summary>
    [Description("UInt��������ת����")]
    public class UIntLib
    {
        /// <summary>
        /// �ֽ������н�ȡת��32λ�޷�������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����UInt����</returns>
        [Description("�ֽ������н�ȡת��32λ�޷�������")]
        public static uint GetUIntFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// ���ֽ������н�ȡת��32λ�޷�����������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����int����</returns>
        [Description("���ֽ������н�ȡת��32λ�޷�����������")]
        public static uint[] GetUIntArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 4 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊ4�ı���");

            uint[] values = new uint[value.Length / 4];

            for (int i = 0; i < value.Length / 4; i++)
            {
                values[i] = GetUIntFromByteArray(value, 4 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// ���ַ���תת��32λ�޷�����������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="spilt">�ָ���</param>
        /// <returns>����UInt����</returns>
        [Description("���ַ���תת��32λ�޷�����������")]
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
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }

        /// <summary>
        /// ͨ����������ȡ����
        /// </summary>
        /// <param name="boolLength">��������</param>
        /// <returns>����</returns>
        [Description("ͨ����������ȡ����")]
        public static uint GetByteLengthFromBoolLength(int boolLength)
        {
            return boolLength % 8 == 0 ? (uint)(boolLength / 8) : (uint)(boolLength / 8 + 1);
        }
    }
}
