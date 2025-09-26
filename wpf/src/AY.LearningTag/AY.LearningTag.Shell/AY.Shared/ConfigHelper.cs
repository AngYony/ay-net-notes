using System.Configuration;

namespace AY.LearningTag.Shared
{
    public static class ConfigHelper
    {
        public static void ReadConnectionString()
        {
            //读取连接字符串
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string sqliteConnection = config.ConnectionStrings.ConnectionStrings["local"].ConnectionString;
            if (string.IsNullOrEmpty(sqliteConnection))
            {
                throw new ConfigurationErrorsException("未找到名为 'local' 的连接字符串。");
            }
        }

        public static void WriteConnectionString(string connectionString)
        {
            //写入连接字符串
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["local"].ConnectionString = connectionString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}