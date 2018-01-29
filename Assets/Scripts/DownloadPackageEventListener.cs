using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bean.Hall
{

    public class DownloadPackageEventListener : MonoBehaviour
    {
        UnityEngine.UI.Image image;

        public List<string> urlGroup;
        public List<string> packageGroup;

        //public int state;//0 ing 1 success 2 failed
        enum State
        {
            None,
            Ing,
            Success,
            Failed,
            Max,
        }
        State state = State.None;
        float percent;
        public bool Doing
        {
            get; set;
        }

        zcode.AssetBundlePacker.PackageDownloader packageDownloader;

        void Toggle()
        {
            Doing = !Doing;
        }

        void StartDownloadPackge()
        {
            packageDownloader = gameObject.GetComponent<zcode.AssetBundlePacker.PackageDownloader>();
            if (packageDownloader == null)
                packageDownloader = gameObject.AddComponent<zcode.AssetBundlePacker.PackageDownloader>();

            if (urlGroup == null || packageGroup == null || urlGroup.Count <= 0 || packageGroup.Count <= 0)
            {
                Debug.LogMsg("Url or Package is invalid");
                EndDownloadPackge();
                return;
            }
            packageDownloader.StartDownload(urlGroup, packageGroup);
            
            Doing = true;
            state = State.None;
            percent = 0;
            image.fillAmount = 0;

        }

        void EndDownloadPackge()
        {
            image.fillAmount = 1;

            state = State.None;
            percent = 0;
            if (packageDownloader != null)
            {
                Destroy(packageDownloader);
                packageDownloader = null;
            }
            Doing = false;

        }

        private void Awake()
        {
            //urlGroup.Add("http://u3download.douzi.com/");
            image = GetComponent<UnityEngine.UI.Image>();
        }

        void Start()
        {

        }

        void Update()
        {
            if (!Doing)
                return;

            var cv = packageDownloader.CurrentStateCompleteValue;
            var tv = packageDownloader.CurrentStateTotalValue;
            percent = cv / tv;
            image.fillAmount = percent;

            if (packageDownloader.IsDone)
            {
                if(packageDownloader.IsFailed)
                {
                    state = State.Failed;
                }
                else
                {
                    state = State.Success;
                }
                EndDownloadPackge();
            }
            else
            {
                //packageDownloader.AbortDownload();
            }

        }

        void OnDestroy()
        {
            urlGroup = null;
            packageGroup = null;
        }

    }

}