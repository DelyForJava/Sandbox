//#define OPEN_DEBUG_INFO
using System.Collections;
using UnityEngine;

public class DebugInfo : MonoBehaviour {

	public static void Message(string text, float time)
	{		
		#if OPEN_DEBUG_INFO
		string str = "DEBUG:" + text;
		Debug.Log (str);
		UIControllerAlert.Instance.SetAlertText (str);
		UIControllerAlert.Instance.SetAutoCloseTime (time);
		Loom.QueueOnMainThread (UIControllerAlert.Instance.RefreshAlertAutoClose);
		#endif
	}

	public static void Message(string text)
	{		
		#if OPEN_DEBUG_INFO
		string str = "DEBUG:" + text;
		Debug.Log (str);
		UIControllerAlert.Instance.SetAlertText (str);
		Loom.QueueOnMainThread (UIControllerAlert.Instance.RefreshAlertClickClose);
		#endif
	}
}
