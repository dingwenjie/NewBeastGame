using UnityEngine;
using System.Collections.Generic;
#region 模块信息
/*----------------------------------------------------------------
// 模块名ActWork
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.24
// 模块描述：所有行为表现的抽象类
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 所有行为表现的抽象类
/// </summary>
public abstract class ActWork
{
    private float m_fStartTime;
    private bool m_bFinished;
    private float m_DuraTime;
    private long m_lBeasstId;
    public float StartTime
    {
        get { return this.m_fStartTime; }
    }
    /// <summary>
    /// Work的生命周期
    /// </summary>
    public float LifeTime
    {
        get { return this.m_DuraTime; }
        set { this.m_DuraTime = value; }
    }
    public bool IsFinished
    {
        get { return this.m_bFinished; }
        set { this.m_bFinished = value; }
    }
    public long BeastId
    {
        get { return this.m_lBeasstId; }
        set { this.m_lBeasstId = value; }
    }


    public ActWork(long beastId)
    {
        this.m_lBeasstId = beastId;
    }

    public virtual void Start()
    {
        this.m_fStartTime = Time.time;
        this.m_bFinished = false;
    }
    public virtual void End()
    {

    }
    public virtual void Update()
    {
        if (!this.m_bFinished)
        {
            if (Time.time - this.m_fStartTime > this.m_DuraTime)
            {
                this.m_bFinished = true;
            }
        }
    }
}
