using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using odao.scmahjong;
using UnityEngine.Networking;
using MP;

public class UIGroupCardsController : SingletonBehaviour<UIGroupCardsController>, UIController{
	public UIGameSeting _viewSeting;
	public Transform _prefab;
	public List<Transform> _prefabs = new List<Transform>();
	SpawnPool _spawnPool;

	//List<SpawnPool> _spawnPools;
	public void Reset (){}
	public void OnRefresh(){}

	public bool Load (){
		Transform _prefab;
		if (!PoolManager.Pools.ContainsKey("Common2dRes"))
		{
			Debug.LogError("No Common2dRes Prefab Loaded!!!");
			return false;
		}
		_spawnPool = PoolManager.Pools["Common2dRes"];
		if (!_spawnPool.prefabs.ContainsKey("GroupCard"))
		{
			Debug.LogError("No Canvas_GameResult Prefab Loaded!!!");
			return false;
		}

		return true;
	}

	public Transform GetHandGroupUI(Transform parentT,List<byte> cards)
	{
		Transform tran = GetGroupUI ();
		if (null == tran)
			return null;
		//cards.Sort(delegate(Student a, Student b) { return a.Age.CompareTo(b.Age); });
		cards.Sort();
		tran.parent = parentT;
		tran.gameObject.SetActive (true);
		UIGroupCardsView rview = tran.gameObject.GetComponent<UIGroupCardsView> () ?? tran.gameObject.AddComponent<UIGroupCardsView>();
		HorizontalLayoutGroup layerGroup = tran.gameObject.GetComponent<HorizontalLayoutGroup> ();
		//layerGroup.spacing = -3;
		for (int i = 0; i < cards.Count; i++) {
			UICardView cview = rview._cardViews [i];
			TileDef def = TileDef.Create (cards[i]);
			cview._imgMj.sprite = UIOperation.Instance.GetMJSprite(def);
			cview.gameObject.SetActive (true);
			cview._imgbsBG.gameObject.SetActive (false);	
			cview._textX.gameObject.SetActive (false);	
			cview._textNum.gameObject.SetActive (false);
		}
		return tran;
	}

	public Transform GetPengGroupUI(Transform parentT,byte card, byte specialType)
	{
		Transform tran = GetGroupUI ();
		if (null == tran)
			return null;
		tran.parent = parentT;
		tran.gameObject.SetActive (true);
		HorizontalLayoutGroup layerGroup = tran.gameObject.GetComponent<HorizontalLayoutGroup> ();
		//layerGroup.spacing = -4;
		int cardNum = 0;
		switch (specialType) {
		case (byte)GameMessage.SPECIAL_TYPE.PONG:
			cardNum = 3;
			break;
		case (byte)GameMessage.SPECIAL_TYPE.KONG:
			cardNum = 4;
			break;
		case (byte)GameMessage.SPECIAL_TYPE.DARK_KONG:
			cardNum = 4;
			break;
		case (byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG:
			cardNum = 4;
			break;
		}
		UIGroupCardsView rview = tran.gameObject.GetComponent<UIGroupCardsView> () ?? tran.gameObject.AddComponent<UIGroupCardsView>();
		for (int i = 0; i < cardNum; i++) {
			if (i == 3) {
				break;
			}
			UICardView cview = rview._cardViews [i];
			TileDef def = TileDef.Create (card);
			cview._imgMj.sprite = UIOperation.Instance.GetMJSprite(def);
			cview.gameObject.SetActive (true);
			cview._imgbsBG.gameObject.SetActive (false);	
			cview._textX.gameObject.SetActive (false);	
			cview._textNum.gameObject.SetActive (false);
			if(cardNum == 4 && i == 1)
			{
				Transform gangTran = cview.transform.Find ("img_gang");
				if(gangTran)
				{
					gangTran.gameObject.SetActive (true);
					gangTran.Find ("img_mj").GetComponent<Image> ().sprite = cview._imgMj.sprite;
				}
			}
		}
		return tran;
	}

	public Transform GetHuGroupUI(Transform parentT, List<GameMessage.HuCards> huCards)
	{
		Transform tran = GetGroupUI ();
		if (null == tran)
			return null;
		tran.parent = parentT;
		tran.gameObject.SetActive (true);
		HorizontalLayoutGroup layerGroup = tran.gameObject.GetComponent<HorizontalLayoutGroup> ();
		//layerGroup.spacing = -1;

		UIGroupCardsView rview = tran.gameObject.GetComponent<UIGroupCardsView> () ?? tran.gameObject.AddComponent<UIGroupCardsView>();
		for (int i = 0; i < huCards.Count; i++) {
			UICardView cview = rview._cardViews [i];
			TileDef def = TileDef.Create (huCards[i].cCard);
			cview._imgMj.sprite = UIOperation.Instance.GetMJSprite(def);
			cview.gameObject.SetActive (true);
			cview._imgbsBG.gameObject.SetActive (true);	
			cview._textX.gameObject.SetActive (true);	
			cview._textNum.gameObject.SetActive (true);
			cview._textNum.text = huCards[i].cNum.ToString ();
		}
		return tran;
	}

	public Transform GetGroupUI()
	{
		if(Load())
		{
			Transform tran = _spawnPool.Spawn("GroupCard");
			_prefabs.Add (tran);
			UIGroupCardsView rview = tran.gameObject.GetComponent<UIGroupCardsView> () ?? tran.gameObject.AddComponent<UIGroupCardsView>();
			for(int i = 0; i < GameMessage.HANDLE_MJ_NUM; i++)
			{
				string cname = "img_bg" + (i + 1);
				Transform temp = tran.Find (cname);
				temp.gameObject.SetActive (false);
				rview._cardViews[i] = temp.GetComponent<UICardView> () ?? temp.gameObject.AddComponent<UICardView> ();
				UICardView cview = rview._cardViews [i];
				cview._imgBg = temp.GetComponent<Image> ();
				cview._imgMj = temp.Find("img_mj").GetComponent<Image> ();
				cview._imgbsBG = temp.Find("img_bsBG").GetComponent<Image> ();
				cview._textX = temp.Find("Text_x").GetComponent<Text> ();
				cview._textNum = temp.Find("Text_num").GetComponent<Text> ();
				if(i == 1)
				{
					Transform gangTran = cview.transform.Find ("img_gang");
					if(gangTran)
					{
						gangTran.gameObject.SetActive (false);
					}
				}

			}
			return tran;
		}
		return null;
	}

	public void Unload (){}

	public bool OpenViewRoot (){

		return Open ();
	}
	public void CloseViewRoot (){}

	public bool Open(){
		return true;
	}
	public void Close(){
		for(int i = 0; i < _prefabs.Count; i++)
		{
			Transform tran = _prefabs [i];
			if (tran) 
			{
				if (_spawnPool) {
					tran.parent = null;
					_spawnPool.Despawn (tran);
				}
			}
		}
		_prefabs.Clear ();
	}

}
