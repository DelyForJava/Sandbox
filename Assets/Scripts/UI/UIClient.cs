using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class UIClient : MonoBehaviour
    {
        /*============================  Model ============================*/

        /*============================  View ============================*/
        Transform canvas_;

        Transform origin_;
        Transform hot_;
        Transform fullscreen_;

        Text tip_;

        /*============================  Event ============================*/

        void Awake()
        {
            /*---------------------------------------    init model  --------------------------------------- */

            /*---------------------------------------    init view  --------------------------------------- */
            canvas_ = GameObject.Find("Canvas").transform;

            origin_ = canvas_.Find("Origin");
            origin_.gameObject.SetActive(true);

            hot_ = canvas_.Find("Hot").transform;
            hot_.gameObject.SetActive(false);

            fullscreen_ = canvas_.Find("Fullscreen").transform;
            fullscreen_.gameObject.SetActive(true);
            tip_ = fullscreen_.Find("Text").GetComponent<Text>();

        }

        void Update()
        {

        }

        void OnDestroy()
        {
            tip_ = null;

            fullscreen_ = null;
            hot_ = null;
            origin_ = null;

            canvas_ = null;
        }

        void OnViewChanged()
        {
            if (Event.FullscreenChanged)
            {
                if (Event.StepIndex >= Event.Step.Max || Event.StepIndex == Event.Step.Download)
                {
                    fullscreen_.gameObject.SetActive(false);
                }
                else
                {
                    tip_.text = Event.StepTips[(int)Event.StepIndex];
                }

            }
            if (Event.HotChanged)
                hot_.gameObject.SetActive(true);
        }

        void OnModelChanged()
        {

        }

        void OnUIChanged()
        {
            OnModelChanged();
            OnViewChanged();
        }

    }

}