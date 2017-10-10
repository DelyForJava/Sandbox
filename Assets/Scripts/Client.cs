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
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.Find("EventSystem"));
            canvas_ = GameObject.Find("Canvas").transform;
            DontDestroyOnLoad(canvas_);

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
            //AssetBundleManager.Instance.Version
            return true;
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

            var resReload = canvas_.Find("Loading").gameObject.AddComponent<ResReload>();
            resReload.SendMessage("OnReload","Main",SendMessageOptions.DontRequireReceiver);
            luaClient_ = gameObject.AddComponent<LuaClient>();

        }

        // -------------------------------------------------  event ---------------------------------------------------------//
        void HotupdateCompleted()
        {
            //Debug.LogMsg("Where am I");
        }

    }

}