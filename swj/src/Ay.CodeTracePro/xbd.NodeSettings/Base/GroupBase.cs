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
    /// GroupBase����
    /// </summary>
    public  class GroupBase
    {
        /// <summary>
        /// ͨ��������
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// ͨ����״̬
        /// </summary>
        public bool IsOK { get; set; } = true;

        /// <summary>
        /// ��ȡ�����������ڶ�ȡʧ��ʱ�������Գ��Զ�����
        /// </summary>
        public int ReadTimes { get; set; } = 1;

        /// <summary>
        /// ��ʱʱ��
        /// </summary>
        public int DelayTime { get; set; } = 100;

    }
}
