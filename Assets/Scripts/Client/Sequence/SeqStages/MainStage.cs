using UnityEngine;
using System.Collections.Generic;
using Client.Common;
using Client.Data;
using Utility;
using Game;
/*----------------------------------------------------------------
// 模块名：MainStage
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.18
// 模块描述：顺序播放主阶段
//--------------------------------------------------------------*/
/// <summary>
/// 顺序播放主阶段
/// </summary>
public class MainStage : SeqBuilder
{
    public enumSequenceType showType = enumSequenceType.e_Sequence_Skill;
    public long AttackerId;//攻击者ID
    public int SkillId;//技能ID
    public List<long> BeAttacker;//被攻击者的ID
    public List<CVector3> TargetPos;//攻击目标位置
    public override void BuildSeq()
    {
        float time2;
        switch (this.showType)
        {
            case enumSequenceType.e_Sequence_Skill:
                time2 = this.BuildSkillAction();
                break;
        }
    }

    public float BuildSkillAction()
    {
        float fLastAnimTime = this.LastAnimEndTime;
        Beast attackBeast = Singleton<BeastManager>.singleton.GetBeastById(this.AttackerId);
        DataSkillShow data;
        if (attackBeast != null)
        {
            data = attackBeast.GetSkillShow(this.SkillId);
        }
        else
        {
            data = new DataSkillShow();
        }
        this.BuildAttackSkillAnimShow(this.StartTime, ref fLastAnimTime, data);
    }
    #region 私有方法
    /// <summary>
    /// 攻击者技能攻击动画的表现，返回攻击动作的时间
    /// </summary>
    /// <param name="fStartTime"></param>
    /// <param name="fAnimEndTime"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private float BuildAttackSkillAnimShow(float fStartTime,ref float fAnimEndTime,DataSkillShow data)
    {
        float time = fStartTime;
        if (data != null)
        {
            float delayTime = data.AttackAnimStartDelayTime > 0 ? data.AttackAnimStartDelayTime : 0f;
            AttackSkillTrigger trigger = new AttackSkillTrigger();
            trigger.SkillId = this.SkillId;
            trigger.AttackerId = this.AttackerId;
            trigger.BeAttackerId = this.BeAttacker;
            trigger.BeAttackerPos = this.TargetPos;
            trigger.StartTime = time + delayTime;
            trigger.Duration = trigger.GetDuration();
            base.AddEvent(trigger);
            time = trigger.StartTime + trigger.Duration;
            fStartTime = time;
        }
        return time;
    }
    private float BuildBeAttackSkillAnimShow(long beAttacker, float fStartTime, bool bMainBeAttack, DataSkillShow data, ref float fBeAttackStartTime)
    {

    }
    #endregion 
}
