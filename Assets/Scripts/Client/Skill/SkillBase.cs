using UnityEngine;
using System.Collections.Generic;
using Utility.Export;
using Client.Data;
using Utility;
using Client.Common;
using Game;
using Client.GameMain;
using Client.Effect;
using Effect;
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
        /// 是否向前攻击
        /// </summary>
        public virtual bool IsAttackForward
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 是否被攻击向前
        /// </summary>
        public virtual bool IsBeAttackForward
        {
            get
            {
                return true;
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
            //按理来说应该根据skillId来判断是那个skill
            string anim = "Attack";
            if (this.m_unskillId == 1)
            {
                anim = "Attack";
            }
            return anim;
        }
        /// <summary>
        /// 取得攻击动作播放的时间
        /// </summary>
        /// <returns></returns>
        public virtual float GetHitTime(long attackerId,long targetId,Vector3 vTargetPos)
        {
            int attackerEffectId = 0;
            int BeAttackerEffectId = 0;
            this.GetEffectId(attackerId, ref attackerEffectId, ref BeAttackerEffectId);
            float effectTime = 0;
            if (targetId > 0)
            {
                effectTime = EffectManager.Instance.GetEffectHitTime(attackerEffectId, attackerId, targetId);
            }
            else
            {
                effectTime = EffectManager.Instance.GetEffectHitTime(attackerEffectId, attackerId, vTargetPos);
            }
            return effectTime;
        }
        /// <summary>
        /// 取得攻击动作播放的时间
        /// </summary>
        /// <param name="attackerId"></param>
        /// <param name="targetId"></param>
        /// <param name="vTargetPos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual float GetHitTime(long attackerId, long targetId, Vector3 vTargetPos, EffectInstanceType type)
        {
            int attackerEffectId = 0;
            int BeAttackerEffectId = 0;
            this.GetEffectId(attackerId, ref attackerEffectId, ref BeAttackerEffectId);
            float effectTime = 0;
            if (targetId > 0)
            {
                effectTime = EffectManager.Instance.GetEffectHitTime(attackerEffectId, attackerId, targetId,type);
            }
            else
            {
                effectTime = EffectManager.Instance.GetEffectHitTime(attackerEffectId, attackerId, vTargetPos,type);
            }
            return effectTime;
        }
        /// <summary>
        /// 取得技能动作的时间延迟
        /// </summary>
        /// <param name="attakerId"></param>
        /// <param name="beAttacker"></param>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        public virtual float GetDuration(long attakerId,List<long> beAttacker,Vector3 targetPos)
        {
            float result = 0;
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(attakerId);
            if (beast != null)
            {
                DataSkillShow data = beast.GetSkillShow(this.m_unskillId);
                if (data != null && data.SkillAction == 1)
                {
                    if (string.IsNullOrEmpty(data.AttackAction))
                    {
                        result = beast.GetAnimPlayTime(this.GetSkillAnimName(attakerId));
                    }
                    else
                    {
                        result = beast.GetAnimPlayTime(data.AttackAction);
                    }
                }
            }
            return result;
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
        /// 技能释放表现
        /// </summary>
        /// <param name="param"></param>
        public virtual void OnCastAction(CastSkillParam param)
        {
            if (param != null)
            {
                long masterBeastId = param.m_unMasterBeastId;
                XLog.Log.Debug("OnCast:" + this.m_unskillId);
                Beast beast = Singleton<BeastManager>.singleton.GetBeastById(masterBeastId);
                if (beast != null)
                {
                    DataSkillShow data = beast.GetSkillShow(this.m_unskillId);
                    if (data != null)
                    {
                        if (this.IsActive)
                        {
                            if (!data.IsEffectForward)
                            {
                                this.AdjustAttackDirection(param, beast);
                            }
                            this.AdjustBeAttackerDirection(param, beast);
                        }
                        if (data.SkillAction == 1)
                        {
                            if (string.IsNullOrEmpty(data.AttackAction))
                            {
                                beast.PlayAnim(this.GetSkillAnimName(masterBeastId));
                            }
                            else
                            {
                                beast.PlayAnim(data.AttackAction);
                            }
                        }
                    }
                }
            }
        }
        public virtual void OnCastSkillEffect(CastSkillParam castSkillParam)
        {

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
        /// 技能释放开始
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        /// <param name="unSkillId"></param>
        public virtual void OnCastSkillBegin(long unMasterBeastId, int unSkillId)
        {

        }
        /// <summary>
        /// 技能释放中
        /// </summary>
        /// <param name="castSkillParam"></param>
        public virtual void Cast(CastSkillParam castSkillParam)
        {
            DataSkill skillData = GameData<DataSkill>.dataMap[castSkillParam.unTargetSkillID];
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(castSkillParam.m_unMasterBeastId);

        }
        /// <summary>
        /// 技能释放结束
        /// </summary>
        /// <param name="unMasterBeastId"></param>
        /// <param name="unSkillId"></param>
        public virtual void OnCastSkillEnd(long unMasterBeastId, int unSkillId)
        {

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
        /// 调整攻击者的方向
        /// </summary>
        /// <param name="param"></param>
        /// <param name="masteBeast"></param>
        public virtual void AdjustAttackDirection(CastSkillParam param,Beast masteBeast)
        {
            Vector2 forward = Vector2.zero;
            if (null == param)
            {
                return;
            }
            if (param.listTargetRoleID.Count > 0)
            {
                if (param.listTargetRoleID[0] != masteBeast.Id)
                {
                    Beast beAttacker = Singleton<BeastManager>.singleton.GetBeastById(param.listTargetRoleID[0]);
                    if (beAttacker != null)
                    {
                        forward = beAttacker.RealPos2D - masteBeast.RealPos2D;
                        masteBeast.Forward = forward;
                    }
                }
            }
            else
            {
                Vector2 targetPos = Hexagon.GetHexPosByIndex(param.vec3TargetPos.nRow, param.vec3TargetPos.nCol, Space.World);
                forward = targetPos - masteBeast.RealPos2D;
                masteBeast.Forward = forward;
            }
        }
        /// <summary>
        /// 调整被攻击者的方向
        /// </summary>
        /// <param name="param"></param>
        /// <param name="masteBeast"></param>
        public virtual void AdjustBeAttackerDirection(CastSkillParam param, Beast masteBeast)
        {
            Vector2 forward = Vector2.zero;
            if (param != null)
            {
                foreach (var current in param.listTargetRoleID)
                {
                    Beast beast = Singleton<BeastManager>.singleton.GetBeastById(current);
                    if (beast != null)
                    {
                        forward = masteBeast.RealPos2D - beast.RealPos2D;
                        beast.Forward = forward; 
                    }
                }
            }
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
        #region 共有方法
        /// <summary>
        /// 根据神兽ID取得技能攻击的特效
        /// </summary>
        /// <param name="beastId"></param>
        /// <param name="attackerEffectId"></param>
        /// <param name="beAttackerEffectId"></param>
        /// <returns></returns>
        public bool GetEffectId(long beastId,ref int attackerEffectId,ref int beAttackerEffectId)
        {
            Beast beast = Singleton<BeastManager>.singleton.GetBeastById(beastId);
            if (null == beast)
            {
                return false;
            }
            //如果普通攻击的话，要根据神兽类型id * 100取得DataSkillShow
            else if (this.m_unskillId == 1)
            {
                DataSkillShow data = beast.GetSkillShow(beast.BeastTypeId * 100);
                if (data != null)
                {
                    attackerEffectId = data.AttackerEffectId;
                    beAttackerEffectId = data.BeAttackerEffectId;
                    return true;
                }
                else
                {
                    this.m_log.Debug("找不到该技能");
                }
            }
            else
            {
                DataSkillShow data = beast.GetSkillShow(this.m_unskillId);
                if (data != null)
                {
                    attackerEffectId = data.AttackerEffectId;
                    beAttackerEffectId = data.BeAttackerEffectId;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
