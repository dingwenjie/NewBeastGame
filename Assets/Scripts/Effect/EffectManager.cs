using UnityEngine;
using System.Collections.Generic;
using Effect.Export;
using Effect;
using Client.UI.UICommon;
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
        /// <summary>
        /// 取得技能攻击特效的存活时间(目标神兽)
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId,long castId,long targetId)
        {
            return 0;
        }
        /// <summary>
        /// 取得技能攻击特效的存活时间(目标神兽 + 技能类型)
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="castId"></param>
        /// <param name="targetId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public float GetEffectHitTime(int effectId, long castId, long targetId,EffectInstanceType type)
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
            return 0;
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
            return 0;
        }
    }
}
