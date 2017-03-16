using UnityEngine;
using System.Collections;
using Game;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：CPtcC2MReq_CastSkill 
// 创建者：chen
// 修改者列表：
// 创建日期：2016.12.16
// 模块描述：客户端发送给服务器释放该技能消息
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 客户端发送给服务器释放该技能消息
/// </summary>
public class CPtcC2MReq_CastSkill : CProtocol
{
    #region 字段
    public long m_dwRoleId;
    public int m_dwSkillId;
    public long m_dwTargetRoleId;
    public CVector3 m_oTargetPos;
    #endregion
    #region 构造方法
    public CPtcC2MReq_CastSkill() : base(1031)
    {
        this.m_oTargetPos = new CVector3();
    }
    #endregion
    #region 公共方法
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
       
    }
    #endregion
    #region 私有方法
    #endregion
    #region 析构方法
    #endregion
}
