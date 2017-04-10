﻿using UnityEngine;
using System.Collections.Generic;
using Client.UI.UICommon;
using UILib.Export;
using Client.UI;
using Utility;
using Utility.Export;
using Client.Data;
using System.Linq;
/*----------------------------------------------------------------
// 模块名：DlgHeadInfo
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.10
// 模块描述：神兽头顶HUD信息条
//--------------------------------------------------------------*/
/// <summary>
/// 神兽头顶HUD信息条
/// </summary>
public class DlgHeadInfo : DlgBase<DlgHeadInfo,DlgHeadInfoBehaviour>
{
    public class HeadInfoEntity
    {
        private IXUIListHeadInfoItem m_uiHeadInfoItem;

        private long m_unBeastId;

        public IXUIListHeadInfoItem HeadInfoItem
        {
            get
            {
                return this.m_uiHeadInfoItem;
            }
        }

        public Beast TargetBeast
        {
            get
            {
                return Singleton<BeastManager>.singleton.GetBeastById(this.m_unBeastId);
            }
        }

        public HeadInfoEntity(long unBeastId, IXUIListHeadInfoItem uiHeadInfo)
        {
            this.m_unBeastId = unBeastId;
            this.m_uiHeadInfoItem = uiHeadInfo;
            Beast heroById = Singleton<BeastManager>.singleton.GetBeastById(unBeastId);
            uiHeadInfo.Id = heroById.BeastTypeId;
        }
    }
    //所有神兽的HUD信息字典
    private Dictionary<long, DlgHeadInfo.HeadInfoEntity> m_dicBeastHeadInfo = new Dictionary<long, DlgHeadInfo.HeadInfoEntity>();

    private IXLog m_log = XLog.GetLog<DlgHeadInfo>();

    public override string fileName
    {
        get
        {
            return "DlgHeadInfo";
        }
    }

    public override int layer
    {
        get
        {
            return 4;
        }
    }

    public override void Init()
    {
        base.uiBehaviour.m_List_HeadInfo.Clear();
    }
    protected override void OnShow()
    {
        Debug.Log("ONFEWFWEFE");
        base.OnShow();
        this.OnRefresh();
    }
    protected override void OnRefresh()
    {
        Debug.Log("OnRefresh");
        base.OnRefresh();
        List<long> allBeastIds = Singleton<BeastManager>.singleton.GetAllBeastIds();
        List<long> exitsIds = this.m_dicBeastHeadInfo.Keys.ToList<long>();
        for (int i = 0; i < allBeastIds.Count; i++)
        {
            long id = allBeastIds[i];
            exitsIds.Remove(id);
            Debug.Log("OnRefreshID:"+id);
            if (!this.m_dicBeastHeadInfo.ContainsKey(id))
            {
                Debug.Log("OnRefresh1");
                IXUIListHeadInfoItem iXUIListHeadInfoItem = base.uiBehaviour.m_List_HeadInfo.AddListItem() as IXUIListHeadInfoItem;             
                DlgHeadInfo.HeadInfoEntity headInfoEntity = new DlgHeadInfo.HeadInfoEntity(id, iXUIListHeadInfoItem);
                this.m_dicBeastHeadInfo.Add(id, headInfoEntity);
                this.Translate(headInfoEntity.TargetBeast, iXUIListHeadInfoItem, false);
                this.Refresh(headInfoEntity.TargetBeast);
            }
        }
    }
    /// <summary>
    /// 更新神兽头顶的HUD信息是否显示
    /// </summary>
    /// <param name="headInfo"></param>
    /// <param name="player"></param>
    public void UpdateHeadeInfoVisible(DlgHeadInfo.HeadInfoEntity headInfo, Beast player)
    {
        Debug.Log("333");
        if (player != null)
        {
            if (!player.IsVisible)
            {
                headInfo.HeadInfoItem.SetVisible(false);
            }
            else
            {
                Debug.Log("33e2e3");
                headInfo.HeadInfoItem.SetVisible(!player.HideHeadInfo);
            }
        }
    }

    private void Translate(Beast beast, IXUIListItem uiListItem, bool bSmooth)
    {
        if (beast != null && uiListItem != null)
        {
            if (null != beast.Object)
            {
                IXUIListHeadInfoItem iXUIListHeadInfoItem = uiListItem as IXUIListHeadInfoItem;
                if (iXUIListHeadInfoItem == null)
                {
                    XLog.Log.Debug("null == uiListItemHeadInfo");
                }
                else
                {
                    Vector3 movingPos = beast.MovingPos;
                    movingPos.y += beast.Height + 0.5f;
                    Vector3 position = Camera.main.WorldToScreenPoint(movingPos);
                    Vector3 vector = Singleton<UIManager>.singleton.UICamera.ScreenToWorldPoint(position);
                    Vector3 position2 = iXUIListHeadInfoItem.CachedGameObject.transform.position;
                    Vector3 position3 = bSmooth ? Vector3.Lerp(position2, vector, 0.6f) : vector;
                    iXUIListHeadInfoItem.CachedTransform.position = position3;
                    Vector3 localPosition = iXUIListHeadInfoItem.CachedTransform.localPosition;
                    localPosition.z = vector.z / 100f;
                    iXUIListHeadInfoItem.CachedTransform.localPosition = localPosition;
                }
            }
        }
    }

    public void Refresh(Beast beast)
    {
        Debug.Log("Head Role Refresh");
        if (null != beast)
        {
            Debug.Log("Head Role Refresh1");
            DlgHeadInfo.HeadInfoEntity headInfoEntity = null;
            if (this.m_dicBeastHeadInfo.TryGetValue(beast.Id, out headInfoEntity))
            {
                Debug.Log("Head Role Refresh2");
                if (null != headInfoEntity)
                {
                    if (null != headInfoEntity.HeadInfoItem)
                    {
                        Debug.Log("Head Role Refresh3");
                        this.UpdateHeadeInfoVisible(headInfoEntity, beast);
                        headInfoEntity.HeadInfoItem.IsFriend = (beast.eCampType == Singleton<BeastRole>.singleton.CampType);
                        headInfoEntity.HeadInfoItem.SetText("lb_hp", beast.Hp.ToString());
                        headInfoEntity.HeadInfoItem.SetText("lb_name", beast.Player.Name);
                        this.UpdatePlayerHp(beast, headInfoEntity.HeadInfoItem);
                        //this.UpdatePlayerStatus(beast, headInfoEntity.HeadInfoItem);
                        //this.UpdatebeastCardStatus(beast, headInfoEntity.HeadInfoItem);
                    }
                }
            }
        }
    }
    private void UpdatePlayerHp(Beast beast, IXUIListHeadInfoItem uiListHeadInfoItem)
    {
        if (beast != null && null != uiListHeadInfoItem)
        {
            float num = beast.Hp / beast.HpMax;
            IXUIProgress iXUIProgress = uiListHeadInfoItem.GetUIObject("sp_hpred") as IXUIProgress;
            iXUIProgress.value = num;
            CampData ourCampData = Singleton<RoomManager>.singleton.GetOurCampData();
            CampData enemyCampData = Singleton<RoomManager>.singleton.GetEnemyCampData();
            if (beast.eCampType == ourCampData.CampType)
            {
                uiListHeadInfoItem.SetSprite("sp_bighp", "BlueHeadInfo");
                uiListHeadInfoItem.SetSprite("sp_hpred", "bluehp");
                //uiListHeadInfoItem.SetSprite("Sprite_Light_Green", "Light_Green");
            }
            else
            {
                uiListHeadInfoItem.SetSprite("sp_bighp", "RedHeadInfo");
                uiListHeadInfoItem.SetSprite("sp_hpred", "redhp");
                //uiListHeadInfoItem.SetSprite("Sprite_Light_Green", "Light_Red");
            }
            /*IXUISprite iXUISprite = uiListHeadInfoItem.GetUIObject("Sprite_Light_Green") as IXUISprite;
            if (iXUISprite != null)
            {
                if (0f < num && num < 1f)
                {
                    iXUISprite.SetVisible(true);
                    Vector3 localPosition = iXUISprite.CachedGameObject.transform.localPosition;
                    iXUISprite.CachedGameObject.transform.localPosition = new Vector3(72.8f - 132.5f * (1f - num), localPosition.y, localPosition.z);
                }
                else
                {
                    iXUISprite.SetVisible(false);
                }
            }*/
        }
    }
}
