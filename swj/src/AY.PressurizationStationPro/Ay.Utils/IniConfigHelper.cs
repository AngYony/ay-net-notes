using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ay.Utils
{
    /// <summary>
    /// IniConfigHelper
    /// </summary>
    public class IniConfigHelper
    {
        private static string filePath = "";

        #region API函数声明

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);

        #endregion

        #region 读取Sections
        /// <summary>
        ///  ReadSections
        /// </summary>
        /// <param name="iniFilename">文件路径</param>
        /// <returns>集合</returns>
        public static List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
            {
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            }
            return result;
        }

        #endregion

        #region 读Keys
        /// <summary>
        /// ReadKeys
        /// </summary>
        /// <param name="SectionName">区域名称</param>
        /// <param name="iniFilename">路径</param>
        /// <returns>集合</returns>
        public static List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
            {
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            }
            return result;
        }
        #endregion

        #region 读Ini文件

        /// <summary>
        ///  ReadIniData
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值</param>
        /// <returns>返回值</returns>
        public static string ReadIniData(string Section, string Key, string Defaut)
        {
            return ReadIniData(Section, Key, Defaut, filePath);
        }

        /// <summary>
        ///  ReadIniData
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值</param>
        /// <param name="iniFilePath">路径</param>
        /// <returns>返回值</returns>
        public static string ReadIniData(string Section, string Key, string Default, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, Default, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else return string.Empty;
        }

        #endregion

        #region 写Ini文件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <returns>是否成功</returns>
        public static bool WriteIniData(string Section, string Key, string Value)
        {
            return WriteIniData(Section, Key, Value, filePath);
        }

        /// <summary>
        ///  WriteIniData
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="iniFilePath">路径</param>
        /// <returns>是否成功</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
            return OpStation != 0;
        }
        #endregion

    }
}
