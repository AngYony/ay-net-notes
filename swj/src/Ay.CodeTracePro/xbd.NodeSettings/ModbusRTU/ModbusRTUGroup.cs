//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
//�űش��Ȩ���У���ע΢�Ź��ںţ���λ��Guide
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// ModbusRTUGroupͨ����
    /// </summary>
    public class ModbusRTUGroup:GroupBase
    {
        /// <summary>
        /// �洢��
        /// </summary>
        public ModbusStore StoreArea { get; set; }

        /// <summary>
        /// ͨ����ID
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// ��ʼ��ַ
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public ushort  Length { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public List<ModbusRTUVariable> VarList { get; set; } = new List<ModbusRTUVariable>();



    }
}
