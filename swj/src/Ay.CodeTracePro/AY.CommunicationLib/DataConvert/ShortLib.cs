using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Short��������ת����
    /// </summary>
    [Description("Short��������ת����")]
    public class ShortLib
    {
        /// <summary>
        /// �ֽ������н�ȡת��16λ����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����Short���</returns>
        [Description("�ֽ������н�ȡת��16λ����")]
        public static short GetShortFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.Get2BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// ���ֽ������н�ȡת��16λ��������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="type">���ݸ�ʽ</param>
        /// <returns>����Short����</returns>
        [Description("���ֽ������н�ȡת��16λ��������")]
        public static short[] GetShortArrayFromByteArray(byte[] value, DataFormat type = DataFormat.ABCD)
        {
            if (value == null) throw new ArgumentNullException("������鳤���Ƿ�Ϊ��");

            if (value.Length % 2 != 0) throw new ArgumentNullException("������鳤���Ƿ�Ϊż��");

            short[] result = new short[value.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetShortFromByteArray(value, i * 2, type);
            }
            return result;
        }

        /// <summary>
        /// ���ַ���תת��16λ��������
        /// </summary>
        /// <param name="value">��ת���ַ���</param>
        /// <param name="spilt">�ָ��</param>
        /// <returns>����Short����</returns>
        [Description("���ַ���תת��16λ��������")]
        public static short[] GetShortArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<short> result = new List<short>();

            try
            {
                if (value.Contains(spilt))
                {
                    string[] str = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in str)
                    {
                        result.Add(Convert.ToInt16(item.Trim()));
                    }
                }
                else
                {
                    result.Add(Convert.ToInt16(value.Trim()));
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }

        /// <summary>
        /// �����ֽ�����ĳ��λ
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="offset">ĳ��λ</param>
        /// <param name="bitVal">True����False</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����short���</returns>
        [Description("�����ֽ�����ĳ��λ")]
        public static short SetBitValueFrom2ByteArray(byte[] value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (offset >= 0 && offset <= 7)
            {
                value[1] = ByteLib.SetbitValue(value[1], offset, bitVal);
            }
            else
            {
                value[0] = ByteLib.SetbitValue(value[0], offset - 8, bitVal);
            }
            return GetShortFromByteArray(value, 0, dataFormat);
        }

        /// <summary>
        /// ����16λ����ĳ��λ
        /// </summary>
        /// <param name="value">Short����</param>
        /// <param name="offset">ĳ��λ</param>
        /// <param name="bitVal">True����False</param>
        /// <param name="dataFormat">���ݸ�ʽ</param>
        /// <returns>����Short���</returns>
        [Description("����16λ����ĳ��λ")]
        public static short SetBitValueFromShort(short value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] data = ByteArrayLib.GetByteArrayFromShort(value, dataFormat);

            return SetBitValueFrom2ByteArray(data, offset, bitVal, dataFormat);
        }

        /// <summary>
        /// ͨ����������ȡ����
        /// </summary>
        /// <param name="boolLength">��������</param>
        /// <returns>����</returns>
        [Description("ͨ����������ȡ����")]
        public static short GetByteLengthFromBoolLength(int boolLength)
        {
            return boolLength % 8 == 0 ? (short)(boolLength / 8) : (short)(boolLength / 8 + 1);
        }
    }
}
