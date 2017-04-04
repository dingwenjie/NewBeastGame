using UnityEngine;
using System.Collections.Generic;
using Game;
using Client.Common;
using Utility;
using Client.Effect;
using Client.Data;
/*----------------------------------------------------------------
// 模块名：ChangePosTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.2
// 模块描述：神兽位置坐标改变的事件
//--------------------------------------------------------------*/
/// <summary>
/// 神兽位置坐标改变的事件
/// </summary>
public class ChangePosTrigger : Triggerable
{
    public bool IsForward = true;

    public int SkillId = 0;

    public PosChange controlData = new PosChange();

    public long TargetPlayerID = 0;

    public float Jumptime;

    public float Delay;

    public float Height;

    public int EffectId;

    public string JumpEndAnim = string.Empty;

    public string JumpDuraAnim = string.Empty;

    public override void Trigger()
    {
        if (this.controlData.type == ChangePosType.e_Jump)
        {
            Singleton<BeastManager>.singleton.JumpBeastAction(this.controlData.PlayerId, this.controlData.DestPos[0], this.Delay, this.Jumptime, this.Height, this.TargetPlayerID, this.EffectId, this.JumpEndAnim, this.JumpDuraAnim, this.IsForward);
        }
        else if (this.controlData.type == ChangePosType.e_Walk)
        {
            EffectManager.Instance.PlayEffect(this.EffectId, this.controlData.PlayerId, 0);
            Singleton<BeastManager>.singleton.MoveBeastAction(this.controlData.PlayerId, this.controlData.DestPos);
        }
    }
    public float GetDuration()
    {
        float result;
        if (this.controlData.type == ChangePosType.e_Jump)
        {
            result = ((this.Delay + this.Jumptime < 0.03f) ? 0.03f : this.Jumptime);
        }
        else if (this.controlData.type == ChangePosType.e_Walk)
        {
            result = Singleton<SequenceShowManager>.singleton.GetWalkMoveTime(this.controlData.PlayerId, this.controlData.DestPos.Count, true);
        }
        else
        {
            result = 1f;
        }
        return result;
    }
}

public class PosChange
{
    public long PlayerId;
    public List<CVector3> DestPos = new List<CVector3>();
    public ChangePosType type;
    public bool IgnoreDuration
    {
        get;set;
    }
}