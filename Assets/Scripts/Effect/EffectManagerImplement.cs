using UnityEngine;
using System.Collections.Generic;
using Utility;
using UnityAssetEx.Export;
using System.Xml;
using System;
using Effect.Export;
using Game;
using Client.Common;
using Client.UI.UICommon;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：EffectManagerImplement
// 创建者：chen
// 修改者列表：
// 创建日期：2016.3.27
// 模块描述：特效管理实现类
//----------------------------------------------------------------*/
#endregion
namespace Effect
{
    /// <summary>
    // 模块描述：特效管理实现类
    /// </summary>
    internal class EffectManagerImplement : Singleton<EffectManagerImplement>
    {
        #region 字段
        public delegate void CameraCtrlByEffect();
        private const int MaxEffectNum = 100;
        private const int PlayerEffectFailed = -1;
        private Camera m_MainCamera;
        public EffectManagerImplement.CameraCtrlByEffect BeginCameraControll = null;
        public EffectManagerImplement.CameraCtrlByEffect EndCameraControll = null;
        private bool m_bHighLight = false;
        private Dictionary<int, EffectData> m_EffectDatas = new Dictionary<int, EffectData>();
        private Dictionary<int, Effect> m_Effects = new Dictionary<int, Effect>();
        private Dictionary<int, Effect> m_HightLightEffect = new Dictionary<int, Effect>();
        public Dictionary<int, List<IAssetRequest>> m_dicAllPreLoadAssetRequest = new Dictionary<int, List<IAssetRequest>>();
        private ushort m_ID;
        #endregion
        #region 属性
        public static int ErrorEffectId
        {
            get
            {
                return -1;
            }
        }
        public bool HighLight
        {
            get
            {
                return this.m_bHighLight;
            }
            set
            {
                this.m_bHighLight = value;
                if (!this.m_bHighLight)
                {
                    this.RevertHightedEffect();
                }
            }
        }
        public Dictionary<int, EffectData> EffectDatas
        {
            get
            {
                return this.m_EffectDatas;
            }
        }
        #endregion
        #region 构造方法
        static EffectManagerImplement()
        {
        }
        #endregion
        #region 公有方法
        /// <summary>
        /// 设置主摄像机
        /// </summary>
        /// <param name="mMainCamera"></param>
        public void SetMainCamera(Camera mMainCamera)
        {
            this.m_MainCamera = mMainCamera;
        }
        /// <summary>
        /// 清除特效
        /// </summary>
        public void ClearEffectData()
        {
            this.m_EffectDatas.Clear();
        }
        public void LoadXml(string strConfigFile)
        {
            if (string.IsNullOrEmpty(strConfigFile))
            {
                EffectLogger.Error("LoadXml:string.IsNullOrEmpty(strConfigFile) == true");
            }
            else
            {
                try
                {
                    XmlDocument xmlDocument = XmlResAdapter.GetXmlDocument(ResourceManager.GetFullPath(strConfigFile, false));
                    XmlNode xmlNode = xmlDocument.SelectSingleNode("EffectPool");
                    if (null == xmlNode)
                    {
                        EffectLogger.Error("null == root");
                    }
                    else
                    {
                        XmlNodeList xmlNodeList = xmlNode.SelectNodes("Effect");
                        if (null == xmlNodeList)
                        {
                            EffectLogger.Error("null == nodeList");
                        }
                        else
                        {
                            foreach (XmlNode effectNode in xmlNodeList)
                            {
                                EffectData effectData = new EffectData();
                                if (effectData.Load(effectNode))
                                {
                                    if (!this.m_EffectDatas.ContainsKey(effectData.Id))
                                    {
                                        this.m_EffectDatas.Add(effectData.Id, effectData);
                                    }
                                    else
                                    {
                                        EffectLogger.Debug("true == m_EffectDatas.ContainsKey(data.Id):" + effectData.Id);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EffectLogger.Error(ex.ToString());
                }
            }
        }
        #region PlayEffect
        public int PlayEffect(int id, IBeast caster, IBeast target)
        {
            int num = -1;
            int result;
            try 
            {
                EffectLogger.Debug("PlayEffect:" + id);
                if (!this.m_EffectDatas.ContainsKey(id))
                {
                    EffectLogger.Error("!m_EffectDatas.ContainsKey(id):" + id);
                    result = -1;
                    return result;
                }
                EffectData effectData = this.m_EffectDatas[id];
                if (effectData == null)
                {
                    result = -1;
                    return result;
                }
                Effect effect = new Effect();
                effect.m_nEffectTypeId = id;
                effect.Caster = caster;
                effect.Target = target;
                num = this.GetID();
                if (this.HighLight)
                {
                    effect.HighLight = true;
                    this.m_HightLightEffect.Add(num, effect);
                }
                effect.Load(effectData);
                if (num != -1)
                {
                    effect.Id = num;
                    this.m_Effects.Add(num, effect);
                }
            }
            catch(Exception e)
            {
                EffectLogger.Fatal(e.ToString());
            }
            result = num;
            return result;
        }
        public int PlayEffect(int id, Beast iCast, Vector3 vec3SrcPos, IXUIObject uiObjectCast,
            Beast iTarget, Vector3 vec3DestPos, IXUIObject uiObjectTarget, Vector3 vec3FixDir)
        {
            int num = -1;
            try
            {
                if (this.m_EffectDatas.ContainsKey(id))
                {
                    EffectData effectData = this.m_EffectDatas[id];
                    if (null != effectData)
                    {
                        if (effectData.Reverse > 0)
                        {
                            if (iCast == null || null == iTarget)
                            {
                                effectData.Reverse = 0;
                            }
                        }
                        Effect effect = new Effect();
                        effect.m_nEffectTypeId = id;
                        effect.Caster = ((effectData.Reverse == 0) ? iCast : iTarget);
                        effect.Target = ((effectData.Reverse == 0) ? iTarget : iCast);
                        /*if (null != uiObjectCast)
                        {
                            effect.CasterUIObject = SafeXUIObject.GetSafeXUIObject(uiObjectCast);
                        }
                        if (null != uiObjectTarget)
                        {
                            effect.TargetUIObject = SafeXUIObject.GetSafeXUIObject(uiObjectTarget);
                        }*/
                        num = this.GetID();
                        if (num != -1)
                        {
                            if (this.HighLight)
                            {
                                effect.HighLight = true;
                                this.m_HightLightEffect.Add(num, effect);
                            }
                            effect.Load(effectData);
                            effect.Id = num;
                            this.m_Effects.Add(num, effect);
                            if (vec3SrcPos != Vector3.zero)
                            {
                                if (vec3DestPos != Vector3.zero)
                                {
                                    this.m_Effects[num].SetSrcPos(vec3SrcPos);
                                }
                                else
                                {
                                    this.m_Effects[num].SourcePos = vec3SrcPos;
                                }
                            }
                            if (vec3DestPos != Vector3.zero)
                            {
                                this.m_Effects[num].TargetPos = vec3DestPos;
                            }
                            if (vec3FixDir != Vector3.zero)
                            {
                                this.m_Effects[num].FixDir = vec3FixDir;
                            }
                            EffectLogger.Debug("PlayEffect:" + id);
                        }
                    }
                }
                else
                {
                    EffectLogger.Error("!m_EffectDatas.ContainsKey(id):" + id);
                }
            }
            catch (Exception ex)
            {
                EffectLogger.Fatal(ex.ToString());
            }
            return num;
        }
        #endregion
        #region GetEffectHitTime
        public float GetEffectHitTime(int effect_id, Beast caster, Beast target)
        {
            EffectData effectData = null;
            float result;
            if (!this.m_EffectDatas.ContainsKey(effect_id))
            {
                result = 0f;
            }
            else
            {
                effectData = this.m_EffectDatas[effect_id];
                float allTime = 0f;
                foreach (EffectInstanceData current in effectData.InstanceDatas)
                {
                    float num2 = 0f;
                    if (current.Type == EffectInstanceType.UITrace)
                    {
                        allTime = current.TraceTime;
                    }
                    if (current.Type == EffectInstanceType.Trace)
                    {
                        if (current.InstanceTraceMoveType == TraceMoveType.FixMoveSpeed)
                        {
                            Vector3 zero = Vector3.zero;
                            Vector3 zero2 = Vector3.zero;
                            if (null != caster)
                            {
                                EffectInstance.GetBindPos(caster, current.CasterBindType, out zero);
                            }
                            if (null != target)
                            {
                                EffectInstance.GetBindPos(target, current.TargetBindType, out zero2);
                            }
                            float num3 = Vector3.Magnitude(zero2 - zero);
                            if (current.MoveSpeed != 0f)
                            {
                                num2 = num3 / current.MoveSpeed;
                            }
                            else
                            {
                                num2 = current.TraceTime;
                            }
                        }
                        else
                        {
                            num2 = current.TraceTime;
                        }
                    }
                    if (allTime < num2)
                    {
                        allTime = num2;
                    }
                }
                allTime += effectData.HitPointTime;
                result = allTime;
            }
            return result;
        }
        public float GetEffectHitTime(int effect_id, Beast caster, Vector3 targetPos)
        {
            float result;
            if (!this.m_EffectDatas.ContainsKey(effect_id))
            {
                result = 0f;
            }
            else
            {
                EffectData effectData = this.m_EffectDatas[effect_id];
                float num = effectData.HitPointTime;
                foreach (EffectInstanceData current in effectData.InstanceDatas)
                {
                    if (current.Type == EffectInstanceType.UITrace)
                    {
                        num = current.StartDelay + current.TraceTime;
                    }
                    float allTime;
                    if (current.Type == EffectInstanceType.Trace)
                    {
                        if (current.InstanceTraceMoveType == TraceMoveType.FixMoveSpeed)
                        {
                            Vector3 zero = Vector3.zero;
                            if (null != caster)
                            {
                                EffectInstance.GetBindPos(caster, current.CasterBindType, out zero);
                            }
                            float num2 = Vector3.Magnitude(targetPos - zero);
                            if (current.MoveSpeed != 0f)
                            {
                                allTime = num2 / current.MoveSpeed + current.StartDelay;
                            }
                            else
                            {
                                allTime = current.StartDelay;
                            }
                        }
                        else
                        {
                            allTime = current.StartDelay + current.TraceTime;
                        }
                    }
                    else
                    {
                        allTime = current.StartDelay;
                    }
                    if (num < allTime)
                    {
                        num = allTime;
                    }
                }
                result = num;
            }
            return result;
        }
        #endregion
        public void DelayEffect(int nEffectId, float deltaTime)
        {
            if (this.m_Effects.ContainsKey(nEffectId))
            {
                Effect effect = this.m_Effects[nEffectId];
                foreach (EffectInstance current in effect.EffectInstances)
                {
                    current.BornTime += deltaTime;
                    current.SetVisible(false);
                    current.Visible = false;
                }
                if (effect.EffectCfgData.CameraShake != null)
                {
                    effect.EffectCfgData.CameraShake.ForcedDelay = deltaTime;
                }
            }
        }
        public int GetEffectCameraControlType(int effectid)
        {
            int result;
            if (this.m_EffectDatas.ContainsKey(effectid))
            {
                EffectData effectData = this.m_EffectDatas[effectid];
                if (effectData.InstanceDatas.Count > 0)
                {
                    if (effectData.InstanceDatas[0].Type == EffectInstanceType.Stand)
                    {
                        result = 1;
                        return result;
                    }
                    if (effectData.InstanceDatas[0].Type == EffectInstanceType.Follow)
                    {
                        result = 0;
                        return result;
                    }
                }
            }
            result = -1;
            return result;
        }
        public Effect GetEffect(int id)
        {
            Effect result;
            if (this.m_Effects.ContainsKey(id))
            {
                result = this.m_Effects[id];
            }
            else
            {
                result = null;
            }
            return result;
        }
        public void StopAllEffect()
        {
            foreach (KeyValuePair<int, Effect> current in this.m_Effects)
            {
                current.Value.Destroy();
            }
            this.m_Effects.Clear();
        }
        public void StopEffect(int id)
        {
            try
            {
                if (this.m_Effects.ContainsKey(id))
                {
                    this.m_Effects[id].Destroy();
                    this.m_Effects.Remove(id);
                }
            }
            catch (Exception ex)
            {
                EffectLogger.Fatal(ex.ToString());
            }
        }
        public void StopEffectByTypeId(int nTypeId)
        {
            List<int> list = new List<int>();
            foreach (Effect current in this.m_Effects.Values)
            {
                if (current.m_nEffectTypeId == nTypeId)
                {
                    list.Add(current.Id);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                this.StopEffect(list[i]);
            }
        }
        public void SetVisible(int id, bool bVisible)
        {
            try
            {
                if (this.m_Effects.ContainsKey(id))
                {
                    this.m_Effects[id].SetVisible(bVisible);
                }
            }
            catch (Exception ex)
            {
                EffectLogger.Fatal(ex.ToString());
            }
        }
        public void Update()
        {
            try
            {
                Dictionary<int, Effect> dictionary = new Dictionary<int, Effect>();
                foreach (KeyValuePair<int, Effect> current in this.m_Effects)
                {
                    if (current.Value.Dead)
                    {
                        current.Value.Destroy();
                    }
                    else
                    {
                        dictionary.Add(current.Key, current.Value);
                    }
                }
                this.m_Effects.Clear();
                this.m_Effects = dictionary;
                foreach (KeyValuePair<int, Effect> current in this.m_Effects)
                {
                    current.Value.Update();
                }
            }
            catch (Exception ex)
            {
                EffectLogger.Fatal(ex.ToString());
            }
        }
        public void LoadEffect(int id)
        {
            if (!this.m_dicAllPreLoadAssetRequest.ContainsKey(id))
            {
                if (!this.m_EffectDatas.ContainsKey(id))
                {
                    EffectLogger.Error("!m_EffectDatas.ContainsKey(id):" + id);
                }
                else
                {
                    this.m_dicAllPreLoadAssetRequest[id] = new List<IAssetRequest>();
                    EffectData effectData = this.m_EffectDatas[id];
                    for (int i = 0; i < effectData.InstanceDatas.Count; i++)
                    {
                        EffectInstanceData effectInstanceData = effectData.InstanceDatas[i];
                        IAssetRequest assetRequest = ResourceManager.singleton.LoadEffect(effectInstanceData.Path, null, AssetPRI.DownloadPRI_Low);
                        if (null != assetRequest)
                        {
                            this.m_dicAllPreLoadAssetRequest[id].Add(assetRequest);
                        }
                    }
                }
            }
        }
        public void UnLoadEffect(int id)
        {
            if (this.m_dicAllPreLoadAssetRequest.ContainsKey(id))
            {
                List<IAssetRequest> list = this.m_dicAllPreLoadAssetRequest[id];
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Dispose();
                }
                list.Clear();
                this.m_dicAllPreLoadAssetRequest.Remove(id);
            }
        }
        public void UnLoadAllEffect()
        {
            foreach (List<IAssetRequest> current in this.m_dicAllPreLoadAssetRequest.Values)
            {
                for (int i = 0; i < current.Count; i++)
                {
                    current[i].Dispose();
                }
            }
            this.m_dicAllPreLoadAssetRequest.Clear();
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 恢复高亮特效为普通特效
        /// </summary>
        private void RevertHightedEffect()
        {
            foreach (KeyValuePair<int, Effect> current in this.m_HightLightEffect)
            {
                if (this.m_Effects.ContainsKey(current.Key))
                {
                    Effect value = current.Value;
                    if (null != value)
                    {
                        value.HighLight = false;
                        value.SetEffectLayer();
                    }
                }
            }
            this.m_HightLightEffect.Clear();
        }
        private int GetID()
        {
            ushort num = (ushort)(this.m_ID + 1);
            int result;
            if (!this.m_Effects.ContainsKey((int)num))
            {
                this.m_ID = num;
                result = (int)num;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        #endregion
    }
}