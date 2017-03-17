using UnityEngine;
using System.Collections.Generic;
using Client.Common;
#region 模块信息
/*----------------------------------------------------------------
// 模块名SequenceBase
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.17
// 模块描述：顺序播放动作基类
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 顺序播放动作基类
/// </summary>
public abstract class SequenceBase
{
    private enumSequenceType type = enumSequenceType.e_Sequence_Skill;
    public List<SeqBuilder> builders = new List<SeqBuilder>();
    private bool m_bIsBuilded = false;
    private bool m_bIsFinished = false;

    public float StartTime
    {
        get; set;
    }
    public float EndTime
    {
        get; set;
    }
    public float LastAnimEndTime
    {
        get; set;
    }

    public SequenceBase()
    {
        this.AddSeqBuilder();
    }
    public abstract void AddSeqBuilder();

}
