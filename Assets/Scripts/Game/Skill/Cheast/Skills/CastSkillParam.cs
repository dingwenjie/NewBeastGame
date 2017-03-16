using UnityEngine;
using System.Collections.Generic;
using Game;
/*----------------------------------------------------------------
// 模块名：CastSkillParam
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.16
// 模块描述：释放技能数据结构
//--------------------------------------------------------------*/
/// <summary>
/// 释放技能数据结构
/// </summary>
public class CastSkillParam
{
    public long m_unMasterBeastId;

    public List<long> listTargetRoleID = new List<long>();

    public CVector3 vec3TargetPos = new CVector3();

    public int unTargetSkillID;
}
