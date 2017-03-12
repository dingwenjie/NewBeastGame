﻿using System;
using System.Collections;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：IData
// 创建者：chen
// 修改者列表：
// 创建日期：#CREATIONDATE#
// 模块描述：
//----------------------------------------------------------------*/
#endregion
namespace Game
{
    [Serializable]
    public abstract class IData
    {
        public abstract CByteStream Serialize(CByteStream bs);
        public abstract CByteStream DeSerialize(CByteStream bs);
    }
}
