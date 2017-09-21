using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using MP;
using odao.scmahjong;

public class UIGameEffectController : SingletonBehaviour<UIGameEffectController>, UIController {
	public List<Transform> _prefabs = new List<Transform>();
	SpawnPool _spawnPool = null;

	public void Reset ()
	{
	}

	public void OnRefresh()
	{
		
	}

	public bool Load ()
	{
		if (_spawnPool != null)
			return true;
		
		if (!PoolManager.Pools.ContainsKey ("InGameEffectRes")) {
			Debug.LogError ("No InGameEffectRes Prefab Loaded!!!");
			return false;
		}
		_spawnPool = PoolManager.Pools["InGameEffectRes"];

		return true;
	}


	//杠碰胡特效
	public void ShowComboEffect(TileDef.ComboType comboType)
	{
		Load ();
		Transform effectTran1 = null;
		Transform effectTran2 = null;
		switch (comboType) {
		case TileDef.ComboType.PONG:
			effectTran1 = _spawnPool.Spawn ("FX_pong");
			break;
		case TileDef.ComboType.KONG_DARK:
			effectTran2 = _spawnPool.Spawn ("FX_rain");
			effectTran1 = _spawnPool.Spawn ("FX_kong");
			break;
		case TileDef.ComboType.KONG:
		case TileDef.ComboType.KONG_TURN:
			effectTran2 = _spawnPool.Spawn ("FX_Tornado");
			effectTran1 = _spawnPool.Spawn ("FX_kong");
			break;
		case TileDef.ComboType.WIN:
		case TileDef.ComboType.WIN_AFTER_KONG_TURN:
			effectTran1 = _spawnPool.Spawn ("FX_win");
			break;
		case TileDef.ComboType.WIN_SELF:
			effectTran1 = _spawnPool.Spawn ("FX_zimo");
			break;

		}

		if(effectTran1 != null)
		{
			effectTran1.gameObject.SetActive (true);
			_prefabs.Add (effectTran1);
		}
		if(effectTran2 != null)
		{
			effectTran2.gameObject.SetActive (true);
			_prefabs.Add (effectTran2);
		}
	}

	public void Unload ()
	{
		
	}

	public void DespawnAllEffect()
	{
		if (Load ()) {
			foreach(var temp in _prefabs)
			{
				_spawnPool.Despawn (temp);
			}
			_prefabs.Clear ();
		}

	}

	public bool OpenViewRoot ()
	{
		return true;
	}

	public void CloseViewRoot ()
	{
	}

	public bool Open()
	{
		

		return true;
	}

	public void Close()
	{
		
	}
}
