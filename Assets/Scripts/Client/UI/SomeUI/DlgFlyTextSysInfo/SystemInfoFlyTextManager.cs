using UnityEngine;
using System.Collections.Generic;
using Client.UI;
using Client.UI.UICommon;
#region 模块信息
/*----------------------------------------------------------------
// 模块名SystemInfoFlyTextManager
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.6
// 模块描述：系统提示消息漂浮管理器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 系统提示消息漂浮管理器
/// </summary>
internal class SystemInfoFlyTextManager : FlyTextManagerBase,IFlyTextManager
{
    public SystemInfoFlyTextManager(IXUIList list) :base(list)
    {
        this.m_fTotalTime = 2.5f;
    }
    protected override void InitFlyText(FlyTextEntity flyText, string strText, long targetBeast)
    {
        base.InitFlyText(flyText, strText, targetBeast);
        flyText.Label.SetText(strText);
        flyText.FlyTextItem.CachedGameObject.SetActive(false);
        flyText.FlyTextItem.CachedGameObject.SetActive (true);
        Animation anim = flyText.FlyTextItem.CachedGameObject.GetComponent<Animation>();
        if (anim != null)
        {
            anim.Stop();
            anim.Play();
        }
    }
    public override FlyTextEntity Add(string strText, long targetBeastId, float PosZ)
    {
        FlyTextEntity flyTextEntity = null;
        if (this.m_flyTextList.First != null)
        {
            flyTextEntity = this.m_flyTextList.First.Value;
            flyTextEntity.Active = true;
        }
        else
        {
            IXUIListItem item = this.m_uiList.AddListItem();
            if (item != null)
            {
                flyTextEntity = new FlyTextEntity(item, targetBeastId);
                this.m_flyTextList.AddFirst(flyTextEntity);
            }
        }
        this.InitFlyText(flyTextEntity, strText, targetBeastId);
        return flyTextEntity;
    }
    protected override void Translate(ref FlyTextEntity flyText, float fElapseTime)
    {
        
    }
}
