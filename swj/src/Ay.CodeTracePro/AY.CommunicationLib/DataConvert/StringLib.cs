using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// �ַ�����������ת���࣬���ڽ��ֽ�����ת��Ϊ�ַ����򽫸�����������ת��Ϊ�ַ���
    /// </summary>
    [Description("�ַ�����������ת����")]
    public class StringLib
    {
        /// <summary>
        /// ���ֽ�����ת�����ַ���
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="count">����</param>
        /// <returns>ת�����</returns>
        [Description("���ֽ�����ת�����ַ���")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value, int start, int count)
        {
            return BitConverter.ToString(value, start, count);
        }

        /// <summary>
        /// ���ֽ�����ת�����ַ���
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <returns>ת�����</returns>
        [Description("���ֽ�����ת�����ַ���")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value)
        {
            return BitConverter.ToString(value, 0, value.Length);
        }

        /// <summary>
        /// ���ֽ�����ת���ɴ������ʽ�ַ���
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="count">����</param>
        /// <param name="encoding">�����ʽ</param>
        /// <returns>ת�����</returns>
        [Description("���ֽ�����ת���ɴ������ʽ�ַ���")]
        public static string GetStringFromByteArrayByEncoding(byte[] value, int start, int count, Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, start, count));
        }

        /// <summary>
        /// ���ֽ�����ת���ɴ������ʽ�ַ���
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="encoding">�����ʽ</param>
        /// <returns>ת�����</returns>
        [Description("���ֽ�����ת���ɴ������ʽ�ַ���")]
        public static string GetStringFromByteArrayByEncoding(byte[] value, Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, 0, value.Length));
        }

        /// <summary>
        /// ������ʼ��ַ�ͳ��Ƚ��ֽ�����ת���ɴ�16�����ַ���
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="count">����</param>
        /// <param name="segment">���ӷ�</param>
        /// <returns>ת�����</returns>
        [Description("������ʼ��ַ�ͳ��Ƚ��ֽ�����ת���ɴ�16�����ַ���")]
        public static string GetHexStringFromByteArray(byte[] value, int start, int count, string segment = " ")
        {
            byte[] b = ByteArrayLib.GetByteArrayFromByteArray(value, start, count);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in b)
            {
                if (segment.Length == 0)
                {
                    stringBuilder.Append(string.Format("{0:X2}", item));
                }
                else
                {
                    stringBuilder.Append(string.Format("{0:X2}{1}", item, segment));
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && (stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment))
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// �������ֽ�����ת���ɴ�16�����ַ���
        /// </summary>
        /// <param name="source">�ֽ�����</param>
        /// <param name="segment">���ӷ�</param>
        /// <returns>ת�����</returns>
        [Description("�������ֽ�����ת���ɴ�16�����ַ���")]
        public static string GetHexStringFromByteArray(byte[] source, string segment = " ")
        {
            return GetHexStringFromByteArray(source, 0, source.Length, segment);
        }

        /// <summary>
        /// ���ֽ�����ת�����������ַ���
        /// </summary>
        /// <param name="source">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="length">����</param>
        /// <param name="emptyStr">���Ϊ�գ���ʾʲô����</param>
        /// <returns>ת�����</returns>
        [Description("���ֽ�����ת�����������ַ���")]
        public static string GetSiemensStringFromByteArray(byte[] data, int start)
        {
            int valid = data[start + 1];
            if (valid > 0)
            {
                return Encoding.GetEncoding("GBK").GetString(ByteArrayLib.GetByteArrayFromByteArray(data, start + 2, valid));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ������ʼ��ַ�ͳ��Ƚ�������������ת�����ַ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="value">����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="length">����</param>
        /// <param name="segment">���ӷ�</param>
        /// <returns>���ؽ��</returns>
        [Description("������ʼ��ַ�ͳ��Ƚ�������������ת�����ַ���")]
        public static string GetStringFromValueArray<T>(T[] value, int start, int length, string segment = " ")
        {
            if (start < 0) throw new ArgumentException("��ʼ��������Ϊ����");

            if (length <= 0) throw new ArgumentException("���ȱ���Ϊ����");

            if (value.Length < (start + length)) throw new ArgumentException("�ֽ����鳤�Ȳ�����ʼ����̫��");

            T[] result = new T[length];

            Array.Copy(value, start, result, 0, length);

            return GetStringFromValueArray(result, segment);
        }

        /// <summary>
        /// ������������ת�����ַ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="value">����</param>
        /// <param name="segment">���ӷ�</param>
        /// <returns>���ؽ��</returns>
        [Description("������������ת�����ַ���")]
        public static string GetStringFromValueArray<T>(T[] value, string segment = " ")
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (value.Length > 0)
            {
                foreach (T item in value)
                {
                    if (segment.Length == 0)
                    {
                        stringBuilder.Append(item.ToString());
                    }
                    else
                    {
                        stringBuilder.Append(item.ToString() + segment.ToString());
                    }
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && (stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment))
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }
    }
}