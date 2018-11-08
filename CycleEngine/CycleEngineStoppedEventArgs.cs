using System;

namespace CycleEngine
{
    /// <summary>
    /// EngineeStoppedEventArgs 当引擎由运行变为停止状态时，参数类型。
    /// </summary>
    public class CycleEngineStoppedEventArgs: EventArgs
    {
        private Exception _Exception;
        public CycleEngineStoppedEventArgs(Exception exception)
        {
            this._Exception = exception;
        }
        public Exception Exception
        {
            get { return this._Exception; }
        }
    }
}
