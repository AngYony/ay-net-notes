using System.Collections.Generic;
using System.ComponentModel;

namespace AY.CommunicationLib.DataConvert
{
    /// <summary>
    /// 字节集合类
    /// </summary>
    public class ByteArray
    {
        #region 初始化

        private List<byte> _list = new List<byte>();

        /// <summary>
        /// 通过索引获取值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回字节</returns>
        public byte this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        /// <summary>
        /// 返回长度
        /// </summary>
        public int Length => _list.Count;

        #endregion 初始化

        #region 获取字节数组

        /// <summary>
        /// 属性，返回字节数组
        /// </summary>
        public byte[] Array
        {
            get { return _list.ToArray(); }
        }

        #endregion 获取字节数组

        #region 相关方法

        /// <summary>
        /// 清空字节数组
        /// </summary>
        [Description("清空字节数组")]
        public void Clear()
        {
            _list = new List<byte>();
        }

        /// <summary>
        /// 添加一个字节
        /// </summary>
        /// <param name="item">字节</param>
        [Description("添加一个字节")]
        public void Add(byte item)
        {
            Add(new byte[] { item });
        }

        /// <summary>
        /// 添加一个字节数组
        /// </summary>
        /// <param name="items">字节数组</param>
        [Description("添加一个字节数组")]
        public void Add(byte[] items)
        {
            _list.AddRange(items);
        }

        /// <summary>
        /// 添加二个字节
        /// </summary>
        /// <param name="item1">字节1</param>
        /// <param name="item2">字节2</param>
        [Description("添加二个字节")]
        public void Add(byte item1, byte item2)
        {
            Add(new byte[] { item1, item2 });
        }

        /// <summary>
        /// 添加三个字节
        /// </summary>
        /// <param name="item1">字节1</param>
        /// <param name="item2">字节2</param>
        /// <param name="item3">字节3</param>
        [Description("添加三个字节")]
        public void Add(byte item1, byte item2, byte item3)
        {
            Add(new byte[] { item1, item2, item3 });
        }

        /// <summary>
        /// 添加四个字节
        /// </summary>
        /// <param name="item1">字节1</param>
        /// <param name="item2">字节2</param>
        /// <param name="item3">字节3</param>
        /// <param name="item4">字节4</param>
        [Description("添加四个字节")]
        public void Add(byte item1, byte item2, byte item3, byte item4)
        {
            Add(new byte[] { item1, item2, item3, item4 });
        }

        /// <summary>
        /// 添加五个字节
        /// </summary>
        /// <param name="item1">字节1</param>
        /// <param name="item2">字节2</param>
        /// <param name="item3">字节3</param>
        /// <param name="item4">字节4</param>
        /// <param name="item5">字节5</param>
        [Description("添加五个字节")]
        public void Add(byte item1, byte item2, byte item3, byte item4, byte item5)
        {
            Add(new byte[] { item1, item2, item3, item4, item5 });
        }

        /// <summary>
        /// 添加一个ByteArray对象
        /// </summary>
        /// <param name="byteArray">ByteArray对象</param>
        [Description("添加一个ByteArray对象")]
        public void Add(ByteArray byteArray)
        {
            Add(byteArray.Array);
        }

        /// <summary>
        /// 添加一个ushort类型数值
        /// </summary>
        /// <param name="value">ushort类型数值</param>
        [Description("添加一个ushort类型数值")]
        public void Add(ushort value)
        {
            _list.Add((byte)(value >> 8));
            _list.Add((byte)value);
        }

        /// <summary>
        /// 添加一个short类型数值
        /// </summary>
        /// <param name="value">short类型数值</param>
        [Description("添加一个short类型数值")]
        public void Add(short value)
        {
            _list.Add((byte)(value >> 8));
            _list.Add((byte)value);
        }

        #endregion 相关方法
    }
}