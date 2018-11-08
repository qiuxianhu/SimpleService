﻿using System;
namespace Logger
{
    /// <summary>
    /// ILogger 用于日志记录的基础接口，线程安全的
    /// </summary>
    public interface ILogger: IDisposable
    {
        void LogWithTime(string msg, ELogLevel logLevel = ELogLevel.Info);
        bool Enabled { get; set; }
    }
}
