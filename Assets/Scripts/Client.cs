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
            canvas_ = GameObject.Find("Canvas").transform;


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
            return true;
        }
        IEnumerator Prepare()
        {
            AssetBundleManager.Instance.WaitForLaunch();

            yield return null;
        }
        IEnumerator GetInfo()
        {
            //yield return Event.GetInfo();
            yield return null;
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

            luaClient_ = gameObject.AddComponent<LuaClient>();
            var resReload = canvas_.Find("Origin").gameObject.GetComponent<ResReload>();
            resReload.gameObject.SendMessage("OnReload");
        }

        // -------------------------------------------------  event ---------------------------------------------------------//
        void HotupdateCompleted()
        {
            //Debug.LogMsg("Where am I");
        }

    }

}