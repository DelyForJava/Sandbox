using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class HotupdateClient : MonoBehaviour
    {
        /*============================  Model ============================*/
        /*============================  View ============================*/
        /*============================  Event ============================*/
        Transform download_;

        Text state_;
        Transform panel_;
        Text link_;
        Image bar_;
        Text percent_;
        Text desc_;
        Text next_;

        int compress_index_ = -1;
        bool changed_;
        void Awake()
        {
            download_ = GameObject.Find("Canvas/Download").transform;

            state_ = download_.Find("State").GetComponent<Text>();
            panel_ = download_.Find("Panel");
            panel_.gameObject.SetActive(false);
            link_ = panel_.Find("Link").GetComponent<Text>();
            link_.text = GlobalData.Cdn;
            bar_ = panel_.Find("Bar").GetComponent<Image>();
            percent_ = panel_.Find("Percent").GetComponent<Text>();
            desc_ = panel_.Find("Desc").GetComponent<Text>();
            next_ = panel_.Find("Next").GetComponent<Text>();

            ResourcesManager.LoadPattern = new AssetBundleLoadPattern();

            //ResourcesManager.LoadPattern = new ResourcesLoadPattern();
            
        }

        void OnDestroy()
        {
            next_ = null;
            desc_ = null;
            percent_ = null;
            bar_ = null;
            link_ = null;
            panel_ = null;
            state_ = null;
            download_ = null;
        }

        void Update()
        {
        }

        void OnViewChanged()
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
        public IEnumerator FlowPackage()
        {
            while (true)
            {
                if (!panel_)
                    break;
                if (!AssetBundleManager.Instance.WaitForLaunch())
                {
                    if (GlobalData.CompressIndex != compress_index_)
                    {
                        changed_ = true;
                    }
                    compress_index_ = GlobalData.CompressIndex;
                    GlobalData.CompressIndex = 0;
                    panel_.gameObject.SetActive(false);
                }
                else
                {
                    if (AssetBundleManager.Instance.IsReady)
                        Ready();
                    else if (AssetBundleManager.Instance.IsFailed)
                        Failed();
                    panel_.gameObject.SetActive(true);
                }
                state_.text = GlobalData.CompressTips[GlobalData.CompressIndex];

                //if (changed_)
                //    gameObject.SendMessage(Event.HotupdateToClient, SendMessageOptions.DontRequireReceiver);
                changed_ = false;
                yield return null;
            }
        }

        public IEnumerator Flow()
        {
            while (true)
            {
                if (!panel_)
                    break;
                if (!AssetBundleManager.Instance.WaitForLaunch())
                {
                    if (GlobalData.CompressIndex != compress_index_)
                    {
                        changed_ = true;
                    }
                    compress_index_ = GlobalData.CompressIndex;
                    GlobalData.CompressIndex = 0;
                    panel_.gameObject.SetActive(false);
                }
                else
                {
                    if (AssetBundleManager.Instance.IsReady)
                        Ready();
                    else if (AssetBundleManager.Instance.IsFailed)
                        Failed();
                    panel_.gameObject.SetActive(true);
                }
                state_.text = GlobalData.CompressTips[GlobalData.CompressIndex];

                //if (changed_)
                //    gameObject.SendMessage(Event.HotupdateToClient, SendMessageOptions.DontRequireReceiver);
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
            url_group.Add(GlobalData.Cdn);
            updater_.StartUpdate(url_group);
        }

        void Ready()
        {
            if (GlobalData.CompressIndex != compress_index_)
            {
                changed_ = true;
            }
            compress_index_ = GlobalData.CompressIndex;
            GlobalData.CompressIndex = 1;

            if (updater_ == null)
            {
            }
            else
            {
                desc_.text = GlobalData.HotupdateTips[(int)updater_.CurrentState];

                float c = updater_.CurrentStateCompleteValue;
                float t = updater_.CurrentStateTotalValue;
                float p = c / t;
                bar_.fillAmount = p;
                percent_.text = (100 * p).ToString();

                if (!updater_.IsDone && !updater_.IsFailed)
                {
                    //if (GUI.Button(new Rect(0, 80f + 200, Screen.width, 20f), "中断更新"))
                    //{
                    //Debug.Log("Abort Update");
                    //updater_.AbortUpdate();
                    //Destroy(updater_);
                    //}

                }
                else if (updater_.IsDone)
                {
                    if (updater_.IsFailed)
                    {
                        //if (GUI.Button(new Rect(0, 80f + 200, Screen.width, 20f), "更新失败，重新开始"))
                        //{
                        //    Destroy(updater_);
                        //}
                        next_.text = "失败!!!";
                    }
                    else
                    {
                        next_.text = "成功!!!";
                        gameObject.SendMessage("HotupdateCompleted");
                        download_.gameObject.SetActive(false);
                        Destroy(updater_);
                        Destroy(this);
                    }

                }

            }

        }

        void Failed()
        {
            if (GlobalData.CompressIndex != compress_index_)
            {
                changed_ = true;
            }
            compress_index_ = GlobalData.CompressIndex;
            GlobalData.CompressIndex = 2;
        }

        void OnClickOk()
        {
            LaunchUpdater();
        }

        void OnClickCancel()
        {
            gameObject.SendMessage("HotupdateCompleted");
            download_.gameObject.SetActive(false);
            Destroy(updater_);
            Destroy(this);
        }

    }

}