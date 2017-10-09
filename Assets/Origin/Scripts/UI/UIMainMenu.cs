using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class UIMainMenu : MonoBehaviour {

	public Button _btnMatchXL;
    public Image headImage;
    public Text nickName;
    public Text goldNum;
    public Text diamondNum;

    public Text shopGoldNum;
    public Text shopDiamondNum;

    public Button playerInfoBtn;
    public GameObject personalInfoPanel;

    // Use this for initialization
    void Start () {
        //_btnMatchXL = GameObject.Find("Canvas/Button").GetComponent<Button>();
        ItemTest();
        playerInfoBtn.onClick.AddListener(delegate
        {
            personalInfoPanel.SetActive(true);
        });
    }

    private void ShowPlayerInfo()
    {
        var player = UIOperation.playerLobbyInfo;
        StartCoroutine(LoadImage(player.szWXIconURL));
        if (player.szWXNickName=="")
        {
            nickName.text = player.szNickName;
        }
        else
        {
            nickName.text = player.szWXNickName;
        }
        
    }

	
	// Update is called once per frame
	void Update () {
	    ShowPlayerInfo();
        var player = UIOperation.playerLobbyInfo;
        goldNum.text = player.llGameCoin.ToString();
	    diamondNum.text = player.llDiamondNum.ToString();
	    shopGoldNum.text = player.llGameCoin.ToString();
	    shopDiamondNum.text = player.llDiamondNum.ToString();
    }

    IEnumerator LoadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture2D texture = www.texture;
            Sprite loadSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
            headImage.sprite = loadSprite;
        }
    }

    #region TestItem
    
    public Button addItemBtn;
    public InputField IdInputField;
    public InputField NumInputField;


    public Button deleteItemBtn;
    public Button updateItemBtn;

    public void ItemTest()
    {
        var client = GameClient.Instance;
        addItemBtn.onClick.AddListener(delegate()
        {
            client.MahjongGamePlayer.PackageAddItemReqDef(Convert.ToInt32(IdInputField.text), Convert.ToInt32(NumInputField.text));
        });

        deleteItemBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.PackageRemoveItemReqDef(Convert.ToInt32(IdInputField.text), Convert.ToInt32(NumInputField.text));
        });

        updateItemBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.PackageUpdateItemReqDef(Convert.ToInt32(IdInputField.text), (sbyte)Convert.ToInt32(NumInputField.text));
        });
    }

    #endregion

}
