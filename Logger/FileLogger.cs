using System;
using System.Diagnostics;
using System.IO;

namespace Logger
{
    /// <summary>
    /// FileLogger 将日志记录到Log4net配置里。FileLogger是线程安全的。（C/S结构专用的）
    /// </summary>
    public class FileLogger:ILogger
    {
        private string _configLogLevel = null;
        private bool _enabled = true;
        private log4net.ILog _log = null;

        public FileLogger()
        {
            this._configLogLevel = GetAppSettingValue("LogLevel");
            string logName = GetAppSettingValue("LogName");
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.xml");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            this._log = log4net.LogManager.GetLogger(logName);
        }

        #region 读取appSettings中的值
        private static string GetAppSettingValue(string key)
        {
            string value = null;
            foreach (string item in System.Configuration.ConfigurationManager.AppSettings)
            {
                if (item.Equals(key,StringComparison.CurrentCultureIgnoreCase))
                {
                    value = System.Configuration.ConfigurationManager.AppSettings[key];
                    break;
                }
            }
            return value;
        }
        #endregion

        #region 实现接口中的方法
        public void LogWithTime(string msg,ELogLevel logLevel=ELogLevel.Info)
        {
            if (string.IsNullOrWhiteSpace(msg)||!this._enabled)
            {
                return;
            }
#if DEBUG
            Trace.TraceInformation(msg);
#endif
            if (string.IsNullOrWhiteSpace(this._configLogLevel))
            {
                this._configLogLevel = ((int)ELogLevel.Error).ToString();
            }
            int configLogLevel = Convert.ToInt32(this._configLogLevel);
            //传过来的日志级别 <= 配置文件的日志级别都会记录到日志文件里
            if ((int)logLevel<=configLogLevel)
            {
                try
                {
                    switch (logLevel)
                    {
                        case ELogLevel.Error:
                            this._log.Error(msg);
                            break;
                        case ELogLevel.Trace:
                            this._log.Warn(msg);
                            break;
                        case ELogLevel.Debug:
                            this._log.Debug(msg);
                            break;
                        case ELogLevel.Info:
                            this._log.Info(msg);
                            break;
                        default:
                            break;
                    }
                }
                catch { }
            }

        }

        public void Dispose()
        {
        }

        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }
        #endregion
    }
}
