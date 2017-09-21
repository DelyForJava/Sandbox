using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class FirstLoadSetting : SingletonBehaviour<FirstLoadSetting>
{
    public static int channelId;
    public static int appId;
    public static string currentVersionId;
    public static string verifyVersionId;
    public static string channelName;
    public static string downloadUrl;
    public static string lobbyConfigUrl;
    public static string updateDesription;
    public static string modifyTime;
    public static int switchDns=1;

    
	// Use this for initialization
	void Start ()
	{
	    SettingsInfoReq();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SettingsInfoReq()
    {
        string url = "http://10.0.70.121:8080/plat/lobbyInfo?channelId=10111&Content-Type=application/json";
        StartCoroutine(InfoGet(url));
    }

    IEnumerator InfoGet(string url)
    {
        WWW getData = new WWW(url);
        yield return getData;
        if (getData.isDone && getData.error == null)
        {
            Debug.LogError(getData.text);
            JsonData jd = JsonMapper.ToObject(getData.text);

            //Debug.LogError("code=" + jd["code"]);
            //Debug.LogError("msg=" + jd["msg"]);
            //Debug.LogError("accountId=" + jd["result"]["accountId"]);
            //Debug.LogError("loginToken=" + jd["result"]["loginToken"]);

            if (Convert.ToInt32(jd["code"].ToString()) == 0)
            {
                channelId = Convert.ToInt32(jd["result"]["channelId"].ToString());
                appId = Convert.ToInt32(jd["result"]["appId"].ToString());
                currentVersionId = jd["result"]["appId"].ToString();
                verifyVersionId = jd["result"]["verifyVersionId"].ToString();
                channelName = jd["result"]["channelName"].ToString();
                downloadUrl = jd["result"]["downloadUrl"].ToString();
                lobbyConfigUrl = jd["result"]["lobbyConfigUrl"].ToString();
                updateDesription = jd["result"]["updateDesription"].ToString();
                modifyTime = jd["result"]["modifyTime"].ToString();
                switchDns = Convert.ToInt32(jd["result"]["switchDns"].ToString());
            }
            else
            {
                Debug.LogError("msg=" + jd["msg"]);
            }

        }
        else
        {
            print("Error downloading: " + getData.error);
        }
    }
}
