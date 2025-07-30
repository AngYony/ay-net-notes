using AY.CommunicationLib.DataConvert;
using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// VariableBase����
    /// </summary>
    public  class VariableBase
    {
        /// <summary>
        /// ��������
        /// </summary>
        [ExcelColumnName("��������")]
        public string VarName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [ExcelColumnName("��������")]
        public DataType DataType { get; set; }

        /// <summary>
        /// ������ַ
        /// </summary>
        [ExcelColumnName("������ַ")]
        public string Address { get; set; }

        /// <summary>
        /// ����ϵ��
        /// </summary>
        [ExcelColumnName("����ϵ��")]
        public float Scale { get; set; }

        /// <summary>
        /// ƫ����
        /// </summary>
        [ExcelColumnName("ƫ����")]
        public float Offset { get; set; }

        /// <summary>
        /// �߱���
        /// </summary>
        [ExcelColumnName("�߱���")]
        public bool HAlarm { get; set; }

        /// <summary>
        /// �߱�����ֵ
        /// </summary>
        [ExcelColumnName("�߱���ֵ")]
        public float HAlarmValue { get; set; }

        /// <summary>
        /// �߱���˵��
        /// </summary>
        [ExcelColumnName("�߱���˵��")]
        public string HAlarmNote { get; set; }

        /// <summary>
        /// �ͱ���
        /// </summary>
        [ExcelColumnName("�ͱ���")]
        public bool LAlarm { get; set; }

        /// <summary>
        /// �ͱ�����ֵ
        /// </summary>
        [ExcelColumnName("�ͱ���ֵ")]
        public float LAlarmValue { get; set; }

        /// <summary>
        /// �ͱ���˵��
        /// </summary>
        [ExcelColumnName("�ͱ���˵��")]
        public string LAlarmNote { get; set; }


        /// <summary>
        /// ����ֵ
        /// </summary>
        [ExcelIgnore]
        public object  VarValue { get; set; }

        /// <summary>
        /// �߱�������ֵ
        /// </summary>
        [ExcelIgnore]
        public float HCacheValue { get; set; } = float.MinValue;

        /// <summary>
        /// �ͱ�������ֵ
        /// </summary>
        [ExcelIgnore]
        public float LCacheValue { get; set; } = float.MaxValue;

    }
}
