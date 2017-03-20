using UnityEngine;
using System.Collections.Generic;
using System;
using Utility.Export;
#region 模块信息
/*----------------------------------------------------------------
// 模块名SeqUpdatable
// 创建者：chen
// 修改者列表：
// 创建日期：
// 模块描述：片段更新器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 片段更新器
/// </summary>
public class SeqUpdatable
{
    public float StartTime;
    public float EndTime;
    public float Duration;
    /// <summary>
    /// 事件列表
    /// </summary>
    public List<Triggerable> triggerEvents = new List<Triggerable>();
    private IXLog m_log = XLog.GetLog<SeqUpdatable>();
    private float m_fAnimEndTime;

    public float LastAnimEndTime
    {
        get { return this.m_fAnimEndTime; }
        set { this.m_fAnimEndTime = value; }
    }

    public void AddEvent(Triggerable evt)
    {
        this.triggerEvents.Add(evt);
    }
    public void Update()
    {
        try
        {
            for (int i = triggerEvents.Count - 1; i >= 0; i--)
            {
                triggerEvents[i].Update();
            }
        }
        catch (Exception e)
        {
            this.m_log.Fatal(e);
        }

    }
    public void ForcedTrigger()
    {
        for (int i = triggerEvents.Count - 1; i >= 0; i--)
        {
            triggerEvents[i].Trigger();
        }
        this.triggerEvents.Clear();
    }
    public void Clear()
    {
        for (int i = triggerEvents.Count - 1; i >= 0; i--)
        {
            triggerEvents[i].Dispose();
        }
        triggerEvents.Clear();
    }
}
