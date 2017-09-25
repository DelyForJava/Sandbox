using System.Collections;

using UnityEngine;

namespace Bean.Hall
{
    public class Client : MonoBehaviour
    {
        HotupdateClient hotupdateClient_;

        LuaClient luaClient_;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            StartCoroutine(Flow());
        }
        void OnDestroy()
        {
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
            hotupdateClient_ = GetComponent<HotupdateClient>();
            hotupdateClient_.enabled = true;

            //shieldClient_.CurrentWaitType = ShieldClient.WaitType.Local;
            //shieldClient_.OnOrOff = true;
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

                yield return hotupdateClient_.Flow();

                //yield return Download();
            }
            yield return null;
            Event.StepIndex = Event.Step.Max;


        }

        // -------------------------------------------------  event ---------------------------------------------------------//
        void HotupdateCompleted()
        {
            Debug.LogMsg("Where am I");

            luaClient_ = gameObject.AddComponent<LuaClient>();
            var resReload = GameObject.Find("Canvas/Origin").GetComponent<ResReload>();
            resReload.gameObject.SendMessage("OnReload");
        }

    }

}