using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;  
using UnityEngine;
using NetworkInterface;

public partial class UIOperation : SingletonBehaviour<UIOperation>
{
	public UIOperation()
	{
	}

	public void OnClickGameChow(UIController ctrl)
	{
		Debug.Log ("chow");
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}

	public void OnClickGameKong(UIController ctrl)
	{
		Debug.Log ("kong");
		GameClient.Instance.MG.Self.Proxy.Kong (odao.scmahjong.TileDef.Create (GameClient.Instance.SpecialCard));
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}

	public void OnClickGamePong(UIController ctrl)
	{
		Debug.Log ("pong");
		GameClient.Instance.MG.Self.Proxy.Pong (odao.scmahjong.TileDef.Create (GameClient.Instance.SpecialCard));
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}

	public void OnClickGameWin(UIController ctrl)
	{
		Debug.Log ("win");
        GameClient.Instance.MG.Self.Proxy.Win(GameClient.Instance.SpecialCard);
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}

	public void OnClickGamePass(UIController ctrl)
	{
		Debug.Log ("pass");
		GameClient.Instance.MG.Self.Proxy.Pass ();
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}

	public void OnClickGameBaoTing(UIController ctrl)
	{
		Debug.Log ("baoting");
		ctrl.Close ();
		UIGameHuPromptController.Instance.Close ();
	}
}

