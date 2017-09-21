using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class UIDQSettingController : SingletonBehaviour<UIDQSettingController>, UIController {

	UIGameDQSetting _view;
	public Transform _prefab;
	SpawnPool _spawnPool;

	public void Reset ()
	{
	}

	public void OnRefresh()
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
		if (!_spawnPool.prefabs.ContainsKey ("Canvas_LackSetting")) {
			return false;
		}
		_prefab = _spawnPool.Spawn ("Canvas_LackSetting");

		_view = _prefab.GetComponent<UIGameDQSetting> () ?? _prefab.gameObject.AddComponent<UIGameDQSetting> ();

		// add combo button event
		_view._btnCrak.onClick.RemoveAllListeners ();
		_view._btnCrak.onClick.AddListener(delegate () { UIOperation.Instance.OnClickDQCrak(this); });
		_view._btnBam.onClick.RemoveAllListeners ();
		_view._btnBam.onClick.AddListener(delegate () { UIOperation.Instance.OnClickDQBam(this); });
		_view._btnDot.onClick.RemoveAllListeners ();
		_view._btnDot.onClick.AddListener(delegate () { UIOperation.Instance.OnClickDQDot(this); });
		AddEffect ();
		return true;
	}

	public void AddEffect ()
	{
		if (_view._btnCrak.transform.childCount > 0) {
			return;
		}
		if (PoolManager.Pools.ContainsKey ("InGameEffectRes")) {
			SpawnPool pool = PoolManager.Pools["InGameEffectRes"];
			if (pool.prefabs.ContainsKey ("FX_Dingque")) {
				Transform effect = pool.Spawn ("FX_Dingque");
				Transform ewan = effect.Find ("Panel/FX_Btn_crak");
				Transform etiao = effect.Find ("Panel/FX_Btn_bam");
				Transform etong = effect.Find ("Panel/FX_Btn_dot");
				if(ewan && etiao && etong)
				{
					ewan.parent = _view._btnCrak.transform;
					etiao.parent = _view._btnBam.transform;
					etong.parent = _view._btnDot.transform;
					ewan.localPosition = Vector3.zero;
					etiao.localPosition = Vector3.zero;
					etong.localPosition = Vector3.zero;

					pool.Despawn (effect);
				}

			}

		} else {
			Debug.LogError ("No InGameEffectRes Prefab Loaded!!!");
		}

	}

	public void Unload ()
	{
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
		if (!Load ())
			return false;

		if (_prefab != null && _prefab.gameObject.activeSelf == false)
			_prefab.gameObject.SetActive (true);

		return true;
	}

	public void Close()
	{
		if (_prefab != null && _prefab.gameObject.activeSelf == true)
			_prefab.gameObject.SetActive (false);
	}
}
