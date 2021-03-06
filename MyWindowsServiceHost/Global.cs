﻿using Logger;
using System;
using System.Configuration;

namespace MyWindowsServiceHost
{
    public static class Global
    {
        private static ILogger _FileLogger = null;

        internal const string SERVICE_NAME = "MyWindowsServiceHost";

        internal static int SleepMinute = 1;//多少分钟执行一次

        internal static int ExecAppMessagePushHours = 15;//几点执行APP消息推送

        internal static bool IsAppMessagePush = false;//是否执行APP消息推送

        private static MyWindowsServiceJob _myWindowsServiceJob = null;
        static Global()
        {
            int.TryParse(GetAppSettingValue("SleepMinute"), out SleepMinute);
            int.TryParse(GetAppSettingValue("ExecAppMessagePushHours"),out ExecAppMessagePushHours);
            bool.TryParse(GetAppSettingValue("IsAppMessagePush"), out IsAppMessagePush);
            //if (SleepMinute < 10) //不能低于60分钟，太频繁造成数据库压力大。
            //{
            //    SleepMinute = 60;
            //}
            _myWindowsServiceJob = new MyWindowsServiceJob();
        }

        #region 日志        
        internal static ILogger Logger
        {
            get { return _FileLogger; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 读取appSettings中的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            string value = null;
            foreach (string item in ConfigurationManager.AppSettings)
            {
                if (item.Equals(key, StringComparison.CurrentCultureIgnoreCase))
                {
                    value = ConfigurationManager.AppSettings[key];
                    break;
                }
            }
            return value;
        }
        /// <summary>
        /// 开始服务
        /// </summary>
        public static void Start()
        {
            _FileLogger.LogWithTime(SERVICE_NAME+" 服务启动中...",ELogLevel.Info);
            _myWindowsServiceJob.Initialize();
            _FileLogger.LogWithTime(SERVICE_NAME+"服务已启动!",ELogLevel.Info);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public static void Stop()
        {
            _FileLogger.LogWithTime(SERVICE_NAME+" 服务停止中...",ELogLevel.Info);
            _myWindowsServiceJob.Dispose();
            _FileLogger.LogWithTime(SERVICE_NAME+"服务已停止!",ELogLevel.Info);
            _FileLogger.Dispose();
        }
        #endregion
    }
}
