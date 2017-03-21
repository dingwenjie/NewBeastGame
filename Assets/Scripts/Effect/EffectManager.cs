using UnityEngine;
using System.Collections.Generic;
using Effect.Export;
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
    }
}
