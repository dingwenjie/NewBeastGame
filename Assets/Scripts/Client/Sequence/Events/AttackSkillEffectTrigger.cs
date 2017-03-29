using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility.Export;
using Utility;
/*----------------------------------------------------------------
// 模块名：AttackSkillEffectTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.29
// 模块描述：攻击技能特效事件
//--------------------------------------------------------------*/
/// <summary>
/// 攻击技能特效事件
/// </summary>
public class AttackSkillEffectTrigger : Triggerable
{
    public int SkillID
    {
        get;
        set;
    }

    public long AttackerId
    {
        get;
        set;
    }

    public List<long> ListBeAttackerId
    {
        get;
        set;
    }

    public List<CVector3> ListBeAttackPos
    {
        get;
        set;
    }
    public float GetHitTime()
    {
        Vector3 vDestPos = (this.ListBeAttackPos.Count > 0) ? Hexagon.GetHex3DPos(this.ListBeAttackPos[0], Space.World) : Vector3.zero;
        long targetId = (this.ListBeAttackerId.Count > 0) ? this.ListBeAttackerId[0] : 0u;
        return SkillGameManager.GetSkillHitTime(this.SkillID,this.AttackerId, targetId, vDestPos);
    }

    public override void Trigger()
    {
        if (this.SkillID > 0u)
        {
            CastSkillParam castSkillParam = new CastSkillParam();
            castSkillParam.m_unMasterBeastId = this.AttackerId;
            castSkillParam.listTargetRoleID = new List<long>();
            castSkillParam.listTargetRoleID.AddRange(this.ListBeAttackerId);
            castSkillParam.unTargetSkillID = this.SkillID;
            if (this.ListBeAttackPos.Count > 0)
            {
                castSkillParam.vec3TargetPos = new CVector3(this.ListBeAttackPos[0]);
            }
            Singleton<BeastManager>.singleton.OnBeastCastSkillEffect(this.AttackerId, this.SkillID, castSkillParam);
        }
    }
}
