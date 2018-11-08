using CycleEngine;

namespace MyWindowsServiceHost
{
    public partial class MyWindowsServiceJob: ICycleAction
    {
        private TaskCycleEngine _taskCycleEngine = null;
        /// <summary>
        /// 实现接口中的方法声明，业务逻辑执行代码
        /// </summary>
        /// <returns></returns>
        public bool EngineAction()
        {           
            System.Console.WriteLine("服务执行");
            return true;
        }

        internal void Initialize()
        {
            _taskCycleEngine = new TaskCycleEngine(this);
            _taskCycleEngine.DetectSpanInSecs= Global.SleepMinute * 60;
            _taskCycleEngine.Start();
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (this._taskCycleEngine != null)
            {
                this._taskCycleEngine.Stop();
                this._taskCycleEngine = null;
            }
        }
    }
}
