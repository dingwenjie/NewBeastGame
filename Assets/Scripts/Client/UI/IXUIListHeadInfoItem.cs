using UnityEngine;
using System.Collections.Generic;
/*----------------------------------------------------------------
// 模块名：IXUIListHeadInfoItem
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.10
// 模块描述：HUD头顶信息接口
//--------------------------------------------------------------*/
/// <summary>
/// HUD头顶信息接口
/// </summary>

namespace Client.UI.UICommon
{
    public interface IXUIListHeadInfoItem : IXUIListItem, IXUIObject
    {
        bool IsFriend
        {
            get;
            set;
        }

        void SetHp(int unHp, int unHpMax);

        IXUIListItem AddStatus(string strStatus);

        void ClearStatus();
    }
}
