using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;

namespace Bean.Hall
{
    public static class Event
    {
        public  enum Step
        {
            Prepare,
            GetInfo,
            Download,
            Max,
        }

        public static string[] StepTips =
        {
            "准备中。。。",
            "正在获取信息。。。",
            "正在下载文件。。。",//解压文件  下载  覆盖
            "准备完毕。。。",
            //"网络正在请求。。。",
        };
        public static Step StepIndex
        {
            get; set;
        }
        public static string cdn = "http://u3download.douzi.com/";

        public static string[] CompressTips =
        {
            "正在解压......",
            "解压完成",
            "解压失败",
        };
        public static int CompressIndex
        {
            get;set;
        }

        public static string[] HotupdateTips =
        {
            "",
            "初始化更新信息",
            "连接服务器",
            "更新主配置文件",
            "下载资源",
            "解析资源",
            "清理缓存目录数据",
            "更新完成",
            "更新失败",
            "更新取消",
            "更新中断",
        };
        // -------------------------------------------------  event ---------------------------------------------------------//
        public static string url = "http://10.0.70.121:8080/plat/lobbyInfo?channelId=10111&Content-Type=application/json";
        public static int channelId;
        public static int appId;
        public static string currentVersionId;
        public static string verifyVersionId;
        public static string channelName;
        public static string downloadUrl;
        public static string lobbyConfigUrl;
        public static string updateDesription;
        public static string modifyTime;
        public static int switchDns;

        public static IEnumerator GetInfo()
        {
            WWW getData = new WWW(url);
            yield return getData;
            if (getData.isDone && getData.error == null)
            {
                Debug.LogMsg(getData.text);
                JsonData jd = JsonMapper.ToObject(getData.text);

                int code = int.Parse(jd["code"].ToString());
                if (code == 0)
                {
                    channelId = int.Parse(jd["result"]["channelId"].ToString());
                    appId = int.Parse(jd["result"]["appId"].ToString());
                    currentVersionId = jd["result"]["appId"].ToString();
                    verifyVersionId = jd["result"]["verifyVersionId"].ToString();
                    channelName = jd["result"]["channelName"].ToString();
                    downloadUrl = jd["result"]["downloadUrl"].ToString();
                    lobbyConfigUrl = jd["result"]["lobbyConfigUrl"].ToString();
                    updateDesription = jd["result"]["updateDesription"].ToString();
                    modifyTime = jd["result"]["modifyTime"].ToString();
                    switchDns = int.Parse(jd["result"]["switchDns"].ToString());
                }
                else
                {
                    Debug.LogMsg("msg=" + jd["msg"].ToString());
                }

            }
            else
            {
                Debug.LogMsg("Error downloading: " + getData.error);
            }

        }
        // -------------------------------------------------  event ---------------------------------------------------------//
        
        public static string HotupdateToClient = "HotupdateToClient";
        public static string ClientToHotupdate = "ClientToHotupdate";

        public static string UIToClient = "UIToClient";
        public static string OnHotupdateChanged = "OnHotupdateChanged";
        public static string OnShieldChanged = "OnShieldChanged";
        
        public  enum UIMessage
        {
            Fullscreen,
            Hotupdate,
        }

        public static bool HotChanged
        {
            get;set;
        }
        public static bool FullscreenChanged
        {
            get; set;
        }
        public static void SendMessage<T>(T t, string msg,params object[] objs) where T : MonoBehaviour
        {
            if (msg == OnShieldChanged)
            {
                bool first;
                ShieldClient.WaitType second;
                first = (bool)objs[0];
                second = (ShieldClient.WaitType)objs[1];
                //t.gameObject.SendMessage(msg,first,second SendMessageOptions.DontRequireReceiver);

            }
            else if (msg == OnHotupdateChanged)
            {

            }



        }
    }

    public class Global
    {


    }

}