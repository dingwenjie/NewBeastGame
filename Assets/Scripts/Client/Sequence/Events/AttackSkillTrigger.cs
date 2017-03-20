using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility;
using Utility.Export;
#region 模块信息
/*----------------------------------------------------------------
// 模块名AttackSkillTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.20
// 模块描述：攻击技能表现事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 攻击技能表现事件
/// </summary>
public class AttackSkillTrigger : Triggerable
{
	public int SkillId
    {
        get;
        set;
    }
    public long AttackerId
    {
        get; set;
    }
    public List<long> BeAttackerId
    {
        get; set;
    }
    public List<CVector3> BeAttackerPos
    {
        get; set;
    }
    public override void Trigger()
    {
        if (this.SkillId > 0)
        {
            CastSkillParam param = new CastSkillParam();
            param.m_unMasterBeastId = this.AttackerId;
            param.listTargetRoleID = new List<long>();
            param.listTargetRoleID.AddRange(this.BeAttackerId);
            param.unTargetSkillID = this.SkillId;
            if (this.BeAttackerId.Count > 0)
            {
                param.vec3TargetPos = new CVector3(this.BeAttackerPos[0]);
            }
            Singleton<BeastManager>.singleton.OnBeastCastSkillAction(this.AttackerId, this.SkillId, param);
        }
    }
    public float GetDuration()
    {
        Vector3 pos = this.BeAttackerPos.Count > 0 ? Hexagon.GetHex3DPos(this.BeAttackerPos[0],Space.World) : Vector3.zero;
        return SkillGameManager.GetSkillDuration(this.SkillId, this.AttackerId, this.BeAttackerId, pos);
    }
}
