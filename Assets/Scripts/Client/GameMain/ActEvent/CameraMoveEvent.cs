using UnityEngine;
using System.Collections.Generic;
using Client;
/*----------------------------------------------------------------
// 模块名：CameraMoveEvent
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：摄像机移动事件
//--------------------------------------------------------------*/
/// <summary>
/// 摄像机移动事件
/// </summary>
public class CameraMoveEvent : ActWork
{
    public Vector3 m_destPos;

    public Vector3 m_srcPos;

    public Vector3 m_destLookAtPos;

    public Vector3 m_srcLookAtPos;

    public float m_fDuration;

    public float m_fTrigerTime;

    public CameraMoveEvent(Vector3 srcPos, Vector3 destPos, Vector3 srtLookAtPos, Vector3 destLookAtPos, float triggerTime, float fDuratin) : base(0u)
    {
        this.m_fTrigerTime = triggerTime;
        this.m_destPos = destPos;
        this.m_destLookAtPos = destLookAtPos;
        this.m_srcPos = srcPos;
        this.m_srcLookAtPos = srtLookAtPos;
        this.m_fDuration = fDuratin;
    }

    public override void Update()
    {
        Vector3 lookAtPos = this.m_destLookAtPos;
        Vector3 cameraPos = this.m_destPos;
        float num = Time.time - this.m_fTrigerTime;
        if (num >= 0f && num <= this.m_fDuration)
        {
            float d = num / this.m_fDuration;
            lookAtPos = this.m_srcLookAtPos + (this.m_destLookAtPos - this.m_srcLookAtPos) * d;
            cameraPos = this.m_srcPos + (this.m_destPos - this.m_srcPos) * d;
            CameraManager.Instance.SetCamerPosAndLookAt(cameraPos, lookAtPos);
        }
        else if (num > 0f)
        {
            this.IsFinished = true;
            CameraManager.Instance.SetCamerPosAndLookAt(cameraPos, lookAtPos);
            CameraManager.Instance.ChgShow();
        }
    }
}
