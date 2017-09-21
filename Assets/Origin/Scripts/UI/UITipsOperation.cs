using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class UIOperation : SingletonBehaviour<UIOperation> {
	public void OnClickTipsBtn1(UIController ctrl, Action act)
	{
		if (act !=null )
			act ();
		ctrl.Close ();
	}

	public void OnClickTipsBtn2(UIController ctrl, Action act)
	{
		if (act !=null )
			act ();
		ctrl.Close ();
	}
}
