using UnityEngine;
using System.Collections.Generic;
using Utility;
using Client.Data;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CameraGoTrigger
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：摄像机的移动表现
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 摄像机的移动表现
/// </summary>
public class CameraGoTrigger : Triggerable
{
    public int SkillId = 0;
    public long AttackerId = 0;
    public float OffDis = 15f;

    public CameraMoveRecord record = null;
    public List<long> BeAttackIdList;
    public override void Trigger()
    {
        Beast attacker = Singleton<BeastManager>.singleton.GetBeastById(AttackerId);
        if (attacker != null && this.BeAttackIdList != null)
        {
            float disTemp = 0;
            foreach (var current in this.BeAttackIdList)
            {
                Beast beast = Singleton<BeastManager>.singleton.GetBeastById(current);
                if (beast != null)
                {
                    float dis = Vector3.Magnitude(attacker.MovingPos - beast.MovingPos);
                    if (dis > disTemp)
                    {
                        disTemp = dis;
                    }
                }
                int distance = (int)(disTemp / 1.4721999943256379f);
                if (this.record != null)
                {
                    DataCameraDist data = DataCameraDist.GetDataByDistance(distance);
                    if (data != null)
                    {
                        this.OffDis = data.CameraDist;
                    }
                }
            }
        }
    }
}
