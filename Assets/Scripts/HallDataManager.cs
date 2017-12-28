using System.Collections;
using System.Collections.Generic;
using odao.scmahjong;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallDataManager : SingletonBehaviour<HallDataManager>
{

    public Button mahjongBtn;
    public AsyncOperation operation;
    private bool isFinished = false;

    // Use this for initialization
    void Start () {

	    var client = GameClient.Instance;
        mahjongBtn.onClick.AddListener(delegate
	    {
	        client.MahjongGamePlayer.ConnectGameServer("10.0.170.26", 11011, delegate ()
	        {
	            Debug.LogError("I am connected success!!!");
	        });
            
	        client.MahjongGamePlayer.DispenseSend();
            GameObject.Find("Canvas").transform.Find("Waiting").gameObject.SetActive(true);
	    });
    }

    public void LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(1);
    }
	
	// Update is called once per frame
	void Update ()
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
            });

            operation = null;
            isFinished = false;
        }
	}

}
