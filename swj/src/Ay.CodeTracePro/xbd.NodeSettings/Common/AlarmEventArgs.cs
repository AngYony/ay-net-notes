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
    public  class AlarmEventArgs:EventArgs
    {
        /// <summary>
        /// �豸����
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// ��ǰֵ
        /// </summary>
        public string CurrentValue { get; set; }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public string AlarmValue { get; set; }

        /// <summary>
        /// ����˵��
        /// </summary>
        public string AlarmNote { get; set; }

        /// <summary>
        /// �Ƿ�Ϊ����
        /// </summary>
        public bool IsTriggered { get; set; }
    }
}
