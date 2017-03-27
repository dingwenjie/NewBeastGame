using UnityEngine;
using System.Collections.Generic;
#region 模块信息
/*----------------------------------------------------------------
// 模块名ActWorkManager
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.24
// 模块描述：行为表现事件管理器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 行为表现事件管理器
/// </summary>
public class ActWorkManager
{
    public List<ActWork> m_listWork = new List<ActWork>();
    public void AddWork(ActWork work)
    {
        if (this.m_listWork != null && work != null)
        {
            this.m_listWork.Add(work);
            work.Start();
        }
    }
    public void Update()
    {
        for (int i = this.m_listWork.Count - 1; i > 0; i--)
        {
            ActWork work = this.m_listWork[i];
            work.Update();
            if (work.IsFinished)
            {
                work.End();
                work.IsFinished = true;
            }
        }
    }
}
