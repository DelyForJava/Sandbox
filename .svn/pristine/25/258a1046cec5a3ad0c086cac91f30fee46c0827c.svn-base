using System;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class Hotupdate : MonoBehaviour
    {
        public string URL = "http://u3download.douzi.com/";

        private static string[] state_table_ =
        {
            "正在解压......",
            "解压完成",
            "解压失败",
        };
        private int state_index_ = 0;

        private static string[] update_table_ =
        {
            "",
            "初始化更新信息",
            "连接服务器",
            "更新主配置文件",
            "下载资源",
            "解析资源",
            "清理缓存目录数据",
            "更新完成",
            "更新失败",
            "更新取消",
            "更新中断",
        };

        private Transform canvas_;

        private Text info_;

        private Transform panel_;
        private Text link_;
        private Image bar_;
        private Text percent_;
        private Text desc_;
        private Text next_;

        private string percent_text_;

        void Awake()
        {

        }
        void Start()
        {

        }

        void OnEnable()
        {
            //if (Directory.Exists(zcode.AssetBundlePacker.Common.PATH))
            //    Directory.Delete(zcode.AssetBundlePacker.Common.PATH, true);
            //if (Directory.Exists(zcode.AssetBundlePacker.Common.INITIAL_PATH))
            //    Directory.Delete(zcode.AssetBundlePacker.Common.INITIAL_PATH, true);

            //zcode.FileHelper.CopyDirectoryAllChildren(PATH, zcode.AssetBundlePacker.Common.INITIAL_PATH);
            ResourcesManager.LoadPattern = new AssetBundleLoadPattern();
            //SceneResourcesManager.LoadPattern = new AssetBundleLoadPattern();

            canvas_ = transform.Find("Canvas");
            canvas_.gameObject.SetActive(true);

            info_ = canvas_.Find("State").GetComponent<Text>();
            state_index_ = 0;

            panel_ = canvas_.Find("Panel");
            panel_.gameObject.SetActive(false);
            link_ = panel_.Find("Link").GetComponent<Text>();
            bar_ = panel_.Find("Bar").GetComponent<Image>();
            percent_ = panel_.Find("Percent").GetComponent<Text>();
            desc_ = panel_.Find("Desc").GetComponent<Text>();
            next_ = panel_.Find("Next").GetComponent<Text>();

        }

        void OnDisable()
        {
            panel_.gameObject.SetActive(false);
            canvas_.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            next_ = null;
            desc_ = null;
            percent_ = null;
            bar_ = null;
            link_ = null;
            panel_ = null;

            info_ = null;

            canvas_ = null;
        }

        private void Update()
        {
            OnUpdateState();
        }

        void OnUpdateState()
        {
            if (!AssetBundleManager.Instance.WaitForLaunch())
            {
                panel_.gameObject.SetActive(false);
                state_index_ = 0;
            }
            else
            {
                panel_.gameObject.SetActive(true);

                if (AssetBundleManager.Instance.IsReady)
                    Ready();
                else if (AssetBundleManager.Instance.IsFailed)
                    Failed();
            }
            info_.text = state_table_[state_index_] + AssetBundleManager.Instance.Version;

        }

        private Updater updater_;

        void LaunchUpdater()
        {
            updater_ = gameObject.GetComponent<Updater>();
            if (updater_ == null)
                updater_ = gameObject.AddComponent<Updater>();

            List<string> url_group = new List<string>();
            url_group.Add(URL);
            updater_.StartUpdate(url_group);
        }

        void Ready()
        {
            state_index_ = 1;

            link_.text = URL;

            if (updater_ == null)
            {
            }
            else
            {
                desc_.text = update_table_[(int)updater_.CurrentState];

                float c = updater_.CurrentStateCompleteValue;
                float t = updater_.CurrentStateTotalValue;
                float p = c / t;
                bar_.fillAmount = p;
                percent_text_ = (100 * p).ToString();
                percent_.text = percent_text_;

                if (!updater_.IsDone && !updater_.IsFailed)
                {
                    //if (GUI.Button(new Rect(0, 80f + 200, Screen.width, 20f), "中断更新"))
                    //{
                    //    Debug.Log("Abort Update");
                    //    updater_.AbortUpdate();
                    //    Destroy(updater_);
                    //}
                    next_.text = "失败!!!";

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
                        next_.text = "成功!!!";
                    }

                }

            }

        }

        void Failed()
        {
            state_index_ = 2;
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