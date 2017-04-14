using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using Client.Data;
using System;
using System.Text;
using System.IO;
using Utility;
#region 模块信息
/*----------------------------------------------------------------
// 模块名：screen
// 创建者：chen
// 修改者列表：
// 创建日期：#CREATIONDATE#
// 模块描述：
//----------------------------------------------------------------*/
#endregion
public class screen : MonoBehaviour
{
    public string path;
    private string dic = "F:/config";
    private string targetPath = "E:/";
    void Start()
    {
        if (!string.IsNullOrEmpty(path))
        {
            string path1 = dic + "/" + path;
            XmlDocument xmlDocument = XmlResAdapter.GetXmlDocument(path1);
            print(xmlDocument.InnerXml);
            File.WriteAllText(targetPath+path, xmlDocument.InnerXml, Encoding.UTF8);
        }
    }
}
