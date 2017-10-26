using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using odao.scmahjong;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using cn.sharesdk.unity3d;
using LitJson;

namespace Bean.Hall
{
    public enum Source
    {
        wechat,
        yk,
        phoneNumber
    }
    enum Platform
    {
        andrioid,
        ios,
        pc,
        web
    }
    public class LoginEvent : MonoSingleton<LoginEvent>
    {
        public bool isTestModel = false;

        public Text message;
        public ShareSDK ssdk;

        private string accessToken;
        private string openId;

        private string machineSerial;
        private int serviceId;
        private Source source;
        private Platform platform;
        private int channelId = GlobalData.ChannelId;


        /// <summary>
        /// 解析域名
        /// </summary>
        private static string domain = "u3d.douzi.com";
        /// <summary>
        /// D+秘钥
        /// </summary>
        private static string key = "x0qemCME";
        /// <summary>
        /// D+ ID
        /// </summary>
        private static string id = "289";
        /// <summary>
        /// ip解析结果
        /// </summary>
        public static string ipRes = "";

        private bool isWechatAuthed = false;
        private bool isTouristAuthed = false;

        void Start()
        {
            //message = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
            ssdk = GameObject.Find("Canvas").GetComponent<ShareSDK>();

            machineSerial = SystemInfo.deviceUniqueIdentifier;


            //授权登录事件
            ssdk.authHandler = OnAuthResultHandler;
            //用户信息事件  
            ssdk.showUserHandler = OnGetUserInfoResultHandler;

            if (PlayerPrefs.HasKey("wechatAccountId") && PlayerPrefs.GetString("wechatAccountId") != "")
            {
                isWechatAuthed = true;
            }
            if (PlayerPrefs.HasKey("tuoristAccountId") && PlayerPrefs.GetString("tuoristAccountId") != "")
            {
                isTouristAuthed = true;
            }
        }



        public void OnClickWechatLogin()
        {
            source = Source.wechat;
            if (!isWechatAuthed)
            {
                ssdk.Authorize(PlatformType.WeChat);
            }
            else
            {
                Resolution(domain, Convert.ToInt32(PlayerPrefs.GetString("wechatAccountId")), PlayerPrefs.GetString("wechatLoginToken"), machineSerial);
                //StartCoroutine(ThirdPartyResolution(domain, Convert.ToInt32(PlayerPrefs.GetString("accountId")), PlayerPrefs.GetString("loginToken"), machineSerial));
            }

        }

        public void OnClickTouristLogin()
        {
            UnityEngine.Debug.Log("+++++++++++++++++++++++++++++++++++++++");
            //UIDebugViewController.Instance.OpenLoadingDebug("游客登录中......");
            source = Source.yk;

            if (isTestModel)
            {
                Resolution(domain, 34, "6558a4e4e8d8", "f93f88bbd26aff9d7f7689faff32ced035e1cf19");
                return;
            }
            //Regist();//test 22 afac70d97abe
            if (!isTouristAuthed)
            {
                Regist(Source.yk);
            }
            else
            {
                Resolution(domain, Convert.ToInt32(PlayerPrefs.GetString("touristAccountId")), PlayerPrefs.GetString("touristLoginToken"),
                    machineSerial);
            }
            
        }


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
                        //message.text = MiniJSON.jsonEncode(result);  //Json  
                        break;
                }
            }
            else if (state == ResponseState.Fail)
            {
                //message.text = ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
            }
            else if (state == ResponseState.Cancel)
            {
                //message.text = ("cancel !");
            }
        }

        /// <summary>
        /// 获取token信息
        /// </summary>
        void GetAuthInfo(PlatformType type)
        {
            Hashtable authInfo = ssdk.GetAuthInfo(type);

            string authInfoJson = MiniJSON.jsonEncode(authInfo);
            UnityEngine.Debug.LogError("授权回调authInfo:" + authInfoJson);


            accessToken = authInfo["token"].ToString();
            openId = authInfo["openID"].ToString();

            Regist(Source.wechat);
        }

        /// <summary>
        /// 刷新token信息
        /// </summary>
        /// <param name="type"></param>
        void RefreshToken(PlatformType type)
        {
            Hashtable authInfo = ssdk.GetAuthInfo(type);
            string authInfoJson = MiniJSON.jsonEncode(authInfo);
            accessToken = authInfo["token"].ToString();
        }



        /// <summary>
        /// 注册用户
        /// </summary>
        public void Regist(Source scource)
        {
            UnityEngine.Debug.LogError("发送注册请求");

            //StartCoroutine(WechatAccessTokenReq());
            switch (scource)
            {
                case Source.wechat:
                    StartCoroutine(WechatRegistPost());
                    break;
                case Source.yk:
                    StartCoroutine(TouristRegistPost());
                    break;
                default:
                    break;
            }
        }

        IEnumerator RegistWWWOrgReq()
        {
            string urlRegist = "http://10.0.70.119:8182/v1/users/oauth";
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
                UnityEngine.Debug.Log(webResPonse.text);
            }
        }

        IEnumerator WechatRegistPost()
        {
            string url = "http://10.0.70.119:8182/plat-api/users/oauth";

            JsonData data = new JsonData();

            data["code"] = "";
            data["accessToken"] = accessToken;
            data["openId"] = openId;
            data["source"] = Source.wechat.ToString();
            data["machineSerial"] = machineSerial;
            data["serviceId"] = serviceId;
            data["channelId"] = channelId;
            byte[] postBytes = Encoding.Default.GetBytes(data.ToJson());

            print("code: " + data["code"]);
            print("accessToken: " + data["accessToken"]);
            print("openId: " + data["openId"]);
            print("source: " + data["source"]);
            print("machineSerial: " + data["machineSerial"]);
            print("serviceId: " + data["serviceId"]);
            print("channelId: " + data["channelId"]);

            Dictionary<string, string> header = new Dictionary<string, string>();
            //添加header校验
            header.Add("Authorization", GetHeaderValue(data, "users/oauth", "POST"));
            header["Content-Type"] = "application/json";
            Debug.LogMsg("WechatRegistPost header.Added");
            WWW www = new WWW(url, postBytes, header);
            yield return www;

            if (www.isDone && www.error == null)
            {
                UnityEngine.Debug.LogError(www.text);
                JsonData jd = JsonMapper.ToObject(www.text);

                UnityEngine.Debug.LogError("code=" + jd["code"]);
                UnityEngine.Debug.LogError("msg=" + jd["msg"]);
                UnityEngine.Debug.LogError("accountId=" + jd["result"]["accountId"]);
                UnityEngine.Debug.LogError("loginToken=" + jd["result"]["loginToken"]);

                PlayerPrefs.SetString("wechatAccountId", jd["result"]["accountId"].ToString());
                PlayerPrefs.SetString("wechatLoginToken", jd["result"]["loginToken"].ToString());

                //UIDebugViewController.Instance.OpenLoadingDebug("微信登录中......");

                Resolution(domain, Convert.ToInt32(jd["result"]["accountId"].ToString()), jd["result"]["loginToken"].ToString(), machineSerial);
            }
            else
            {
                print("Error downloading: " + www.error);
            }
        }

        IEnumerator TouristRegistPost()
        {
            string url = "http://10.0.70.119:8182/plat-api/users/oauth";

            JsonData data = new JsonData();

            data["code"] = "";
            data["accessToken"] = accessToken;
            //data["openId"] = openId;
            data["source"] = Source.yk.ToString();
            data["machineSerial"] = machineSerial;
            data["serviceId"] = serviceId;
            data["channelId"] = channelId;
            byte[] postBytes = Encoding.Default.GetBytes(data.ToJson());

            print("code: " + data["code"]);
            print("accessToken: " + data["accessToken"]);
            //print("openId: " + data["openId"]);
            print("source: " + data["source"]);
            print("machineSerial: " + data["machineSerial"]);
            print("serviceId: " + data["serviceId"]);
            print("channelId: " + data["channelId"]);

            Dictionary<string, string> header = new Dictionary<string, string>();
            //添加header校验
            header.Add("Authorization", GetHeaderValue(data, "users/oauth", "POST"));
            header["Content-Type"] = "application/json";
            Debug.LogMsg("TouristRegistPost header.Added");
            WWW www = new WWW(url, postBytes, header);
            yield return www;

            if (www.isDone && www.error == null)
            {
                UnityEngine.Debug.LogError(www.text);
                JsonData jd = JsonMapper.ToObject(www.text);

                UnityEngine.Debug.LogError("code=" + jd["code"]);
                UnityEngine.Debug.LogError("msg=" + jd["msg"]);
                UnityEngine.Debug.LogError("accountId=" + jd["result"]["accountId"]);
                UnityEngine.Debug.LogError("loginToken=" + jd["result"]["loginToken"]);
                //Debug.LogError("secret=" + jd["result"]["secret"]);

                PlayerPrefs.SetString("touristAccountId", jd["result"]["accountId"].ToString());
                PlayerPrefs.SetString("touristLoginToken", jd["result"]["loginToken"].ToString());

                Resolution(domain, Convert.ToInt32(jd["result"]["accountId"].ToString()), jd["result"]["loginToken"].ToString(), machineSerial);
            }
            else
            {
                print("Error downloading: " + www.error);
            }
        }

        /// <summary>
        /// 获取header
        /// </summary>
        /// <param name="header"></param>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHeaderValue(JsonData data, string url,string request_method)
        {
            string APP_SECRET_KEY = Md5Sum("Haha");
            Debug.LogMsg("APP_SECRET_KEY" + APP_SECRET_KEY);
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
            Debug.LogMsg("token:"+token);
            //if (isWechatAuthed)
            //{
            //    RefreshToken(PlatformType.WeChat);
            //}

            //signature
            //string request_method = "post";
            long timestamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string content;
            if (data.Inst_Object.ContainsKey("license") )
            {
                content = "{}";
            }
            else
            {
                content = data.ToJson();
            }
            string secret = APP_SECRET_KEY;
            string signature = Md5Sum("request_url=" + url + "&content=" + content + "&request_method=" + request_method + "&timestamp=" + timestamp + "&secret=" + secret);

            Debug.LogMsg("signature:" + signature);
            Debug.LogMsg("Details signature:" + "request_url=" + url + "&content=" + content + "&request_method=" + request_method + "&timestamp=" + timestamp + "&secret=" + secret);
            //once
            string once = Md5Sum(APP_SECRET_KEY + timestamp);
            Debug.LogMsg("once:" + once);
            string headerValue = "oauth2=" + token + ";signature=" + signature + ";timestamp=" + timestamp + ";scene =" + "app" + ";once=" + once;
            Debug.LogMsg("headerValue:  " + headerValue);


            //header.Add("Authorization", "oauth2=793e8fe4e23f4e51aa4fc4e03b4ccca5;signature=7a971f3e5111b3b323ffa484e78daa62;timestamp=1376993022";once=zzzz);
            return headerValue;
        }

        private void Resolution(string domainAddress, int accountId, string loginToken, string machineSerial)
        {
            //0 本地，1第三方
            if (FirstLoadSetting.switchDns == 0)
            {
                LocalResolution(domainAddress, accountId, loginToken, machineSerial);
            }
            else
            {
                StartCoroutine(ThirdPartyResolution(domainAddress, accountId, loginToken, machineSerial));
            }
        }
        /// <summary>
        /// D+第三方域名解析
        /// </summary>
        private IEnumerator ThirdPartyResolution(string domainAddress, int accountId, string loginToken, string machineSerial)
        {
            string encryptStr = ECBEncrypt(domainAddress, key);
            string thirdPartyUrl = "http://119.29.29.29/d?dn=" + encryptStr + "&id=" + id + "&ttl=1";
            WWW www = new WWW(thirdPartyUrl);
            yield return www;
            string returnCode = www.text;
            string decryptStr = ECBDecrypt(returnCode, key);
            string ip = decryptStr.Split(',')[0];
            ipRes = ip;
            UnityEngine.Debug.Log("第三方IP解析结果=====！！！！！！！！！！！！！！=======：" + decryptStr);

            var client = GameClient.Instance;
            client.MahjongGamePlayer.ConnectGameServer(ipRes, 36667, delegate ()
            {
                client.MahjongGamePlayer.OriginMsgLoginReqDef(accountId, loginToken, machineSerial, source);
            });

        }

        /// <summary>
        /// 本地域名解析
        /// </summary>
        private void LocalResolution(string domainAddress, int accountId, string loginToken, string machineSerial)
        {
            IPHostEntry host = Dns.GetHostEntry(domainAddress);
            IPAddress ip = host.AddressList[0];
            ipRes = ip.ToString();
            UnityEngine.Debug.Log("本地IP解析结果：" + ip.ToString());

            var client = GameClient.Instance;
            client.MahjongGamePlayer.ConnectGameServer(ipRes, 36667, delegate ()
            {
                client.MahjongGamePlayer.OriginMsgLoginReqDef(accountId, loginToken, machineSerial, source);
            });
        }

        /// <summary>  
        /// DES加密  ECB
        /// </summary>  
        /// <param name="pToEncrypt"></param>  
        /// <param name="sKey"></param>  
        /// <returns></returns>  
        public string ECBEncrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>  
        /// DES解密  ECB
        /// </summary>  
        /// <param name="pToDecrypt"></param>  
        /// <param name="sKey"></param>  
        /// <returns></returns>  
        public string ECBDecrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

    }

}