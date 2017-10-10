using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cn.sharesdk.unity3d;
using LitJson;
using System.Text;


public class UILogin : MonoBehaviour {

    public Button touristBtn;
    public Button weChatBtn;
    //   public Text message;
    //   public ShareSDK ssdk;

    //   private string accessToken;
    //   private string openId;
    //   //private Source source;
    //   private string machineSerial;
    //   private int serviceId;
    //   private Platform platform;
    //   private int channelId;

    // Use this for initialization
    void Start () {
        touristBtn = GameObject.Find("Canvas/Login/yk_bg").GetComponent<Button>();
        weChatBtn = GameObject.Find("Canvas/Login/wx_bg").GetComponent<Button>();

        //      ssdk = gameObject.GetComponent<ShareSDK>();

        //      //授权登录事件
        //      ssdk.authHandler = OnAuthResultHandler;
        //      //用户信息事件  
        //      ssdk.showUserHandler = OnGetUserInfoResultHandler;

    }
	
	// Update is called once per frame
	void Update () {

    }

    //public void OnWeChatBtnClick()
    //{
    //    message.text = "WechatClicked";
    //    //bool isAuthed = false;
    //    if (PlayerPrefs.GetString("loginToken") == "")
    //    {
    //        ssdk.Authorize(PlatformType.WeChat);
    //    }

    //    //Regist();
    //}
    /*
    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //授权成功的话，获取用户信息  
            ssdk.GetUserInfo(type);
            //获取用户授权信息
            GetAuthInfo(type);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    //获取用户信息  
    void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //获取成功的话 可以写一个类放不同平台的结构体，用PlatformType来判断，用户的Json转化成结构体，来做第三方登录。 
            switch (type)
            {
                case PlatformType.WeChat:
                    message.text = MiniJSON.jsonEncode(result);  //Json  

                    
                    
                    break;
            }
        }
        else if (state == ResponseState.Fail)
        {
            message.text = ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            message.text = ("cancel !");
        }
    }

    /// <summary>
    /// 获取token信息
    /// </summary>
    void GetAuthInfo(PlatformType type)
    {
        Hashtable authInfo = ssdk.GetAuthInfo(type);

        string authInfoJson = MiniJSON.jsonEncode(authInfo);
        Debug.LogError("授权回调authInfo:" + authInfoJson);


        accessToken = authInfo["token"].ToString();
        openId = authInfo["openID"].ToString();

        Regist();
    }



    /// <summary>
    /// 注册用户
    /// </summary>
    /// <param name="a"></param>
    public void Regist()
    {
        Debug.LogError("发送注册请求");

        //StartCoroutine(WechatAccessTokenReq());
        
        StartCoroutine(RegistPost());
    }

    IEnumerator RegistWWWOrgReq()
    {
        string urlRegist = "http://10.0.70.121:8080/users/oauth";
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();
        form.AddField("code", "666666");
        form.AddField("accessToken", "accessToken123");
        form.AddField("openId", "openId123");
        form.AddField("source", "wechat");
        form.AddField("machineSerial", "machineSerial1234567890");
        form.AddField("serviceId", "868");

        PlayerPrefs.SetString("ID", "123456");
        PlayerPrefs.SetString("Password", "abc");

        // Create a download object
        WWW webResPonse = new WWW(urlRegist, form);

        // Wait until the download is done
        yield return webResPonse;

        if (!string.IsNullOrEmpty(webResPonse.error))
        {
            print("Error downloading: " + webResPonse.error);
        }
        else
        {
            Debug.Log(webResPonse.text);
        }
    }

    IEnumerator RegistPost()
    {
        string url = "http://10.0.70.121:8080/v1/users/oauth";

        JsonData data = new JsonData();
        
        data["code"] = "";
        data["accessToken"] = accessToken;
        data["openId"] = openId;
        data["source"] = Source.wechat.ToString();
        data["machineSerial"] = SystemInfo.deviceUniqueIdentifier;
        data["serviceId"] = serviceId;
        byte[] postBytes = Encoding.Default.GetBytes(data.ToJson());

        print(" downloading: " + data["code"]);
        print(" downloading: " + data["accessToken"]);
        print(" downloading: " + data["openId"]);
        print(" downloading: " + data["source"]);
        print(" downloading: " + data["machineSerial"]);
        print(" downloading: " + data["serviceId"]);

        Dictionary<string, string> header = new Dictionary<string, string>();
        header["Content-Type"] = "application/json";
        //header.Add("CLEARANCE", "I_AM_ADMIN");

        WWW www = new WWW(url, postBytes, header);
        yield return www;

        if (www.isDone && www.error == null)
        {
            Debug.LogError(www.text);
            JsonData jd = JsonMapper.ToObject(www.text);

            Debug.LogError("code=" + jd["code"]);
            Debug.LogError("msg=" + jd["msg"]);
            Debug.LogError("accountId=" + jd["result"]["accountId"]);
            Debug.LogError("loginToken=" + jd["result"]["loginToken"]);

            PlayerPrefs.SetString("accountId", jd["result"]["accountId"].ToString());
            PlayerPrefs.SetString("loginToken", jd["result"]["loginToken"].ToString());
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }
    */
}
