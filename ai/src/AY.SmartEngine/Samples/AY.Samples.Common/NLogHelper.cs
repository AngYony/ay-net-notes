using NLog.Extensions.Logging;
using NLog.Targets;
using System.ClientModel;
using System.ClientModel.Primitives;
using Microsoft.Extensions.Logging;

namespace AY.Samples.Common
{
    public static class NLogHelper
    {
        public static ILoggerFactory CreateNLogLoggerFactory()
        {
            // 定义文件日志输出目标
            var fileTarget = new FileTarget()
            {
                FileName = "maf.log",
                AutoFlush = true,
                DeleteOldFileOnStartup = true
            };
            // 定义控制台日志输出目标
            var consoleTarget = new ConsoleTarget();
        

            var config = new NLog.Config.LoggingConfiguration();
            // 定义日志输出规则(输出所有Trace级别及以上的日志到控制台)
            config.AddRule(
                NLog.LogLevel.Trace,
                NLog.LogLevel.Fatal,
                target: fileTarget,  // 这里采用文件输出
                "*");// * 表示所有Logger

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.AddNLog(config);
            });

            return loggerFactory;
        }

        /// <summary>
        /// 初始化 ClientLoggingOptions，以打印ChatClient请求日志到控制台
        /// </summary>
        /// <returns></returns>
        public static ClientLoggingOptions CreateClientLoggingOptions()
        {
            var loggingOptions = new ClientLoggingOptions();

            loggingOptions.LoggerFactory = CreateNLogLoggerFactory();

            loggingOptions.EnableLogging = true;                    // 总开关：启用日志
            loggingOptions.EnableMessageLogging = true;             // 记录请求/响应的行与头
            loggingOptions.EnableMessageContentLogging = true;      // 记录请求/响应的完整内容
            loggingOptions.MessageContentSizeLimit = 64 * 1024;     // 增大到 64KB

            // 可选：白名单（避免默认打码影响诊断）
            loggingOptions.AllowedHeaderNames.Add("Content-Type");
            loggingOptions.AllowedHeaderNames.Add("Accept");
            loggingOptions.AllowedHeaderNames.Add("Content-Length");
            loggingOptions.AllowedQueryParameters.Add("api-version");


            return loggingOptions;
        }
    }
}
