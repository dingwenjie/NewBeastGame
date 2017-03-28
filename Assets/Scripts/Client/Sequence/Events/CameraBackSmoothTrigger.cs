using UnityEngine;
using System.Collections.Generic;
using Client;
using Utility;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CameraBackSmoothTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：创建摄像机震动的特效表现
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 创建摄像机震动的特效表现
/// </summary>
public class CameraBackSmoothTrigger : Triggerable
{
    public CameraMoveRecord record;
    public long AttackerId;
    public override void Trigger()
    {
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(AttackerId);
        if (beast != null)
        {
            Vector3 lookAtPos = CameraManager.Instance.LookAtPos;
            Vector3 position = CameraManager.Instance.GameNode.position;
            Vector3 recoverLookAtPos = this.record.RecoverLookAtPos;
            Vector3 destPos = recoverLookAtPos - this.record.RecoverLookAtPos * this.record.RecoverDist * this.record.RecoverScale;
            float durationTime = this.Duration;
            CameraMoveRecoverEvent work = new CameraMoveRecoverEvent
                (position,destPos,lookAtPos,recoverLookAtPos,Time.time,durationTime,this.record);
            beast.AddWork(work);
        }
    }
}
