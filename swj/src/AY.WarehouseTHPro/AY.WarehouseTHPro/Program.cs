using AY.WarehouseTH.DAL;
using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //CodeFirst 初始化数据库表，这里为了省事，直接调用了DAL层，生产开发中不允许这样操作
            //SqlSugarHelper.CreateTable<MonitorData>();
            //SqlSugarHelper.CreateTable<SysAlarm>();

            Application.Run(new FrmMain());

           
        }
    }
}
