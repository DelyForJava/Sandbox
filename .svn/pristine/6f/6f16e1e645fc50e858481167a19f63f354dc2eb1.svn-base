using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using odao.scmahjong;
using PathologicalGames;


public class UITipsController :  SingletonBehaviour<UITipsController> , UIController{
	public UITipsView _view;
	public Transform _prefab;
	SpawnPool _spawnPool;
	
	public void Reset (){
		
	}
	public void OnRefresh(){
		
	}
	public bool Load (){
		
		if (_prefab != null)
			return true;
		if(!PoolManager.Pools.ContainsKey("Common2dRes"))
		{
			Debug.LogError ("PoolManager pools not have Common2dRes!!!");
			return false;
		}
		_spawnPool = PoolManager.Pools ["Common2dRes"];
		if (!_spawnPool.prefabs.ContainsKey ("Canvas_Tips")) {
			Debug.LogError ("Common2dRes PoolManager not have prefabs!!! ");
			return false;
		}
		_prefab = _spawnPool.Spawn ("Canvas_Tips");
		InitUi ();
		return true;
	}

	public void Unload (){
	}

	public bool OpenViewRoot (){
		return true;
	}
	public void CloseViewRoot (){
		
	}
	public bool Open(){
		if (!Load ())
			return false;
		if (_prefab != null && _prefab.gameObject.activeSelf == false)
			_prefab.gameObject.SetActive (true);
		return true;
	}
	public void Close(){
		if (!Load ())
			return;
		if (_prefab != null && _prefab.gameObject.activeSelf == true)
			_prefab.gameObject.SetActive (false);
	}


	private void InitUi()
	{
		_view = _prefab.GetComponent<UITipsView>() ?? _prefab.gameObject.AddComponent<UITipsView>();
		_view.img_bg = _prefab.Find ("Panel/Image").GetComponent<Image> ();
		_view.text_tips = _prefab.Find ("Panel/Image/Text").GetComponent<Text> ();
		_view.btn_01 = _prefab.Find ("Panel/Image/GameObject/Button").GetComponent<Button> ();
		_view.btn_02 = _prefab.Find ("Panel/Image/GameObject/Button2").GetComponent<Button> ();
	}

	public bool OpenTips(string str, Action back1, Action back2)
	{
		if (!Load ())
			return false;
		Open ();
		if (_view.text_tips)
			_view.text_tips.text = str;
		if (_view.btn_01) {
			_view.btn_01.gameObject.SetActive (true);
			_view.btn_01.onClick.RemoveAllListeners ();
			_view.btn_01.onClick.AddListener (delegate() {UIOperation.Instance.OnClickTipsBtn1(this, back1);});
		}
		if (_view.btn_02) {
			_view.btn_02.gameObject.SetActive (true);
			_view.btn_02.onClick.RemoveAllListeners ();
			_view.btn_02.onClick.AddListener (delegate() {UIOperation.Instance.OnClickTipsBtn2(this, back2);});
		}
		return true;

	}

	public bool OpenTips(string str, Action back1)
	{
		if (!Load ())
			return false;
		Open ();
		if (_view.text_tips)
			_view.text_tips.text = str;
		if (_view.btn_01) {
			_view.btn_01.gameObject.SetActive (true);
			_view.btn_01.onClick.RemoveAllListeners ();
			_view.btn_01.onClick.AddListener (delegate() {UIOperation.Instance.OnClickTipsBtn1(this, back1);});
		}
		if (_view.btn_02) {
			_view.btn_02.gameObject.SetActive (false);
			_view.btn_02.onClick.RemoveAllListeners ();
		}
		return true;
	}
}
