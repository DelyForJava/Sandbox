using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIOperation : SingletonBehaviour<UIOperation> {

	public bool Matching = false;
	public int SelfIndex = 0;//我的绝对位置
    IEnumerator _debugInfo(string str)
    {
        int i = 1;
        UIDebugViewController.Instance.Open();
        while (Matching)
        {
            UIDebugViewController.Instance.SetText(str.Substring(0, i));
            yield return new WaitForSeconds(0.5f);
            i = ++i % str.Length;
            if (i == 0)
                i = 1;
        }
    }

	public void OnClickMatchXL(UIController ctrl)
	{
		Debug.Log ("click xl");
		GameClient.Instance.MahjongGamePlayer.Match ();
        //StartCoroutine (_debugInfo ("正在匹配中......"));
        UIDebugViewController.Instance.OpenLoadingDebug("正在匹配中......");
	}

	public void OnClickMatchXZ(UIController ctrl)
	{
		Debug.Log ("click xz");
	}
}
