using UnityEngine;
using System.Collections.Generic;
using Utility;
#region 模块信息
/*----------------------------------------------------------------
// 模块名BeAttackSkillTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.21
// 模块描述：被攻击者的攻击事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 被攻击者的攻击事件
/// </summary>
public class BeAttackSkillTrigger : Triggerable
{
    public bool ShowAnim
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
    public int HpChange
    {
        get; set;
    }
    public bool IsSpaceAnim
    {
        get; set;
    }
    public bool IsDuraAnim
    {
        get; set;
    }
    /// <summary>
    /// 主要被攻击的神兽Id，因为有些技能是AOE
    /// </summary>
    public long MainBeAttcker
    {
        get;set;
    }
    public override void Trigger()
    {
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(BeAttackerId);
        if (beast != null && this.ShowAnim)
        {
            Debug.Log("OnAttack");
            beast.OnBeAttack(HpChange, IsSpaceAnim, IsDuraAnim);
        }
    }
    public float GetDuration()
    {
        float dura = 0.2f;
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(BeAttackerId);
        if (beast != null)
        {
            dura = beast.GetAnimPlayTime(HpChange, IsSpaceAnim, IsDuraAnim);
        }
        Debug.Log("Dra:" + dura);
        return dura;
    }
}
