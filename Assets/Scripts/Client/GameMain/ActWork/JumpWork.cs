using UnityEngine;
using System.Collections.Generic;
using Utility;
using Effect.Export;
using Client.Effect;
using Game;
using Utility.Export;
/*----------------------------------------------------------------
// 模块名：JumpWork
// 创建者：chen
// 修改者列表：
// 创建日期：2017.4.2
// 模块描述：跳跃表现
//--------------------------------------------------------------*/
/// <summary>
/// 跳跃表现
/// </summary>
public class JumpWork : ActWork
{

    public float m_fTrigerTime;

    public long m_AttackerId = 0;

    private Beast beast = null;

    private Vector3 m_vBeginPos = Vector3.zero;

    private Vector3 m_vEndPos = Vector3.zero;

    private float m_fDuration = 0f;

    private float g = 0f;

    private float v = 0f;

    public bool bInvDIr = true;

    private float m_fDelayTime = 0f;

    private int m_nEffectID = 0;

    public bool m_bTriggerAnim = false;

    private string m_strJumpEndAnim = string.Empty;

    private string m_strJumpDuraAnim = string.Empty;

    private bool m_bForward = false;

    public JumpWork(long uPlayerId, Vector3 BeginPos, Vector3 EndPos, float height, float delay, float duration, long AttId, int effctId) : base(uPlayerId)
    {
        this.m_fDuration = duration;
        if (this.m_fDuration < 0.03f)
        {
            this.m_fDuration = 0.03f;
        }
        this.m_vBeginPos = BeginPos;
        this.m_vEndPos = EndPos;
        this.g = 8f * height / (this.m_fDuration * this.m_fDuration);
        this.v = this.g * duration / 2f;
        this.m_AttackerId = AttId;
        this.m_fDelayTime = delay;
        this.m_nEffectID = effctId;
    }
    public JumpWork(long uPlayerId, Vector3 BeginPos, Vector3 EndPos, float height, float delay, float duration, long AttId, int effctId, string AnimName, string strDuraAnim, bool bForward) : base(uPlayerId)
    {
        this.m_bTriggerAnim = true;
        this.m_strJumpEndAnim = AnimName;
        this.m_strJumpDuraAnim = strDuraAnim;
        this.m_fDuration = duration;
        if (this.m_fDuration < 0.03f)
        {
            this.m_fDuration = 0.03f;
        }
        this.m_vBeginPos = BeginPos;
        this.m_vEndPos = EndPos;
        this.g = 8f * height / (this.m_fDuration * this.m_fDuration);
        this.v = this.g * this.m_fDuration / 2f;
        this.m_AttackerId = AttId;
        this.m_fDelayTime = delay;
        this.m_nEffectID = effctId;
        this.m_bForward = bForward;
    }
    public override void Start()
    {
        this.beast = Singleton<BeastManager>.singleton.GetBeastById(this.BeastId);
        this.m_fTrigerTime = Time.time + this.m_fDelayTime;
        EffectManager.Instance.PlayEffect(this.m_nEffectID, this.BeastId, 0);
        if (this.beast != null && !string.IsNullOrEmpty(this.m_strJumpDuraAnim))
        {
            this.beast.PlayAnim(this.m_strJumpDuraAnim);
        }
    }
    public override void Update()
    {
        float time = Time.time - this.m_fTrigerTime;
        if (time >= 0f && time <= this.m_fDuration)
        {
            if (this.beast != null)
            {
                float d = 1f;
                if (this.m_fDuration != 0f)
                {
                    d = time / this.m_fDuration;
                }
                Vector3 vector = this.m_vBeginPos + (this.m_vEndPos - this.m_vBeginPos) * d;
                vector.y = this.v * time - this.g * time * time / 2f;
                if (this.m_bForward)
                {
                    Beast heroById = Singleton<BeastManager>.singleton.GetBeastById(this.m_AttackerId);
                    if (heroById != BeastManager.BeastError)
                    {
                        Vector3 forward = heroById.MovingPos - vector;
                        forward.y = 0f;
                        this.beast.Object.transform.forward = forward;
                    }
                    else
                    {
                        Vector3 forward = vector - this.beast.MovingPos;
                        forward.y = 0f;
                        this.beast.Object.transform.forward = forward;
                    }
                }
                this.beast.Object.transform.position = vector;
            }
        }
        else if (time > 0f)
        {
            this.IsFinished = true;
        }
    }
    public override void End()
    {
        if (null != this.beast)
        {
            this.BeastId = 0u;
            CVector3 cVector = new CVector3();
            int nRow;
            int nCol;
            Hexagon.GetHexIndexByPos(this.m_vEndPos, out nRow, out nCol);
            cVector.nRow = nRow;
            cVector.nCol = nCol;
            this.beast.MoveAction(cVector);
            this.beast.UpdateTargetPosEffect(this.m_vEndPos);
            if (this.m_bForward)
            {
                Beast heroById = Singleton<BeastManager>.singleton.GetBeastById(this.m_AttackerId);
                if (heroById != BeastManager.BeastError)
                {
                    Vector3 a = heroById.MovingPos - this.m_vEndPos;
                    a.y = 0f;
                    if (Vector3.SqrMagnitude(a) > 0f)
                    {
                        this.beast.Forward = new Vector2(a.x, a.z);
                    }
                }
            }
            if (this.m_bTriggerAnim)
            {
                this.m_bTriggerAnim = false;
                if (!string.IsNullOrEmpty(this.m_strJumpEndAnim))
                {
                    this.beast.PlayAnim(this.m_strJumpEndAnim);
                }
            }
        }
    }
}
