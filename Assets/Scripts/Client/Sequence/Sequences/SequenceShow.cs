using UnityEngine;
using System.Collections.Generic;
using System;
using Utility.Export;
using Client.Common;
/*----------------------------------------------------------------
// 模块名：SequenceShow
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.18
// 模块描述：顺序展示
//--------------------------------------------------------------*/
/// <summary>
/// 顺序展示
/// </summary>
public class SequenceShow : SequenceBase
{
    public MainStage mainStage = new MainStage();
    private float mainStageStartTime = 0f;

    private IXLog m_log = XLog.GetLog<SequenceShow>();

    public override void AddSeqBuilder()
    {
        this.m_seqBuilders.Add(this.mainStage);
    }
    public override void Clear()
    {
        this.mainStage.Clear();
    }
    public override void Update()
    {
        if (this.Builded)
        {
            this.mainStage.Update();
        }
    }
    public override void BeginShow(float AdvStartTime, float fLastAnimEndTime)
    {
        try
        {
            float starTime = AdvStartTime;
            float animEndTime = Time.time > fLastAnimEndTime ? Time.time : fLastAnimEndTime;
            if (Time.time > starTime)
            {
                starTime = Time.time;
            }
            this.mainStageStartTime = starTime;
            this.mainStage.showType = enumSequenceType.e_Sequence_Skill;
            this.mainStage.Build(this.mainStageStartTime, animEndTime);
            base.LastAnimEndTime = this.mainStage.LastAnimEndTime;
            base.EndTime = this.mainStage.EndTime;
            this.Builded = true;
        }
        catch (Exception e)
        {
            this.m_log.Fatal(string.Format("Exception in BeginShow build process{0}", e.ToString()));
            this.Builded = true;
        }
    }
    public override bool End()
    {
        if (!this.Builded)
        {
            return false;
        }
        else
        {
            if (Time.time > base.EndTime)
            {
                this.m_bIsFinished = true;
            }
            return this.m_bIsFinished;
        }
    }
    #region OnMsg
    public override void OnMsg(CPtcM2CNtf_CastSkill msg)
    {
        this.mainStage.AttackerId = msg.m_dwRoleId;
        this.mainStage.SkillId = msg.m_dwSkillId;
        if (msg.m_dwTargetRoleId != 0)
        {
            this.mainStage.BeAttackerList.Add(msg.m_dwTargetRoleId);
            if (!this.mainStage.HpChangeInfo.ContainsKey(msg.m_dwTargetRoleId))
            {
                this.mainStage.HpChangeInfo[msg.m_dwTargetRoleId] = new List<KeyValuePair<int, int>>();
            }
        }
        else
        {
            //如果没有目标神兽
            foreach (var beast in msg.m_oHurtList)
            {
                this.mainStage.BeAttackerList.Add(beast);
                if (!this.mainStage.HpChangeInfo.ContainsKey(beast))
                {
                    this.mainStage.HpChangeInfo[beast] = new List<KeyValuePair<int, int>>();
                }
            }
        }
        this.mainStage.BeAttackPosList.Add(msg.m_oTargetPos);
    }
    public override void OnMsg(CPtcM2CNtf_EndCastSkill msg)
    {
        this.BeginShow(base.StartTime, base.LastAnimEndTime);
    }
    #endregion
}
