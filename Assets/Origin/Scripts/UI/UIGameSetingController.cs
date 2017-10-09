using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using odao.scmahjong;
using CPP;
using UnityEngine.Networking;

public class UIGameSetingController : SingletonBehaviour<UIGameSetingController>, UIController
{ 
    

    public UIGameSeting _viewSeting;
    public Transform _prefab;
    SpawnPool _spawnPool;

    public void Reset()
    {
        clearHeadInfo();
    }
    public void OnRefresh()
    {
        OnRefreshHeadAll();
    }

    public bool Load()
    {
        if (_prefab != null)
            return true;

        if (!PoolManager.Pools.ContainsKey("InGame2dRes"))
        {
            Debug.LogError("No InGame2dRes Prefab Loaded!!!");
            return false;
        }
        _spawnPool = PoolManager.Pools["InGame2dRes"];
        if (!_spawnPool.prefabs.ContainsKey("Canvas_GameUIInfo"))
        {
            Debug.LogError("No Canvas_GameUIInfo Prefab Loaded!!!");
            return false;
        }
        _prefab = _spawnPool.Spawn("Canvas_GameUIInfo");

        loadSetingUI();
        loadPlayerInfoUI();
        
        return true;
    }

    public void Unload()
    {
        if (_spawnPool != null && _spawnPool.IsSpawned(_prefab.transform))
            _spawnPool.Despawn(_prefab.transform, _spawnPool.transform);
        _spawnPool = null;
        _prefab = null;
        _viewSeting = null;
    }

    public bool OpenViewRoot()
    {
        if (!Load())
            return false;

        if (_prefab != null && _prefab.gameObject.activeSelf == false)
            _prefab.gameObject.SetActive(true);
        return true;
    }
    public void CloseViewRoot()
    {
        if (!Load())
            return;
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
            _prefab.gameObject.SetActive(false);
    }

    public bool Open()
    {
        if (!Load())
            return false;

        if (_prefab != null && _prefab.gameObject.activeSelf == false)
            _prefab.gameObject.SetActive(true);
        return true;
    }
    public void Close()
    {
        if (!Load())
            return;
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
            _prefab.gameObject.SetActive(false);
    }

	public bool IsOpen()
	{
		if(_prefab != null && _prefab.gameObject.activeSelf == true)
		{
			return true;
		}
		return false;
	}

    #region SetingUI
    public void loadSetingUI()
    {
        //binding ui view

        _viewSeting = _prefab.transform.Find("PanelSeting").GetComponent<UIGameSeting>() ?? _prefab.transform.Find("PanelSeting").gameObject.AddComponent<UIGameSeting>();
        //_viewSeting._panel = _viewSeting.transform.Find("PanelSeting");
        _viewSeting._panel = _viewSeting.transform;
        _viewSeting._btnMenu = _viewSeting._panel.Find("Button_Menu").GetComponent<Button>();

        _viewSeting._imgMenu2Bg = _viewSeting._panel.Find("Image_Bg").GetComponent<Image>();
        _viewSeting._btnHelp = _viewSeting._panel.Find("Image_Bg/Button_Help").GetComponent<Button>();
        _viewSeting._btnSeting = _viewSeting._panel.Find("Image_Bg/Button_Seting").GetComponent<Button>();
        _viewSeting._btnExit = _viewSeting._panel.Find("Image_Bg/Button_Exit").GetComponent<Button>();


        // add  button event
        _viewSeting._btnMenu.onClick.RemoveAllListeners();
		_viewSeting._btnMenu.onClick.AddListener(delegate() { UIOperation.Instance.OnClickMenu(this); });

        _viewSeting._btnHelp.onClick.RemoveAllListeners();
        _viewSeting._btnHelp.onClick.AddListener(delegate() { UIOperation.Instance.OnClickHelp(this); });

        _viewSeting._btnSeting.onClick.RemoveAllListeners();
        _viewSeting._btnSeting.onClick.AddListener(delegate() { UIOperation.Instance.OnClickSeting(this); });

        _viewSeting._btnExit.onClick.RemoveAllListeners();
        _viewSeting._btnExit.onClick.AddListener(delegate() { UIOperation.Instance.OnClickExit(this); });
    }

    /// <summary>
    /// 显示或者隐藏菜单按钮
    /// </summary>
    public void showMenuBtn()
    {
        if (_viewSeting)
        {
            bool isVisible = _viewSeting._imgMenu2Bg.gameObject.activeSelf;
            Debug.Log("***isVisible:" + isVisible);
            _viewSeting._imgMenu2Bg.gameObject.SetActive(!isVisible);
        }
    }
    #endregion

    #region PlayerInfoUI
    public void loadPlayerInfoUI()
    {
   
        for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++ )
        {
            string viewName = "PanelUserInfo/player" + (i + 1);
            _viewSeting._viewHeadInfos[i] = _prefab.transform.Find(viewName).GetComponent<UIGameHeadInfoView>() ?? _prefab.transform.Find(viewName).gameObject.AddComponent<UIGameHeadInfoView>();
            bindingHeadUIView(_viewSeting._viewHeadInfos[i]);
            _viewSeting._viewHeadInfos[i]._btnHead.onClick.RemoveAllListeners();
            _viewSeting._viewHeadInfos[i]._btnHead.onClick.AddListener(delegate() { UIOperation.Instance.OnClickHead(i, this); });
        }
        //clearHeadInfo();
		_viewSeting._tranHuanpai = _prefab.transform.Find("PanelUserInfo/img_huanpai");
		_viewSeting._tranHuanpai.gameObject.SetActive (false);
		_viewSeting._btnHuanpai = _prefab.transform.Find("PanelUserInfo/img_huanpai/btn_huanpai").GetComponent<Button>();

		_viewSeting._btnHuanpai.onClick.RemoveAllListeners();
		_viewSeting._btnHuanpai.onClick.AddListener(delegate() { UIOperation.Instance.OnClickHuanpai(this); });

    }

    public void bindingHeadUIView(UIGameHeadInfoView headInfoView)
    {
        headInfoView._btnHead = headInfoView.transform.Find("Button_head").GetComponent<Button>();
        headInfoView._imgZhuang = headInfoView.transform.Find("Image_zhuang").GetComponent<Image>();
        headInfoView._imgDingqueWan = headInfoView.transform.Find("Image_dingqueWan").GetComponent<Image>();
        headInfoView._imgDingqueTiao = headInfoView.transform.Find("Image_dingqueTiao").GetComponent<Image>();
        headInfoView._imgDingqueTong = headInfoView.transform.Find("Image_dingqueTong").GetComponent<Image>();
        headInfoView._imgScore = headInfoView.transform.Find("Image_score").GetComponent<Image>();
        headInfoView._textMoney = headInfoView.transform.Find("text_money").GetComponent<Text>();
        headInfoView._textName = headInfoView.transform.Find("text_name").GetComponent<Text>();
        headInfoView._imgGameStateDQ = headInfoView.transform.Find("image_playerStateDQ").GetComponent<Image>();
        headInfoView._imgGameStateXP = headInfoView.transform.Find("image_playerStateXP").GetComponent<Image>();

        headInfoView._imgZhuang.gameObject.SetActive(false);
        headInfoView._imgDingqueWan.gameObject.SetActive(false);
        headInfoView._imgDingqueTiao.gameObject.SetActive(false);
        headInfoView._imgDingqueTong.gameObject.SetActive(false);
        headInfoView._imgGameStateDQ.gameObject.SetActive(false);
        headInfoView._imgGameStateXP.gameObject.SetActive(false);
    }


    public void OnRefreshHeadAll()
    {
        OnRefreshHeadInfo();
        OnRefreshHeadDingqueState();
        OnRefreshHeadZhuangState();
        OnRefreshPlayerState();
    }

    /// <summary>
    /// 刷新头像ui
    /// </summary>
    public void OnRefreshHeadInfo()
    {
        for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++ )
        {
            _viewSeting._viewHeadInfos[i].gameObject.SetActive(true);
            _viewSeting._viewHeadInfos[i]._textMoney.text = UIOperation.Instance._headDatas[i].money.ToString();
            _viewSeting._viewHeadInfos[i]._textName.text = UIOperation.Instance._headDatas[i].name;
           
        }
    }

    /// <summary>
    /// 刷新定缺状态
    /// </summary>
    public void OnRefreshHeadDingqueState()
    {
        for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
        {
            _viewSeting._viewHeadInfos[i]._imgDingqueWan.gameObject.SetActive(false);
            _viewSeting._viewHeadInfos[i]._imgDingqueTiao.gameObject.SetActive(false);
            _viewSeting._viewHeadInfos[i]._imgDingqueTong.gameObject.SetActive(false);
            if (UIOperation.Instance._headDatas[i].dingque != odao.scmahjong.TileDef.Kind.NONE)
            {
                switch (UIOperation.Instance._headDatas[i].dingque)
                {
                    case odao.scmahjong.TileDef.Kind.CRAK:
                        _viewSeting._viewHeadInfos[i]._imgDingqueWan.gameObject.SetActive(true);
                        break;
                    case odao.scmahjong.TileDef.Kind.BAM:
                        _viewSeting._viewHeadInfos[i]._imgDingqueTiao.gameObject.SetActive(true);
                        break;
                    case odao.scmahjong.TileDef.Kind.DOT:
                        _viewSeting._viewHeadInfos[i]._imgDingqueTong.gameObject.SetActive(true);
                        break;
                }
            }
            

        }
    }

    /// <summary>
    /// 刷新庄家状态
    /// </summary>
    public void OnRefreshHeadZhuangState()
    {
		if(IsOpen())
		{
			for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
			{
				if (GameClient.Instance.MG.DealerIndex == i) {
					UIOperation.Instance._headDatas [i].isZhuang = true;
					_viewSeting._viewHeadInfos[i]._imgZhuang.gameObject.SetActive(true);
				} else {
					UIOperation.Instance._headDatas [i].isZhuang = false;
					_viewSeting._viewHeadInfos[i]._imgZhuang.gameObject.SetActive(false);
				}
				//_viewSeting._viewHeadInfos[i]._imgZhuang.gameObject.SetActive(UIOperation.Instance._headDatas[i].isZhuang);
			}
		}
        
    }

    public void clearHeadInfo()
    {
        for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
        {
            UIOperation.Instance._headDatas[i].clear();
        }
    }

    public void clearHeadInfoAndRefresh()
    {
        clearHeadInfo();
        OnRefreshHeadAll();
    }

    /// <summary>
    /// 显示玩家信息
    /// </summary>
    /// <param name="index">绝对位置索引, 0：自己 1：左侧，2：对面， 3：右侧</param>
    /// <param name="headIndex">头像icon</param>
    /// <param name="money">携带积分</param>
    /// <param name="name">名字</param>
    public void setHeadInfo(int index, int headIndex, long money, string name) 
    {
        if(index < GameMessage.TABLE_PLAYER_NUM)
        {
            UIOperation.Instance._headDatas[index].isShow = true;
            UIOperation.Instance._headDatas[index].headIndex = headIndex;
            UIOperation.Instance._headDatas[index].money = money;
            UIOperation.Instance._headDatas[index].name = name;
        }else{
            Debug.LogError("UIGameSetingController setHeadInfo index cross:"+index);
        }
    }

    public void setHeadInfoAndRefresh(int index, int headIndex, long money, string name)
    {
        setHeadInfo(index, headIndex, money, name);
        OnRefreshHeadInfo();
    }


    public void setDingqueInfo(int index, odao.scmahjong.TileDef.Kind dingque)
    {
        if (index < GameMessage.TABLE_PLAYER_NUM)
        {
            UIOperation.Instance._headDatas[index].dingque = dingque;
        }
        else
        {
            Debug.LogError("UIGameSetingController setDingqueInfo index cross:" + index);
        }
    }
    public void setDingqueInfoAndRefresh(int index, odao.scmahjong.TileDef.Kind dingque)
    {
        setDingqueInfo(index, dingque);
        OnRefreshHeadDingqueState();
    }

    /// <summary>
    /// 设置玩家游戏状态(定缺中，换牌中)
    /// </summary>
    /// <param name="index"></param>
    /// <param name="state"></param>
    public void setGamePlayerState(int index, UIOperation.gameState state)
    {
        if (index < GameMessage.TABLE_PLAYER_NUM)
        {
            UIOperation.Instance._headDatas[index].gameState = state;
        }
        else
        {
            Debug.LogError("UIGameSetingController setGamePlayerState index cross:" + index);
        }
    }
    public void setGamePlayerStateAndOnRefresh(int index, UIOperation.gameState state)
    {
        setGamePlayerState(index, state);
        OnRefreshPlayerState();
    }
    public void OnRefreshPlayerState()
    {
        for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
        {
            if (UIOperation.Instance._headDatas[i].gameState != UIOperation.gameState.NONE)
            {
                _viewSeting._viewHeadInfos[i]._imgGameStateDQ.gameObject.SetActive(false);
                _viewSeting._viewHeadInfos[i]._imgGameStateXP.gameObject.SetActive(false);
                
                switch (UIOperation.Instance._headDatas[i].gameState)
                {
				case UIOperation.gameState.DINGQUE:
					{
						if (UIOperation.Instance.SelfIndex != i) {
							_viewSeting._viewHeadInfos[i]._imgGameStateDQ.gameObject.SetActive(true);
						}
					}
                        break;
				case UIOperation.gameState.XUANPAI:
					{
						if (UIOperation.Instance.SelfIndex != i) {
							_viewSeting._viewHeadInfos[i]._imgGameStateXP.gameObject.SetActive(true);
						} else {
							_viewSeting._viewHeadInfos[i]._imgGameStateXP.gameObject.SetActive(false);
							_viewSeting._tranHuanpai.gameObject.SetActive (true);
						}
					}
                        break;
                }
                
            }
            else {
                _viewSeting._viewHeadInfos[i]._imgGameStateDQ.gameObject.SetActive(false);
                _viewSeting._viewHeadInfos[i]._imgGameStateXP.gameObject.SetActive(false);
            }
        }
    }
   
	public void CloseHuanpaiTips()
	{
		if(_prefab != null && _prefab.gameObject.activeSelf == true)
		{
			if(_viewSeting._tranHuanpai != null && _viewSeting._tranHuanpai.gameObject.activeSelf)
				_viewSeting._tranHuanpai.gameObject.SetActive (false);
		}
	}


    #endregion

    public void StartLoadImg(string str)
    {
        StartCoroutine(LoadHeadImg(str));
    }

    IEnumerator LoadHeadImg(string str)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(str))
        {
            yield return www.Send();
            
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D tex = new Texture2D(1, 1);
                tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                //test
                Image someGameObject = new GameObject().AddComponent<Image>();
                someGameObject.sprite = image;
            }
        }
    }

    
}
