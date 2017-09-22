using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bean.Hall
{
    public class HotupdateClient : MonoBehaviour
    {
        /*============================  Model ============================*/
        int compress_index_ = -1;
        bool changed_;

        /*============================  View ============================*/

        /*============================  Event ============================*/
        void Awake()
        {
            ResourcesManager.LoadPattern = new AssetBundleLoadPattern();
        }

        void OnDestroy()
        {
        }

        void Update()
        { 
        }

        void  OnViewChanged()
        {

        }

        void OnModelChanged()
        {

        }

        void OnHotupdateChanged()
        {
            OnModelChanged();
            OnViewChanged();
        }
        // -------------------------------------------------  logic ---------------------------------------------------------//

        public IEnumerator Flow()
        {
            while (true)
            {
                if (!AssetBundleManager.Instance.WaitForLaunch())
                {
                    if( Event.CompressIndex != compress_index_ )
                    {
                        changed_ = true;
                    }
                    compress_index_ = Event.CompressIndex;
                    Event.CompressIndex = 0;

                }
                else
                {
                    if (AssetBundleManager.Instance.IsReady)
                        Ready();
                    else if (AssetBundleManager.Instance.IsFailed)
                        Failed();
                }
                if (changed_)
                    gameObject.SendMessage(Event.HotupdateToClient, SendMessageOptions.DontRequireReceiver);
                changed_ = false;
                yield return null;
            }

        }

        private Updater updater_;

        void LaunchUpdater()
        {
            updater_ = gameObject.GetComponent<Updater>();
            if (updater_ == null)
                updater_ = gameObject.AddComponent<Updater>();

            List<string> url_group = new List<string>();
            url_group.Add(Event.cdn);
            updater_.StartUpdate(url_group);
        }

        void Ready()
        {
            if (Event.CompressIndex != compress_index_)
            {
                changed_ = true;
            }
            compress_index_ = Event.CompressIndex;
            Event.CompressIndex = 1;

            if (updater_ == null)
            {
            }
            else
            {
                //desc_.text = update_table_[(int)updater_.CurrentState];

                float c = updater_.CurrentStateCompleteValue;
                float t = updater_.CurrentStateTotalValue;
                float p = c / t;
                //bar_.fillAmount = p;
                //percent_text_ = (100 * p).ToString();
                //percent_.text = percent_text_;

                if (!updater_.IsDone && !updater_.IsFailed)
                {
                    //if (GUI.Button(new Rect(0, 80f + 200, Screen.width, 20f), "中断更新"))
                    //{
                    //    Debug.Log("Abort Update");
                    //    updater_.AbortUpdate();
                    //    Destroy(updater_);
                    //}
                    //next_.text = "失败!!!";

                }
                else if (updater_.IsDone)
                {
                    if (updater_.IsFailed)
                    {
                        //if (GUI.Button(new Rect(0, 80f + 200, Screen.width, 20f), "更新失败，重新开始"))
                        //{
                        //    Destroy(updater_);
                        //}
                    }
                    else
                    {
                        //next_.text = "成功!!!";
                    }

                }

            }

        }

        void Failed()
        {
            if (Event.CompressIndex != compress_index_)
            {
                changed_ = true;
            }
            compress_index_ = Event.CompressIndex;
            Event.CompressIndex = 2;
        }

        void OnClickOk()
        {
            LaunchUpdater();
        }

        void OnClickCancel()
        {
            enabled = false;
        }

    }

}