using UnityEngine;
using System.Collections.Generic;
using Client;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CameraMoveRecoverEvent
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：摄像机移动恢复事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 摄像机移动恢复事件
/// </summary>
public class CameraMoveRecoverEvent : ActWork
{
    public Vector3 m_destPos;

    public Vector3 m_srcPos;

    public Vector3 m_destLookAtPos;

    public Vector3 m_srcLookAtPos;

    public float m_fDuration;

    public float m_fTrigerTime;

    private CameraMoveRecord m_record = null;

    public CameraMoveRecoverEvent(Vector3 srcPos, Vector3 destPos, Vector3 srtLookAtPos, Vector3 destLookAtPos, float triggerTime, float fDuratin, CameraMoveRecord record):base(0)
    {
        this.m_fTrigerTime = triggerTime;
        this.m_destPos = destPos;
        this.m_destLookAtPos = destLookAtPos;
        this.m_srcPos = srcPos;
        this.m_srcLookAtPos = srtLookAtPos;
        this.m_fDuration = fDuratin;
        this.m_record = record;
    }
    public override void Update()
    {
        Vector3 lookAtPos = this.m_destLookAtPos;
        Vector3 cameraPos = this.m_destPos;
        float time = Time.time - this.m_fTrigerTime;
        if (time > 0 && time <= this.m_fDuration)
        {
            float d = time / this.m_fDuration;
            lookAtPos = this.m_srcLookAtPos + (this.m_destLookAtPos - this.m_srcLookAtPos) * d;
            cameraPos = this.m_srcPos + (this.m_destPos = this.m_srcPos) * d;
            CameraManager.Instance.SetCamerPosAndLookAt(cameraPos, lookAtPos);
        }
        else if (time > 0)
        {
            this.IsFinished = true;
            CameraManager.Instance.SetCamerPosAndLookAt(cameraPos, lookAtPos);
            if (this.m_record != null)
            {
                CameraManager.Instance.SetCamerPosAndLookAt(this.m_record.RecoverCameraPos, this.m_record.RecoverDist, this.m_record.RecoverScale);
                CameraManager.Instance.Lock = false;
                CameraManager.Instance.CameraMoveEffect = false;
            }
        }
    }
}
