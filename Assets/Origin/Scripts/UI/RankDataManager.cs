using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Bean.Hall;
using LitJson;
using UnityEngine;
using Debug = UnityEngine.Debug;

//public delegate void CallBack(string text);

public class RankDataManager : SingletonBehaviour<RankDataManager>
{

    //public event CallBack doTextEventHandler;

    // Use this for initialization
    void Start ()
	{
	    StartCoroutine(BeanDataReqPost());
	    //doTextEventHandler += new CallBack(DoText);
	    //doTextEventHandler += new CallBack(DoText1);
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    


    IEnumerator BeanDataReqPost()
    {
        string url = "http://10.0.70.119:8182/plat-api/lobbyrank/imazamox";

        JsonData data = new JsonData();

        data["userId"] = HallData.iUserID;
        
        byte[] postBytes = Encoding.Default.GetBytes(data.ToJson());

        print("userId: " + data["userId"]);

        Dictionary<string, string> header = new Dictionary<string, string>();
        //添加header校验
        header.Add("Authorization", GetHeaderValue(data, "lobbyrank/imazamox", "POST"));
        header["Content-Type"] = "application/json";
        WWW www = new WWW(url, postBytes, header);
        yield return www;

        if (www.isDone && www.error == null)
        {
            UnityEngine.Debug.LogError(www.text);
            JsonData jd = JsonMapper.ToObject(www.text);

            
            //doTextEventHandler(www.text);
        }
        else
        {
            //doTextEventHandler(www.text);
            print("Error downloading: " + www.error);
        }


    }

    //public void DoText(string text)
    //{
    //    JsonData jd = JsonMapper.ToObject(text);

    //    UnityEngine.Debug.LogError("DoText");
    //}

    //public void DoText1(string text)
    //{
    //    UnityEngine.Debug.LogError("DoText1=");
    //}

    /// <summary>
    /// 获取header
    /// </summary>
    /// <param name="header"></param>
    /// <param name="data"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public string GetHeaderValue(JsonData data, string url, string request_method)
    {
        string APP_SECRET_KEY = Md5Sum("Haha");
        //token
        string token = null;
        if (string.IsNullOrEmpty(HallData.szPasswdToken))
        {
            token = "12345";
        }
        else
        {
            token = HallData.szPasswdToken;
        }
        long timestamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        string content;
        if (data.Inst_Object.ContainsKey("license"))
        {
            content = "{}";
        }
        else
        {
            content = data.ToJson();
        }
        string secret = APP_SECRET_KEY;
        string signature = Md5Sum("request_url=" + url + "&content=" + content + "&request_method=" + request_method + "&timestamp=" + timestamp + "&secret=" + secret);
        //once
        string once = Md5Sum(APP_SECRET_KEY + timestamp);
        
        string headerValue = "oauth2=" + token + ";signature=" + signature + ";timestamp=" + timestamp + ";scene =" + "app" + ";once=" + once;
        return headerValue;
    }

    public string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString().ToLower();

    }


    private void UpdateCellAtIndex(BaseTableViewCell cell)
    {
        var c = cell as RankTableViewCell;
        if (!c)
            return;

        c.rankId.text = c.BaseIndex.ToString();
        //Debug.Log("I am cell BaseIndex at:" + c.BaseIndex);
        //Debug.Log("I am cell rankId at:" + c.rankId);
    }
}
