using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility.Export;
using Utility;
using Client.Effect;
#region 模块信息
/*----------------------------------------------------------------
// 模块名BeAttackSkillEffectTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：被攻击的特效表现
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 被攻击的特效表现
/// </summary>
public class BeAttackSkillEffectTrigger : Triggerable
{
    /// <summary>
    /// 是否显示攻击miss的特效
    /// </summary>
    public bool bShowMissEffect
    {
        get; set;
    }
    /// <summary>
    /// 是否是主被攻击者
    /// </summary>
    public bool MainBeAttacker
    {
        get; set;
    }
    public long AttackerId
    {
        get; set;
    }
    public long BeAttackerId
    {
        get; set;
    }
    public int SkillId
    {
        get; set;
    }
    public List<CVector3> BeAttackPos
    {
        get; set;
    }
    public float GetHitTime()
    {
        Vector3 vDestPos = this.BeAttackPos.Count > 0 ? Hexagon.GetHex3DPos(this.BeAttackPos[0], Space.World) : Vector3.zero;
        return SkillGameManager.GetSkillHitTime(this.SkillId, this.AttackerId, this.BeAttackerId, vDestPos);
    }
    public override void Trigger()
    {
        int attackEftId = 0;
        int beAttackEftId = 0;
        SkillGameManager.GetSkillAttackEffectId(this.SkillId, this.AttackerId, ref attackEftId, ref beAttackEftId);
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.AttackerId);
        if (beast != null)
        {
            DataSkillShow data = beast.GetSkillShow(this.SkillId);
            if (!this.MainBeAttacker && data != null)
            {
                if (data.SubHitEffectId != 0)
                {
                    beAttackEftId = data.SubHitEffectId;
                }
                if (bShowMissEffect)
                {
                    //显示MIss漂浮字样
                }
                else if(beAttackEftId > 0)
                {
                    //取得被攻击者的位置，然后播放特效
                    Vector3 vTargetPos = this.BeAttackPos.Count > 0 ? Hexagon.GetHex3DPos(this.BeAttackPos[0], Space.World) : Vector3.zero;
                    EffectManager.Instance.PlayEffect(beAttackEftId,
                        this.BeAttackerId, Vector3.zero, null, 0L, vTargetPos, null, Vector3.zero);
                }
            }
        }
    }
}
