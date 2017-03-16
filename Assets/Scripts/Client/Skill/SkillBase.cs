﻿using UnityEngine;
using System.Collections.Generic;
using Utility.Export;
using Client.Data;
using Utility;
using Client.Common;
using Game;
using Client.GameMain;
namespace Client.Skill
{
    /// <summary>
    /// 技能基础类
    /// </summary>
    public abstract class SkillBase
    {
        #region 字段
        protected int m_unskillId = 0;
        protected DataSkill m_skillConfig = null;
        protected int m_nEffectId = 0;
        private IXLog m_log = XLog.GetLog<SkillBase>();
        #endregion
        #region 属性
        /// <summary>
        /// 技能类型id
        /// </summary>
        public int SkillTypeId
        {
            get
            {
                return this.m_unskillId;
            }
        }
        /// <summary>
        /// 是否该技能是激活的
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (this.m_skillConfig != null && this.m_skillConfig.IsActive)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 技能图标
        /// </summary>
        public string IconFile
        {
            get
            {
                if (this.m_skillConfig != null)
                {
                    return this.m_skillConfig.Icon;
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 技能描述
        /// </summary>
        public string TipInfo
        {
            get
            {
                if (this.m_skillConfig != null)
                {
                    return this.m_skillConfig.Desc;
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 技能使用距离
        /// </summary>
        public int UseDis
        {
            get
            {
                if (this.m_skillConfig != null)
                {
                    return this.m_skillConfig.UseDis;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
        #region 构造函数
        public SkillBase()
        {

        }
        #endregion 
        #region 公有方法
        /// <summary>
        /// 初始化技能配置类数据
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            DataSkill dataSkill = GameData<DataSkill>.dataMap[this.m_unskillId];
            if (dataSkill != null)
            {
                this.m_skillConfig = dataSkill;
                return true;
            }
            else
            {
                this.m_log.Error("m_skillConfig == null，skillId:" + this.m_unskillId);
                return false;
            }
        }
        /// <summary>
        /// 取得受影响的格子列表
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        /// <param name="unTargetBeastId"></param>
        /// <returns></returns>
        public List<CVector3> GetAffectAreaByTargetBeast(long unMasterBeastId, long unTargetBeastId)
        {
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(unTargetBeastId);
            List<CVector3> result;
            if (beast != null)
            {
                result = this.GetAffectAreaByTargetPos(unMasterBeastId, beast.Pos);
            }
            else
            {
                result = new List<CVector3>();
            }
            return result;
        }
        /// <summary>
        /// 取得受影响的英雄列表
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        /// <param name="unTargetBeastId"></param>
        /// <returns></returns>
        public List<long> GetAffectBeastsByTargetBeast(long unMasterBeastId, long unTargetBeastId)
        {
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(unTargetBeastId);
            List<long> result;
            if (beast != null)
            {
                result = this.GetAffectBeastsByTargetPos(unMasterBeastId, beast.Pos);
            }
            else
            {
                result = new List<long>();
            }
            return result;
        }
        #endregion
        #region 子类重写方法
        /// <summary>
        /// 激活技能添加到战斗记录
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        public virtual void Active(long unMasterBeastId)
        {
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(unMasterBeastId);
            if (beast != null && this.m_skillConfig != null)
            {
                if (this.m_unskillId != 1u)
                {
                    //添加激活什么技能到战斗记录
                }
            }
            else
            {
                this.m_log.Error("null == heroMaster || null == m_skillConfig:" + this.m_unskillId);
            }
        }
        /// <summary>
        /// 神兽血量改变的回调函数
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        public virtual void OnBeastHpChange(long unMasterBeastId)
        {

        }
        /// <summary>
        /// 获取技能的攻击范围
        /// </summary>
        /// <param name="BeastId"></param>
        /// <returns></returns>
        public virtual int GetUseDistance(long BeastId)
        {
            if (this.m_skillConfig != null)
            {
                if (this.m_skillConfig.UseDis < 0)
                {
                    Debug.LogWarning("攻击距离小于0");
                    return int.MaxValue;
                }
                else
                {
                    return this.m_skillConfig.UseDis;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 取得该神兽最小释放技能距离
        /// </summary>
        /// <param name="beastId"></param>
        /// <returns></returns>
        public virtual int GetUseDistanceMin(long beastId)
        {
            int result;
            if (this.m_skillConfig != null)
            {
                result = this.m_skillConfig.UseDisMin;
            }
            else
            {
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 获取技能的动画字符
        /// </summary>
        /// <param name="beastId"></param>
        /// <returns></returns>
        public virtual string GetSkillAnimName(long beastId)
        {
            return "attack";
        }
        /// <summary>
        /// 检测是否该神兽能使用该技能
        /// </summary>
        /// <param name="beastId"></param>
        /// <returns></returns>
        public virtual EnumErrorCodeCheckUse CheckUse(long beastId)
        {
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(beastId);
            if (beast == null || beast.IsError)
            {
                return EnumErrorCodeCheckUse.eCheckErr_FatalErr;
            }
            else
            {
                if (this.GetValidTargetHexs(beastId).Count > 0)
                {
                    return EnumErrorCodeCheckUse.eCheckErr_Success;
                }
                else if (this.GetValidTargetPlayers(beastId).Count > 0)
                {
                    return EnumErrorCodeCheckUse.eCheckErr_Success;
                }
                else
                {
                    return EnumErrorCodeCheckUse.eCheckErr_NoneTarget;
                }
            }
        }
        /// <summary>
        /// 技能使用
        /// </summary>
        /// <param name="param"></param>
        public virtual void OnUse(UseSkillParam param)
        {
            this.m_log.Debug("OnUse:" + this.m_unskillId);
            UseSkillEvent useSkillEvent = new UseSkillEvent();
            useSkillEvent.DurationTime = 1f;
            useSkillEvent.UseSkillParam = param;
            Singleton<ActEventManager>.singleton.AddEvent(useSkillEvent);
        }
        /// <summary>
        /// 具体技能使用表现
        /// </summary>
        /// <param name="useSkillParam"></param>
        public virtual void OnUseSkillAction(UseSkillParam useSkillParam)
        {
            if (useSkillParam != null)
            {
                Beast beast = Singleton<BeastManager>.singleton.GetBeastById(useSkillParam.m_dwRoleId);
                if (beast != null)
                {
                    Vector3 forward = beast.Forward;
                    if (useSkillParam.m_dwTargetRoleId > 0 && useSkillParam.m_dwTargetRoleId != useSkillParam.m_dwRoleId)
                    {
                        Beast targetBeast = Singleton<BeastManager>.singleton.GetBeastById(useSkillParam.m_dwTargetRoleId);
                        if (targetBeast != null)
                        {
                            forward = beast.RealPos3D - targetBeast.RealPos3D;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 取得技能的范围所以格子
        /// </summary>
        /// <param name="beastId"></param>
        /// <returns></returns>
        public virtual List<CVector3> GetCastRange(long beastId)
        {
            List<CVector3> result = new List<CVector3>();
            int useDistanceMin = this.GetUseDistanceMin(beastId);
            int useDistance = this.GetUseDistance(beastId);
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(beastId);
            if (beast != null)
            {
                Singleton<ClientMain>.singleton.scene.GetNearNodesIgnoreObstruct(useDistanceMin, useDistance, beast.Pos, ref result, true, true);
            }
            return result;
        }
        /// <summary>
        /// 取得攻击范围内的所有格子
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        /// <returns></returns>
        public virtual List<CVector3> GetValidTargetHexs(long unMasterBeastId)
        {
            List<CVector3> list = new List<CVector3>();
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(unMasterBeastId);
            if (beast == null || beast.IsError)
            {
                return list;
            }
            else
            {
                int useDistance = this.GetUseDistance(unMasterBeastId);
                Singleton<ClientMain>.singleton.scene.GetNearNodesIgnoreObstruct(useDistance,beast.Pos,ref list);
            }
            return list;
        }
        public virtual List<long> GetValidTargetPlayers(long unMasterBeastId)
        {
            List<long> list = new List<long>();
            return list;
        }
        public virtual List<CVector3> GetAffectAreaByTargetPos(long unMasterBeastId,CVector3 vecTargetHexPos)
        {
            return new List<CVector3> { vecTargetHexPos };
        }
        public virtual List<long> GetAffectBeastsByTargetPos(long unMasterBeastId, CVector3 vecTargetHexPos)
        {
            List<long> list = new List<long>();
            Beast beast = Singleton<BeastManager>.singleton.GetBeastByPos(vecTargetHexPos);
            if (beast != null || !beast.IsError)
            {
                list.Add(beast.Id);
            }
            return list;
        }
        #endregion
    }
}
