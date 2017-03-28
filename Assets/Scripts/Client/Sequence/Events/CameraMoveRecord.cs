using UnityEngine;
using System.Collections.Generic;
#region 模块信息
/*----------------------------------------------------------------
// 模块名CameraMoveRecord
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.28
// 模块描述：摄像机移动记录
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 摄像机移动记录
/// </summary>
public class CameraMoveRecord
{
    public Vector3 RecorverDir
    {
        get;
        set;
    }

    public float RecoverDist
    {
        get;
        set;
    }

    public float RecoverScale
    {
        get;
        set;
    }

    public Vector3 RecoverLookAtPos
    {
        get;
        set;
    }

    public Vector3 RecoverCameraPos
    {
        get;
        set;
    }
}
