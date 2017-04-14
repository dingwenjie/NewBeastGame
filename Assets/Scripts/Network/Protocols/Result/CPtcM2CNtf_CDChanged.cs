using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility;
using System;
/*----------------------------------------------------------------
// 模块名：CPtcM2CNtf_CDChanged
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.12
// 模块描述：服务器发送给客户端技能CD改变
//--------------------------------------------------------------*/
/// <summary>
/// 服务器发送给客户端技能CD改变
/// </summary>
public class CPtcM2CNtf_CDChanged : CProtocol
{
    public long m_dwRoleId;
    public int m_dwSkillId;
    public byte m_btValue;
    public CPtcM2CNtf_CDChanged() : base(1036)
    {

    }
    public override CByteStream DeSerialize(CByteStream bs)
    {
        bs.Read(ref this.m_dwRoleId);
        bs.Read(ref this.m_dwSkillId);
        bs.Read(ref this.m_btValue);
        return bs;
    }
    public override CByteStream Serialize(CByteStream bs)
    {
        bs.Write(this.m_dwRoleId);
        bs.Write(this.m_dwSkillId);
        bs.Write(this.m_btValue);
        return bs;
    }
    public override void Process()
    {
        Singleton<BeastManager>.singleton.OnCDChange(this.m_dwRoleId, this.m_dwSkillId, this.m_btValue, this);
    }
}
