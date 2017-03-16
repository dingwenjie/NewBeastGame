using UnityEngine;
using System.Collections.Generic;
using Client.GameMain;
using Client.Skill;
#region 模块信息
/*----------------------------------------------------------------
// 模块名UseSkillEvent
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.16
// 模块描述：使用技能事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 使用技能事件
/// </summary>
public class UseSkillEvent : ActEvent
{
    private UseSkillParam m_param = null;
    public UseSkillParam UseSkillParam
    {
        get { return this.m_param; }
        set { this.m_param = value; }
    }
    public UseSkillEvent()
    {
        XLog.Log.Debug("UseSkillEvent:New()");
    }
    public override void Trigger()
    {
        XLog.Log.Debug("UseSkillEvent:Trigger:"+this.UseSkillParam.m_dwSkillId);
        base.Trigger();
        SkillBase skill = SkillGameManager.GetSkillBase(this.m_param.m_dwSkillId);
        if (skill != null)
        {
            skill.OnUseSkillAction(this.m_param);
        }
    }
}
