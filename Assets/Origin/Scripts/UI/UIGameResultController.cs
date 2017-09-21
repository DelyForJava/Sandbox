using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using odao.scmahjong;
using MP;

public class UIGameResultController : SingletonBehaviour<UIGameResultController>, UIController{

	public UIGameResultView _view;
	public Transform _prefab;
	SpawnPool _spawnPool;

	public void Reset ()
	{
		
	}

	public bool Load ()
	{
		if (_prefab != null)
			return true;

		if (!PoolManager.Pools.ContainsKey ("InGame2dRes")) {
			Debug.LogError ("No InGame2dRes Prefab Loaded!!!");
			return false;
		}

		_spawnPool = PoolManager.Pools["InGame2dRes"];
		if (!_spawnPool.prefabs.ContainsKey ("Canvas_GameResult")) {
			return false;
		}
		_prefab = _spawnPool.Spawn ("Canvas_GameResult");
		LoadGameResultUI ();


		CloseViewRoot ();

		return true;
	}

	public void Unload ()
	{
		if (_spawnPool != null && _spawnPool.IsSpawned (_prefab.transform))
			_spawnPool.Despawn (_prefab.transform, _spawnPool.transform);
		_spawnPool = null;
		_prefab = null;
		_view = null;
		_spawnPool = null;
	}

	public bool OpenViewRoot ()
	{
		if (!Load ())
			return false;

		if (_prefab != null && _prefab.gameObject.activeSelf == false)
			_prefab.gameObject.SetActive (true);

		return true;
	}

	public void CloseViewRoot ()
	{
		if (_prefab != null && _prefab.gameObject.activeSelf == true)
			_prefab.gameObject.SetActive (false);
	}

		public bool Open()
	{
		if (!Load ())
			return false;

		if (_prefab != null && _prefab.gameObject.activeSelf == false)
			_prefab.gameObject.SetActive (true);

		InitGameResultUI ();

		return true;
	}

	public void Close()
	{
		if (_prefab != null && _prefab.gameObject.activeSelf == true)
		{
			_prefab.gameObject.SetActive(false);
		}
	}

	public void OnRefresh()
	{

	}

	#region coustom
	public void LoadGameResultUI()
	{
		_view = _prefab.GetComponent<UIGameResultView> () ?? _prefab.gameObject.AddComponent<UIGameResultView> ();
		_view._btnXuanyao = _prefab.Find ("Panel/btn_xuanyao").GetComponent<Button> ();
		_view._btnContinue = _prefab.Find ("Panel/btn_jixu").GetComponent<Button> ();
		_view._scrollDetail = _prefab.Find ("Panel/ScrollDetail");
		_view._scrollDetail.gameObject.SetActive (false);
		for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
		{
			string tempName = "Panel/GameResult/result" + (i + 1);
			_view._resultItems[i] = _prefab.Find(tempName).GetComponent<UIResultItem> () ?? _prefab.Find(tempName).gameObject.AddComponent<UIResultItem> ();
			UIResultItem resultItem = _view._resultItems[i];
			resultItem._btnResult = _prefab.Find(tempName).GetComponent<Button> ();
			resultItem._imgBgWin = resultItem._btnResult.transform.Find("img_bgWin");
			resultItem._imgBgLose = resultItem._btnResult.transform.Find("img_bgLose");
			resultItem._textscore = resultItem._btnResult.transform.Find("text_score").GetComponent<Text> ();
			resultItem._handCards = resultItem._btnResult.transform.Find("handCards");
			resultItem._imgHu = resultItem._btnResult.transform.Find("imghu").GetComponent<Image> ();
			resultItem._imgWeiting = resultItem._btnResult.transform.Find("imgWeiTing").GetComponent<Image> ();
			resultItem._imgHuazhu = resultItem._btnResult.transform.Find("imgHuazhu").GetComponent<Image> ();
			resultItem._textHubs = resultItem._btnResult.transform.Find("text_hubs").GetComponent<Text> ();
			resultItem._huGroup = resultItem._btnResult.transform.Find("huGroup");

			resultItem._imgHeadkuang = resultItem._btnResult.transform.Find("img_headkuang").GetComponent<Image> ();
			resultItem._imgHeadIcon = resultItem._btnResult.transform.Find("img_headkuang/Image/img_headIcon").GetComponent<Image> ();
			resultItem._imgBenjia = resultItem._btnResult.transform.Find("img_headkuang/img_benjia").GetComponent<Image> ();
			resultItem._imgMaxWin = resultItem._btnResult.transform.Find("img_headkuang/img_maxWin").GetComponent<Image> ();
			resultItem._textName = resultItem._btnResult.transform.Find("img_headkuang/text_name").GetComponent<Text> ();
			resultItem._btnResult.onClick.RemoveAllListeners ();

		}
		_view._resultItems[0]._btnResult.onClick.AddListener(delegate () { UIOperation.Instance.OnClickResult(this, 0); });
		_view._resultItems[1]._btnResult.onClick.AddListener(delegate () { UIOperation.Instance.OnClickResult(this, 1); });
		_view._resultItems[2]._btnResult.onClick.AddListener(delegate () { UIOperation.Instance.OnClickResult(this, 2); });
		_view._resultItems[3]._btnResult.onClick.AddListener(delegate () { UIOperation.Instance.OnClickResult(this, 3); });

		for(int i = 0; i < 20; i++)
		{
			string tempName = "Panel/ScrollDetail/Viewport/Content/PanelDetail/detail" + (i + 1);
			_view._dtailtems[i] = _prefab.Find(tempName).GetComponent<UIDetailItem> () ?? _prefab.Find(tempName).gameObject.AddComponent<UIDetailItem> ();
			UIDetailItem dtailtem = _view._dtailtems[i];
			dtailtem.gameObject.SetActive (false);
			dtailtem._textWinType = dtailtem.transform.Find ("text_winType").GetComponent<Text>();
			dtailtem._textHuType = dtailtem.transform.Find ("text_huType").GetComponent<Text>();
			dtailtem._textHuBS = dtailtem.transform.Find ("text_huBS").GetComponent<Text>();
			dtailtem._textWinScore = dtailtem.transform.Find ("text_winScore").GetComponent<Text>();
			dtailtem._textWinPenson = dtailtem.transform.Find ("text_winPenson").GetComponent<Text>();
		}

		// add combo button event
		_view._btnContinue.onClick.RemoveAllListeners ();
		_view._btnXuanyao.onClick.RemoveAllListeners ();

		_view._btnContinue.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameContinue(this); });
		_view._btnXuanyao.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameXuanyao(this); });
	}

	public void InitGameResultUI(){
//		for (int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++) {
//			_view._resultItems [i].gameObject.SetActive (true);
//		}
//		return;
		if (UIOperation.Instance._gameResultDatas == null)
			return;
		UIGroupCardsController.Instance.Close ();
		int listCount = UIOperation.Instance._gameResultDatas.Count;
		int maxWinindex = 0;
		if(listCount > 0)
		{
			for(int i = 1; i < UIOperation.Instance._gameResultDatas.Count;i++)
			{
				if (UIOperation.Instance._gameResultDatas [maxWinindex].llScore < UIOperation.Instance._gameResultDatas [i].llScore) {
					maxWinindex = i;
				}
			}
		}
		for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
		{
			_view._resultItems [i].gameObject.SetActive (false);
			if(i < listCount)
			{
				bool isWin = false;
				bool isHu = false;
				bool isWeiting = false;
				bool isHuazhu = false;
				GameMessage.PlayerMessage playerMsg = UIOperation.Instance._gameResultDatas[i];
				isWin = playerMsg.llScore >= 0;
				isHu = playerMsg.cHu > 0;
				isWeiting = playerMsg.cNOTING > 0;
				isHuazhu = playerMsg.cHuaZhu > 0;
				UIResultItem resultItem = _view._resultItems [i];
				resultItem.gameObject.SetActive (true);

				resultItem._textName.text = playerMsg.iUserID.ToString();
				resultItem._textscore.text = playerMsg.llScore.ToString ();
				resultItem._textHubs.text = "x" + playerMsg.cWinCounts;
				resultItem._imgBgWin.gameObject.SetActive (isWin);
				resultItem._imgBgLose.gameObject.SetActive (!isWin);

				resultItem._imgHu.gameObject.SetActive(isHu);
				resultItem._imgWeiting.gameObject.SetActive(isWeiting);
				resultItem._imgHuazhu.gameObject.SetActive(isHuazhu);
				resultItem._textHubs.gameObject.SetActive(isHu);
				resultItem._huGroup.gameObject.SetActive(isHu);

				resultItem._imgBenjia.gameObject.SetActive (i == UIOperation.Instance.SelfIndex);
				resultItem._imgMaxWin.gameObject.SetActive (i == maxWinindex);

				//peng kang card
				for(int n = 0; n < playerMsg.vStructMj.Count; n++)
				{
					UIGroupCardsController.Instance.GetPengGroupUI (resultItem._handCards, playerMsg.vStructMj[n].card, playerMsg.vStructMj[n].specialType);
				}
				//hand card
				UIGroupCardsController.Instance.GetHandGroupUI (resultItem._handCards, playerMsg.vhandCards);

				if(isHu)
				{
					//hu card
					UIGroupCardsController.Instance.GetHuGroupUI (resultItem._huGroup, playerMsg.vHuCards);
				}
			}

		}

	}

	public void OnclickGameResult(int index)
	{
		if (_view._scrollDetail.gameObject.activeSelf) {
			_view._scrollDetail.gameObject.SetActive (false);	
			for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
			{
				if(i != index)
					_view._resultItems [i].gameObject.SetActive (true);
			}
		} else {
			for(int i = 0; i < GameMessage.TABLE_PLAYER_NUM; i++)
			{
				if(i != index)
					_view._resultItems [i].gameObject.SetActive (false);
			}
			InitDetail (UIOperation.Instance._gameResultDatas[index].vAllRecors, index);
			_view._scrollDetail.gameObject.SetActive (true);
		}
	}

	public void InitDetail(List<GameMessage.ScoreRecord> vAllRecors, int playerIndex)
	{
		for (int i = 0; i < 20; i++) {
			_view._dtailtems [i].gameObject.SetActive (false);
		}
		
		for(int i = 0; i < vAllRecors.Count; i++)
		{
			_view._dtailtems [i].gameObject.SetActive (true);
			_view._dtailtems [i]._textHuBS.text = vAllRecors[i].times+"倍";
			_view._dtailtems [i]._textHuType.text = GetHuType (vAllRecors[i].huType);

			long winScore = vAllRecors [i].winScore;
			string strScore = "";
			if (winScore > 0) {
				strScore = "+" + winScore.ToString ();
			} else if (winScore < 0) {
				strScore = "" + winScore.ToString ();
			} else {
				strScore = winScore.ToString ();
			}
			_view._dtailtems [i]._textWinScore.text = strScore;

			_view._dtailtems [i]._textWinPenson.text = GetWanjia(vAllRecors[i].vIndex, playerIndex);


			_view._dtailtems [i]._textWinType.text = GetWinType (vAllRecors[i].winLostType);
		}

	}

	public string GetWanjia(List<byte> indexs, int playerIndex){
		if (null == indexs)
			return "";
		string wanjia = "";
		if(indexs.Count > 1)
		{
			wanjia = "三家";
			return wanjia;
		}
		if(indexs.Count == 1){
			int xiajia = (playerIndex + 1)%4;
			int shangjia = (playerIndex - 1) >= 0 ? (playerIndex - 1) : 3;
			if(xiajia == indexs [0])
			{
				wanjia = "下家";
			}else if(shangjia == indexs [0])
			{
				wanjia = "上家";
			}else{
				wanjia = "对家";
			}

		}
		return wanjia;
	}

	public string GetHuType(int type){
		string sType = "平胡";
		switch (type) {
		case (int)GameMessage.SCMJ_HU_TYPE.HU_NOHU:
			sType = "未胡";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_TIANHU:
			sType = "天胡";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_DIHU:
			sType = "地胡";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QINGLONG7DUI:
			sType = "清龙七对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_18LUOHAN:
			sType = "十八罗汉";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_LONG7DUI:
			sType = "龙七对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QING7DUI:
			sType = "清七对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QING19:
			sType = "清幺九";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_JIANGJINGOUDIAO:
			sType = "将金钩钓";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QINGJINGOUDIAO:
			sType = "清金钩钓";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QINGDUI:
			sType = "清对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_JIANGDUI:
			sType = "将对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_QING1SE:
			sType = "清一色";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_DAI19:
			sType = "带幺九";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_7DUI:
			sType = "七对";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_JINGOUDIAO:
			sType = "金钩钓";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_DUIDUIHU:
			sType = "对对胡";
			break;
		case (int)GameMessage.SCMJ_HU_TYPE.HU_PINGHU:
			sType = "平胡";
			break;
		}
		return sType;
	}

	public string GetWinType(int type){
		string sType = "自摸";
		switch (type) {
		case (int)GameMessage.WINLOST_TYPE.WINLOST_ZIMO:
			sType = "自摸";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_ZIMO:
			sType = "被自摸";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_DIANPAO:
			sType = "点炮";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_DIANPAOHU:
			sType = "点炮胡";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_QIANGGANGHU:
			sType = "抢杠胡";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_QIANGGANGHU:
			sType = "被抢杠胡";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_GANGSHANGHUA:
			sType = "杠上开花";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_GANGSHANGPAO:
			sType = "杠上炮";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_GANGSHANGPAO:
			sType = "被杠上炮";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_GUAFENG_ZHI:
			sType = "明杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_GUAFENG_XIA:
			sType = "碰杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_GUAFENG_ZHI:
			sType = "被明杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_GUAFENG_XIA:
			sType = "被碰杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_XIAYU:
			sType = "暗杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_XIAYU:
			sType = "被暗杠";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_HUAZHU:
			sType = "花猪";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_CHAHUAZHU:
			sType = "查花猪";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_CHAJIAO:
			sType = "查叫";
			break;
		case (int)GameMessage.WINLOST_TYPE.WINLOST_BEI_CHAJIAO:
			sType = "被查叫";
			break;
		}
		return sType;
	}


	#endregion
}
