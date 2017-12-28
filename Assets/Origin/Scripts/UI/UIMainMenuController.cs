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
		//_view._btnMatchXL.onClick.AddListener(delegate () { UIOperation.Instance.OnClickMatchXL(this); });

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

    public void InstantiateMessagePanel(Transform parentTransform)
    {
        //if (!parentTransform.Find("Canvas/messagePanel"))
        //{
        var InstantiateMessagePanel = Instantiate(Resources.Load("Prefabs_InGame/messagePanel"), parentTransform) as GameObject;
        InstantiateMessagePanel.name = "messagePanel";
        InstantiateMessagePanel.transform.SetAsLastSibling();
        InstantiateMessagePanel.GetComponent<RectTransform>().localPosition=new Vector3(0,0,-100);
        InstantiateMessagePanel.GetComponent<RectTransform>().localScale=new Vector3(0.6f, 0.6f,1);
        //}
    }


}
