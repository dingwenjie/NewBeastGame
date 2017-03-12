using UnityEngine;
using System.Collections.Generic;
using Game;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CptM2CNtf_CDResult
// 创建者：chen
// 修改者列表：
// 创建日期：2016.3.6
// 模块描述：服务器发送给客户端更新魔法值
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 服务器发送给客户端更新魔法值
/// </summary>
public class CptM2CNtf_CDResult : CProtocol
{
    public List<long> playerIds;
    public List<int> cds;
    public CptM2CNtf_CDResult() : base(1032)
    {
        playerIds = new List<long>();
        cds = new List<int>();
    }
    public override void Process()
    {
        
    }
    public override CByteStream Serialize(CByteStream bs)
    {
        return bs;
    }
    public override CByteStream DeSerialize(CByteStream bs)
    {
        return bs;
    }
}
