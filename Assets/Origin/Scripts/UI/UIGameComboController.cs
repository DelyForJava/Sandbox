using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using odao.scmahjong;
using MP;

public class UIGameComboController : SingletonBehaviour<UIGameComboController>, UIController
{
	public UIGameComboView _view;
	public Transform _prefab;
	SpawnPool _spawnPool;
	//
	//Dictionary<MahjongPlayer.ComboType, UIControllerAnimation.Type> _comboAnima;   // combo 的动画

	int _chow;
	int _pong;
	int _kong;
	int _win;
	int _pass;
	int _baoting;

	public void Reset ()
	{
		_chow = 0;
		_pong = 0;
		_kong = 0;
		_win = 0;
		_pass = 0;
		_baoting = 0;
	}

	public bool Load ()
	{
		if (_prefab != null)
			return true;
		
		if (!PoolManager.Pools.ContainsKey ("InGame2dRes")) {
			Debug.LogError ("No InGame2dRes Prefab Loaded!!!");
			return false;
		}
		//
		//
		_spawnPool = PoolManager.Pools["InGame2dRes"];
		if (!_spawnPool.prefabs.ContainsKey ("Canvas_Combo")) {
			return false;
		}
		_prefab = _spawnPool.Spawn ("Canvas_Combo");
		//
		//
		_view = _prefab.GetComponent<UIGameComboView> () ?? _prefab.gameObject.AddComponent<UIGameComboView> ();
		CloseCombo ();

		// add combo button event
		_view._btnChow.onClick.RemoveAllListeners ();
		_view._btnChow.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameChow(this); });
		_view._btnKong.onClick.RemoveAllListeners ();
		_view._btnKong.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameKong(this); });
		_view._btnPong.onClick.RemoveAllListeners ();
		_view._btnPong.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGamePong(this); });
		_view._btnWin.onClick.RemoveAllListeners ();
		_view._btnWin.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameWin(this); });
		_view._btnPass.onClick.RemoveAllListeners ();
		_view._btnPass.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGamePass(this); });
		_view._btnBaoTing.onClick.RemoveAllListeners ();
		_view._btnBaoTing.onClick.AddListener(delegate () { UIOperation.Instance.OnClickGameBaoTing(this); });

		// init combo effect
		//_comboAnima = new Dictionary<MahjongPlayer.ComboType, UIControllerAnimation.Type> ();
		//_comboAnima.Add(MahjongPlayer.ComboType.CHOW, UIControllerAnimation.Type.CHOW);
		//_comboAnima.Add(MahjongPlayer.ComboType.PONG, UIControllerAnimation.Type.PONG);
		//_comboAnima.Add(MahjongPlayer.ComboType.KONG, UIControllerAnimation.Type.KONG);
		//_comboAnima.Add(MahjongPlayer.ComboType.KONG_DARK, UIControllerAnimation.Type.KONG_DARK);
		//_comboAnima.Add(MahjongPlayer.ComboType.KONG_TURN, UIControllerAnimation.Type.KONG_TURN);
		//_comboAnima.Add(MahjongPlayer.ComboType.BAO_TING, UIControllerAnimation.Type.BAO_TING);
		//_comboAnima.Add(MahjongPlayer.ComboType.WIN, UIControllerAnimation.Type.HU);
		//_comboAnima.Add(MahjongPlayer.ComboType.WIN_AFTER_KONG_TURN, UIControllerAnimation.Type.WIN_AFTER_KONG_TURN);
		//_comboAnima.Add(MahjongPlayer.ComboType.WIN_SELF, UIControllerAnimation.Type.ZI_MO);

		CloseViewRoot ();
		AddEffect ();
		return true;
	}

	public void AddEffect ()
	{
		if (_view._btnWin.transform.childCount > 0) {
			return;
		}
		if (PoolManager.Pools.ContainsKey ("InGameEffectRes")) {
			SpawnPool pool = PoolManager.Pools["InGameEffectRes"];
			if (pool.prefabs.ContainsKey ("FX_PengGang")) {
				Transform effect = pool.Spawn ("FX_PengGang");
				Transform kong = effect.Find ("Panel/FX_Btn_kong");
				Transform win = effect.Find ("Panel/FX_Btn_win");
				Transform pong = effect.Find ("Panel/FX_Btn_pong");
				if(kong && win && pong)
				{
					win.parent = _view._btnWin.transform;
					pong.parent = _view._btnPong.transform;
					kong.parent = _view._btnKong.transform;
					win.localPosition = Vector3.zero;
					pong.localPosition = Vector3.zero;
					kong.localPosition = Vector3.zero;
					win.gameObject.SetActive (true);
					pong.gameObject.SetActive (true);
					kong.gameObject.SetActive (true);
					pool.Despawn (effect);
				}

			}

		} else {
			Debug.LogError ("No InGameEffectRes Prefab Loaded!!!");
		}

	}

	public void Unload ()
	{
		if (_spawnPool != null && _spawnPool.IsSpawned (_prefab.transform))
			_spawnPool.Despawn (_prefab.transform, _spawnPool.transform);
		_spawnPool = null;
		_prefab = null;
		_view = null;
		//
		//if (_comboAnima != null)
		//	_comboAnima.Clear ();
		//_comboAnima = null;
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

		return true;
	}

	public void Close()
	{
		if (_prefab != null && _prefab.gameObject.activeSelf == true)
        {
            _prefab.gameObject.SetActive(false);
            CloseCombo();
        }
	}

	public void OnRefresh()
	{

	}

	/// <summary>
	/// 当要显示 combo 的动画效果时调用
	/// </summary>
	public void ShowComboEffect (TileDef.ComboType type, MahjongPlayer player)
	{
		/*
		if (!OpenViewRoot ())
			return;

		// 如果没有对应的　efffect　忽略
		UIControllerAnimation.Type animType;
		if (!_comboAnima.TryGetValue (type, out animType))
			return;

		if (player == null || GameClient.Instance == null || GameClient.Instance.gameMode == null)
			return;

		MahjongGame game = GameClient.Instance.gameMode as MahjongGame;
		int displayIndex = game.GetPlayerDisplayIndex (player);

		UIControllerAnimation.Instance.Play (animType, player);
		*/
	}

	public void OpenCombo(TileDef.ComboType type, MahjongPlayer player)
	{
		if (!OpenViewRoot ())
			return;

		switch (type)
		{
		case TileDef.ComboType.CHOW:
			++_chow;
			_view._btnChow.gameObject.SetActive(true);
			break;
		case TileDef.ComboType.PONG:
			++_pong;
			_view._btnPong.gameObject.SetActive(true);
			break;
		case TileDef.ComboType.KONG:
		case TileDef.ComboType.KONG_DARK:
		case TileDef.ComboType.KONG_TURN:
			++_kong;
			_view._btnKong.gameObject.SetActive(true);
			break;
		case TileDef.ComboType.WIN:
		case TileDef.ComboType.WIN_SELF:
		case TileDef.ComboType.WIN_AFTER_KONG_TURN:
			++_win;
			_view._btnWin.gameObject.SetActive(true);
			break;
		case TileDef.ComboType.PASS:
		case TileDef.ComboType.PASS_CANCEL:
			++_pass;
			_view._btnPass.gameObject.SetActive(true);
			break;
		case TileDef.ComboType.BAO_TING:
			++_baoting;
			_view._btnBaoTing.gameObject.SetActive(true);
			break;
		}
	}

	public void CloseCombo (TileDef.ComboType type, MahjongPlayer player)
	{
		if (!OpenViewRoot ())
			return;

		switch (type)
		{
		case TileDef.ComboType.CHOW:
			if (--_chow <= 0)
				_view._btnChow.gameObject.SetActive(false);
			break;
		case TileDef.ComboType.PONG:
			if (--_pong <= 0)
				_view._btnPong.gameObject.SetActive(false);
			break;
		case TileDef.ComboType.KONG:
		case TileDef.ComboType.KONG_DARK:
		case TileDef.ComboType.KONG_TURN:
			if (--_kong <= 0)
				_view._btnKong.gameObject.SetActive(false);
			break;
		case TileDef.ComboType.WIN:
		case TileDef.ComboType.WIN_SELF:
		case TileDef.ComboType.WIN_AFTER_KONG_TURN:
			if (--_win <= 0)
				_view._btnWin.gameObject.SetActive(false);
			break;
		case TileDef.ComboType.PASS:
		case TileDef.ComboType.PASS_CANCEL:
			if (--_pass <= 0)
				_view._btnPass.gameObject.SetActive(false);
			break;
		case TileDef.ComboType.BAO_TING:
			if (--_baoting <= 0)
				_view._btnBaoTing.gameObject.SetActive(false);
			break;
		}
	}

	public void CloseCombo ()
	{
		if (!OpenViewRoot ())
			return;

		_view._btnChow.gameObject.SetActive (false);
		_view._btnKong.gameObject.SetActive (false);
		_view._btnPong.gameObject.SetActive (false);
		_view._btnWin.gameObject.SetActive (false);
		_view._btnBaoTing.gameObject.SetActive (false);
		_view._btnPass.gameObject.SetActive (false);

		_chow = 0;
		_pong = 0;
		_kong = 0;
		_win = 0;
		_pass = 0;
		_baoting = 0;
	}

	public void OpenAllCombos(byte specialType)
	{
		if(IsSpecialType(specialType, (byte)GameMessage.SPECIAL_TYPE.PASS))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PASS,GameClient.Instance.MG.Self);
		}

		if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.PONG))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.PONG,GameClient.Instance.MG.Self);
		}

		if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.KONG))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
		}

		if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.DARK_KONG))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_DARK,GameClient.Instance.MG.Self);
		}

		if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.SPECIAL_TYPE_PENGGANG))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.KONG_TURN,GameClient.Instance.MG.Self);
		}

		if(IsSpecialType(specialType,(byte)GameMessage.SPECIAL_TYPE.WIN))
		{
			UIGameComboController.Instance.OpenCombo(TileDef.ComboType.WIN,GameClient.Instance.MG.Self);
		}
	}

	public bool IsSpecialType(byte specialType, byte value)
	{
		return ((byte)(specialType & value)) == value;
	}

	//	void Update ()
	//	{
	//		if (Input.GetKeyDown (KeyCode.A))
	//		{
	//			int random = Random.Range (0, 8);
	//			MahjongGame game = (MahjongGame)(GameClient.Instance.gameMode);
	//			ShowComboEffect (MahjongPlayer.ComboType.BAO_TING, game._self);
	//		}
	//	}
}
