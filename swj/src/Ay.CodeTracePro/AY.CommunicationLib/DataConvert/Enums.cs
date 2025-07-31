using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib.DataConvert
{

    /// <summary>
    /// �ֽ�˳�򣬴�С��
    /// </summary>
    [Description("�ֽ�˳�򣬴�С��")]
    public enum DataFormat
    {
        /// <summary>
        /// ����˳������
        /// </summary>
        [Description("����˳�����򣬴��")]
        ABCD = 0,
        /// <summary>
        /// ���յ��ַ�ת
        /// </summary>
        [Description("���յ��ַ�ת����˷�ת")] 
        BADC = 1,
        /// <summary>
        /// ����˫�ַ�ת
        /// </summary>
        [Description("����˫�ַ�ת��С�˷�ת")] 
        CDAB = 2,
        /// <summary>
        /// ���յ�������
        /// </summary>
        [Description("���յ�������С��")] 
        DCBA = 3,
    }

    /// <summary>
    /// ������������
    /// </summary>
    [Description("������������")]
    public enum DataType
    {
        /// <summary>
        /// ��������
        /// </summary>
        [Description("��������")] 
        Bool,
        /// <summary>
        /// �ֽ�����
        /// </summary>
        [Description("�ֽ�����")]
        Byte,
        /// <summary>
        /// �з���16λ������
        /// </summary>
        [Description("�з���16λ������")]
        Short,
        /// <summary>
        /// �޷���16λ������
        /// </summary>
        /// 
        [Description("�޷���16λ������")]
        UShort,
        /// <summary>
        /// �з���32λ������
        /// </summary>
        /// 
        [Description("�з���32λ������")]
        Int,
        /// <summary>
        /// �޷���32λ������
        /// </summary>
        [Description("�޷���32λ������")]
        UInt,
        /// <summary>
        /// 32λ�����ȸ�����
        /// </summary>
        [Description("32λ�����ȸ�����")]
        Float,
        /// <summary>
        /// 64λ˫���ȸ�����
        /// </summary>
        [Description("64λ˫���ȸ�����")]
        Double,
        /// <summary>
        /// �з���64λ������
        /// </summary>
        [Description("�з���64λ������")]
        Long,
        /// <summary>
        /// �޷���64λ������
        /// </summary>
        [Description("�޷���64λ������")]
        ULong,
        /// <summary>
        /// �ַ�������
        /// </summary>
        [Description("�ַ�������")]
        String,
        /// <summary>
        /// �ֽ�����
        /// </summary>
        [Description("�ֽ�����")]
        ByteArray,
        /// <summary>
        /// 16�����ַ���
        /// </summary>
        [Description("16�����ַ���")]
        HexString
    }

    /// <summary>
    /// ������������
    /// </summary>
    [Description("������������")]
    public enum ComplexDataType
    {
        /// <summary>
        /// ����
        /// </summary>
        [Description("����")] 
        Bool,
        /// <summary>
        /// �޷����ֽ�
        /// </summary>
        [Description("�޷����ֽ�")] 
        Byte,
        /// <summary>
        /// �з����ֽ�
        /// </summary>
        [Description("�з����ֽ�")] 
        SByte,
        /// <summary>
        /// �з���16λ����
        /// </summary>
        [Description("�з���16λ����")] 
        Short,
        /// <summary>
        /// �޷���16λ����
        /// </summary>
        [Description("�޷���16λ����")] 
        UShort,
        /// <summary>
        /// �з���32λ����
        /// </summary>
        [Description("�з���32λ����")] 
        Int,
        /// <summary>
        /// �޷���32λ����
        /// </summary>
        [Description("�޷���32λ����")] 
        UInt,
        /// <summary>
        /// 32λ������
        /// </summary>
        [Description("32λ������")] 
        Float,
        /// <summary>
        /// 64λ������
        /// </summary>
        [Description("64λ������")] 
        Double,
        /// <summary>
        /// �з���64λ����
        /// </summary>
        [Description("�з���64λ����")] 
        Long,
        /// <summary>
        /// �޷���64λ����
        /// </summary>
        [Description("�޷���64λ����")] 
        ULong,
        /// <summary>
        /// �ַ���
        /// </summary>
        [Description("�ַ���")] 
        String,
        /// <summary>
        /// ���ı��ַ���
        /// </summary>
        [Description("���ı��ַ���")] 
        WString,
        /// <summary>
        /// �ṹ��
        /// </summary>
        [Description("�ṹ��")] 
        Struct,
        /// <summary>
        /// ��������
        /// </summary>
        [Description("��������")] 
        BoolArray,
        /// <summary>
        /// �޷����ֽ�����
        /// </summary>
        [Description("�޷����ֽ�����")] 
        ByteArray,
        /// <summary>
        /// �з����ֽ�����
        /// </summary>
        [Description("�з����ֽ�����")] 
        SByteArray,
        /// <summary>
        /// �з���16λ��������
        /// </summary>
        [Description("�з���16λ��������")] 
        ShortArray,
        /// <summary>
        /// �޷���16λ��������
        /// </summary>
        [Description("�޷���16λ��������")] 
        UShortArray,
        /// <summary>
        /// �з���32λ��������
        /// </summary>
        [Description("�з���32λ��������")] 
        IntArray,
        /// <summary>
        /// �޷���32λ��������
        /// </summary>
        [Description("�޷���32λ��������")] 
        UIntArray,
        /// <summary>
        /// 32λ����������
        /// </summary>
        [Description("32λ����������")] 
        FloatArray,
        /// <summary>
        /// 64λ����������
        /// </summary>
        [Description("64λ����������")] 
        DoubleArray,
        /// <summary>
        /// 64λ�з�����������
        /// </summary>
        [Description("64λ�з�����������")] 
        LongArray,
        /// <summary>
        /// 64λ�޷�����������
        /// </summary>
        [Description("64λ�޷�����������")]
        ULongArray,
        /// <summary>
        /// �ַ�������
        /// </summary>
        [Description("�ַ�������")] 
        StringArray,
        /// <summary>
        /// ���ı��ַ�������
        /// </summary>
        [Description("���ı��ַ�������")] 
        WStringArray,
    }

}
