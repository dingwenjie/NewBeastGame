using UnityEngine;
using System.Collections.Generic;
using Client.Common;
using Utility;
/*----------------------------------------------------------------
// 模块名：MainStage
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.18
// 模块描述：顺序播放主阶段
//--------------------------------------------------------------*/
/// <summary>
/// 顺序播放主阶段
/// </summary>
public class MainStage : SeqBuilder
{
    public enumSequenceType showType = enumSequenceType.e_Sequence_Skill;
    public long AttackerId;//攻击者ID

    public override void BuildSeq()
    {
        float time2;
        switch (this.showType)
        {
            case enumSequenceType.e_Sequence_Skill:
                time2 = this
                break;
        }
    }

    public float BuildSkillAction()
    {
        Beast attackBeast = Singleton<BeastManager>.singleton.GetBeastById(this.AttackerId);
    }
}
