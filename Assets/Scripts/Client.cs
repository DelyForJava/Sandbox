using System.Collections;

using UnityEngine;

using LitJson;

namespace Bean.Hall
{
    public class Client : MonoBehaviour
    {
        int channelId;
        int appId;
        string currentVersionId;
        string verifyVersionId;
        string channelName;
        string downloadUrl;
        string lobbyConfigUrl;
        string updateDesription;
        string modifyTime;
        int switchDns;

        private Hotupdate hotupdate_;
        private Fullscreen fullscreen_;

        private string url = "http://10.0.70.121:8080/plat/lobbyInfo?channelId=10111&Content-Type=application/json";

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            hotupdate_ = transform.Find("Hotupdate").GetComponent<Hotupdate>();
            fullscreen_ = transform.Find("Fullscreen").GetComponent<Fullscreen>();

        }
        void Start()
        {
            hotupdate_.enabled = true;
            //StartCoroutine(GetInfo(url));
        }
        void OnDestroy()
        {
            fullscreen_ = null;
            hotupdate_ = null;
        }
        // -------------------------------------------------  logic ---------------------------------------------------------//
        IEnumerator GetInfo(string url)
        {
            OnToggleFullscreen(true);
            WWW getData = new WWW(url);
            yield return getData;
            if (getData.isDone && getData.error == null)
            {
                Debug.LogMsg(getData.text);
                JsonData jd = JsonMapper.ToObject(getData.text);

                //Debug.LogError("accountId=" + jd["result"]["accountId"]);
                //Debug.LogError("loginToken=" + jd["result"]["loginToken"]);

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
            OnToggleFullscreen(false);

        }
        bool IsNeedHotupdate()
        {
            return true;
        }
        IEnumerator Flow()
        {
            yield return GetInfo(url);
            bool isNeed = IsNeedHotupdate();
            if (isNeed)
            {
                hotupdate_.enabled = true;
            }
            yield return null;
        }
        // -------------------------------------------------  event ---------------------------------------------------------//
        void OnToggleFullscreen(bool onOrOff,int index=1)
        {
            fullscreen_.TipIndex = index;
            fullscreen_.enabled = onOrOff;
        }
        void OnScreenPointerDown()
        {
            Debug.LogMsg("OnScreenPointerDown");
            OnToggleFullscreen(false);
        }

    }

}