using Ay.Utils;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.BusinessServices
{
    public class SysAdminService
    {
        public List<SysAdmin> QuerySysAdmins()
        {
            string sql = "select LoginId,LoginName,LoginPwd,RoleName from SysAdmin";
            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql);
            List<SysAdmin> sysAdmins = new List<SysAdmin>();
            while (dataReader.Read())
            {
                SysAdmin sysAdmin = new SysAdmin
                {
                    LoginId = Convert.ToInt32(dataReader["LoginId"]),
                    LoginName = dataReader["LoginName"].ToString(),
                    LoginPwd = dataReader["LoginPwd"].ToString(),
                    RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString()),
                    LoginTime = DateTime.Now // 默认登录时间为当前时间
                };
                sysAdmins.Add(sysAdmin);
            }
            dataReader.Close();

            return sysAdmins;
        }


        public SysAdmin AdminLogin(SysAdmin sysAdmin)
        {
            string sql = "select LoginId,LoginName,LoginPwd,RoleName from SysAdmin where LoginName=@LoginName and LoginPwd=@LoginPwd";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@LoginName", sysAdmin.LoginName),
                new SQLiteParameter("@LoginPwd", sysAdmin.LoginPwd)
            };
            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql, parameters);
            SysAdmin loggedInAdmin = null;
            if (dataReader.Read())
            {
                loggedInAdmin = new SysAdmin
                {
                    LoginId = Convert.ToInt32(dataReader["LoginId"]),
                    LoginName = dataReader["LoginName"].ToString(),
                    LoginPwd = dataReader["LoginPwd"].ToString(),
                    RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString()),
                    LoginTime = DateTime.Now // 登录时间为当前时间
                };
            }
            dataReader.Close();
            return loggedInAdmin;
        }
    }
}
