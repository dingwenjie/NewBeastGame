using UnityEngine;
using System.Collections.Generic;
using Utility;
using System;
using Utility.Export;
using Game;
using Client.Common;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：SequenceShowManager
// 创建者：chen
// 修改者列表：
// 创建日期：2017.3.18
// 模块描述：顺序播放管理器
//----------------------------------------------------------------*/
#endregion
namespace Client.Data
{
    public class SequenceShowManager : Singleton<SequenceShowManager>
    {
        public List<SequenceBase> ShowList = new List<SequenceBase>();
        public List<int> delShowIdx = new List<int>();
        private Action m_eventHandlerOnPlayFinished = null;
        private IXLog m_log = XLog.GetLog<SequenceShowManager>();
        public bool UseSeqShow
        {
            get;
            set;
        }
        public float PreStageSeg
        {
            get;
            set;
        }
        public float ShowStageSeg
        {
            get;
            set;
        }
        public float StatusSeg
        {
            get;
            set;
        }
        public Vector3 LookAtSrcPos
        {
            get;
            set;
        }
        public Vector3 CameraSrcPos
        {
            get;
            set;
        }
        public bool CanRecevieMsg
        {
            get
            {
                return this.UseSeqShow && this.ShowList.Count > 0 && !this.ShowList[this.ShowList.Count - 1].Builded;
            }
        }
        public void Clear()
        {
            foreach (SequenceBase current in this.ShowList)
            {
                current.Clear();
            }
            this.ShowList.Clear();
        }
        /// <summary>
        /// 取得当前的顺序播放
        /// </summary>
        /// <returns></returns>
        public SequenceBase GetCurrentShow()
        {
            if (this.ShowList.Count > 0)
            {
                return this.ShowList[this.ShowList.Count - 1];
            }
            else
            {
                Debug.Log("当前没有可以播放的顺序播放列表");
                return null;
            }
        }

        public bool IsPlaying()
        {
            return this.ShowList.Count > 0;
        }

        public void RegisterPlayFinishEventHandle(Action eventHandler)
        {
            this.m_eventHandlerOnPlayFinished = (Action)Delegate.Combine(this.m_eventHandlerOnPlayFinished, eventHandler);
        }
        /// <summary>
        /// 取得移动的时间
        /// </summary>
        /// <param name="unHeroId"></param>
        /// <param name="nPosCount"></param>
        /// <param name="bCountMoveTime"></param>
        /// <returns></returns>
        public float GetWalkMoveTime(long unHeroId, int nPosCount, bool bCountMoveTime)
        {
            float result;
            if (bCountMoveTime && nPosCount > 1)
            {
                float num = 4f;
                Beast heroById = Singleton<BeastManager>.singleton.GetBeastById(unHeroId);
                if (heroById != null)
                {
                    num = heroById.Speed;
                }
                result = (float)(nPosCount - 1) * 0.85f * 1.732f / num;
            }
            else
            {
                result = 0f;
            }
            return result;
        }

        public void Update()
        {
            try
            {
                this.delShowIdx.Clear();
                for (int i = 0; i < this.ShowList.Count; i++)
                {
                    try
                    {
                        this.ShowList[i].Update();
                        if (GameConfig.singleton.SeqShowTimeOut > 0f && !this.ShowList[i].Builded && Time.time - this.ShowList[i].StartTime > GameConfig.singleton.SeqShowTimeOut)
                        {
                            if (this.ShowList[i].IsGameOver)
                            {
                                this.ShowList[i].BeginShow(this.ShowList[i].StartTime, this.ShowList[i].LastAnimEndTime);
                            }
                            else
                            {
                                this.ShowList[i].BeginShow(this.ShowList[i].StartTime, this.ShowList[i].LastAnimEndTime);
                                this.ShowList[i].ForceTrigger();
                                this.delShowIdx.Add(i);
                            }
                        }
                        else if (this.ShowList[i].End())
                        {
                            this.delShowIdx.Add(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.m_log.Fatal(string.Format(" Fatal Error in SeqShowManager Update {0}", ex.ToString()));
                    }
                }
                for (int i = this.delShowIdx.Count - 1; i >= 0; i--)
                {
                    this.ShowList.RemoveAt(this.delShowIdx[i]);
                }
                if (null != this.m_eventHandlerOnPlayFinished)
                {
                    if (this.ShowList.Count == 0 && this.m_eventHandlerOnPlayFinished != null)
                    {
                        this.m_eventHandlerOnPlayFinished();
                        this.m_eventHandlerOnPlayFinished = null;
                    }
                }
            }
            catch (Exception ex2)
            {
                this.m_log.Fatal(ex2.ToString());
            }
        }
        #region OnMsg
        public void OnMsg(CPtcM2CNtf_CastSkill msg)
        {
            this.NewShow(enumSequenceType.e_Sequence_Skill);
            this.GetCurrentShow().OnMsg(msg);
        }
        public void OnMsg(CPtcM2CNtf_EndCastSkill msg)
        {
            this.GetCurrentShow().OnMsg(msg);
        }
        public void OnMsg(CptcM2CNtf_ChangeHp msg,int orgiHpVal)
        {
            this.GetCurrentShow().OnMsg(msg,orgiHpVal);
        }
        #endregion

        /// <summary>
        /// 创建一个新的顺序播放器，加到ShowList列表中
        /// </summary>
        /// <param name="type"></param>
        private void NewShow(enumSequenceType type)
        {
            SequenceBase sequence;
            switch (type)
            {
                case enumSequenceType.e_Sequence_Skill:
                case enumSequenceType.e_Sequence_Equip:
                    sequence = new SequenceShow();
                    break;
                default:
                    sequence = new SequenceShow();
                    break;
            }
            sequence.type = type;
            if (this.ShowList.Count == 0)
            {
                sequence.StartTime = Time.time;
                sequence.LastAnimEndTime = Time.time;
            }
            else
            {
                SequenceBase curSequence = this.GetCurrentShow();
                try
                {
                    sequence.StartTime = curSequence.EndTime + 0.1f;
                    sequence.LastAnimEndTime = curSequence.LastAnimEndTime;
                    if (Time.time > sequence.StartTime)
                    {
                        sequence.StartTime = Time.time;
                    }
                    if (Time.time > sequence.LastAnimEndTime)
                    {
                        sequence.LastAnimEndTime = Time.time;
                    }
                }
                catch (Exception e)
                {
                    this.m_log.Fatal(e);
                }
            }
            this.ShowList.Add(sequence);
        }        
    }
}
