using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility;
using Client.Data;
using Client.GameMain;
using System;
/*----------------------------------------------------------------
// 模块名：CPtcM2CNtf_EndCastSkill
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.9
// 模块描述：服务器通知客户端释放技能表现
//--------------------------------------------------------------*/
/// <summary>
/// 服务器通知客户端释放技能表现
/// </summary>
public class CPtcM2CNtf_EndCastSkill : CProtocol
{

    public CPtcM2CNtf_EndCastSkill() : base(1035)
    {

    }

    public override CByteStream DeSerialize(CByteStream bs)
    {
        return bs;
    }
    public override CByteStream Serialize(CByteStream bs)
    {
        return bs;
    }
    public override void Process()
    {
        if (Singleton<SequenceShowManager>.singleton.CanRecevieMsg)
        {
            Singleton<SequenceShowManager>.singleton.OnMsg(this);
        }
        else
        {
            Singleton<ActEventManager>.singleton.NewQueue();
        }
        XLog.Log.Debug("Rec CPtcG2CNtf_EndCastSkill");
    }
}
