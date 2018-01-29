using System.Collections;
using System.Collections.Generic;
using odao.scmahjong;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallDataManager : SingletonBehaviour<HallDataManager>
{
    public Button collection1;
    public Button collection2;
    public GameObject collectionPanel;
    public Button moreBtn;
    public Button pokerBtn;
    public Button fishingBtn;
    public Button mahjongBtn;
    public AsyncOperation operation;
    private bool isFinished = false;

    // Use this for initialization
    void Start()
    {

        var client = GameClient.Instance;
        mahjongBtn.onClick.AddListener(delegate
        {
            GameObject.Find("Canvas").transform.Find("Waiting").gameObject.SetActive(true);

            client.MahjongGamePlayer.ConnectGameServer("10.0.170.26", 11011, delegate ()
            {
                Debug.LogError("I am connected success!!!");
            });

            client.MahjongGamePlayer.DispenseSend();
        });

        //fishingBtn.onClick.AddListener(delegate
        //{
        //    List<string> list = zcode.AssetBundlePacker.AssetBundleManager.Instance.FindAllAssetBundleFilesNameByPackage("Fishing");

        //    list.RemoveAll((assetbundle_name) =>
        //    {
        //        return System.IO.File.Exists(zcode.AssetBundlePacker.Common.GetFileFullName(assetbundle_name));
        //    });
        //    if (list.Count <= 0)
        //    {
        //        zcode.AssetBundlePacker.SceneResourcesManager.LoadSceneAsync("Fishing", null, LoadSceneMode.Additive);
        //    }
        //    else
        //    {
        //        fishingBtn.gameObject.SendMessage(Bean.Hall.EventName.StartDownloadPackge);
        //    }

        //});

        pokerBtn.onClick.AddListener(delegate
        {
            List<string> list = zcode.AssetBundlePacker.AssetBundleManager.Instance.FindAllAssetBundleFilesNameByPackage("Poker");

            list.RemoveAll((assetbundle_name) =>
            {
                return System.IO.File.Exists(zcode.AssetBundlePacker.Common.GetFileFullName(assetbundle_name));
            });
            if (list.Count <= 0)
            {
                zcode.AssetBundlePacker.SceneResourcesManager.LoadSceneAsync("Poker", null, LoadSceneMode.Additive);
            }
            else
            {
                pokerBtn.gameObject.SendMessage(Bean.Hall.EventName.StartDownloadPackge);
            }
        });

        moreBtn.onClick.AddListener(delegate
        {
            collectionPanel.SetActive(true);
        });

        collection1.onClick.AddListener(delegate
        {
            List<string> list = zcode.AssetBundlePacker.AssetBundleManager.Instance.FindAllAssetBundleFilesNameByPackage("Fishing");

            list.RemoveAll((assetbundle_name) =>
            {
                return System.IO.File.Exists(zcode.AssetBundlePacker.Common.GetFileFullName(assetbundle_name));
            });
            if (list.Count <= 0)
            {
                zcode.AssetBundlePacker.SceneResourcesManager.LoadSceneAsync("Fishing", null, LoadSceneMode.Additive);
            }
            else
            {
                collection1.gameObject.SendMessage(Bean.Hall.EventName.StartDownloadPackge);
            }
        });
        
    }

    public void LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(1);
    }

    // Update is called once per frame
    void Update()
    {

        isFinished = operation != null && operation.isDone;

        if (isFinished)
        {
            //
            var client = GameClient.Instance;
            client.MahjongGamePlayer.ConnectGameServer(RoomData.szServerIP, (int)RoomData.ulServerPort, delegate ()
            {
                Debug.LogError("222222222222I am connected success!!!");
                client.MahjongGamePlayer.LoginToRoomServerSend();
                client.MahjongGamePlayer.GetChannelInfoSend();
                GameObject.Find("Canvas").transform.Find("Waiting").gameObject.SetActive(false);

            });

            operation = null;
            isFinished = false;
        }
    }

}
