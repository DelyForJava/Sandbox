using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class UIDebugViewController : SingletonBehaviour<UIDebugViewController>, UIController {

	public UIDebug _view;
	public Transform _prefab;
	SpawnPool _spawnPool;
    public bool _isStopCoroutine = false;

	public void Reset ()
	{
	}

	public bool Load ()
	{
        if (_prefab != null)
			return true;

        if (!PoolManager.Pools.ContainsKey("Common2dRes"))
        {
            Debug.LogError("No Common2dRes Prefab Loaded!!!");
			return false;
		}
		//
		//
        _spawnPool = PoolManager.Pools["Common2dRes"];
        if (!_spawnPool.prefabs.ContainsKey("Canvas_Debug"))
        {
            Debug.LogError("No find Prefab Canvas_Debug!!!");
			return false;
		}
        _prefab = _spawnPool.Spawn("Canvas_Debug");
		//
		//
		_view = _prefab.GetComponent<UIDebug>();
        Close();
        return true;
        /*if (GameObject.Find("Canvas_Debug") == null)
        {
            Debug.Log("GameObject.Find(Canvas_Debug) = null");
            return false;
        }
        else 
        {
            _view = GameObject.Find("Canvas_Debug").GetComponent<UIDebug>();
            return true;
        }*/
		
	}

	public void Unload ()
	{
	}

	public bool OpenViewRoot ()
	{
		if (Load ()) {
			_view.gameObject.SetActive (true);
            _view._debugInfo.text = "";
		}
		return true;
	}

	public void CloseViewRoot () {
	}

	public bool Open()
	{
		if (Load ()) {
			_view.gameObject.SetActive (true);
            _view._debugInfo.text = "";
		}
		return true;
	}

	public void Close()
	{
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
        {
            _prefab.gameObject.SetActive(false);
            _view._isNeedUpdate = false;
            _view._isVisibleRotateImg = false;
            _view._infoTextIdx = 1;
            stopDebugCoroutine();
        }
			
	}

	public void OnRefresh()
	{
        if (_prefab != null && _prefab.gameObject.activeSelf == true)
        {
            _prefab.gameObject.SetActive(false);
            _view._debugInfo.text = "";
            _view._debugInfo.transform.position = new Vector3(-100, -70, 0);
            _view._objInfo.SetActive(false);
        }
	}

	#region custom
	public void SetText(string str)
	{
		_view._debugInfo.text = str;
        _view._objInfo.SetActive(true);
	}

	public void AppendText(string str)
	{
		_view._debugInfo.text = _view._debugInfo.text + str;
	}

    public void OpenLoadingDebug(string str)
    {
        stopDebugCoroutine();
        OpenLoadingDebug(str, new Vector3(-100, -70, 0));
    }
    public void OpenLoadingDebug(string str, Vector3 textPosition)
    {
        Debug.Log("***OpenLoadingDebug:"+str);
        Open();
        _view._debugInfo.text = "";
        _view._debugInfo.transform.localPosition = textPosition;
        _view._isVisibleRotateImg = true;
        _view._objInfo.SetActive(true);
        _view._isNeedUpdate = true;
        _view._strText = str;
    }

    public void OpenCommonDebug(string str)
    {
        OpenCommonDebug(str, new Vector3(-100, -70, 0));
    }
    public void OpenCommonDebug(string str, Vector3 textPosition)
    {
        Open();
        _view._debugInfo.text = "";
        _view._debugInfo.transform.localPosition = textPosition;
        _view._isVisibleRotateImg = false;
        _view._objInfo.SetActive(false);
        _view._isNeedUpdate = true;
        _view._strText = str;
    }


    public void OpenAndShowRotateImg()
    {
        Open();
        setRotateVisible(true);
    }

    public void setRotateVisible(bool isShow)
    {
        _view._isNeedUpdate = isShow;
        _view._isVisibleRotateImg = isShow;
    }

    IEnumerator showDebug(string str)
    {
        int i = 1;
        OpenAndShowRotateImg();
        while (_isStopCoroutine)
        {
            SetText(str.Substring(0, i));
            yield return new WaitForSeconds(0.5f);
            i = ++i % str.Length;
            if (i == 0)
                i = 1;
        }
    }
    public void startDebugCoroutine(string str)
    {
        _isStopCoroutine = true;
        StartCoroutine(showDebug(str));
    }

    public void stopDebugCoroutine()
    {
        _isStopCoroutine = false;
        StopCoroutine(showDebug(""));
    }

	#endregion
}
