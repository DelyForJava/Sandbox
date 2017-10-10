using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using odao.scmahjong;
using UnityEngine.UI;


public class UILoginController : SingletonBehaviour<UILoginController>, UIController {

	UILogin _view;

	public void Reset ()
	{
	}

	public bool Load ()
	{
		_view = GameObject.Find ("Canvas").GetComponent<UILogin> ();

		_view.touristBtn.onClick.RemoveAllListeners ();
		_view.touristBtn.onClick.AddListener(delegate () { UIOperation.Instance.OnClickTouristLogin(); });
        _view.weChatBtn.onClick.AddListener(delegate () { UIOperation.Instance.OnClickWechatLogin(); });
        loadImgSprite ();
		return true;
	}

	public void loadImgSprite()
	{
		SpawnPool spawnPool;
		if (!PoolManager.Pools.ContainsKey("Common2dRes"))
		{
			Debug.LogError("No Common2dRes Prefab Loaded!!!");

		}
		spawnPool = PoolManager.Pools["Common2dRes"];
		if (!spawnPool.prefabs.ContainsKey("Canvas_ImgSprite"))
		{
			Debug.LogError("No img_01 Prefab Loaded!!!");

		}
		Transform prefab = spawnPool.Spawn("Canvas_ImgSprite");
		prefab.Find("Panel").localPosition = new Vector3 (-2000, -2000, 0);
		UIOperation.Instance._wanSprites.Clear ();
		UIOperation.Instance._tiaoSprites.Clear ();
		UIOperation.Instance._tongSprites.Clear ();
		UIOperation.Instance._ziSprites.Clear ();
		//load wan,tiao,tong,zi sprite
		for(int i = 0; i < 9; i++)
		{
			Image img_wan = prefab.Find("Panel/img_0"+(i+1)).GetComponent<Image> ();
			UIOperation.Instance._wanSprites.Add (img_wan.sprite);

			Image img_tiao = prefab.Find("Panel/img_1"+(i+1)).GetComponent<Image> ();
			UIOperation.Instance._tiaoSprites.Add (img_tiao.sprite);

			Image img_tong = prefab.Find("Panel/img_2"+(i+1)).GetComponent<Image> ();
			UIOperation.Instance._tongSprites.Add (img_tong.sprite);

			if(i < 6)
			{
				Image img_zi = prefab.Find("Panel/img_3"+(i+1)).GetComponent<Image> ();
				UIOperation.Instance._ziSprites.Add (img_zi.sprite);
			}

		}

		spawnPool.Despawn (prefab);
		//prefab.gameObject.SetActive (false);

	}

	public void Unload ()
	{
	}

	public bool OpenViewRoot ()
	{
		if (Load ()) {
            UIDebugViewController.Instance.OnRefresh();
			_view.gameObject.SetActive (true);
		}
		return true;
	}

	public void CloseViewRoot () {
	}

	public bool Open()
	{
		if (Load ()) {
            UIDebugViewController.Instance.OnRefresh();
			_view.gameObject.SetActive (true);
		}
		return true;
	}

	public void Close()
	{
	}

	public void OnRefresh()
	{

	}
}
