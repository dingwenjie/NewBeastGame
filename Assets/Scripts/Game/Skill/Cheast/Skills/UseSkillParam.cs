using UnityEngine;
using System.Collections.Generic;
using Game;
#region 模块信息
/*----------------------------------------------------------------
// 模块名UseSkillParam
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.16
// 模块描述：释放技能的数据结构
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 使用技能（装备）的数据结构
/// </summary>
public class UseSkillParam
{
    public long m_dwRoleId;//释放者的id
    public int m_dwSkillId;//技能id
    public long m_dwTargetRoleId;//被释放者的id
    public CVector3 m_oTargetPos;//释放的位置坐标
}
