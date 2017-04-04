using UnityEngine;
using System.Collections.Generic;
using Utility.Export;
using Utility;
using Client.Skill;
using Client.Common;
using Effect;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：SkillGameManager
// 创建者：chen
// 修改者列表：
// 创建日期：2016.3.10
// 模块描述：战棋技能管理器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 战棋技能管理器
/// </summary>
public class SkillGameManager
{
    #region 字段
    //已经攻击的列表
    private List<int> m_listUsedAttacksInCurRound = new List<int>();
    //已经使用过的技能列表
    private List<int> m_listUsedSkillIdsInCurRound = new List<int>();
    private List<SkillGameData> m_listSkillData = new List<SkillGameData>();
    private static Dictionary<int, SkillBase> m_dicAllSkillBase = new Dictionary<int, SkillBase>();
    private IXLog m_log = XLog.GetLog<SkillGameManager>();
    private long m_unMasterBeastId = 0;
    #endregion
    #region 属性
    public Beast MasterBeast
    {
        get
        {
            return Singleton<BeastManager>.singleton.GetBeastById(this.m_unMasterBeastId);
        }
    }
    public List<SkillGameData> Skills
    {
        get
        {
            return this.m_listSkillData;
        }
    }
    /// <summary>
    /// 已经使用过的攻击次数
    /// </summary>
    public int UseAttackToBaseBuildingCount
    {
        get { return this.m_listUsedAttacksInCurRound.Count; }
    }
    #endregion
    #region 构造方法
    public SkillGameManager(long unBeastId)
    {
        this.m_unMasterBeastId = unBeastId;
    }
    #endregion
    #region 公有方法
    /// <summary>
    /// 初始化，注册所有技能
    /// </summary>
    /// <returns></returns>
    public static bool Init()
    {
        SkillGameManager.m_dicAllSkillBase.Clear();
        SkillGameManager.Register(new SkillNone());
        SkillGameManager.Register(new SkillNormalAttack());
        return true;
    }
    /// <summary>
    /// 注册该技能，加到总的精通技能字典中
    /// </summary>
    /// <param name="skillBase"></param>
    public static void Register(SkillBase skillBase)
    {
        if (skillBase != null && !SkillGameManager.m_dicAllSkillBase.ContainsKey(skillBase.SkillTypeId))
        {
            Debug.Log("祖册技能"+skillBase.SkillTypeId);
            SkillGameManager.m_dicAllSkillBase[skillBase.SkillTypeId] = skillBase;
            skillBase.Init();
        }
        else
        {
            return;
        }
    }
    public static SkillBase GetSkillBase(int unskillId)
    {
        SkillBase skillBase = null;
        Debug.Log("需要取得的技能id" + unskillId+SkillGameManager.m_dicAllSkillBase.Count);
        if (SkillGameManager.m_dicAllSkillBase.TryGetValue(unskillId, out skillBase))
        {
            return skillBase;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 该技能是否向前攻击
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    public static bool IsAttackForward(int skillId)
    {
        SkillBase skill = SkillGameManager.GetSkillBase(skillId);
        return skill != null && skill.IsAttackForward;
    }
    /// <summary>
    /// 取得技能延迟
    /// </summary>
    /// <param name="skillID"></param>
    /// <param name="attakerId"></param>
    /// <param name="beAttacker"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public static float GetSkillDuration(int skillID,long attakerId, List<long> beAttacker, Vector3 targetPos)
    {
        SkillBase skill = SkillGameManager.GetSkillBase(skillID);
        if (skill != null)
        {
            return skill.GetDuration(attakerId, beAttacker, targetPos);
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 取得技能攻击的特效ID，包括攻击者和被攻击者
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="beastId"></param>
    /// <param name="attackerEftId"></param>
    /// <param name="beAttackerEftId"></param>
    /// <returns></returns>
    public static bool GetSkillAttackEffectId(int skillId,long beastId,ref int attackerEftId,ref int beAttackerEftId)
    {
        return SkillGameManager.m_dicAllSkillBase.ContainsKey(skillId) && SkillGameManager.m_dicAllSkillBase[skillId].GetEffectId(beastId, ref attackerEftId, ref beAttackerEftId);
    }
    /// <summary>
    /// 获取技能特效的播放时间
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="attackerId"></param>
    /// <param name="targetId"></param>
    /// <param name="vTargetPos"></param>
    /// <returns></returns>
    public static float GetSkillHitTime(int skillId,long attackerId, long targetId, Vector3 vTargetPos)
    {
        SkillBase skill = SkillGameManager.GetSkillBase(skillId);
        if (skill != null)
        {
            return skill.GetHitTime(attackerId, targetId, vTargetPos);
        }
        else
        {
            return -1;
        }
    }
    /// <summary>
    /// 获取技能特效的播放时间
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="attackerId"></param>
    /// <param name="targetId"></param>
    /// <param name="vTargetPos"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static float GetSkillHitTime(int skillId, long attackerId, long targetId, Vector3 vTargetPos, EffectInstanceType type)
    {
        SkillBase skill = SkillGameManager.GetSkillBase(skillId);
        if (skill != null)
        {
            return skill.GetHitTime(attackerId, targetId, vTargetPos, type);
        }
        else
        {
            return -1;
        }
    }
    /// <summary>
    /// 通过技能id获取技能数据
    /// </summary>
    /// <param name="unSkillId"></param>
    /// <returns></returns>
    public SkillGameData GetSkillById(int unSkillId)
    {
        SkillGameData result;
        for (int i = 0; i < this.m_listSkillData.Count; i++)
        {
            SkillGameData skillData = this.m_listSkillData[i];
            if (skillData.Id == unSkillId)
            {
                result = skillData;
                return result;
            }
        }
        result = SkillGameData.SkillDataError;
        return result;
    }
    /// <summary>
    /// 取得该神兽能使用的技能列表
    /// </summary>
    /// <returns></returns>
    public List<int> GetCanUseSkills()
    {
        List<int> list = new List<int>();
        foreach (var data in this.m_listSkillData)
        {
            SkillBase skillStrategy = SkillGameManager.GetSkillBase(data.Id);
            if (skillStrategy != null)
            {
                if (skillStrategy.CheckUse(this.m_unMasterBeastId) == EnumErrorCodeCheckUse.eCheckErr_Success)
                {
                    list.Add(data.Id);
                }
            }
        }
        return list;
    }
    /// <summary>
    /// 激活所有技能
    /// </summary>
    public void ActiveSkills()
    {
        for (int i = 0; i < this.m_listSkillData.Count; i++)
        {
            SkillGameData skillData = this.m_listSkillData[i];
            if (!skillData.IsActive)
            {

            }
            SkillBase skillBase = SkillGameManager.GetSkillBase(skillData.Id);
            if (skillBase != null)
            {
                skillBase.Active(this.m_unMasterBeastId);
            }
        }
    }
    /// <summary>
    /// 激活某个技能
    /// </summary>
    /// <param name="unSkillId"></param>
    public void ActiveSkill(int unSkillId)
    {
        SkillGameData skill = this.GetSkillById(unSkillId);
        if (skill != null && !skill.IsError)
        {
            if (!skill.IsActive)
            {
                //this.MasterBeast
            }
            SkillBase skillStrategy = SkillGameManager.GetSkillBase(skill.Id);
            if (skillStrategy != null)
            {
                skillStrategy.Active(this.m_unMasterBeastId);
            }
        }
    }
    /// <summary>
    /// 改变技能cd
    /// </summary>
    /// <param name="unSkillId"></param>
    /// <param name="byteCD"></param>
    public void OnSkillCDChange(int unSkillId, byte byteCD)
    {
        SkillGameData skillById = this.GetSkillById(unSkillId);
        if (skillById != null && !skillById.IsError)
        {
            skillById.CDTime = byteCD;
        }
    }
    /// <summary>
    /// 神兽血量改变
    /// </summary>
    public void OnBeastHpChange()
    {
        foreach (var skill in this.m_listSkillData)
        {
            SkillBase skillStrategy = SkillGameManager.GetSkillBase(skill.Id);
            if (skillStrategy != null)
            {
                skillStrategy.OnBeastHpChange(this.m_unMasterBeastId);
            }
        }
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="param"></param>
    public void OnUseSkill(int skillId,UseSkillParam param)
    {
        if (SkillGameManager.m_dicAllSkillBase.ContainsKey(skillId))
        {
            SkillBase skill = SkillGameManager.m_dicAllSkillBase[skillId];
            if (skill != null)
            {
                skill.OnUse(param);
            }
        }
    }
    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="param"></param>
    public void OnCastSkill(int skillId,CastSkillParam param)
    {
        try
        {
            SkillGameData skillData = this.GetSkillById(skillId);
            if (skillData != null && !skillData.IsError)
            {
                foreach (var current in this.m_listSkillData)
                {
                    SkillBase skillBegin = SkillGameManager.GetSkillBase(current.Id);
                    if (skillBegin != null)
                    {
                        skillBegin.OnCastSkillBegin(this.m_unMasterBeastId, skillId);
                    }
                }
                SkillBase skill = SkillGameManager.GetSkillBase(skillData.Id);
                if (skill != null)
                {
                    param.unTargetSkillID = skillId;
                    skill.Cast(param);
                }
                foreach (var current in this.m_listSkillData)
                {
                    SkillBase skillEnd = SkillGameManager.GetSkillBase(current.Id);
                    if (skillEnd != null)
                    {
                        skillEnd.OnCastSkillBegin(this.m_unMasterBeastId, skillId);
                    }
                }
            }
        }
        catch (Exception e)
        {
            this.m_log.Fatal(e);
        }
    }
    /// <summary>
    /// 神兽释放技能表现
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="param"></param>
    public void OnCastSkillAction(int skillId, CastSkillParam param)
    {
        try
        {
            SkillBase skill = SkillGameManager.GetSkillBase(skillId);
            if (skill != null)
            {

            }
        }
        catch (Exception e)
        {
            this.m_log.Fatal(e.ToString());
        }
    }
    /// <summary>
    /// 技能释放特效
    /// </summary>
    /// <param name="unSkillId"></param>
    /// <param name="castSkillParam"></param>
    public void OnCastSkillEffect(int unSkillId, CastSkillParam castSkillParam)
    {
        try
        {
            SkillBase skillStrategy = SkillGameManager.GetSkillBase(unSkillId);
            if (null != skillStrategy)
            {
                skillStrategy.OnCastSkillEffect(castSkillParam);
            }
        }
        catch (Exception ex)
        {
            this.m_log.Fatal(ex.ToString());
        }
    }

    /// <summary>
    /// 添加技能
    /// </summary>
    /// <param name="unSkillId"></param>
    /// <param name="bNeedActive"></param>
    public void AddSkill(int unSkillId, bool bNeedActive)
    {
        this.AddSkill(unSkillId, bNeedActive, ESkillActivateType.SKILL_ACTIVATE_TYPE_INVALID);
    }
    public void AddSkill(int skillId, bool bNeedActive, ESkillActivateType eSkillActiveType)
    {
        SkillGameData skillData = new SkillGameData(skillId);
        this.AddSkill(skillData, bNeedActive,eSkillActiveType);
    }
    public void AddSkill(SkillGameData data, bool bNeedActive, ESkillActivateType eSkillACtiveType)
    {
        if (data != null)
        {
            //如果还不存在该技能
            if (!this.HasSkill(data.Id))
            {
                this.m_listSkillData.Add(data);
                if (bNeedActive)
                {
                    this.ActiveSkill(data.Id);
                }
                if (this.MasterBeast.Role)
                {
                    Singleton<BeastRole>.singleton.OnAddSkill(data.Id);
                }
            }
            else
            {
                this.m_log.Error(string.Format("m_dicSkill.ContainsKey{0} == true", data.Id));
            }
        }
    }
    /// <summary>
    /// 是否已经存在该技能了
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    public bool HasSkill(int skillId)
    {
        foreach (var skill in this.m_listSkillData)
        {
            if (skillId == skill.Id)
            {
                return true;
            }
        }
        return false;
    }
	#endregion
	#region 私有方法
	#endregion
}
