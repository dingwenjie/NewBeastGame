using UnityEngine;
using System.Collections.Generic;
using Client.UI.UICommon;
/*----------------------------------------------------------------
// 模块名：XUIListHeadInfoItem
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.10
// 模块描述：HUD头顶信息
//--------------------------------------------------------------*/
/// <summary>
/// HUD头顶信息
/// </summary>
public class XUIListHeadInfoItem : XUIListItem, IXUIObject, IXUIListItem, IXUIListHeadInfoItem
{
    public IXUIList m_List_Hp;
    public IXUIProgress m_Progress_Time;
    private bool m_bIsFriend;
    private int m_unHp;
    private int m_unHpMax = 10;
    public bool IsFriend
    {
        get
        {
            return this.m_bIsFriend;
        }
        set
        {
            this.m_bIsFriend = value;
            if (this.m_List_Hp != null)
            {
                for (int i = 0; i < this.m_List_Hp.Count; i++)
                {
                    IXUIListItem item = this.m_List_Hp.GetItemByIndex(i);
                    if (item != null)
                    {
                        string hpBarSprite = (!this.m_bIsFriend) ? "redhp" : "bluehp";
                        item.SetIconSprite(hpBarSprite);
                    }
                }
            }
            IXUISprite iXUISprite = this.GetUIObject("sp_bighp") as IXUISprite;
            if (iXUISprite != null)
            {
                string sprite = (!this.m_bIsFriend) ? "RedHeadInfo" : "BlueHeadInfo";
                iXUISprite.SetSprite(sprite);
            }
        }
    }
    public Color HpColor
    {
        get
        {
            return Color.white;
        }
        set
        {
            if (this.m_List_Hp != null)
            {
                for (int i = 0; i < this.m_List_Hp.Count; i++)
                {
                    IXUIListItem itemByIndex = this.m_List_Hp.GetItemByIndex(i);
                    if (itemByIndex != null)
                    {
                        itemByIndex.SetColor(value);
                    }
                }
            }
        }
    }
    public void SetHp(int unHp, int unHpMax)
    {
        this.m_unHp = unHp;
        this.m_unHpMax = unHpMax;
        this.RefreshHp();
    }

    public IXUIListItem AddStatus(string strStatus)
    {
        return null;
    }

    public void ClearStatus()
    {
        
    }
    public override void Init()
    {
        base.Init();
        this.m_List_Hp = this.GetUIObject("list_hp") as IXUIList;
        this.m_Progress_Time = this.GetUIObject("pb_time") as IXUIProgress;
    }


    private void RefreshHp()
    {
        if (this.m_List_Hp == null)
        {
            return;
        }
        float cellwidth = 150f / this.m_unHpMax;
        if (this.m_unHpMax != this.m_List_Hp.Count)
        {
            while (this.m_unHpMax > this.m_List_Hp.Count)
            {
                this.m_List_Hp.AddListItem();
            }
            while (this.m_unHpMax < this.m_List_Hp.Count)
            {
                this.m_List_Hp.DelItemByIndex(this.m_List_Hp.Count - 1);
            }
            this.m_List_Hp.SetSize(cellwidth, 18f);
            this.IsFriend = this.m_bIsFriend;
        }
        for (int i = 0; i < this.m_List_Hp.Count; i++)
        {
            IXUIListItem itemByIndex = this.m_List_Hp.GetItemByIndex(i);
            if (itemByIndex != null)
            {
                if (i < this.m_unHp)
                {
                    itemByIndex.SetVisible(true);
                }
                else
                {
                    itemByIndex.SetVisible(false);
                }
                itemByIndex.SetSize(cellwidth, 18f);
            }
        }
        this.m_List_Hp.Refresh();
    }
}
