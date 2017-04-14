using UnityEngine;
using System.Collections.Generic;
/*----------------------------------------------------------------
// 模块名：ICameraManager
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.12
// 模块描述：摄像机管理器接口
//--------------------------------------------------------------*/
/// <summary>
/// 摄像机管理器接口
/// </summary>
public interface ICameraManager
{
    void SetCamerPosAndDir(Vector3 vPos, Vector3 vDir);

    void BeginCtrlByEffect();

    void EndCtrlByEffect();

    void SetCameraFov(float fFov);
}
