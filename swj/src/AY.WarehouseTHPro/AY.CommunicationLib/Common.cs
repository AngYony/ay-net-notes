using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib
{
    public class Common
    {
        /// <summary>
        /// 获取可用端口号
        /// </summary>
        /// <param name="usefullName">是否使用完全名称</param>
        /// <returns>端口号集合</returns>
        public static string[] GetPortNames(bool usefullName = false)
        {
            List<string> result = new List<string>();
            try
            {
                //搜索设备管理器中的所有条目
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
