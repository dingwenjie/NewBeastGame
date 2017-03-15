using UnityEngine;
using System.Collections.Generic;
using Client.UI.UICommon;
#region 模块信息
/*----------------------------------------------------------------
// 模块名DlgFlyTextSysInfoBehaviour
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.5
// 模块描述：系统提示漂浮文字组件
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 系统提示漂浮文字组件
/// </summary>
public class DlgFlyTextSysInfoBehaviour : DlgBehaviourBase
{
    public IXUIList m_List_Info = null;
    public IXUIButton b = null;
    public override void Init()
    {
        base.Init();
        this.m_List_Info = base.GetUIObject("list_SystemInfo") as IXUIList;
        if (this.m_List_Info == null)
        {
            Debug.Log("this.m_List_Info == null");
        }
    }
}
