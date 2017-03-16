using UnityEngine;
using System.Collections.Generic;
using Game;
using System;
using Utility;
using Client.Data;
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
/// 服务器通知玩家结束该玩家的阶段,玩家点击结束按钮，服务器自动推送
/// </summary>
public class CPtcG2CNtf_StopPlayerRound : CProtocol
{
    public long playerId;
    public CPtcG2CNtf_StopPlayerRound() : base(1033)
    {

    }
    public override CByteStream DeSerialize(CByteStream bs)
    {
        bs.Read(ref this.playerId);
        return bs;
    }
    public override CByteStream Serialize(CByteStream bs)
    {
        bs.Write(this.playerId);
        return bs;
    }
    public override void Process()
    {
        XLog.Log.Debug(string.Format("CPtcG2CNtf_StopPlayerRound：PlayerId={0}", playerId));
        //取得该玩家对应的所有的在游戏中的神兽数据，然后停止战斗阶段
        //但是服务器public void startBeastRound()这个方法只是按照神兽顺序来做，所以下面代码有点问题，但是也是可以用
        List<BeastData> data;
        Singleton<RoomManager>.singleton.PlayerInGameBeastData.TryGetValue(playerId, out data);
        if (data != null)
        {
            foreach (var beast in data)
            {
                Singleton<RoomManager>.singleton.StopBeastRound(beast.Id);
            }
        }

    }
}
