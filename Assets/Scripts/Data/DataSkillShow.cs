using UnityEngine;
using System.Collections.Generic;
using Client.Data;
#region 模块信息
/*----------------------------------------------------------------
// 模块名DataSkillShow
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.20
// 模块描述：技能展示数据
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 技能展示数据
/// </summary>
public class DataSkillShow : GameData<DataSkillShow>
{
    public static string fileName = "dataSkillShow";
    /// <summary>
    /// 根据skillId来索引
    /// </summary>
    public int ID
    {
        get; set;
    }
    public int ShowId
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    /// <summary>
    /// 技能动作动画名称
    /// </summary>
    public string AttackAction
    {
        get; set;
    }
    /// <summary>
    /// 是否释放技能的时候，调整攻击者的旋转方向
    /// </summary>
    public bool IsEffectForward
    {
        get; set;
    }
    /// <summary>
    /// 攻击开始延迟的时间
    /// </summary>
    public float AttackAnimStartDelayTime
    {
        get; set;
    }
    /// <summary>
    /// 攻击者的技能特效ID
    /// </summary>
    public int AttackerEffectId
    {
        get; set;
    }
    /// <summary>
    /// 被攻击者的技能特效ID
    /// </summary>
    public int BeAttackerEffectId
    {
        get; set;
    }
}

