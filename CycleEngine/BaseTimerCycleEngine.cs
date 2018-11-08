#region << 版 本 注 释 >>
/*
     * ========================================================================
     * Copyright Notice © 2018-2019  All rights reserved .
     * ========================================================================
     * 机器名称： 
     * 文件名：  BaseTimerCycleEngine 
     * 版本号：  V1.0.0.0 
     * 创建人：  QXH
     * 创建时间：2018/11/27
     * 描述    : BaseTimerCycleEngine 按照时间间隔方式循环引擎
     * =====================================================================
     * 修改时间：2018/11/27
     * 修改人  ：  
     * 版本号  ： V1.0.0.0 
     * 描述    ：
*/
#endregion
using System;
using System.Threading;

namespace CycleEngine
{
    /// <summary>
    /// BaseTimerCycleEngine 按照时间间隔方式循环引擎，循环引擎直接继承它并实现DoDetect方法即可。
    /// </summary>
    public abstract class BaseTimerCycleEngine:ICycleEngine
    {
        public BaseTimerCycleEngine()
        {
        }

        #region 字段和定义的抽象方法
        private Timer _timer = null;
        /// <summary>
        /// 每次轮训的时间间隔（单位：秒）
        /// </summary>
        private int _detectSpanInSecs = 0;
        /// <summary>
        /// 轮训任务是否运行
        /// </summary>
        private bool _isRunning = false;

        protected abstract bool DoDetect();
        #endregion

        # region 属性
        /// <summary>
        /// 轮训任务是否运行
        /// </summary>
        public bool IsRunning
        {
            get { return this._isRunning; }
        }

        /// <summary>
        /// 每次轮训的时间间隔(单位：秒)
        /// </summary>
        public int DetectSpanInSecs
        {
            get
            {
                return this._detectSpanInSecs;
            }
            set
            {
                this._detectSpanInSecs = value;
            }
        }
        #endregion

        #region ICycleEngine成员
        public virtual void Start()
        {
            if (this._timer == null)
            {
                this._timer = new Timer(new TimerCallback(this.Worker), null, 0, this._detectSpanInSecs * 1000);
            }
        }

        public virtual void Stop()
        {
            this._timer.Dispose();
            this._timer = null;
        }

        private void Worker(Object obj)
        {
            try
            {
                this.DoDetect();
            }
            catch (Exception ex)
            {
                this.OnEngineStopped(new CycleEngineStoppedEventArgs(ex));
                this._timer.Dispose();
                this._timer = null;
            }
        }
        #endregion

        public event EventHandler<CycleEngineStoppedEventArgs> EngineStopped;

        private void OnEngineStopped(CycleEngineStoppedEventArgs e)
        {
            if (this.EngineStopped != null)
                this.EngineStopped(this, e);
        }

        
    }
}
