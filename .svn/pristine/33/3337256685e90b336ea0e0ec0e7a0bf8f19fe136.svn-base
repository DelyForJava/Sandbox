using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionInstant : FiniteTimeAction 
{
	public override bool IsDone() 
	{
		return true;
	}
	public override void Step(float dt) 
	{
		Update (1.0f);
	}
	override public void Update(float time)
	{
	}
};

public class Show : ActionInstant
{
};

public class Hide : ActionInstant
{
};

public class ToggleVisibility : ActionInstant
{
};

public class RemoveSelf : ActionInstant
{
};

public class FlipX : ActionInstant
{
};

public class FlipY : ActionInstant
{
};

public class Place : ActionInstant //<NSCopying>
{
};

public class InvokeAction : ActionInstant
{
	public delegate void CallFunc(GameObject target, object userdata);
	
	CallFunc mCallFunc;
	
	object mUserdata;
	
	InvokeAction()
	{
	}
	
	static public InvokeAction Create(CallFunc callfunc, object userdata = null)
	{
		InvokeAction obj = new InvokeAction ();
		obj.mCallFunc = callfunc;
		obj.mUserdata = userdata;
		return obj;
	}
	
	//static public InvokeAction Create(LuaFunction lfunc)
	//{
	//	lfunc.Call ();
	//	return null;
	//}
	
	override public void Update(float t)
	{
		execute ();
	}
	
	void execute()
	{
		if(mCallFunc!=null)
			mCallFunc (Target, mUserdata);
	}
}

