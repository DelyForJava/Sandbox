using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;

namespace Bean.Hall
{
    public class GlobalData
    {
        public static string Url = "http://10.0.70.121:8080/plat/lobbyInfo?channelId=10111&Content-Type=application/json";
        public static int ChannelId;
        public static int AppId;
        public static string CurrentVersionId;
        public static string VerifyVersionId;
        public static string ChannelName;
        public static string DownloadUrl;
        public static string LobbyConfigUrl;
        public static string UpdateDesription;
        public static string ModifyTime;
        public static int SwitchDns;


        public Dictionary<string, string> Localization = new Dictionary<string, string>();


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
        public static string[] StepTips =
{
            "准备中。。。",
            "正在获取信息。。。",
            "正在下载文件。。。",//解压文件  下载  覆盖
            "准备完毕。。。",
            //"网络正在请求。。。",
        };


        public static string Cdn = "http://u3download.douzi.com/";

        public static string[] CompressTips =
        {
            "正在解压......",
            "解压完成",
            "解压失败",
        };
        public static int CompressIndex
        {
            get; set;
        }
    }
    public static class Event
    {
        // -------------------------------------------------  mono ---------------------------------------------------------//
        // -------------------------------------------------  data ---------------------------------------------------------//

        public enum Step
        {
            Prepare,
            GetInfo,
            Download,
            Max,
        }


        public static Step StepIndex
        {
            get; set;
        }



        // -------------------------------------------------  event ---------------------------------------------------------//


        public static IEnumerator GetInfo()
        {
            WWW getData = new WWW(GlobalData.Url);
            yield return getData;
            if (getData.isDone && getData.error == null)
            {
                Debug.LogMsg(getData.text);
                JsonData jd = JsonMapper.ToObject(getData.text);

                int code = int.Parse(jd["code"].ToString());
                if (code == 0)
                {
                    GlobalData.ChannelId = int.Parse(jd["result"]["channelId"].ToString());
                    GlobalData.AppId = int.Parse(jd["result"]["appId"].ToString());
                    GlobalData.CurrentVersionId = jd["result"]["appId"].ToString();
                    GlobalData.VerifyVersionId = jd["result"]["verifyVersionId"].ToString();
                    GlobalData.ChannelName = jd["result"]["channelName"].ToString();
                    GlobalData.DownloadUrl = jd["result"]["downloadUrl"].ToString();
                    GlobalData.LobbyConfigUrl = jd["result"]["lobbyConfigUrl"].ToString();
                    GlobalData.UpdateDesription = jd["result"]["updateDesription"].ToString();
                    GlobalData.ModifyTime = jd["result"]["modifyTime"].ToString();
                    GlobalData.SwitchDns = int.Parse(jd["result"]["switchDns"].ToString());
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

    }


}