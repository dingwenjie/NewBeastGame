using UnityEngine;
using System.Collections.Generic;
using Effect;
using Client.Effect;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CameraAnimTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.29
// 模块描述：摄像机动画事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 摄像机动画事件
/// </summary>
public class CameraAnimTrigger : Triggerable
{
    public long playerId;
    public int EffectId;
    public override void Trigger()
    {
        if (EffectManager.Instance.GetEffectCameraControlType(this.EffectId) == 0)
        {
            EffectManager.Instance.PlayEffect(this.EffectId, this.playerId, this.playerId);
        }
    }
    public float GetDuration()
    {
        return 1f;
    }
}
