using System;
using System.Threading;

namespace CycleEngine
{
    public abstract class BaseTaskCycleEngine:ICycleEngine
    {
        public BaseTaskCycleEngine()
        {
            this._thread = new Thread(this.Worker) { IsBackground = true };
        }

        #region 定义字段和相关方法名

        /// <summary>
        /// 一次休息的时间
        /// </summary>
        private const int SLEEP_TIME = 1000;
        /// <summary>
        /// 轮训任务是否停止
        /// https://blog.csdn.net/zwk_9/article/details/32939339
        /// </summary>
        private volatile bool _isStop = true;
        /// <summary>
        /// 每次轮训的时间间隔（单位：秒） 
        /// </summary>
        private int _detectSpanInSecs = 0;
        /// <summary>
        /// 总共休息的次数（每次休息SLEEP_TIME变量的值*totalSleepCount）
        /// 一共休息的时间=SLEEP_TIME*totalSleepCount
        /// </summary>
        private int _totalSleepCount = 0;
        /// <summary>
        /// 线程
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// 轮训任务结束后执行的事件处理方法
        /// </summary>
        public event EventHandler<CycleEngineStoppedEventArgs> EngineStopped;

        /// <summary>
        /// DoDetect 每次循环时，引擎需要执行的核心动作。
        /// (1)该方法不允许抛出异常。
        /// (2)该方法中不允许调用BaseCycleEngine.Stop()方法，否则会导致死锁。
        /// </summary>
        /// <returns>返回值如果为false，表示退出引擎循环线程</returns>
        protected abstract bool DoDetect();

        #endregion

        #region 属性

        /// <summary>
        /// 每次轮训的时间间隔(单位：秒)
        /// 如果将DetectSpanInSecs设为0，则表示无间隙的执行DoDetect方法。而如果将DetectSpanInSecs设为负数，则表示不启动循环引擎。
        /// </summary>
        public int DetectSpanInSecs
        {
            get { return this._detectSpanInSecs; }
            set { this._detectSpanInSecs = value; }
        }

        /// <summary>
        /// 判断是否正在执行轮训任务
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return !this._isStop;
            }
        }
        #endregion        

        #region 方法
        /// <summary>
        /// 轮训执行的任务
        /// </summary>
        private void Worker()
        {
            Exception exception = null;
            try
            {
                while (!this._isStop)
                {
                    if (!this.DoDetect())
                    {
                        this._isStop = true;
                        break;
                    }
                    #region 轮训过程中连续休息_totalSleepCount次，每次休息SLEEP_TIME时间
                    for (int i = 0; i < this._totalSleepCount; i++)
                    {
                        if (this._isStop)
                        {
                            break;
                        }
                        Thread.Sleep(BaseTaskCycleEngine.SLEEP_TIME);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                this.OnEngineStopped(exception);
            }
        }

        private void OnEngineStopped(Exception exception)
        {
            this.Stop();
            if (this.EngineStopped != null)
            {
                this.EngineStopped(this, new CycleEngineStoppedEventArgs(exception));
            }
        }
        #endregion

        #region ICycleEngine成员

        /// <summary>
        /// 启动执行轮训任务的方法
        /// </summary>
        public void Start()
        {
            if (this._detectSpanInSecs<0)
            {
                return;
            }
            if (!this._isStop)
            {
                return;
            }
            this._totalSleepCount=this._detectSpanInSecs*1000 / BaseTaskCycleEngine.SLEEP_TIME;
            this._isStop = false;
            if (this._thread!=null)
            {
                //在C#中怎样推断线程当前所处的状态 https://www.cnblogs.com/lytwajue/p/7225774.html
                if ((this._thread.ThreadState&ThreadState.Unstarted)== ThreadState.Unstarted
                    ||(this._thread.ThreadState&ThreadState.Stopped)==ThreadState.Stopped
                    ||(this._thread.ThreadState&ThreadState.Aborted)==ThreadState.Aborted)
                {
                    this._thread.Start();
                }
            }
        }

        /// <summary>
        /// 停止正在执行的轮训方法
        /// </summary>
        public void Stop()
        {
            if (this._isStop)
            {
                return;
            }
            this._isStop = true;
            try
            {
                if ((this._thread.ThreadState & ThreadState.Unstarted) != ThreadState.Unstarted
                    && (this._thread.ThreadState & ThreadState.Stopped) != ThreadState.Stopped
                    && (this._thread.ThreadState & ThreadState.Aborted) != ThreadState.Aborted
                    && (this._thread.ThreadState & ThreadState.AbortRequested) != ThreadState.AbortRequested)
                {
                    this._thread.Abort();
                }
            }
            catch 
            {
            }
        }

        
        #endregion
    }
}
