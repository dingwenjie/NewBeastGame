using UnityEngine;
using System.Collections.Generic;
using Client;
using Client.Effect;
using Utility;
/*----------------------------------------------------------------
// 模块名：ScreenBlurActEvent
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：屏幕特效处理事件
//--------------------------------------------------------------*/
/// <summary>
/// 屏幕特效处理事件
/// </summary>
public class ScreenBlurActEvent : ActWork
{
    public bool endDisable = false;

    public float fStartAlpha = 0f;

    public float fEndAlpha = 0f;

    public float m_fTrigerTime;

    private float m_fDuration = 0f;

    private List<long> m_vAffectedPlayer = null;

    private List<int> m_vEffectList = new List<int>();

    private void ConfigAllPlayers()
    {
    }

    public void Prepare()
    {
        CameraManager.Instance.BeginScreenBlur();
        CameraManager.Instance.StartEffectHighLightCameraEnable();
        CameraManager.Instance.SetEffectCamearDepth(0);
        //CameraManager.Instance.SetScreenBlurAlpha(this.fStartAlpha);
        //this.ConfigAllPlayers();
        EffectManager.Instance.HighLight = true;
    }

    public ScreenBlurActEvent(float startAlpha, float endAlpha, float duration, bool bEndDisable, List<long> vAffectedPlayer) : base(0)
    {
        this.m_fTrigerTime = Time.time;
        this.fStartAlpha = startAlpha;
        this.fEndAlpha = endAlpha;
        this.endDisable = bEndDisable;
        this.m_fDuration = duration;
        this.m_vAffectedPlayer = vAffectedPlayer;
        this.Prepare();
    }

    public override void Update()
    {
        float num = Time.time - this.m_fTrigerTime;
        if (num >= 0f && num <= this.m_fDuration)
        {
            //float screenBlurAlpha = this.fStartAlpha + num / this.m_fDuration * (this.fEndAlpha - this.fStartAlpha);
            //CameraManager.Instance.SetScreenBlurAlpha(screenBlurAlpha);
        }
        else if (num > this.m_fDuration)
        {
            this.IsFinished = true;
        }
    }

    public override void End()
    {
        base.End();
        if (this.endDisable)
        {
            EffectManager.Instance.HighLight = false;
            CameraManager.Instance.StopEffectHighLightCameraEnable();
            CameraManager.Instance.EndScreenBlur();
            if (this.m_vAffectedPlayer != null)
            {
                foreach (Beast current in Singleton<BeastManager>.singleton.GetAllBeasts())
                {
                    if (!this.m_vAffectedPlayer.Contains(current.Id) && !current.IsDead && current.IsVisible)
                    {
                        //DlgBase<DlgHeadInfo, DlgHeadInfoBehaviour>.singleton.SetPlayerHeadInfoVisible(current, true);
                    }
                }
            }
        }
        else
        {
            //CameraManager.Instance.SetScreenBlurAlpha(this.fEndAlpha);
        }
    }
}
