using UnityEngine;
using System.Collections.Generic;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名Triggerable
// 创建者：chen
// 修改者列表：
// 创建日期：
// 模块描述：顺序播放事件触发器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 顺序播放事件触发器
/// </summary>
public class Triggerable : IDisposable
{
    private bool m_bTriggered = false;
    private bool m_bFinished = false;

    public virtual float Duration
    {
        get; set;
    }
    public float StartTime
    {
        get; set;
    }
    #region 子类重写方法
    /// <summary>
    /// 触发事件
    /// </summary>
    public virtual void Trigger()
    {

    }
    /// <summary>
    /// 事件完成回调函数
    /// </summary>
    public virtual void FinishCallback()
    {

    }
    public virtual void  Update()
    {
        if (!this.m_bFinished)
        {
            if (this.IsFinished())
            {
                this.m_bFinished = true;
            }

            if (!this.m_bTriggered && Time.time > this.StartTime)
            {
                this.Trigger();
                this.m_bTriggered = true;
            }
        }
    }
    public virtual void Dispose(bool disposed)
    {

    }
    #endregion
    public bool IsFinished()
    {
        if (this.m_bFinished)
        {
            return true;
        }
        else if (Time.time > this.StartTime + this.Duration)
        {
            this.m_bFinished = true;
            if (!this.m_bTriggered)
            {
                this.Trigger();
                this.m_bTriggered = true;
            }
            this.FinishCallback();
            return true;
        }
        else
        {
            return false;
        }
    }
    ~Triggerable()
    {
        this.Dispose(false);
    }
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

}
