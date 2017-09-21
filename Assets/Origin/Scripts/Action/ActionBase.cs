using UnityEngine;
using System.Collections;
	
public class ActionBase
{
	public int Tag { get; set; }

	public GameObject OriginalTarget { get; set; }
	
	public GameObject Target { get; set; }
	//clone
	public virtual ActionBase Reverse () { return null; }

	public virtual bool IsDone() { return true; }

	public virtual void StartWithTarget(GameObject target) { OriginalTarget = Target = target; }

	public virtual void Stop() { Target = null; }
	
	public virtual void Step(float dt) {}

	public virtual void Update(float time) {}
};

public class FiniteTimeAction : ActionBase
{
	public float Duration { get; set; }

	//public FiniteTimeAction Reverse() 
	//{
	//	return null;
	//}

	public FiniteTimeAction()
	{
		Duration = 0.0f;
	}
};

public class Speed : ActionBase
{
};

public class Follow : ActionBase
{
};
