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
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    public  class Common
    {
        /// <summary>
        /// ��ȡ���ö˿ں�
        /// </summary>
        /// <param name="usefullName">�Ƿ�ʹ����ȫ����</param>
        /// <returns>�˿ںż���</returns>
        public static string[] GetPortNames(bool usefullName = false)
        {
            List<string> result = new List<string>();
            try
            {
                //�����豸�������е�������Ŀ
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity"))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties["Name"].Value != null)
                        {
                            string name = hardInfo.Properties["Name"].Value.ToString();
                            if (name.Contains("(COM") && name.EndsWith(")"))
                            {
                                if (usefullName)
                                {
                                    result.Add(name);
                                }
                                else
                                {
                                    if (name.Contains("->"))
                                    {
                                        result.Add(name.Substring(name.IndexOf('(') + 1, name.IndexOf(')') - name.IndexOf('-') - 2));
                                    }
                                    else
                                    {
                                        result.Add(name.Substring(name.IndexOf('(') + 1, name.IndexOf(')') - name.IndexOf('(') - 1));
                                    }
                                }
                            }
                        }
                    }
                    searcher.Dispose();
                }
            }
            catch
            {
                result = new List<string>();
            }
            return result.ToArray();
        }
    }
}
