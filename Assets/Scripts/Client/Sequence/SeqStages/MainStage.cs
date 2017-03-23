using UnityEngine;
using System.Collections.Generic;
using Client.Common;
using Client.Data;
using Utility;
using Utility.Export;
using Effect;
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
    public Dictionary<long, List<KeyValuePair<int, int>>> HpChangeInfo = new Dictionary<long, List<KeyValuePair<int, int>>>(); //血量改变的信息
    public HashSet<long> MissInfo = new HashSet<long>();//攻击MIss的信息
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
        float allTime = fStartTime;
        if (data != null)
        {
            Vector3 pos = this.TargetPos.Count > 0 ? Hexagon.GetHex3DPos(this.TargetPos[0], Space.World) : Vector3.zero;
            float hitTime = 0;
            //如果是普通攻击的话
            if (this.SkillId == 1)
            {
                hitTime = SkillGameManager.GetSkillHitTime(this.SkillId, this.AttackerId, beAttacker, pos);
            }
            else
            {
                //如果是技能的话
                hitTime = SkillGameManager.GetSkillHitTime(this.SkillId, this.AttackerId, beAttacker, pos, EffectInstanceType.Trace);
                if (data.AttackJumpSpeed > 0)
                {
                    hitTime += this.GetDistanceByTargetBeastId(beAttacker) / data.AttackJumpSpeed;
                }
            }
            float beAttackAnimStartDelayTime = data.AttackAnimStartDelayTime;
            allTime += beAttackAnimStartDelayTime + hitTime;
            fBeAttackStartTime = allTime;
            if (this.HpChangeInfo.ContainsKey(beAttacker))
            {
                bool ShowAnim = false;
                if (!this.MissInfo.Contains(beAttacker))
                {
                    for (int i = 0; i < this.HpChangeInfo[beAttacker].Count; i++)
                    {
                        KeyValuePair<int, int> keyValuePair = this.HpChangeInfo[beAttacker][i];
                        if (keyValuePair.Key < keyValuePair.Value)
                        {
                            ShowAnim = true;
                        }
                    }
                }
                //配置文件里面配置被攻击者是否被攻击次数多次，如果不是就是默认1次
                int attackCount = data.BeAttackCount > 0 ? data.BeAttackCount : 1;
                string[] arrayTime = string.IsNullOrEmpty(data.BeAttackSpaceTime) ? new string[0] : data.BeAttackSpaceTime.Split(';');
                for (int i=0; i < attackCount;i++)
                {
                    BeAttackSkillTrigger trigger = new BeAttackSkillTrigger();
                    trigger.AttackerId = this.AttackerId;
                    trigger.BeAttackerId = beAttacker;
                    trigger.StartTime = allTime;
                    if (arrayTime.Length > 0)
                    {
                        trigger.IsSpaceAnim = i < attackCount - 1;
                        float duration = 0;
                        if (i < arrayTime.Length)
                        {
                            float.TryParse(arrayTime[i], out duration);
                        }
                        trigger.Duration = duration;
                    }
                    else if (data.BeAttackDuraTime > 0)
                    {
                        trigger.IsDuraAnim = true;
                        trigger.Duration = data.BeAttackDuraTime;
                    }
                    else
                    {

                    }
                }
            }
        }
    }
    /// <summary>
    /// 取得攻击者和被攻击者之间的距离
    /// </summary>
    /// <param name="beAttackerId"></param>
    /// <returns></returns>
    private uint GetDistanceByTargetBeastId(long beAttackerId)
    {
        Beast attacker = Singleton<BeastManager>.singleton.GetBeastById(this.AttackerId);
        Beast beAttacker = Singleton<BeastManager>.singleton.GetBeastById(beAttackerId);
        if (attacker != null && beAttacker != null)
        {
            int nRow = 0;
            int nCol = 0;
            Hexagon.GetHexIndexByPos(attacker.MovingPos, out nRow, out nCol);
            CVector3 pos = new CVector3();
            pos.nRow = nRow;
            pos.nCol = nCol;
            Hexagon.GetHexIndexByPos(beAttacker.MovingPos, out nRow, out nCol);
            CVector3 pos2 = new CVector3();
            pos2.nRow = nRow;
            pos2.nCol = nCol;
            return Singleton<ClientMain>.singleton.scene.CalDistance(pos, pos2);
        }
        else
        {
            return 0u;
        }
    }
    #endregion 
}
