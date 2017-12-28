using System.Collections;

using UnityEngine;

namespace Bean.Hall
{
    public class Client : MonoBehaviour
    {
        HotupdateClient hotupdateClient_;

        LuaClient luaClient_;

       Transform canvas_;

        void Awake()
        {
            var client = GameClient.Instance;

            DontDestroyOnLoad(gameObject);
            canvas_ = GameObject.Find("Canvas").transform;
            canvas_.Find("Login").gameObject.SetActive(false);
            canvas_.Find("Loading").gameObject.SetActive(false);
            //DontDestroyOnLoad(canvas_);
            //DontDestroyOnLoad(GameObject.Find("EventSystem"));

            StartCoroutine(Flow());
        }
        void OnDestroy()
        {
            canvas_ = null;

            luaClient_ = null;
            hotupdateClient_ = null;
        }

        // -------------------------------------------------  logic ---------------------------------------------------------//
        bool IsNeedHotupdate()
        {
            uint web_cdn_version = uint.Parse(GlobalData.VerifyVersionId) ;
            if (web_cdn_version == 1000)
                return false;
            Debug.LogMsg(web_cdn_version);
            Debug.LogMsg(AssetBundleManager.Instance.Version);

            if (web_cdn_version != AssetBundleManager.Instance.Version)
            {
                return true;
            }
            return false;
        }
        IEnumerator Prepare()
        {
            AssetBundleManager.Instance.WaitForLaunch();

            yield return null;
        }
        IEnumerator GetInfo()
        {
            yield return Event.GetInfo();
            //yield return null;
        }
        IEnumerator Download()
        {
            yield return hotupdateClient_.Flow();

        }
        IEnumerator Flow()
        {
            Event.StepIndex = Event.Step.Prepare;
            yield return Prepare();

            Event.StepIndex = Event.Step.GetInfo;
            yield return GetInfo();

            bool isNeed = IsNeedHotupdate();
            if (isNeed)
            {
                Event.StepIndex = Event.Step.Download;
                GameObject.Find("Canvas").transform.Find("Download").gameObject.SetActive(true);
                hotupdateClient_ = gameObject.AddComponent<HotupdateClient>();
                hotupdateClient_.enabled = true;
                yield return hotupdateClient_.Flow();

                //yield return Download();
            }
            yield return null;
            Event.StepIndex = Event.Step.Max;

            //var resReload = canvas_.Find("Loading").gameObject.AddComponent<ResReload>();
            //resReload.SendMessage("OnReload","Main",SendMessageOptions.DontRequireReceiver);
            luaClient_ = gameObject.AddComponent<LuaClient>();

        }

        // -------------------------------------------------  event ---------------------------------------------------------//
        void HotupdateCompleted()
        {
            //Debug.LogMsg("Where am I");
        }

    }

}