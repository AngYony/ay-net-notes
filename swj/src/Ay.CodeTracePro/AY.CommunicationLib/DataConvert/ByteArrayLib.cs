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
    /// �ֽ�������������ת����
    /// </summary>
    [Description("�ֽ�������������ת����")]
    public class ByteArrayLib
    {
        /// <summary>
        /// ������ʼ��ַ�ͳ����Զ����ȡ�ֽ�����
        /// </summary>
        /// <param name="data">�ֽ�����</param>
        /// <param name="start">��ʼ�ֽ�</param>
        /// <param name="length">��ȡ����</param>
        /// <returns>�ֽ�����</returns>
        [Description("������ʼ��ַ�ͳ����Զ����ȡ�ֽ�����")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start, int length)
        {
            if (start < 0) throw new ArgumentException("��ʼ��������Ϊ����");

            if (length <= 0) throw new ArgumentException("���ȱ���Ϊ����");

            if (data.Length < (start + length)) throw new ArgumentException("�ֽ����鳤�Ȳ�����ʼ����̫��");

            byte[] result = new byte[length];

            Array.Copy(data, start, result, 0, length);

            return result;
        }


        /// <summary>
        /// ������ʼ��ַ�Զ����ȡ�ֽ�����
        /// </summary>
        /// <param name="data">�ֽ�����</param>
        /// <param name="start">��ʼ�ֽ�</param>
        /// <returns>�ֽ�����</returns>
        [Description("������ʼ��ַ�Զ����ȡ�ֽ�����")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start)
        {
            return GetByteArrayFromByteArray(data, start, data.Length - start);
        }



        /// <summary>
        /// ���ֽ������н�ȡ2���ֽ�,����ָ���ֽ��򷵻�
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">�ֽ�˳��Ĭ��ΪABCD</param>
        /// <returns>�ֽ�����</returns> 
        [Description("���ֽ������н�ȡ2���ֽ�,����ָ���ֽ��򷵻�")]
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
        /// ���ֽ������н�ȡ4���ֽ�,����ָ���ֽ��򷵻�
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">�ֽ�˳��Ĭ��ΪABCD</param>
        /// <returns>�ֽ�����</returns>
        [Description("���ֽ������н�ȡ4���ֽ�,����ָ���ֽ��򷵻�")]
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
        /// ���ֽ������н�ȡ8���ֽ�,����ָ���ֽ��򷵻�
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="dataFormat">�ֽ�˳��Ĭ��ΪABCD</param>
        /// <returns>�ֽ�����</returns>
        [Description("���ֽ������н�ȡ8���ֽ�,����ָ���ֽ��򷵻�")]
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
        /// �Ƚ������ֽ������Ƿ���ȫ��ͬ
        /// </summary>
        /// <param name="value1">�ֽ�����1</param>
        /// <param name="value2">�ֽ�����2</param>
        /// <returns>�Ƿ���ͬ</returns>
        [Description("�Ƚ������ֽ������Ƿ���ȫ��ͬ")]
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
        /// �������ֽ�ת�����ֽ�����
        /// </summary>
        /// <param name="value">�����ֽ�</param>
        /// <returns>�ֽ�����</returns>
        [Description("�������ֽ�ת�����ֽ�����")]
        public static byte[] GetByteArrayFromByte(byte value)
        {
            return new byte[] { value };
        }

        /// <summary>
        /// ��Short������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">Short������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Short������ֵת�����ֽ�����")]
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
        /// ��UShort������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">UShort������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��UShort������ֵת�����ֽ�����")]
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
        /// ��Int������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">Int������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Int������ֵת�����ֽ�����")]
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
        /// ��UInt������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">UInt������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��UInt������ֵת�����ֽ�����")]
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
        /// ��Float��ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">Float������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Float��ֵת�����ֽ�����")]
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
        /// ��Double������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">Double������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Double������ֵת�����ֽ�����")]
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
        /// ��Long������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">Long������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Long������ֵת�����ֽ�����")]
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
        /// ��ULong������ֵת�����ֽ�����
        /// </summary>
        /// <param name="value">ULong������ֵ</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��ULong������ֵת�����ֽ�����")]
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
        /// ��Short����ת�����ֽ�����
        /// </summary>
        /// <param name="value">Short����</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Short����ת�����ֽ�����")]
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
        /// ��UShort����ת�����ֽ�����
        /// </summary>
        /// <param name="value">UShort����</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��UShort����ת�����ֽ�����")]
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
        /// ��Int��������ת�����ֽ�����
        /// </summary>
        /// <param name="value">Int��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Int��������ת�����ֽ�����")]
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
        /// ��UInt��������ת�����ֽ�����
        /// </summary>
        /// <param name="value">UInt��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��UInt��������ת�����ֽ�����")]
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
        /// ��Float��������ת���ֽ�����
        /// </summary>
        /// <param name="value">Float��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Float��������ת���ֽ�����")]
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
        /// ��Double��������ת���ֽ�����
        /// </summary>
        /// <param name="value">Double��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Double��������ת���ֽ�����")]
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
        /// ��Long��������ת�����ֽ�����
        /// </summary>
        /// <param name="value">Long��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Long��������ת�����ֽ�����")]
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
        /// ��ULong��������ת�����ֽ�����
        /// </summary>
        /// <param name="value">ULong��������</param>
        /// <param name="dataFormat">�ֽ�˳��</param>
        /// <returns>�ֽ�����</returns>
        [Description("��ULong��������ת�����ֽ�����")]
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
        /// ��ָ�������ʽ���ַ���ת�����ֽ�����
        /// </summary>
        /// <param name="value">�ַ���</param>
        /// <param name="encoding">�����ʽ</param>
        /// <returns>�ֽ�����</returns>
        [Description("��ָ�������ʽ���ַ���ת�����ֽ�����")]
        public static byte[] GetByteArrayFromString(string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// ��16�����ַ������տո�ָ����ֽ�����
        /// </summary>
        /// <param name="value">16�����ַ���</param>
        /// <param name="spilt">�ָ���</param>
        /// <returns>�ֽ�����</returns>
        [Description("��16�����ַ������տո�ָ����ֽ�����")]
        public static byte[] GetByteArrayFromHexString(string value, string spilt = " ")
        {
            value = value.Trim();//ȥ���ո�

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
                throw new ArgumentNullException("����ת��ʧ�ܣ�"+ex.Message);
            }
        }

        /// <summary>
        /// ��16�����ַ������÷ָ���ת�����ֽ����飨ÿ2���ַ�Ϊ1���ֽڣ�
        /// </summary>
        /// <param name="value">16�����ַ���</param>
        /// <returns>�ֽ�����</returns>
        [Description("��16�����ַ������÷ָ���ת�����ֽ����飨ÿ2���ַ�Ϊ1���ֽڣ�")]
        public static byte[] GetByteArrayFromHexStringWithoutSpilt(string value)
        {
            if (value.Length % 2 != 0) throw new ArgumentNullException("����ַ��������Ƿ�Ϊż��"); 
            
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
                throw new ArgumentNullException("����ת��ʧ�ܣ�" + ex.Message);
            }
        }

        /// <summary>
        /// ��byte����ת����һ��Asii��ʽ�ֽ�����
        /// </summary>
        /// <param name="value">byte����</param>
        /// <returns>�ֽ�����</returns>
        [Description("��byte����ת����һ��Asii��ʽ�ֽ�����")]
        public static byte[] GetAsciiByteArrayFromValue(byte value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X2"));
        }

        /// <summary>
        /// ��short����ת����һ��Ascii��ʽ�ֽ�����
        /// </summary>
        /// <param name="value">short����</param>
        /// <returns>�ֽ�����</returns>
        [Description("��short����ת����һ��Ascii��ʽ�ֽ�����")]
        public static byte[] GetAsciiByteArrayFromValue(short value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// ��ushort����ת����һ��Ascii��ʽ�ֽ�����
        /// </summary>
        /// <param name="value">ushort����</param>
        /// <returns>�ֽ�����</returns>
        [Description("��ushort����ת����һ��Ascii��ʽ�ֽ�����")]
        public static byte[] GetAsciiByteArrayFromValue(ushort value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// ��string����ת����һ��Ascii��ʽ�ֽ�����
        /// </summary>
        /// <param name="value">string����</param>
        /// <returns>�ֽ�����</returns>
        [Description("��string����ת����һ��Ascii��ʽ�ֽ�����")]
        public static byte[] GetAsciiByteArrayFromValue(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// ����������ת�����ֽ�����
        /// </summary>
        /// <param name="data">��������</param>
        /// <returns>�ֽ�����</returns>
        [Description("����������ת�����ֽ�����")]
        public static byte[] GetByteArrayFromBoolArray(bool[] data)
        {

            if (data == null || data.Length == 0)  throw new ArgumentNullException("������鳤���Ƿ���ȷ"); ;

            byte[] result = new byte[data.Length % 8 != 0 ? data.Length / 8 + 1 : data.Length / 8];

            //����ÿ���ֽ�
            for (int i = 0; i < result.Length; i++)
            {
                int total = data.Length < 8 * (i + 1) ? data.Length - 8 * i : 8;

                //������ǰ�ֽڵ�ÿ��λ��ֵ
                for (int j = 0; j < total; j++)
                {
                    result[i] = ByteLib.SetbitValue(result[i], j, data[8 * i + j]);
                }
            }
            return result;
        }

        /// <summary>
        /// ���������ַ���ת�����ֽ�����
        /// </summary>
        /// <param name="value">�������ַ���</param>
        /// <returns>�ֽ�����</returns>
        [Description("���������ַ���ת�����ֽ�����")]
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
        /// ��ŷķ��CIP�ַ���ת�����ֽ�����
        /// </summary>
        /// <param name="data">�������ַ���</param>
        /// <returns>�ֽ�����</returns>
        [Description("��ŷķ��CIP�ַ���ת�����ֽ�����")]
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
        /// ��չΪż�������ֽ�����
        /// </summary>
        /// <param name="data">ԭʼ�ֽ�����</param>
        /// <returns>�����ֽ�����</returns>
        [Description("��չΪż�������ֽ�����")]
        public static byte[] GetEvenByteArray(byte[] data)
        {
            if (data == null) return new byte[0];

            if (data.Length % 2 !=0)
                return GetFixedLengthByteArray(data, data.Length + 1);
            else
                return data;
        }

        /// <summary>
        /// ��չ��ѹ���ֽ����鵽ָ������
        /// </summary>
        /// <param name="data">ԭʼ�ֽ�����</param>
        /// <param name="length">ָ������</param>
        /// <returns>�����ֽ�����</returns>
        [Description("��չ��ѹ���ֽ����鵽ָ������")]
        public static byte[] GetFixedLengthByteArray(byte[] data, int length)
        {
            if (data == null) return new byte[length];

            if (data.Length == length) return data;

            byte[] buffer = new byte[length];

            Array.Copy(data, buffer, Math.Min(data.Length, buffer.Length));

            return buffer;
        }


        /// <summary>
        /// ���ֽ�����ת����Ascii�ֽ�����
        /// </summary>
        /// <param name="value">�ֽ�����</param>
        /// <param name="segment">�ָ���</param>
        /// <returns>ASCII�ֽ�����</returns>
        [Description("���ֽ�����ת����Ascii�ֽ�����")]
        public static byte[] GetAsciiBytesFromByteArray(byte[] value, string segment = "")
        {
            return Encoding.ASCII.GetBytes(StringLib.GetHexStringFromByteArray(value, segment));
        }


        /// <summary>
        /// ��Ascii�ֽ�����ת�����ֽ�����
        /// </summary>
        /// <param name="value">ASCII�ֽ�����</param>
        /// <returns>�ֽ�����</returns>
        [Description("��Ascii�ֽ�����ת�����ֽ�����")]
        public static byte[] GetBytesArrayFromAsciiByteArray(byte[] value)
        {
            return GetByteArrayFromHexStringWithoutSpilt(Encoding.ASCII.GetString(value));
        }



        /// <summary>
        /// ��2���ֽ�������кϲ�
        /// </summary>
        /// <param name="bytes1">�ֽ�����1</param>
        /// <param name="bytes2">�ֽ�����2</param>
        /// <returns>�����ֽ�����</returns>
        [Description("��2���ֽ�������кϲ�")]
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
        /// ��3���ֽ�������кϲ�
        /// </summary>
        /// <param name="bytes1">�ֽ�����1</param>
        /// <param name="bytes2">�ֽ�����2</param>
        /// <param name="bytes3">�ֽ�����3</param>
        /// <returns>�����ֽ�����</returns>
        [Description("��3���ֽ�������кϲ�")]
        public static byte[] GetByteArrayFromThreeByteArray(byte[] bytes1, byte[] bytes2, byte[] bytes3)
        {
            return GetByteArrayFromTwoByteArray(GetByteArrayFromTwoByteArray(bytes1, bytes2), bytes3);
        }

        /// <summary>
        /// ���ֽ������е�ĳ�������޸�
        /// </summary>
        /// <param name="sourceArray">�ֽ�����</param>
        /// <param name="value">���ݣ�ȷ��������</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="offset">ƫ�ƣ��������ַ�����������</param>
        /// <returns>�����ֽ�����</returns>
        [Description("���ֽ������е�ĳ�������޸�")]
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
