using UnityEngine;
using System.Collections.Generic;
using Effect.Export;
using Effect;
using Client.UI.UICommon;
using Utility;
using GameClient.Audio;
using Client.UI;
using Game;
using System;
#region 模块信息
/*----------------------------------------------------------------
// 模块名EffectManager
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.21
// 模块描述：特效管理器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 特效管理器
/// </summary>
namespace Client.Effect
{
    public class EffectManager : EffectManagerBase
    {
        private static EffectManager m_sInstance = new EffectManager();
        public static EffectManager Instance
        {
            get { return m_sInstance; }
        }
        #region 属性
        public bool HighLight
        {
            get
            {
                return Singleton<EffectManagerImplement>.singleton.HighLight;
            }
            set
            {
                Singleton<EffectManagerImplement>.singleton.HighLight = value;
            }
        }
        #endregion

        public void Init()
        {
            this.ClearEffectData();
            EffectManagerBase.SetAudioManager(Singleton<AudioManager>.singleton);
            EffectManagerBase.SetCameraManager(CameraManager.Instance);
            EffectManagerBase.SetUIManager(UIManager.singleton);
            string strConfigFile = "config/effect.xml";
            base.LoadXml(strConfigFile);
            strConfigFile = "config/effectskill.xml";
            base.LoadXml(strConfigFile);
        }

        public void Update()
        {
            try
            {
                Singleton<EffectManagerImplement>.singleton.Update();
            }
            catch (Exception message)
            {
                EffectLogger.Fatal(message);
            }
        }
        /// <summary>
        /// 取得技能攻击到目标时间(目标神兽)
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId, long castId, long targetId)
        {
            Beast attacker = Singleton<BeastManager>.singleton.GetBeastById(castId);
            Beast beAttacker = Singleton<BeastManager>.singleton.GetBeastById(targetId);
            return base.GetEffectHitTime(effectId,attacker,beAttacker);
        }
        /// <summary>
        /// 取得技能攻击特效的存活时间(目标神兽 + 技能类型)
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId, long castId, long targetId, EffectInstanceType type)
        {
            return 0;
        }
        /// <summary>
        /// 取得技能攻击特效的存活时间（目标地点）
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId, long castId, Vector3 targetPos)
        {
            Beast attacker = Singleton<BeastManager>.singleton.GetBeastById(castId);
            return base.GetEffectHitTime(effectId, attacker, targetPos);
        }
        /// <summary>
        /// 取得技能攻击特效的存活时间（目标地点 + 技能类型）
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetPos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId, long castId, Vector3 targetPos, EffectInstanceType type)
        {
            return 0;
        }

        #region PlayEffect
        /// <summary>
        /// 播放特效
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unCastId"></param>
        /// <param name="vec3SrcPos"></param>
        /// <param name="uiObjCast"></param>
        /// <param name="unTargetId"></param>
        /// <param name="vec3DestPos"></param>
        /// <param name="uiObjTarget"></param>
        /// <param name="vec3FixDir"></param>
        /// <returns></returns>
        public int PlayEffect(int id, long unCastId, Vector3 vec3SrcPos, IXUIObject uiObjCast, long unTargetId, Vector3 vec3DestPos, IXUIObject uiObjTarget, Vector3 vec3FixDir)
        {
            Beast caster = Singleton<BeastManager>.singleton.GetBeastById(unCastId);
            Beast target = Singleton<BeastManager>.singleton.GetBeastById(unTargetId);
            return base.PlayEffect(id, caster, vec3SrcPos, uiObjCast, target, vec3DestPos, uiObjTarget, vec3FixDir);
        }
        public int PlayEffect(int id, long unCastId, long unTargetId)
        {
            Beast caster = Singleton<BeastManager>.singleton.GetBeastById(unCastId);
            Beast target = Singleton<BeastManager>.singleton.GetBeastById(unTargetId);
            return base.PlayEffect(id, caster, Vector3.zero, null, target, Vector3.zero, null, Vector3.zero);
        }
        public int PlayEffect(int id, long casterId, Vector3 targetPos)
        {
            Beast caster = Singleton<BeastManager>.singleton.GetBeastById(casterId);
            return base.PlayEffect(id, caster, Vector3.zero, null, null, targetPos, null, Vector3.zero);
        }
        #endregion
        /// <summary>
        /// 取得特效摄像机跟随的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetEffectCameraControlType(int effectId)
        {
            return 0;
        }
        /// <summary>
        /// 取得特效摄像机控制时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetEffectCameraControlTime(int id)
        {
            return 0f;
        }
        /// <summary>
        /// 取得特效摄像机控制延迟时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetEffectCameraControlDelay(int id)
        {
            return 0f;
        }
    }
}

