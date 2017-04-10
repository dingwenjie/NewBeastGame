using UnityEngine;
using System.Collections.Generic;
using Client.UI.UICommon;
/*----------------------------------------------------------------
// 模块名：DlgHeadInfoBehaviour
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.10
// 模块描述：血条信息组件
//--------------------------------------------------------------*/
/// <summary>
/// 血条信息组件
/// </summary>
public class DlgHeadInfoBehaviour : DlgBehaviourBase
{
    public IXUIList m_List_HeadInfo = null;
    public override void Init()
    {
        base.Init();
        this.m_List_HeadInfo = (base.GetUIObject("list_headinfo") as IXUIList);
        if (null == this.m_List_HeadInfo)
        {
            Debug.Log("List_HeadInfo is null!");
        }
    }
}
