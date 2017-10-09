using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : SingletonBehaviour<PlayerDataManager> {

    private PlayerLobbyInfo player = UIOperation.playerLobbyInfo;

    public Image headImage;
    public Text userId;
    public Image sexImage;
    public Slider expSlider;

    public Text nickName;
    public Text goldNum;
    public Text diaNum;
    public Text beanNum;
    public Text expText;
    public Text vipLvText;
    public Text goldNumTop;
    public Text diaNumTop;

    public Button editNameBtn;
    public Button addGoldBtn;
    public Button addDiaBtn;
    public Button getBeanBtn;
    public Button changeGenderBtn;
    public Button confirmEditNameBtn;
    public Button confirmGenderBtn;

    public InputField nameInputField;
    public GameObject comfirmPanel;
    public GameObject genderConfirmPanel;

    public Toggle manToggle;
    public Toggle womanToggle;

    private GameClient client = GameClient.Instance;

    // Use this for initialization
    void Start () {
        editNameBtn.onClick.AddListener(delegate
        {
            comfirmPanel.SetActive(true);
            nameInputField.text = "";
            nameInputField.placeholder.GetComponent<Text>().text = "";
        });
        confirmEditNameBtn.onClick.AddListener(delegate
        {
            if (nameInputField.text !="")
            {
                client.MahjongGamePlayer.ChangeNameReqDef(nameInputField.text);
            }
            else
            {
                //弹窗 昵称不能为空
            }
        });
        addGoldBtn.onClick.AddListener(delegate
        {

        });
        addDiaBtn.onClick.AddListener(delegate
        {

        });
        getBeanBtn.onClick.AddListener(delegate
        {

        });
        changeGenderBtn.onClick.AddListener(delegate
        {
            genderConfirmPanel.SetActive(true);
        });
        confirmGenderBtn.onClick.AddListener(delegate
        {
            if (manToggle.isOn || womanToggle.isOn)
            {
                client.MahjongGamePlayer.ChangeGenderReqDef(manToggle.isOn ? 0 : 1);
            }
            genderConfirmPanel.SetActive(false);
        });

        StartCoroutine(LoadImage(player.szWXIconURL));
        userId.text = "ID:"+player.iUserID.ToString();
        LoadGenderImage();
    }
	
	// Update is called once per frame
	void Update () {
	    if (player.szWXNickName == "")
	    {
	        nickName.text = player.szNickName;
	    }
	    else
	    {
	        nickName.text = player.szWXNickName;
	    }
	    goldNum.text = player.llGameCoin.ToString();
	    diaNum.text = player.llDiamondNum.ToString();
	    beanNum.text = player.llGoldBean.ToString();
	    goldNumTop.text = goldNum.text;
	    diaNumTop.text = diaNum.text;
	    vipLvText.text = player.cVipLv.ToString();
	    


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

    public void LoadGenderImage()
    {
        //load Sex
        Texture2D sexTexture2D;
        if (player.cGender == 0)
        {
            sexTexture2D = (Texture2D)Resources.Load("Texture_InGame/sex_man");
        }
        else
        {
            sexTexture2D = (Texture2D)Resources.Load("Texture_InGame/sex_women");
        }
        Sprite sexSprite = Sprite.Create(sexTexture2D, new Rect(0, 0, sexTexture2D.width, sexTexture2D.height), new Vector2(0.5f, 0.5f));
        sexImage.sprite = sexSprite;
        sexImage.SetNativeSize();
    }
}
