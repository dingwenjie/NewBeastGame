﻿using UnityEngine;
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
    /// <summary>
    /// 如不是技能主被攻击者就显示这个被攻击特效ID
    /// </summary>
    public int SubHitEffectId
    {
        get; set;
    }
    /// <summary>
    /// 攻击跳跃速度
    /// </summary>
    public float AttackJumpSpeed
    {
        get; set;
    }
    /// <summary>
    /// 被攻击次数
    /// </summary>
    public int BeAttackCount
    {
        get; set;
    }
    /// <summary>
    /// 受击动作的间隔时间
    /// </summary>
    public string BeAttackSpaceTime
    {
        get; set;
    }
    /// <summary>
    /// 被攻击延迟时间
    /// </summary>
    public float BeAttackDuraTime
    {
        get; set;
    }
    /// <summary>
    /// 摄像机移动标示量，为1代表移动
    /// </summary>
    public int CameraMove
    {
        get; set;
    }
    /// <summary>
    /// 摄像机移动的时间间隔
    /// </summary>
    public float CameraMoveDurationTime
    {
        get; set;
    }
    /// <summary>
    /// 屏幕特效
    /// </summary>
    public int ScreenBlur
    {
        get;set;
    }
    /// <summary>
    /// 屏幕特效时间1
    /// </summary>
    public float ScreenBlurDurationTime1
    {
        get;set;
    }
    /// <summary>
    /// 屏幕特效时间2
    /// </summary>
    public float ScreenBlurDurationTime2
    {
        get; set;
    }
    /// <summary>
    /// 屏幕Alpha
    /// </summary>
    public float BlackDepth
    {
        get;set;
    }
    /// <summary>
    /// 攻击者跳跃攻击前摇
    /// </summary>
    public float AttackJumpStartDelayTime
    {
        get;set;
    }
    /// <summary>
    /// 攻击跳跃结束动画
    /// </summary>
    public string AttackJumpEndAnim
    {
        get;set;
    }
    /// <summary>
    /// 攻击跳跃延迟动画
    /// </summary>
    public string AttackJumpDuraAnim
    {
        get;set;
    }
    /// <summary>
    /// 跳跃特效
    /// </summary>
    public int AttackJumpEffect
    {
        get;set;
    }
    /// <summary>
    /// 跳跃高度
    /// </summary>
    public float AttackJumpHeight
    {
        get;set;
    }
    /// <summary>
    /// 攻击跳跃需要的时间
    /// </summary>
    public float AttackJumpTime
    {
        get;set;
    }
}

