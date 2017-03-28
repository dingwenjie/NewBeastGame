using UnityEngine;
using System.Collections.Generic;
using Client.Data;
#region 模块信息
/*----------------------------------------------------------------
// 模块名DataCameraDist
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：摄像机数据
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 摄像机数据
/// </summary>
public class DataCameraDist : GameData<DataCameraDist>
{
    public static string fileName = "dataCameraDist";
	public int Distance
    {
        get; set;
    }
    public float CameraDist
    {
        get; set;
    }
    public static DataCameraDist GetDataByDistance(int distance)
    {
        foreach (var data in GameData<DataCameraDist>.dataMap.Values)
        {
            if (data.Distance.Equals(distance))
            {
                return data;
            }
        }
        return null;
    }
}
