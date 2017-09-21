using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CPP;

public class UIGameSeting : MonoBehaviour {
    //Canvas_GameUIInfo PanelSeting 

    public Transform _panel;
    public Image _imgMenu2Bg;
    public Button _btnHelp;
    public Button _btnSeting;
    public Button _btnExit;
    public Button _btnMenu;
    public UIGameHeadInfoView[] _viewHeadInfos;
	public Button _btnHuanpai;
	public Transform _tranHuanpai;

    void Awake()
    {
        _viewHeadInfos = new UIGameHeadInfoView[GameMessage.TABLE_PLAYER_NUM];
    }
	void Start () {
        
	}
	
	void Update () {
		
	}
}
