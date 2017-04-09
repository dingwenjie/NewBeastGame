using UnityEngine;
using System;
using System.Collections.Generic;
using Client.UI.UICommon;
using Client.UI;
using Client.Common;
using Utility.Export;
#region 模块信息
/*----------------------------------------------------------------
// 模块名DlgFlyTextSysInfo
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.5
// 模块描述：系统提示漂浮文字
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 系统提示漂浮文字
/// </summary>
public class DlgFlyTextSysInfo : DlgBase<DlgFlyTextSysInfo,DlgFlyTextSysInfoBehaviour>
{
    private Dictionary<enumFlyTextType, IFlyTextManager> m_dicFlyTextManager = new Dictionary<enumFlyTextType, IFlyTextManager>();
    private IXLog m_log = XLog.GetLog<DlgFlyTextSysInfo>();

    public override string fileName
    {
        get
        {
            return "DlgFlyTextSysInfo";
        }
    }
    public override uint Type
    {
        get
        {
            return 65536u;
        }
    }
    public override int layer
    {
        get
        {
            return -6;
        }
    }
    public override EnumDlgCamera ShowType
    {
        get
        {
            return EnumDlgCamera.Top;
        }
    }

    public override bool IsPersist
    {
        get
        {
            return true;
        }
    }
    public override void Init()
    {
        base.Init();
        this.m_dicFlyTextManager.Add(enumFlyTextType.eFlyTextType_SystemInfo, new SystemInfoFlyTextManager(this.uiBehaviour.m_List_Info));
    }
    public override void RegisterEvent()
    {
        
    }
    protected override void OnShow()
    {
        
    }
    public override void Update()
    {
        base.Update();
        foreach (var current in this.m_dicFlyTextManager)
        {
            try
            {
                current.Value.Update();
            }
            catch (Exception e)
            {
                XLog.GetLog<DlgFlyTextSysInfo>().Fatal(e);
            }
        }
    }

    public void AddSystemInfo(string strText)
    {
        if (base.Prepared)
        {
            if (this.m_dicFlyTextManager.ContainsKey(enumFlyTextType.eFlyTextType_SystemInfo))
            {
                this.m_dicFlyTextManager[enumFlyTextType.eFlyTextType_SystemInfo].Add(strText, 0, 0);
            }
            XLog.GetLog<DlgFlyTextSysInfo>().Debug("AddSystemInfo:" + strText);
        }
    }
}
