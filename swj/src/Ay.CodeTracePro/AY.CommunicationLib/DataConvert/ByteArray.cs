using System.Collections.Generic;
using System.ComponentModel;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// �ֽڼ�����
    /// </summary>
    public class ByteArray
    {
        #region ��ʼ��

        private List<byte> _list = new List<byte>();

        /// <summary>
        /// ͨ��������ȡֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>�����ֽ�</returns>
        public byte this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        /// <summary>
        /// ���س���
        /// </summary>
        public int Length => _list.Count;

        #endregion ��ʼ��

        #region ��ȡ�ֽ�����

        /// <summary>
        /// ���ԣ������ֽ�����
        /// </summary>
        public byte[] Array
        {
            get { return _list.ToArray(); }
        }

        #endregion ��ȡ�ֽ�����

        #region ��ط���

        /// <summary>
        /// ����ֽ�����
        /// </summary>
        [Description("����ֽ�����")]
        public void Clear()
        {
            _list = new List<byte>();
        }

        /// <summary>
        /// ���һ���ֽ�
        /// </summary>
        /// <param name="item">�ֽ�</param>
        [Description("���һ���ֽ�")]
        public void Add(byte item)
        {
            Add(new byte[] { item });
        }

        /// <summary>
        /// ���һ���ֽ�����
        /// </summary>
        /// <param name="items">�ֽ�����</param>
        [Description("���һ���ֽ�����")]
        public void Add(byte[] items)
        {
            _list.AddRange(items);
        }

        /// <summary>
        /// ��Ӷ����ֽ�
        /// </summary>
        /// <param name="item1">�ֽ�1</param>
        /// <param name="item2">�ֽ�2</param>
        [Description("��Ӷ����ֽ�")]
        public void Add(byte item1, byte item2)
        {
            Add(new byte[] { item1, item2 });
        }

        /// <summary>
        /// ��������ֽ�
        /// </summary>
        /// <param name="item1">�ֽ�1</param>
        /// <param name="item2">�ֽ�2</param>
        /// <param name="item3">�ֽ�3</param>
        [Description("��������ֽ�")]
        public void Add(byte item1, byte item2, byte item3)
        {
            Add(new byte[] { item1, item2, item3 });
        }

        /// <summary>
        /// ����ĸ��ֽ�
        /// </summary>
        /// <param name="item1">�ֽ�1</param>
        /// <param name="item2">�ֽ�2</param>
        /// <param name="item3">�ֽ�3</param>
        /// <param name="item4">�ֽ�4</param>
        [Description("����ĸ��ֽ�")]
        public void Add(byte item1, byte item2, byte item3, byte item4)
        {
            Add(new byte[] { item1, item2, item3, item4 });
        }

        /// <summary>
        /// �������ֽ�
        /// </summary>
        /// <param name="item1">�ֽ�1</param>
        /// <param name="item2">�ֽ�2</param>
        /// <param name="item3">�ֽ�3</param>
        /// <param name="item4">�ֽ�4</param>
        /// <param name="item5">�ֽ�5</param>
        [Description("�������ֽ�")]
        public void Add(byte item1, byte item2, byte item3, byte item4, byte item5)
        {
            Add(new byte[] { item1, item2, item3, item4, item5 });
        }

        /// <summary>
        /// ���һ��ByteArray����
        /// </summary>
        /// <param name="byteArray">ByteArray����</param>
        [Description("���һ��ByteArray����")]
        public void Add(ByteArray byteArray)
        {
            Add(byteArray.Array);
        }

        /// <summary>
        /// ���һ��ushort������ֵ
        /// </summary>
        /// <param name="value">ushort������ֵ</param>
        [Description("���һ��ushort������ֵ")]
        public void Add(ushort value)
        {
            _list.Add((byte)(value >> 8));
            _list.Add((byte)value);
        }

        /// <summary>
        /// ���һ��short������ֵ
        /// </summary>
        /// <param name="value">short������ֵ</param>
        [Description("���һ��short������ֵ")]
        public void Add(short value)
        {
            _list.Add((byte)(value >> 8));
            _list.Add((byte)value);
        }

        #endregion ��ط���
    }
}