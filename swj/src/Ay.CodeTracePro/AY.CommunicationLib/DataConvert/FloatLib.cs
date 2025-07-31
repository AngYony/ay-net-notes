using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Float��������ת����
    /// </summary>
    [Description("Float��������ת����")]
    public class FloatLib
    {
        /// <summary>
        /// ���ֽ�������ĳ4���ֽ�ת����Float����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����һ��������</returns>
        [Description("���ֽ�������ĳ4���ֽ�ת����Float����")]
        public static float GetFloatFromByteArray(byte[] value, int start = 0, EndianType dataFormat = EndianType.ABCD)
        {
            byte[] b = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// ���ֽ�����ת����Float����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>���ظ�������</returns>
        [Description("���ֽ�����ת����Float����")]
        public static float[] GetFloatArrayFromByteArray(byte[] value, EndianType dataFormat = EndianType.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 4 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊ4�ı���");

            float[] values = new float[value.Length / 4];

            for (int i = 0; i < value.Length / 4; i++)
            {
                values[i] = GetFloatFromByteArray(value, 4 * i, dataFormat);
            }

            return values;
        }

        /// <summary>
        /// ��Float�ַ���ת���ɵ����ȸ���������
        /// </summary>
        /// <param name="value">Float�ַ���</param>
        /// <param name="spilt">�ָ���</param>
        /// <returns>�����ȸ���������</returns>
        [Description("��Float�ַ���ת���ɵ����ȸ���������")]
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
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }
    }
}
