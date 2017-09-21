using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using odao.scmahjong;
using MP;

public class UIGameHuPromptController : SingletonBehaviour<UIGameHuPromptController>, UIController
{


    public UIGameHuPromptView _viewHuPrompt;
    public Transform _prefab;
    SpawnPool _spawnPool;

    public void Reset()
    {
        
    }
    public void OnRefresh()
    {
        
    }

    public bool Load()
    {
        if (_prefab != null)
            return true;
        string poolMangerName = "InGame2dRes";
        //string poolMangerName = "Common2dRes";
        if (!PoolManager.Pools.ContainsKey(poolMangerName))
        {
            Debug.LogError("No InGame2dRes Prefab Loaded!!!");
            return false;
        }
        _spawnPool = PoolManager.Pools[poolMangerName];
        if (!_spawnPool.prefabs.ContainsKey("Canvas_HuPrompt"))
        {
            Debug.LogError("No Canvas_HuPrompt Prefab Loaded!!!");
            return false;
        }
        _prefab = _spawnPool.Spawn("Canvas_HuPrompt");

        loadUI();
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
            _prefab.gameObject.SetActive(false);
        
        return true;
    }

    public void Unload()
    {
        if (_spawnPool != null && _spawnPool.IsSpawned(_prefab.transform))
            _spawnPool.Despawn(_prefab.transform, _spawnPool.transform);
        _spawnPool = null;
        _prefab = null;
        _viewHuPrompt = null;
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

	public bool OpenHuPrompt(TileDef def)
	{
		if(Open())
		{
			OnRefreshItem (def);
			return true;
		}
		return false;
	}

    public bool Open()
    {
        if (!Load())
            return false;

		if (_prefab != null && _prefab.gameObject.activeSelf == false) {
			_prefab.gameObject.SetActive (true);
		}
        return true;
    }
    public void Close()
    {
        if (!Load())
            return;
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
            _prefab.gameObject.SetActive(false);
    }

    #region loadUI
    public void loadUI()
    {
        //binding ui view
        _viewHuPrompt = _prefab.transform.Find("Panel").GetComponent<UIGameHuPromptView>() ?? _prefab.transform.Find("Panel").gameObject.AddComponent<UIGameHuPromptView>();
        
        _viewHuPrompt._imgBg = _viewHuPrompt.transform.Find("Image_Bg");
        _viewHuPrompt._objHuPrompt = _viewHuPrompt.transform.Find("huPrompt");
        
        for (int i = 0; i < UIOperation.HUPROMPT_MJTYPE_NUM; i++ )
        {
            string viewName = "item_" + (i + 1);
            UIGameHuItemView viewItem = _viewHuPrompt._objHuPrompt.Find(viewName).GetComponent<UIGameHuItemView>() ?? _viewHuPrompt._objHuPrompt.Find(viewName).gameObject.AddComponent<UIGameHuItemView>();
			//viewItem.gameObject.SetActive(false);
			viewItem._img_MJ = viewItem.transform.Find("img_mj").GetComponent<Image>();
			viewItem._textBeishu = viewItem.transform.Find("text_beishu").GetComponent<Text>();
			viewItem._textZhangshu = viewItem.transform.Find("text_zhangshu").GetComponent<Text>();
			_viewHuPrompt._viewHuItems [i] = viewItem;
        }
        
    }
		

//    public void setHuPrompt(List<UIOperation.HUPROMPT_INFO> hupaiInfo)
//    {
//        UIOperation.Instance.hupai_infos = hupaiInfo;
//        
//    }
//    public void setHuPromptAndOnRefresh(List<UIOperation.HUPROMPT_INFO> hupaiInfo)
//    {
//        setHuPrompt(hupaiInfo);
//        OnRefreshItem();
//    }
	public void OnRefreshItem(TileDef def)
    {
		foreach (var viewItem in _viewHuPrompt._viewHuItems)
        {
            viewItem.gameObject.SetActive(false);
        }
		Debug.Log ("*****************************OnRefreshItem:"+def.ToString());


		if (UIOperation.Instance._huTipsCards != null && UIOperation.Instance._huTipsCards.Count > 0) {
			List<GameMessage.HuTips> huTips = null;
			foreach(var huTipsCard in UIOperation.Instance._huTipsCards)
			{
				//TileDef tdef = TileDef.Create (huTipsCard.cOutCard);
				if (huTipsCard.cOutCard == def.Value) 
				{
					huTips = huTipsCard.v_Tips;
					break;
				}
			}
			if (huTips != null && huTips.Count > 0) {
				for(int n = 0; n < huTips.Count; n++)
				{
					var tips = huTips[n];
					TileDef tempDef = TileDef.Create (tips.cCard);
					UIGameHuItemView viewItem = _viewHuPrompt._viewHuItems[n];
					if(viewItem != null)
					{
						viewItem.gameObject.SetActive(true);
						viewItem._img_MJ.sprite = UIOperation.Instance.GetMJSprite (tempDef);
						viewItem._textBeishu.text = tips.cTimes.ToString();
						viewItem._textZhangshu.text = tips.cLeftNum.ToString();
					}
				}

				int count = huTips.Count;
				float itemSize = 130 * count;
				float itemScape = 10 * count - 10;
				float mySize = 105;
				float maxSize = itemSize + itemScape + mySize;
				_viewHuPrompt._imgBg.GetComponent<RectTransform>().sizeDelta = new Vector2(maxSize, 100);
				_viewHuPrompt._imgBg.localPosition = new Vector3( - maxSize / 2, -166, 0);
				_viewHuPrompt._objHuPrompt.localPosition = new Vector3(91 - maxSize / 2, -125, 0);
			} else {
				_prefab.gameObject.SetActive(false);
			}
		} else {
			_prefab.gameObject.SetActive(false);
		}


//		int count = 0;
//        foreach (var huPromptInfo in UIOperation.Instance.hupai_infos)
//        {
//            string keyName = huPromptInfo.mjType.ToString() + huPromptInfo.mjView.ToString();
//            UIGameHuItemView viewItem;
//            if (_viewHuPrompt._viewHuItems.TryGetValue(keyName, out viewItem))
//            {
//                count++;
//                viewItem.gameObject.SetActive(true);
//                viewItem._textBeishu.text = huPromptInfo.beishu.ToString();
//                viewItem._textZhangshu.text = huPromptInfo.zhangshu.ToString();
//            }
//        }
//        if (count > 0) {
//            _prefab.gameObject.SetActive(true);
//            float itemSize = 130 * count;
//            float itemScape = 10 * count - 10;
//            float mySize = 105;
//            float maxSize = itemSize + itemScape + mySize;
//            _viewHuPrompt._imgBg.GetComponent<RectTransform>().sizeDelta = new Vector2(maxSize, 100);
//            _viewHuPrompt._imgBg.localPosition = new Vector3( - maxSize / 2, -166, 0);
//            _viewHuPrompt._objHuPrompt.localPosition = new Vector3(91 - maxSize / 2, -125, 0);
//        } else {
//            _prefab.gameObject.SetActive(false);
//        }
        
    }

    public void clearData()
    {
        
    }
   
    #endregion
   

    
}
