using UnityEngine;
using System.Collections.Generic;
using Client.UI.UICommon;
/*----------------------------------------------------------------
// 模块名：XUIProgress
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.10
// 模块描述：进度条
//--------------------------------------------------------------*/
/// <summary>
/// 进度条
/// </summary>
[AddComponentMenu("XUI/XUIProgress")]
public class XUIProgress : XUIObject, IXUIObject, IXUIProgress
{
    private UISlider m_uiSlider;

    private UISprite m_uiSpriteFG;

    public float value
    {
        get
        {
            return this.m_uiSlider.value;
        }
        set
        {
            this.m_uiSlider.value = value;
        }
    }

    public Color Color
    {
        get
        {
            if (null != this.m_uiSpriteFG)
            {
                return this.m_uiSpriteFG.color;
            }
            return Color.white;
        }
        set
        {
            if (null != this.m_uiSpriteFG)
            {
                this.m_uiSpriteFG.color = value;
            }
        }
    }

    public override void Init()
    {
        base.Init();
        this.m_uiSlider = base.GetComponent<UISlider>();
        if (null == this.m_uiSlider)
        {
            Debug.LogError("null == m_uiSlider");
        }
        else if (this.m_uiSlider.foregroundWidget != null)
        {
            this.m_uiSpriteFG = this.m_uiSlider.foregroundWidget.GetComponent<UISprite>();
        }
        if (null == this.m_uiSpriteFG)
        {
            Debug.LogError("null == m_uiSpriteFG");
        }
    }
}

