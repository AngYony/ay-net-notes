using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// Bit��������ת���࣬���ڽ�������������ת��Ϊ����ֵ�򲼶����顣
    /// </summary>
    [Description("Bit��������ת����")]
    public class BitLib
    {
        /// <summary>
        /// ����ĳ���ֽڵ�ָ��λ
        /// </summary>
        /// <param name="value">�ֽ�</param>
        /// <param name="offset">ƫ��λ</param>
        /// <remarks>ƫ��λ0-7��Ч������������ȷ</remarks>
        /// <returns>�������</returns>
        [Description("����ĳ���ֽڵ�ָ��λ")]
        public static bool GetBitFromByte(byte value, int offset)
        {
            return (value & (1 << offset)) != 0;

            //return (data & (byte)Math.Pow(2, offset)) != 0;
        }

        /// <summary>
        /// ��ȡ�ֽ�����(����Ϊ2)�е�ָ��λ
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="offset">ƫ��λ</param>
        /// <param name="isLittleEndian">��С��</param>
        /// <remarks>ƫ��λ0-15��Ч������������ȷ</remarks>
        /// <returns>�������</returns>
        [Description("��ȡ�ֽ�����(����Ϊ2)�е�ָ��λ")]
        public static bool GetBitFrom2Bytes(byte[] value, int offset, bool isLittleEndian = true)
        {
            if (value.Length < 2) throw new ArgumentException("���鳤��С��2");

            if (isLittleEndian)
            {
                return GetBitFrom2Bytes(value[1], value[0], offset);
            }
            else
            {
                return GetBitFrom2Bytes(value[0], value[1], offset);
            }
        }

        /// <summary>
        /// ��ȡ�ߵ��ֽڵ�ָ��λ
        /// </summary>
        /// <param name="high">��λ�ֽ�</param>
        /// <param name="low">��λ�ֽ�</param>
        /// <param name="offset">ƫ��λ</param>
        /// <remarks>ƫ��λ0-15��Ч������������ȷ</remarks>
        /// <returns>�������</returns>
        [Description("��ȡ�ߵ��ֽڵ�ָ��λ")]
        public static bool GetBitFrom2Bytes(byte high, byte low, int offset)
        {
            if (offset >= 0 && offset <= 7)
            {
                return GetBitFromByte(low, offset);
            }
            else
            {
                return GetBitFromByte(high, offset - 8);
            }
        }

        /// <summary>
        /// �����ֽ�������ĳ���ֽڵ�ָ��λ
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">�ֽ�����</param>
        /// <param name="offset">ƫ��λ</param>
        /// <remarks>ƫ��λ0-7��Ч������������ȷ</remarks> 
        /// <returns>�������</returns>
        [Description("�����ֽ�������ĳ���ֽڵ�ָ��λ")]
        public static bool GetBitFromByteArray(byte[] value, int start, int offset)
        {
            if (start > value.Length - 1) throw new ArgumentException("���鳤�Ȳ�����ʼ����̫��");

            return GetBitFromByte(value[start], offset);
        }

        /// <summary>
        /// �����ֽ�������ĳ2���ֽڵ�ָ��λ
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">�ֽ�����</param>
        /// <param name="offset">ƫ��λ</param>
        /// <param name="isLittleEndian">��С��</param>
        /// <remarks>ƫ��λ0-15��Ч������������ȷ</remarks> 
        /// <returns>�������</returns>
        [Description("�����ֽ�������ĳ2���ֽڵ�ָ��λ")]
        public static bool GetBitFrom2BytesArray(byte[] value, int start, int offset, bool isLittleEndian = true)
        {
            if (start > value.Length - 2) throw new ArgumentException("���鳤�Ȳ�����ʼ����̫��");

            byte[] array = new byte[] { value[start], value[start + 1] };

            return GetBitFrom2Bytes(array, offset, isLittleEndian);
        }

        /// <summary>
        /// ����һ��Short����ָ��λ
        /// </summary>
        /// <param name="value">short��ֵ</param>
        /// <param name="offset">ƫ��λ</param>
        /// <param name="isLittleEndian">��С��</param>
        /// <remarks>ƫ��λ0-15��Ч������������ȷ</remarks>
        /// <returns>�������</returns>
        [Description("����һ��Short����ָ��λ")]
        public static bool GetBitFromShort(short value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        /// <summary>
        /// ����һ��UShort����ָ��λ
        /// </summary>
        /// <param name="value">short��ֵ</param>
        /// <param name="offset">ƫ��λ</param>
        /// <param name="isLittleEndian">��С��</param>
        /// <remarks>ƫ��λ0-15��Ч������������ȷ</remarks>
        /// <returns>�������</returns>
        [Description("����һ��UShort����ָ��λ")]
        public static bool GetBitFromUShort(ushort value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        /// <summary>
        /// ���ֽ�����ת���ɲ�������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="length">�������鳤��</param>
        /// <returns>��������</returns>
        [Description("���ֽ�����ת���ɲ�������")]
        public static bool[] GetBitArrayFromByteArray(byte[] value,  int length)
        {
            return GetBitArrayFromByteArray(value,0,length);
        }


        /// <summary>
        /// ���ֽ�����ת���ɲ�������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="length">�������鳤��</param>
        /// <returns>��������</returns>
        [Description("���ֽ�����ת���ɲ�������")]
        public static bool[] GetBitArrayFromByteArray(byte[] value, int start, int length)
        {
            if (length <= 0) throw new ArgumentException("���ȱ���Ϊ����");

            if (start < 0) throw new ArgumentException("��ʼ��������Ϊ�Ǹ���");

            if (start + length > value.Length * 8) throw new ArgumentException("���鳤�Ȳ����򳤶�̫��");

            var bitArr = new BitArray(value);

            var bools = new bool[length];

            for (var i = 0; i < length; i++)
            {
                bools[i] = bitArr[i + start];
            }
            return bools;
        }

        /// <summary>
        /// ���ֽ�����ת���ɲ�������
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <returns>��������</returns>
        [Description("���ֽ�����ת���ɲ�������")]
        public static bool[] GetBitArrayFromByteArray(byte[] value)
        {
            return GetBitArrayFromByteArray(value, value.Length * 8);
        }

        /// <summary>
        /// ��һ���ֽ�ת���ɲ�������
        /// </summary>
        /// <param name="value">�ֽ�</param>
        /// <returns>��������</returns>
        [Description("��һ���ֽ�ת���ɲ�������")]
        public static bool[] GetBitArrayFromByte(byte value)
        {
            return GetBitArrayFromByteArray(new byte[] { value });
        }

        /// <summary>
        /// ����λ��ʼ�ͳ��Ƚ�ȡ��������
        /// </summary>
        /// <param name="value">��������</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="length">����</param>
        /// <returns>���ز�������</returns>
        [Description("����λ��ʼ�ͳ��Ƚ�ȡ��������")]
        public static bool[] GetBitArrayFromBitArray(bool[] value, int start, int length)
        {
            if (start < 0) throw new ArgumentException("��ʼ��������Ϊ����");

            if (length <= 0) throw new ArgumentException("���ȱ���Ϊ����");

            if (value.Length < (start + length)) throw new ArgumentException("���鳤�Ȳ�����ʼ����̫��");

            bool[] result = new bool[length];

            Array.Copy(value, start, result, 0, length);

            return result;
        }

        /// <summary>
        /// �Ӳ��������н�ȡĳ������
        /// </summary>
        /// <param name="value">��������</param>
        /// <param name="start">��ʼ����</param>
        /// <returns>���ز���</returns>
        [Description("�Ӳ��������н�ȡĳ������")]
        public static bool GetBitFromBitArray(bool[] value, int start)
        {
            if (start > value.Length - 1) throw new ArgumentException("�������鳤�Ȳ�����ʼ����̫��");

            return value[start];
        }


        /// <summary>
        /// ���ַ�������ָ���ķָ���ת���ɲ�������
        /// </summary>
        /// <param name="value">��ת���ַ���</param>
        /// <param name="spilt">�ָ��</param>
        /// <returns>���ز�������</returns>
        [Description("���ַ�������ָ���ķָ���ת���ɲ�������")]
        public static bool[] GetBitArrayFromBitArrayString(string value, string spilt = " ")
        {
            value = value.Trim();

            List<bool> result = new List<bool>();

            if (value.Contains(spilt))
            {
                string[] strings = value.Split(new string[] { spilt }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in strings)
                {
                    result.Add(IsBoolean(item));
                }
            }
            else
            {
                result.Add(IsBoolean(value));
            }

            return result.ToArray();
        }

        /// <summary>
        /// �ж��Ƿ�Ϊ����
        /// </summary>
        /// <param name="value">�����ַ���</param>
        /// <returns>�������</returns>
        [Description("�ж��Ƿ�Ϊ����")]
        public static bool IsBoolean(string value)
        {
            return value == "1" || value.ToLower() == "true";
        }
    }
}
