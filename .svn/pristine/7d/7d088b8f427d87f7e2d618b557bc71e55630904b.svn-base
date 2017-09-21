using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PathologicalGames;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Net;

public class Loading : MonoBehaviour {
	
	public Slider slider;
    public static Common2dRes _common2dRes = null;
	int count = 0;
	PathologicalGames.SpawnPool spawnPool;

    public Text httpReturnMessageText;
    public Text loadingMessageText;
    private float loadspeed; 

    private string loginMessageStr;
    private string loadingMessageStr= "loading...";
    
    private Image barImage;
    private float fillAmount = 0.0f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void StartUp()
	{
	}

	void OnEnable()
	{
		Debug.Log("Loading OnEnable...");
		GameLoading.OnStartLoading += OnStartLoading;
		GameLoading.OnLoading += OnLoading;
		GameLoading.OnFinishLoading += OnFinishLoading;
	}

	void OnDisable()
	{
		Debug.Log("Loading OnDisable...");
		GameLoading.OnStartLoading -= OnStartLoading;
		GameLoading.OnLoading -= OnLoading;
		GameLoading.OnFinishLoading -= OnFinishLoading;
	}

	void Awake()
	{
		Debug.Log("Loading Awake...");
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Loading Start...");
        var login = UILoginController.Instance;
		var mainmenu = UIMainMenuController.Instance;

        InitInfo();
       
        //LoadAssetBundles();
    }

    /// <summary>
    /// 初始化配置信息
    /// </summary>
    private void InitInfo()
    {
        loginMessageStr = "开始请求Https...";
        loadspeed = 0.2f;
    }


    void OnStartLoading(GameLoading gl)
	{
		Debug.Log ("start loading " + GameLoading.level);
		if (slider != null) {
			slider.maxValue = 100;
		}

		Resources.UnloadUnusedAssets ();

		GameClient.Instance.AssetLoader.CheckVersion ("", delegate() {
		});

		CreateDontDestroy ();

		switch (GameLoading.level)
		{
		case 0://
			break;
		case 1://login
			{
				
			}
			break;
		case 2://mainmenu
			break;
		case 3://game
			{		
				var effectRes = new InGameEffectRes ();
				Debug.Log ("current task count -> " + GameClient.Instance.AssetLoader.TaskCount());
				var res0 = new MahjongGameRes ();
				Debug.Log ("current task count -> " + GameClient.Instance.AssetLoader.TaskCount());
				var res1 = new InGame2dRes ();
				Debug.Log ("current task count -> " + GameClient.Instance.AssetLoader.TaskCount());
			}
			break;
		default:
			Debug.LogError("Unknown level :" + GameLoading.level);
			break;
		}
	}

	void OnLoading()
	{
		if (slider == null) {
			//strange Start is called after OnLoading 
			Debug.LogWarning("No Slider...");
			return;
		} 

		//Debug.Log ("current task count -> " + ResourceManager.Instance.Loader.TaskCount());
		slider.value = ++count*loadspeed % 100;

		if (GameClient.Instance.AssetLoader.TaskCount() == 0) {
			if (slider.value >= 99) {
				GameLoading.FinishLoading = true;
				slider.value = 100;

                loadingMessageStr = "OK!";
            }

        }
        loadingMessageText.text = loadingMessageStr + Convert.ToInt32(slider.value) +"%";
        //loadingMessageText.text = savePath;

    }

	void OnFinishLoading(GameLoading gl)
	{
	}
		
	static bool _easytouch = false;
	static bool _unitybridge = false;
	static bool _debugcanvas = false;
	void CreateDontDestroy()
	{
		/*
		if (GameObject.Find ("EasyTouch") == null && _easytouch == false) {//Debug.LogError ("P.");
			_easytouch = true;
			ResourceManager.Instance.LoadAsync ("Prefabs_InGame", "EasyTouch", delegate(Object obj) {
				GameObject prefab = (GameObject)obj;
				GameObject go = GameObject.Instantiate (prefab) as GameObject; 
				go.name = "EasyTouch";
				GameObject.DontDestroyOnLoad (go);
			});
		}

		if (GameObject.Find ("UnityBridge") == null && _unitybridge == false) {//Debug.LogError ("P..");
			_unitybridge = true;
			ResourceManager.Instance.LoadAsync ("Prefabs_InGame", "UnityBridge", delegate(Object obj) {
				GameObject prefab = (GameObject)obj;
				GameObject go = GameObject.Instantiate (prefab) as GameObject; 
				go.name = "UnityBridge";
				GameObject.DontDestroyOnLoad (go);
			});
		}
		*/
        Debug.Log("Canvas_Debug***");
        if (null == _common2dRes)
        {
            _common2dRes = new Common2dRes();
        }
        
		/*if (GameObject.Find ("Canvas_Debug") == null && _unitybridge == false) {
            Debug.Log("Canvas_Debug ok***");
			_unitybridge = true;
			ResourceManager.Instance.LoadAsync ("Debug", "Canvas_Debug", delegate(Object obj) {
				GameObject prefab = (GameObject)obj;
				GameObject go = GameObject.Instantiate (prefab) as GameObject; 
				go.name = "Canvas_Debug";
				GameObject.DontDestroyOnLoad (go);
			});
		}*/
	}

    private string savePath;
    private void LoadAssetBundles()
    {
        //savePath = Application.dataPath + "!assets/"; 
        savePath = Application.persistentDataPath;
        //ShowTip.Instance.ShowMessage(savePath);

        //本地测试成功  ftp://10.0.70.79/StreamingAssets   http://ov0u5wsye.bkt.clouddn.com/StreamingAssets          
        try
        {
            LoadAssetBundle.Instance.DownLoadAssets2LocalWithDependencies("http://ovw4qubl1.bkt.clouddn.com/StreamingAssets", "StreamingAssets", "logo", savePath, () =>
            {
                AssetBundle assetTarget = LoadAssetBundle.Instance.GetLoadAssetFromLocalFile("StreamingAssets", "logo", "logo", Application.persistentDataPath);
                //GameObject.Instantiate(obj);
                //Sprite sprite = GameObject.Find("logo").GetComponent<Image>().sprite;
                Texture2D loadTexture = assetTarget.LoadAsset("logo") as Texture2D;
                Sprite loadSprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0f, 0f));
                GameObject.Find("logo").GetComponent<Image>().sprite = loadSprite;
            });

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
