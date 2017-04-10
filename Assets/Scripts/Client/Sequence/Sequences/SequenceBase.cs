using UnityEngine;
using System.Collections.Generic;
using Client.Common;
#region 模块信息
/*----------------------------------------------------------------
// 模块名SequenceBase
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.17
// 模块描述：顺序播放动作基类
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 顺序播放动作基类
/// </summary>
public abstract class SequenceBase
{
    public enumSequenceType type = enumSequenceType.e_Sequence_Skill;
    public List<SeqBuilder> m_seqBuilders = new List<SeqBuilder>();
    protected bool m_bIsBuilded = false;
    protected bool m_bIsFinished = false;

    public bool IsGameOver = false;

    public float StartTime
    {
        get; set;
    }
    public float EndTime
    {
        get; set;
    }
    public float LastAnimEndTime
    {
        get; set;
    }
    public bool Builded
    {
        get { return this.m_bIsBuilded; }
        set { this.m_bIsBuilded = value; }
    }
    public SequenceBase()
    {
        this.AddSeqBuilder();
    }
    #region 子类重写方法
    public abstract void AddSeqBuilder();

    public virtual void Clear()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void BeginShow(float AdvStartTime, float fLastAnimEndTime)
    {
    }
    public virtual void ForceTrigger()
    {
        foreach (SeqBuilder current in this.m_seqBuilders)
        {
            current.ForcedTrigger();
        }
        this.m_seqBuilders.Clear();
    }
    public virtual bool End()
    {
        return true;
    }

    #region OnMsg
    public virtual void OnMsg(CPtcM2CNtf_CastSkill msg)
    {

    }
    public virtual void OnMsg(CPtcM2CNtf_EndCastSkill msg)
    {

    }
    public virtual void OnMsg(CptcM2CNtf_ChangeHp msg,int origHpVal)
    {

    }
    #endregion
    #endregion

}
