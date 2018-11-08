#region << 版 本 注 释 >>
/*
     * ========================================================================
     * Copyright Notice © 2018-2019  All rights reserved .
     * ========================================================================
     * 机器名称： 
     * 文件名：  ICycleAction 
     * 版本号：  V1.0.0.0 
     * 创建人：  QXH
     * 创建时间：2018/11/27
     * 描述    : ICycleAction 循环执行引擎动作
     * =====================================================================
     * 修改时间：2018/11/27
     * 修改人  ：  
     * 版本号  ： V1.0.0.0 
     * 描述    ：
*/
#endregion
namespace CycleEngine
{
    /// <summary>
    /// ICycleAction循环执行引擎动作
    /// </summary>
    public interface ICycleAction
    {
        /// <summary>
        /// EngineAction 执行引擎动作，返回false表示停止引擎。
        /// 注意，该方法不能抛出异常，否则会导致引擎停止运行（循环线程遭遇异常退出）。
        /// </summary>       
        bool EngineAction();
    }
}
