using AY.BusinessServices;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
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

            string connString = "Data Source=E:\\Wy_Work\\AngYony\\ay-net-notes\\swj\\src\\AY.PressurizationStationPro\\PressurizationStation.db;Pooling=true;FaillfMissing=false;";
            new SQLiteService().SetConnectionString(connString);
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// 锁屏时间监听次数
        /// </summary>
        public static DateTime ProgramUseStartTime = DateTime.Now;

        /// <summary>
        /// 当前登录人员
        /// </summary>
        public static SysAdmin CurrentUser = null;
    }
}
