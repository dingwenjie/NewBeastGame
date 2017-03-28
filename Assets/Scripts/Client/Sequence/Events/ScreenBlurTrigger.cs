using UnityEngine;
using System.Collections.Generic;
using Utility;
/*----------------------------------------------------------------
// 模块名：ScreenBlurTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：屏幕特效事件表现
//--------------------------------------------------------------*/
/// <summary>
/// 屏幕特效事件表现
/// </summary>
public class ScreenBlurTrigger : Triggerable
{
    public bool endDisable = false;

    public float m_fStartAlpha;

    public float m_fEndAlpha;

    public long bindPlayerID;

    public List<long> m_vAffectedPlayer = new List<long>();

    public override void Trigger()
    {
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.bindPlayerID);
        if (beast != null && beast != BeastManager.BeastError)
        {
            ScreenBlurActEvent work = new ScreenBlurActEvent(this.m_fStartAlpha, this.m_fEndAlpha, this.Duration, this.endDisable, this.m_vAffectedPlayer);
            beast.AddWork(work);
        }
    }
}
