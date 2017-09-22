using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{

    public class ShieldClient : MonoBehaviour
    {
        public enum WaitType
        {
            Local,
            Network,
        }

        string[] waitTypeTips_ =
        {
            "正在准备。。。",
            "网络请求中。。。",
        };

        float overtime_;
        /*============================  Model ============================*/
        public bool OnOrOff
        {
            get; set;
        }
        public WaitType CurrentWaitType
        {
            get; set;
        }
        /*============================  View ============================*/
        Transform shield_;
        Text tip_;
        /*============================  Event ============================*/

        void Awake()
        {
            overtime_ = 0.2f;
            OnOrOff = false;

            shield_ = GameObject.Find("Canvas/Shield").transform;
            shield_.gameObject.SetActive(false);

            tip_ = shield_.Find("Text").GetComponent<Text>();
        }

        void ChangeView()
        {
            if (!shield_ || !tip_)
                return;

            tip_.text = waitTypeTips_[(int)CurrentWaitType];
            shield_.gameObject.SetActive(OnOrOff);
        }

        public void ChangeModel(bool onOrOff,WaitType waitType=WaitType.Local)
        {
            OnOrOff = onOrOff;
            CurrentWaitType = waitType;
            ChangeView();
        }

        void OnShieldToggle()
        {
            ChangeView();
        }

    }

}