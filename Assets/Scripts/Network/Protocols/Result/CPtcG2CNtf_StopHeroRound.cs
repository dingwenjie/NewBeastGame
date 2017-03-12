using UnityEngine;
using System.Collections.Generic;
using Game;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CPtcG2CNtf_StopHeroRound
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.6
// 模块描述：服务器通知玩家结束该玩家的阶段
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 服务器通知玩家结束该玩家的阶段
/// </summary>
public class CPtcG2CNtf_StopPlayerRound : CProtocol
{
    public long playerId;
    public CPtcG2CNtf_StopPlayerRound() : base(1033)
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
        
    }
}
