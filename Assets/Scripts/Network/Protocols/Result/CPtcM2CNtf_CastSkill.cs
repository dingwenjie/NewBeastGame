using UnityEngine;
using System.Collections.Generic;
using Game;
using Utility;
using Client.Common;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CPtcM2CNtf_CastSkill
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.16
// 模块描述：服务器发送给客户端技能释放
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 服务器发送给客户端技能释放
/// </summary>
public class CPtcM2CNtf_CastSkill : CProtocol
{
    public long m_dwRoleId;
    public int m_dwSkillId;
    public long m_dwTargetRoleId;
    public CVector3 m_oTargetPos;
    public CPtcM2CNtf_CastSkill() : base(1034)
    {
        this.m_oTargetPos = new CVector3();
    }
    public override CByteStream DeSerialize(CByteStream bs)
    {
        bs.Read(ref this.m_dwRoleId);
        bs.Read(ref this.m_dwSkillId);
        bs.Read(ref this.m_dwTargetRoleId);
        bs.Read(this.m_oTargetPos);
        return bs;
    }
    public override CByteStream Serialize(CByteStream bs)
    {
        bs.Write(this.m_dwRoleId);
        bs.Write(this.m_dwSkillId);
        bs.Write(this.m_dwTargetRoleId);
        bs.Write(this.m_oTargetPos);
        return bs;
    }
    public override void Process()
    {
        XLog.Log.Debug("CPtcM2CNtf_CastSkill");
        if (Singleton<ClientMain>.singleton.EGameState == EnumGameState.eState_GameMain)
        {
            UseSkillParam useSkillParam = new UseSkillParam();
            useSkillParam.m_dwRoleId = this.m_dwRoleId;
            useSkillParam.m_dwSkillId = this.m_dwSkillId;
            useSkillParam.m_dwTargetRoleId = this.m_dwTargetRoleId;
            useSkillParam.m_oTargetPos = this.m_oTargetPos;

            Singleton<BeastManager>.singleton.OnUseSkill(this.m_dwRoleId, EnumSkillType.eSkillType_Skill, this.m_dwSkillId, useSkillParam);
            //让神兽说出话
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.m_dwRoleId);
            if (beast != null)
            {
                beast.ResetMomentForVoiceInRound();
            }
        }
    }
}
