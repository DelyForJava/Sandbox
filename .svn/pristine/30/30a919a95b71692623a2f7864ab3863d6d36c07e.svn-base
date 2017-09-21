using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;  
using UnityEngine;
using NetworkInterface;
using MP;

public partial class UIOperation : SingletonBehaviour<UIOperation>
{
	
	public List<GameMessage.PlayerMessage> _gameResultDatas;

	public void OnClickGameXuanyao(UIController ctrl)
	{
		Debug.Log ("OnClickGameXuanyao");
		GameClient.Instance.MG.Self.Proxy.ChangeTable ();
		GameClient.Instance.MG.Reset ();
		ctrl.Close ();
	}

	public void OnClickGameContinue(UIController ctrl)
	{
		Debug.Log ("OnClickGameContinue");
		GameClient.Instance.MG.Self.Proxy.ContinueGame ();
		GameClient.Instance.MG.Reset ();
		ctrl.Close ();
	}

	public void OnClickResult(UIController ctrl, int index)
	{
		Debug.Log ("OnClickResult:"+index);
		UIGameResultController resultCtrl = (UIGameResultController)ctrl;
		resultCtrl.OnclickGameResult (index);
		//ctrl.Close ();
	}
		
}