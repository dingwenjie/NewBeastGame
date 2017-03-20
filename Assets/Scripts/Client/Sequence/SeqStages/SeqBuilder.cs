using UnityEngine;
using System.Collections.Generic;
#region 模块信息
/*----------------------------------------------------------------
// 模块名SeqBuilder
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.17
// 模块描述：片段建造器
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 片段建造器
/// </summary>
public class SeqBuilder : SeqUpdatable
{
    public void Build(float startTime,float animEndTime)
    {
        this.StartTime = startTime;
        this.LastAnimEndTime = animEndTime;
        this.BuildSeq();
    }
    public virtual void BuildSeq()
    {
        this.EndTime = this.StartTime;
    }
}
