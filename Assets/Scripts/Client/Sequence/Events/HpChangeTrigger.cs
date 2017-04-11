using UnityEngine;
using System.Collections.Generic;
using Client.Common;
using Utility;
using Client.UI.UICommon;
using Client.UI;
#region 模块信息
/*----------------------------------------------------------------
// 模块名HpChangeTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.24
// 模块描述：血量改变表现事件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 血量改变表现事件
/// </summary>
public class HpChangeTrigger : Triggerable
{
	public long AttackId
    {
        get; set;
    }
    public long BeAttackId
    {
        get; set;
    }
    /// <summary>
    /// 新的血量
    /// </summary>
    public int HpValue
    {
        get; set;
    }
    /// <summary>
    /// 原来的血量
    /// </summary>
    public int OgrinHpValue
    {
        get; set;
    }
    public enumHpChangeType HpChangeType
    {
        get; set;
    }
    public override void Trigger()
    {
        Beast beAttackBeast = Singleton<BeastManager>.singleton.GetBeastById(BeAttackId);
        if (beAttackBeast != null)
        {
            Singleton<BeastManager>.singleton.OnBeastHpChangeAction(this.BeAttackId, this.HpValue);
        }
        int changeHpValue = this.HpValue - this.OgrinHpValue;
        Debug.Log("HPChange:" + changeHpValue);
        if (this.AttackId != this.BeAttackId)
        {
            //如果改变的血量为增值，就是加血类型
            if (changeHpValue > 0)
            {
                Debug.Log("AddHpEffect2");
                DlgBase<DlgFlyText, DlgFlyTextBehaviour>.singleton.AddHpEffect(changeHpValue, this.BeAttackId, EnumHpEffectType.eHpEffectType_Heal);
            }
            else
            {
                Debug.Log("AddHpEffect1");
                DlgBase<DlgFlyText, DlgFlyTextBehaviour>.singleton.AddHpEffect(changeHpValue, this.BeAttackId, EnumHpEffectType.eHpEffectType_Damage);
            }
        }
        else if (changeHpValue > 0)
        {
            //如果是对自己释放的话，就是加血
            DlgBase<DlgFlyText, DlgFlyTextBehaviour>.singleton.AddHpEffect(changeHpValue, this.BeAttackId, EnumHpEffectType.eHpEffectType_Heal);
        }
        else
        {
            DlgBase<DlgFlyText, DlgFlyTextBehaviour>.singleton.AddHpEffect(changeHpValue, this.BeAttackId, EnumHpEffectType.eHpEffectType_Damage);
        }
        //如果自己是被攻击者，就应该让摄像机显示屏幕变红的特效
    }
    /// <summary>
    /// 取得播放的长度
    /// </summary>
    /// <returns></returns>
    public float GetDuration()
    {
        return 0.2f;
    }
}
