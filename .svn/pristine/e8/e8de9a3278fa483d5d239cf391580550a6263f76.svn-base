using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuController : SingletonBehaviour<UIMainMenuController>, UIController {
	
	UIMainMenu _view;

	public void Reset ()
	{
	}

	public bool Load ()
	{
		_view = GameObject.Find ("Canvas").GetComponent<UIMainMenu> ();

		_view._btnMatchXL.onClick.RemoveAllListeners ();
		_view._btnMatchXL.onClick.AddListener(delegate () { UIOperation.Instance.OnClickMatchXL(this); });

		return true;
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
