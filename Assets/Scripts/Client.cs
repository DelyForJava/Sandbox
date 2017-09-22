using System.Collections;

using UnityEngine;

namespace Bean.Hall
{
    public class Client : MonoBehaviour
    {
        HotupdateClient hotupdateClient_;

        ShieldClient shieldClient_;

        LuaClient luaClient_;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            StartCoroutine(Flow());
        }
        void OnDestroy()
        {
            shieldClient_ = null;
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

            shieldClient_ = GetComponent<ShieldClient>();
            shieldClient_.enabled = true;
            //shieldClient_.CurrentWaitType = ShieldClient.WaitType.Local;
            //shieldClient_.OnOrOff = true;
            shieldClient_.ChangeModel(true);
            yield return null;
        }
        IEnumerator GetInfo()
        {
            yield return Event.GetInfo();
        }
        IEnumerator Download()
        {
            while (true)
            {
                yield return hotupdateClient_.Flow();
            }

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
                yield return Download();
            }

            Event.StepIndex = Event.Step.Max;


        }

        // -------------------------------------------------  event ---------------------------------------------------------//
        void HotupdateToClient()
        {
            Debug.LogMsg(Event.HotupdateToClient);

            //Global.SendMessage(gameObject, Event.UIMessage.Hotupdate);

        }

    }

}