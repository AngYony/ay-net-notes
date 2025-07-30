using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.DAL
{
    public class SqlSugarHelper
    {
        public static string ConnectionString = "Data Source=E:\\Wy_Work\\AngYony\\ay-net-notes\\swj\\src\\AY.WarehouseTHPro\\WarehouseTHPro.db;Version=3;Pooling=true;FailIfMissing=false";   // SQLite 示例连接字符串

        public static SqlSugarClient SqlSugarClient
        {
            get
            {
                if (string.IsNullOrEmpty(ConnectionString))
                {
                    throw new InvalidOperationException("Connection string is not set.");
                }
                var client = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = ConnectionString,
                    DbType = DbType.Sqlite, // 根据实际数据库类型修改
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute, // 根据实际情况选择
                    
                });

                client.Aop.OnLogExecuting = (sql, pars) =>
                {
                    // 可以在这里记录SQL执行日志
                    Console.WriteLine($"SQL: {sql}");
                    if (pars != null)
                    {
                        foreach (var par in pars)
                        {
                            Console.WriteLine($"Param: {par.ParameterName} = {par.Value}");
                        }
                    }
                };

                return client;
            }
        }


        public static void CreateTable<T>(int stringLength =100)
        {
            SqlSugarClient.CodeFirst.SetStringDefaultLength(stringLength).InitTables(typeof(T));
        }
    }
}
