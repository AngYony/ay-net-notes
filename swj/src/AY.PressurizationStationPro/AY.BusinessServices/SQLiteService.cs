using Ay.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.BusinessServices
{
    public class SQLiteService
    {
        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        public void SetConnectionString(string connectionString)
        {
            // 设置SQLite数据库连接字符串
            // 这里可以使用SQLite的相关库来设置连接字符串
            // 例如：SQLiteConnection.ConnectionString = connectionString;
            SQLiteHelper.ConnString = connectionString;
        }
    }
}
