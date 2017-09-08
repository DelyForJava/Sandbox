using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using zcode.AssetBundlePacker;

public class Main : MonoBehaviour
{
    public string URL = "http://ovw9js99r.bkt.clouddn.com/";

    public const string PATH = "Assets/Version_1_0/AssetBundle";
    //public const string PATH = "Assets/Version_1_1/AssetBundle";
    //public const string PATH = "Assets/Version_1_2/AssetBundle";
    //public const string PATH = "Assets/Version_1_3/AssetBundle";

    //public const string PATH = "Assets/Version_2_0/AssetBundle";
    //public const string PATH = "Assets/Version_3_0/AssetBundle";
    //public const string PATH = "Assets/Version_4_0/AssetBundle";
    //public const string PATH = "Assets/Version_5_0/AssetBundle";

    private int stage_index_ = 1;
    private Action[] stage_funcs_;

    void Awake()
    {
        stage_funcs_ = new Action[2];
        stage_funcs_[0] = OnPrepare;
        stage_funcs_[1] = OnWork;
    }
    void Start()
    {
        GameObject prefab = Resources.Load("Canvas") as GameObject;

        var canvas = Instantiate(prefab);
        canvas.transform.parent = transform;

    }



    void OnGUI()
    {
        if (stage_funcs_[stage_index_] != null)
            stage_funcs_[stage_index_]();
    }
    void OnPrepare()
    {
        if (Directory.Exists(zcode.AssetBundlePacker.Common.PATH))
            Directory.Delete(zcode.AssetBundlePacker.Common.PATH, true);
        if (Directory.Exists(zcode.AssetBundlePacker.Common.INITIAL_PATH))
            Directory.Delete(zcode.AssetBundlePacker.Common.INITIAL_PATH, true);

        zcode.FileHelper.CopyDirectoryAllChildren(PATH, zcode.AssetBundlePacker.Common.INITIAL_PATH);

        ResourcesManager.LoadPattern = new AssetBundleLoadPattern();

        SceneResourcesManager.LoadPattern = new AssetBundleLoadPattern();

        stage_index_ = 1;
    }

    void OnWork()
    {
        if (!AssetBundleManager.Instance.WaitForLaunch())
        {
            Launching();
        }
        else
        {
            if (AssetBundleManager.Instance.IsReady)
                Ready();
            else if (AssetBundleManager.Instance.IsFailed)
                Failed();
        }

    }


    public static readonly string[] STATE_DESCRIBE_TABLE =
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

    /// <summary>
    /// 更新器
    /// </summary>
    private Updater updater_;

    /// <summary>
    /// 
    /// </summary>
    void LaunchUpdater()
    {
        updater_ = gameObject.GetComponent<Updater>();
        if (updater_ == null)
            updater_ = gameObject.AddComponent<Updater>();

        List<string> url_group = new List<string>();
        url_group.Add(URL);
        updater_.StartUpdate(url_group);
    }

    /// <summary>
    /// 
    /// </summary>

    void Launching()
    {
        GUI.Label(new Rect(0f, 0f, 200f, 20f), "AssetBundlePacker is launching！");
    }

    void Ready()
    {
        //启动成功
        GUI.Label(new Rect(0f, 0f, Screen.width, 20f), "AssetBundlePacker launch succeed, Version is " + AssetBundleManager.Instance.Version);
        //下载地址
        GUI.Label(new Rect(0f, 20f, 100f, 20f), "下载地址：");
        URL = GUI.TextField(new Rect(100f, 20f, Screen.width - 100f, 20f), URL);

        //启动更新器
        if (updater_ == null)
        {
            if (GUI.Button(new Rect(0f, 40f, Screen.width, 30f), "启动更新器"))
            {
                LaunchUpdater();
            }
        }
        else
        {
            //当前更新阶段
            GUI.Label(new Rect(0, 40f, Screen.width, 20f), STATE_DESCRIBE_TABLE[(int)updater_.CurrentState]);
            //当前阶段进度
            GUI.HorizontalScrollbar(new Rect(0f, 60f, Screen.width, 30f), 0f, updater_.CurrentStateCompleteValue, 0f, updater_.CurrentStateTotalValue);

            if (!updater_.IsDone && !updater_.IsFailed)
            {
                if (GUI.Button(new Rect(0, 80f, Screen.width, 20f), "中断更新"))
                {
                    Debug.Log("Abort Update");
                    updater_.AbortUpdate();
                    Destroy(updater_);
                }
            }
            else if (updater_.IsDone)
            {
                if (updater_.IsFailed)
                {
                    if (GUI.Button(new Rect(0, 80f, Screen.width, 20f), "更新失败，重新开始"))
                    {
                        Destroy(updater_);
                    }
                }
                else
                    GUI.Label(new Rect(0, 80f, Screen.width, 20f), "更新成功");
            }
        }
    }

    void Failed()
    {
        //启动失败
        GUI.color = Color.red;
        GUI.Label(new Rect(0f, 0f, 200f, 20f), "AssetBundlePacker launch occur error!");
    }

}