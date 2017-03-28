using UnityEngine;
using System.Collections.Generic;
using Client.Data;
using Utility;
#region 模块信息
/*----------------------------------------------------------------
// 模块名DeadTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.24
// 模块描述：死亡表现事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 死亡表现事件
/// </summary>
public class DeadTrigger : Triggerable
{
    public bool bIsFirstBoold = false;

    public long AttackerId
    {
        get; set;
    }
    public long BeAttackId
    {
        get; set;
    }
    public int OverHurtNum
    {
        get; set;
    }
    /// <summary>
    /// 是否是第一滴血
    /// </summary>
    public bool IsFirstBlood
    {
        get; set;
    }
    public override void Trigger()
    {
        Singleton<BeastManager>.singleton.OnBeastDeadAction(this.BeAttackId);
        if (this.BeAttackId != this.AttackerId)
        {

        }
    }
    public float GetDuration()
    {
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.BeAttackId);
        DataBeastlist data = GameData<DataBeastlist>.dataMap[beast.BeastTypeId];
        float time = 0;
        if (beast != null && data != null)
        {
            time = data.DeadFadeout + data.DeadDelay;
        }
        return Mathf.Max(time, beast.GetAnimPlayTime("dead"));
    }
}
