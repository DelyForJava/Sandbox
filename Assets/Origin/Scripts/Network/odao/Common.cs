using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

class XmlHelper
{
    /// <summary>
    /// 以UTF-8无BOM编码保存xml至文件。
    /// </summary>
    /// <param name="savePath">保存至路径</param>
    /// <param name="xml"></param>
    public static void SaveXmlWithUTF8NotBOM(string savePath, XmlDocument xml)
    {
        StreamWriter sw = new StreamWriter(savePath, false, new UTF8Encoding(false));
        xml.Save(sw);
        sw.WriteLine();
        sw.Close();
    }
}

class FileUtils
{   
    //gbk转utf8
    public static string GBK2UTF8(string strGBK)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(strGBK);
        string strUTF8 = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        return strUTF8;
    }

    /// <summary>
    /// 实现多种编码方式的转换
    /// </summary>
    /// <param name="str">要转换的字符</param>
    /// <param name="From">从哪种方式转换，如UTF-8</param>
    /// <param name="To">转换成哪种编码,如GB2312</param>
    /// <returns>转换结果</returns>
    public static string ConvertStr(string str, string From, string To)
    {

        byte[] bs = System.Text.Encoding.GetEncoding(From).GetBytes(str);
        bs = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(From), System.Text.Encoding.GetEncoding(To), bs);
        string res = System.Text.Encoding.GetEncoding(To).GetString(bs);
        return res;

    }

    public static string UTF82GB2312(string strText)
    {
       
        //声明字符集   
        System.Text.Encoding utf8, gb2312;
        //utf8   
        utf8 = System.Text.Encoding.GetEncoding("utf-8");
        //gb2312   
        gb2312 = System.Text.Encoding.GetEncoding("gb2312");
        byte[] utf;
        utf = utf8.GetBytes(strText);
        utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
        //返回转换后的字符   
        return gb2312.GetString(utf);
    }


    //public static byte[] gFileByteArray = new byte[1024*1024*20];
   public static void CopyFile(string sourceFile, string destFile, bool bOverride)
    {
        byte[] line = ReadFileToByte(sourceFile);
        WirteByteToFile(destFile, line,line.Length, true);
    }
    public static bool IsExist(string filepath)
    {
        return File.Exists(filepath);
    }
    public static void WriteFile(string filename, string info)
    {
        WirteFile2(filename, info, true);
    }
    public static void WirteFile2(string filename, string info, bool overwrite)
    {
        FileInfo tempfile = new FileInfo(filename);
        //文件流
        StreamWriter sw;
        if(!tempfile.Exists)
        {
           sw =  tempfile.CreateText();
        }
        else
        {
            if(overwrite)
            {
                sw = tempfile.CreateText();
            }
            else
            {
                sw = tempfile.AppendText();
            }
        }
        //以行的形式写入
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    public static void WirteByteToFile(string destFilePath, byte[] byteInfo, int length, bool bOverride = false)
    {

        FileStream AssetIO = null;
        if(bOverride)
           AssetIO =  new FileStream(destFilePath, FileMode.Create, FileAccess.ReadWrite); //创建文件流（对象现含有中文）
        else
            AssetIO = new FileStream(destFilePath, FileMode.Append, FileAccess.ReadWrite); //创建文件流（对象现含有中文）
        int iBegin = (int)AssetIO.Seek(0, SeekOrigin.Begin);
        AssetIO.Write(byteInfo, iBegin, length);
        AssetIO.Close();
        AssetIO.Dispose();
    }
    public static ArrayList ReadFile(string filename)
    {
        StreamReader sr;
        try
        {
            sr = File.OpenText(filename);
        }
        catch(Exception ex)
        {
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();

        while((line = sr.ReadLine()) != null)
        {
            arrlist.Add(line);
        }
        sr.Close();
        sr.Dispose();
        return arrlist;
    }
    public static byte[] ReadFileToByte(string filename)
    {
        //gFileByteArray.Initialize();
        FileStream AssetIO = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite); //创建文件流（对象现含有中文）
        //AssetIO.Seek(0, SeekOrigin.Begin);
        byte[] assetbytes = new byte[AssetIO.Length];
        AssetIO.Read(assetbytes, 0, (int)AssetIO.Length);
        AssetIO.Close();
        AssetIO.Close();
        AssetIO.Dispose();
        return assetbytes;
    }
}

/// <summary>
/// 解析游戏配置
/// 格式：
/// [Module]
/// key = value
/// </summary>
class ConfigFile
{
    public bool InitFile(string filename)
    {
        m_strFileName = filename;
        byte[]contByte  = FileUtils.ReadFileToByte(filename);
        //ParaseArrayList(m_arrInfo);
        InitFile(contByte);
        return contByte.Length > 0;
    }
    public bool InitFile(byte[]info, string fileName = "")
    {
        m_strFileName = fileName;
        return ParaseLine(info);
    }

    public int GetValueInt(string ModulName, string key, int iDefault = 0)
    {
        int iValue = iDefault;
        if (m_dicInfo[ModulName][key] != null)
        {
            iValue = int.Parse(m_dicInfo[ModulName][key]);
        }
        return iValue;
    }
    public float GetValueFloat(string ModulName, string key,float fDefault = 0.0f)
    {
        float fValue = fDefault;
        if (m_dicInfo[ModulName][key] != null)
        {
            fValue = float.Parse(m_dicInfo[ModulName][key]);
        }
        return fValue;
    }
    public string GetValueString(string ModulName, string key)
    {
        string strValue = null;
        if (m_dicInfo.Count > 0 && m_dicInfo[ModulName][key] != null)
        {
            strValue = m_dicInfo[ModulName][key];
        }
        return strValue;
    }

    public void SetValueInt(string modulName, string key, int iValue)
    {
        if(m_dicInfo.ContainsKey(modulName)
            && m_dicInfo[modulName].ContainsKey(key))
        {
            m_dicInfo[modulName][key] = iValue.ToString();
            StringBuilder strinfo = new StringBuilder();
            foreach(KeyValuePair<string, Dictionary<string, string>> pair in m_dicInfo)
            {
                strinfo.AppendLine("[" + pair.Key + "]");
                foreach(KeyValuePair<string, string> subpair in m_dicInfo[pair.Key])
                {
                    strinfo.AppendLine(subpair.Key + "=" + subpair.Value);
                }
            }
            FileUtils.WriteFile(m_strFileName, strinfo.ToString());
        }
    }
    public void SetValueString(string modulName, string key, string strValue)
    {
        if (false == m_dicInfo.ContainsKey(modulName))//如果没有key
        {
            Dictionary<string, string> ssValue = new Dictionary<string, string>();
            ssValue.Add(key, strValue);
            m_dicInfo.Add(modulName, ssValue);
        }

        if (m_dicInfo.ContainsKey(modulName) 
            && m_dicInfo[modulName].ContainsKey(key))
        {
            m_dicInfo[modulName][key] = strValue;
            StringBuilder strinfo = new StringBuilder();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in m_dicInfo)
            {
                strinfo.AppendLine("[" + pair.Key + "]");
                foreach (KeyValuePair<string, string> subpair in m_dicInfo[pair.Key])
                {
                    strinfo.AppendLine(subpair.Key + "=" + subpair.Value);
                }
            }
            FileUtils.WriteFile(m_strFileName, strinfo.ToString());
        }
    }
    public void SetValueFloat(string moduleName, string key, float fValue)
    {
        if (m_dicInfo.ContainsKey(moduleName) 
            && m_dicInfo[moduleName].ContainsKey(key))
        {
            m_dicInfo[moduleName][key] = fValue.ToString();
            StringBuilder strinfo = new StringBuilder();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in m_dicInfo)
            {
                strinfo.AppendLine("[" + pair.Key + "]");
                foreach (KeyValuePair<string, string> subpair in m_dicInfo[pair.Key])
                {
                    strinfo.AppendLine(subpair.Key + "=" + subpair.Value);
                }
            }
            FileUtils.WriteFile(m_strFileName, strinfo.ToString());
        }
    }

    /// <summary>
    /// 查找模块名下所有的键
    /// </summary>
    /// <param name="strModule">模块名</param>
    /// <param name="tmpKeyList">返回list列表</param>
    public void GetModuleKeys(string strModule, ref List<string> tmpKeyList)
    {
         if (m_dicInfo.ContainsKey(strModule))
         {
             foreach (var item in m_dicInfo[strModule].Keys)
	        {
		      tmpKeyList.Add(item);
	        }
         }
    }
    private bool ParaseArrayList(ArrayList arrlist)
    {
        string strModulName = null;
        string[] strlist = (string[])arrlist.ToArray(typeof(string));
        for (int i = 0; i < strlist.Length; )
        {
            string tempName = strlist[i];
            
            //第一次匹配到模块
            int iIndexStart = tempName.IndexOf("[");
            int iIndexEnd = tempName.IndexOf("]");
            if ( iIndexStart != -1 && iIndexEnd != - 1)
            {
                strModulName = tempName.Substring(iIndexStart + 1, iIndexEnd - iIndexStart - 1).Trim();
                Dictionary<string, string> tempDic = new Dictionary<string, string>();
                int j;
                for (j = i + 1; j < strlist.Length; j++)
                {
                    //第二次遇到模块立即跳出
                    string tempName2 = strlist[j];
                    int iIndexStart2 = tempName2.IndexOf("[");
                    int iIndexEnd2   = tempName2.IndexOf("]");
                    if (iIndexStart2 != -1 && iIndexEnd2 != -1)
                        break;
                    string[] key_value = tempName2.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if(key_value.Length > 1)
                    {
                        StringBuilder strValueB = new StringBuilder();
                        for (int k = 1; k < key_value.Length; k++)
                        {
                            strValueB.Append(key_value[k].Trim());
                            if (k != key_value.Length - 1)
                            {
                                strValueB.Append("=");
                            }
                        }
                        string strValue = strValueB.ToString();
                        strValue.Trim();
                        tempDic.Add(key_value[0].Trim(), strValue);
                        
                    }
                }
                if (tempDic.Count > 0)
                    m_dicInfo.Add(strModulName, tempDic);
                i = j;
            }
            else
            {
                i++;
            }

        }
        return true;
    }
    private bool ParaseLine(byte[] info)
    {
        string strinfo = System.Text.Encoding.UTF8.GetString(info);
        string[] strLine = strinfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        ArrayList arrlist = new ArrayList();
        for (int i = 0; i < strLine.Length; i++ )
        {
            arrlist.Add(strLine[i]);
        }
        ParaseArrayList(arrlist);
        return m_dicInfo.Count > 0;
    }
    private Dictionary<string, Dictionary<string, string>> m_dicInfo = new Dictionary<string, Dictionary<string, string>>();
    private string m_strFileName = null;
}