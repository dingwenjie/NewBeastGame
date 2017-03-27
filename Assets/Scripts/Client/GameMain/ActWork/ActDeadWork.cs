using UnityEngine;
using System.Collections.Generic;
using Utility;
#region 模块信息
/*----------------------------------------------------------------
// 模块名ActDeadWork
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.24
// 模块描述：死亡具体表现
//----------------------------------------------------------------*/
#endregion
/// <summary>
/// 死亡具体表现
/// </summary>
public class ActDeadWork : ActWork
{
    public int EffectDead = 0;//玩家死亡特效
    public float StartDownTime = 0;//玩家开始躺下的时刻
    public float fDepth = 0;//玩家死亡之后距离地面的高度
    public float DownDurationTime = 0;//玩家躺下过程的时间间隔
    public ActDeadWork(long beastId,float fStartTime,float fDepth,float downDurtionTime,int deadEffect) : base(beastId)
    {
        this.StartDownTime = fStartTime + Time.time;
        this.DownDurationTime = downDurtionTime;
        this.fDepth = fDepth;
        this.EffectDead = deadEffect;
    }
    public override void Start()
    {
        base.Start();
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.BeastId);
        //死亡之后让玩家隐藏头部信息
        if (beast != null)
        {
            beast.HideHeadInfo = true;
        }
    }
    public override void End()
    {
        base.End();
        Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.BeastId);
        if (beast != null)
        {
            beast.SetVisible(false);
        }
    }
    public override void Update()
    {
        if (Time.time - this.StartDownTime > 0 && this.DownDurationTime > 0)
        {
            float deltaTime = Time.time - this.StartDownTime;
            float realTime = 1 / deltaTime;
            if (realTime >= 0 && realTime <= 1)
            {
                Beast beast = Singleton<BeastManager>.singleton.GetBeastById(this.BeastId);
                if (beast != null)
                {
                    Vector3 pos = beast.Object.transform.position;
                    pos.y = -this.fDepth * realTime;
                    beast.Object.transform.position = pos;
                }
            }
        }
        base.Update();
    }
}
